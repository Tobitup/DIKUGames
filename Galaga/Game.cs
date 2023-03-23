using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Events;
using DIKUArcade.Physics;
using DIKUArcade.Input;
using System.Collections.Generic;
using Galaga.Squadron;
using Galaga.MovementStrategy;
using Galaga.GalagaStates;

namespace Galaga;
public class Game : DIKUGame , IGameEventProcessor{
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
    public Game(WindowArgs windowArgs) : base(windowArgs) {
        stateMachine = new StateMachine();
        /*player = new Player(
        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
        new Image(Path.Combine("Assets", "Images", "Player.png")));

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));
        */
        eventBus = GalagaBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, 
                                                            GameEventType.WindowEvent,
                                                            GameEventType.GameStateEvent });
        window.SetKeyEventHandler(KeyHandler);
        window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
        //eventBus.Subscribe(GameEventType.InputEvent, player);
        eventBus.Subscribe(GameEventType.WindowEvent, this);      
        eventBus.Subscribe(GameEventType.InputEvent, this);
         GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, stateMachine);

        /*
        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));

        enemyStridesRed = ImageStride.CreateStrides(2, Path.Combine("Assets",
                                                                    "Images", "RedMonster.png"));

        const int numEnemies = 8;

        wave = new WaveControl(images,enemyStridesRed);
        enemies = wave.ActiveSquadron.Enemies;

        
        enemyExplosions = new AnimationContainer(numEnemies);

        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));*/
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
                isGameover = true;
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

    private void KeyPress(KeyboardKey key) {
        switch(key) {
            case KeyboardKey.Escape:
                GalagaBus.GetBus().RegisterEvent(
                                    new GameEvent{
                                        EventType = GameEventType.GameStateEvent,
                                        Message = "CHANGE_STATE",
                                        StringArg1 = "GAME_PAUSED"
                                    });
                break;
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_LEFT"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_RIGHT"});
                break;
            case KeyboardKey.Up:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_UP"});
                break;
            case KeyboardKey.Down:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "MOVE_DOWN"});
                break;
            case KeyboardKey.Space:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                        Message = "SHOOT"});
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        switch(key){
            case KeyboardKey.Left:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_LEFT_STOP"});
                break;
            case KeyboardKey.Right:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_RIGHT_STOP"});
                break;
            case KeyboardKey.Up:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_UP_STOP"});
                break;
            case KeyboardKey.Down:
                eventBus.RegisterEvent(new GameEvent {EventType = GameEventType.InputEvent, 
                                                                    Message = "MOVE_DOWN_STOP"});
                break;
            }
        }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        if (action == KeyboardAction.KeyPress) {
            KeyPress(key);
        }
        if (action == KeyboardAction.KeyRelease) {
            KeyRelease(key);
        }
    }

    public void ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.EventType == GameEventType.WindowEvent) {
            switch (gameEvent.Message) {
                case "CLOSE_WINDOW":
                    window.CloseWindow();
                break;
            }
        }
    }

    private void AddMovingShot() {
        playerShots.AddEntity(
            new PlayerShot(GetShotSpawnpoint(),playerShotImage));
    }

    private Vec2F GetShotSpawnpoint() {
        return new Vec2F(player.GetPosition().X+player.Extent().X/2.0f
                ,player.GetPosition().Y);
    }

    public void isDead() {
        if (player.Health.IsDead) {
            isGameover = true;
        }
    }
    public override void Render() {
        stateMachine.ActiveState.RenderState();
        /*if (!isGameover) {
            player.Render();
            enemies.RenderEntities();
            playerShots.RenderEntities();
            enemyExplosions.RenderAnimations();
            player.Health.RenderHealth();
        }
        wave.Scoreboard.RenderText();*/
    }
    public override void Update() {
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
        /*if (!isGameover) {
            player.Move();
            enemies = wave.ActiveSquadron.Enemies;
            wave.generateWave(enemies);
            wave.ActiveStrategy.MoveEnemies(enemies);
            PlayerCollideWithEnemy(enemies);
            IterateShots();
            isDead();
        }*/
    }
}