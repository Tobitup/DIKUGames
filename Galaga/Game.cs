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

namespace Galaga;
public class Game : DIKUGame , IGameEventProcessor{
    private GameEventBus eventBus;
    private Player player;
    private EntityContainer<Enemy> enemies;

    private EntityContainer<PlayerShot> playerShots;
    private IBaseImage playerShotImage;

    private AnimationContainer enemyExplosions;
    private List<Image> explosionStrides;
    private const int EXPLOSION_LENGTH_MS = 500;

    private Score score;

    public Game(WindowArgs windowArgs) : base(windowArgs) {
        player = new Player(
        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
        new Image(Path.Combine("Assets", "Images", "Player.png")));

        playerShots = new EntityContainer<PlayerShot>();
        playerShotImage = new Image(Path.Combine("Assets", "Images", "BulletRed2.png"));

        eventBus = new GameEventBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent });
        window.SetKeyEventHandler(KeyHandler);
        eventBus.Subscribe(GameEventType.InputEvent, this);


        List<Image> images = ImageStride.CreateStrides
            (4, Path.Combine("Assets", "Images", "BlueMonster.png"));
        const int numEnemies = 8;
        enemies = new EntityContainer<Enemy>(numEnemies);
        for (int i = 0; i < numEnemies; i++) {
            enemies.AddEntity(new Enemy(
            new DynamicShape(new Vec2F(0.1f + (float)i * 0.1f, 0.9f), new Vec2F(0.1f, 0.1f)),
            new ImageStride(80, images)));
        
        score = new Score("0", new Vec2F(0.5f,0.5f), new Vec2F(0.3f,0.3f));
        }

        enemyExplosions = new AnimationContainer(numEnemies);
        explosionStrides = ImageStride.CreateStrides(8,
            Path.Combine("Assets", "Images", "Explosion.png"));
    }

    public void AddExplosion(Vec2F position, Vec2F extent) {
        enemyExplosions.AddAnimation(
            new StationaryShape(position,extent), EXPLOSION_LENGTH_MS, 
            new ImageStride(EXPLOSION_LENGTH_MS/8, explosionStrides));
    }

    private void IterateShots() {
        playerShots.Iterate(shot => {
            shot.Move();

            if (shot.Shape.Position.Y > 1.0f) {
                shot.DeleteEntity();
            } else {
                enemies.Iterate(enemy => {
                    if(CollisionDetection.Aabb(shot.Shape.AsDynamicShape(),enemy.Shape).Collision){
                        AddExplosion(enemy.Shape.Position,enemy.Shape.Extent);
                        score.incrementScore();
                        shot.DeleteEntity();
                        enemy.DeleteEntity();
                    }
                });
            }
        });
    }

    private void KeyPress(KeyboardKey key) {
        switch(key) {
            case KeyboardKey.Escape:
                window.CloseWindow();
                break;
            case KeyboardKey.Left:
                player.SetMoveLeft(true);
                break;
            case KeyboardKey.Right:
                player.SetMoveRight(true);
                break;
            case KeyboardKey.Up:
                player.SetMoveUp(true);
                break;
            case KeyboardKey.Down:
                player.SetMoveDown(true);
                break;
            case KeyboardKey.Space:
                playerShots.AddEntity(
                new PlayerShot(
                    new Vec2F(player.GetPosition().X+player.Extent().X/2.0f
                    ,player.GetPosition().Y),playerShotImage));
                break;
        }
    }
    private void KeyRelease(KeyboardKey key) {
        if (key == KeyboardKey.Left) {
            player.SetMoveLeft(false);
        }
        if (key == KeyboardKey.Right) {
            player.SetMoveRight(false);
        }
        if (key == KeyboardKey.Up) {
            player.SetMoveUp(false);
        }
        if (key == KeyboardKey.Down) {
            player.SetMoveDown(false);
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
        // Leave this empty for now
    }

    public override void Render() {
        player.Render();
        enemies.RenderEntities();
        playerShots.RenderEntities();
        enemyExplosions.RenderAnimations();
        score.RenderText();
    }
    public override void Update() {
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }
}