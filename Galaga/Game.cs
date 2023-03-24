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

        eventBus = GalagaBus.GetBus();
        eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, 
                                                            GameEventType.WindowEvent,
                                                            GameEventType.GameStateEvent });
        window.SetKeyEventHandler(KeyHandler);
        //window.SetKeyEventHandler(stateMachine.ActiveState.HandleKeyEvent);
        //window.SetKeyEventHandler(GamePaused.GetInstance().HandleKeyEvent);
        //window.SetKeyEventHandler(GameLost.GetInstance().HandleKeyEvent);

        eventBus.Subscribe(GameEventType.WindowEvent, this);      
        eventBus.Subscribe(GameEventType.InputEvent, this);
        eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);
    }
/*
    public void updateActiveKeyhandler() {
        switch (stateMachine.ActiveState) {
            case GameRunning:
                window.SetKeyEventHandler()
                break;
            default:
                break;
        }
    }
*/
    

    private void KeyHandler(KeyboardAction action, KeyboardKey key) {
        stateMachine.ActiveState.HandleKeyEvent(action, key);
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

    public override void Render() {
        stateMachine.ActiveState.RenderState();
    }
    public override void Update() {
        window.PollEvents();
        eventBus.ProcessEventsSequentially();
        stateMachine.ActiveState.UpdateState();
    }
}