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
        }
    }

    private void IterateShots() {
        playerShots.Iterate(shot => {
        shot.Move();
        // TODO: move the shot's shape

        if (shot.Shape.Position.Y > 1.5f) {
            shot.DeleteEntity();
        } else {
        enemies.Iterate(enemy => {
        // TODO: if collision btw shot and enemy -> delete both entities

        /// VIRKER IKKE ####
        System.Console.WriteLine(CollisionDetection.Aabb(shot.Shape.AsDynamicShape(),enemy.Shape).Collision);
        if (CollisionDetection.Aabb(shot.Shape.AsDynamicShape(),enemy.Shape).Collision) {
            shot.DeleteEntity();
            enemy.DeleteEntity();
        }
        
        
        });
        }
        });
        }

    private void KeyPress(KeyboardKey key) {
        if (key == KeyboardKey.Escape) {
            window.CloseWindow();
        }
        if (key == KeyboardKey.Left) {
            player.SetMoveLeft(true);
        }
        if (key == KeyboardKey.Right) {
            player.SetMoveRight(true);
        }
        if (key == KeyboardKey.Space) {
            playerShots.AddEntity(
                new PlayerShot(
                    new Vec2F(player.GetPosition().X+player.Extent().X/2.0f
                    ,player.GetPosition().Y),playerShotImage));
        }
        // TODO: switch on key string and set the player's move direction
        }
    private void KeyRelease(KeyboardKey key) {
        // TODO: switch on key string and disable the player's move direction
        if (key == KeyboardKey.Left) {
            player.SetMoveLeft(false);
        }
        if (key == KeyboardKey.Right) {
            player.SetMoveRight(false);
        }
    }

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        // TODO: Switch on KeyBoardAction and call proper method
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
    }
    public override void Update() {
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        player.Move();
        IterateShots();
    }
}