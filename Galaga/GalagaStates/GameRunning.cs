using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.State;
using DIKUArcade.Input;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.Math;
using System.IO;
using System.Collections.Generic;


namespace Galaga.GalagaStates;
public class GameRunning : IGameState, IGameEventProcessor {
    private GameEventBus eventBus;
    private Player player;
    private EntityContainer<Enemy> enemies;

    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;
    private WaveControl wave;

    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    private bool isGameover = false;
    private StateMachine stateMachine;


    private List<Image> enemyStridesRed;
    private static GameRunning instance = null;
    public static GameRunning GetInstance() {
        if (GameRunning.instance == null) {
            GameRunning.instance = new GameRunning();
            GameRunning.instance.InitializeGameState();
    }
        return GameRunning.instance;
    }

    private void InitializeGameState() {
        player = new Player(
        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
        new Image(Path.Combine("Assets", "Images", "Player.png")));
        
        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        eventBus = GalagaBus.GetBus();
        eventBus.Subscribe(GameEventType.InputEvent, this);
        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));

        enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets",
                                                                    "Images", "RedMonster.png"));

        const int numEnemies = 8;

        wave = new WaveControl(images,enemyStridesRed);
        enemies = wave.ActiveSquadron.Enemies;

        
        enemyExplosions = new AnimationContainer(numEnemies);

        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));
        
        wave.Scoreboard.ResetScore();
    
    }

        public void AddExplosion(Vec2F position, Vec2F extent) {
        enemyExplosions.AddAnimation(
            new StationaryShape(position,extent), EXPLOSION_LENGTH_MS, 
            new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
    }

    private void PlayerCollideWithEnemy(EntityContainer<Enemy> enemies) {
        enemies.Iterate(enemy => {
            if (CollisionDetection.Aabb(player.Shape,enemy.Shape).Collision) {
                player.Health.LoseHealth();
            }

            if (enemy.Shape.Position.Y <= 0.0f) {
                GalagaBus.GetBus().RegisterEvent(
                                    new GameEvent{
                                        EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_OVER"
                                    });
            }
        });
    }

    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Move();

            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    if(CollisionDetection.Aabb(shot.Shape.AsDynamicShape(),enemy.Shape).Collision) {  
                        if (!enemy.IsDead()) {
                            enemy.GetsShot();
                            shot.DeleteEntity();
                        } else {
                            enemy.DeleteEntity();
                            AddExplosion(enemy.Shape.Position,enemy.Shape.Extent);
                            shot.DeleteEntity();
                        }
                    }
                });
            }
        });
    }

    private void AddMovingShot() {
        playerShots.AddEntity(
            new PlayerShot(GetShotSpawnpoint(),playerShotImage));
    }

    private Vec2F GetShotSpawnpoint() {
        return new Vec2F(player.GetPosition().X+player.Extent().X/2.0f
                ,player.GetPosition().Y);
    }


    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
    }

    public void RenderState()
    {
        if (!isGameover) {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
            player.Health.RenderHealth();
        }
        wave.Scoreboard.RenderText();
        
    }

    public void ResetState()
    {
        InitializeGameState();
    }

    public void UpdateState()
    {
        if (!isGameover) {
            player.Move();
            enemies = wave.ActiveSquadron.Enemies;
            wave.generateWave(enemies);
            wave.ActiveStrategy.MoveEnemies(enemies);
            PlayerCollideWithEnemy(enemies);
            IterateShots();
        }
        player.Health.IsDead();
    }

    public void ProcessEvent(GameEvent gameEvent)
    {
        if (gameEvent.EventType == GameEventType.InputEvent) {
            switch (gameEvent.Message) {
                case "SHOOT":
                    AddMovingShot();
                    break;
            }
        }
    }
}
