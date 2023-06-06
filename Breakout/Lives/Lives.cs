using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using Breakout.Levels;
using DIKUArcade.Events;
using Breakout.Effect;

namespace Breakout.PlayerLives;

public class Lives : IGameEventProcessor{
    private uint lives = 0;
    private int originalLives = 0;
    private const int MAX_LIVES = 20;
    private Vec2F lastHeartPos = new Vec2F(0.9f,0.88f);
    private Vec2F heartSize = new Vec2F(0.05f,0.05f);
    private IBaseImage fullImage = new Image (Path.Combine
                    (LevelLoader.MAIN_PATH, "Assets", "Images", "heart_filled.png"));
    private IBaseImage emptyImage = new Image(Path.Combine
                    (LevelLoader.MAIN_PATH, "Assets", "Images", "heart_empty.png"));
    private EntityContainer<Entity> lifeContainer = new EntityContainer<Entity>();
    public EntityContainer<Entity> LifeContainer { get { return lifeContainer; } }
    public uint GetCurrentLives { get {return lives;}}

    /// <summary> 
    /// Initializes a new instance of the Lives class with a starting number of lives 
    /// </summary>
    /// <param name="playerLives"> The beginning number of lives </param>
    public Lives(uint playerLives){

        BreakoutBus.GetBus().Subscribe(GameEventType.PlayerEvent, this);
        lives = playerLives;
        originalLives = (int)playerLives;
        UpdateLifeContainer();
    }
    /// <summary> Calculates the position of the next heart </summary>
    /// <param name="heartIndex"> the index of the heart </param>
    /// <return> Returns a Vec2F position. </return>
    private Vec2F nextHeartPos(int heartIndex){
        float distance = (float)originalLives + NumberOfExtraLives() - 1 -heartIndex;
        float newHeartX = (lastHeartPos.X-(heartSize.X*distance)-0.01f);
        return new Vec2F(newHeartX,lastHeartPos.Y);
    } 

    /// <summary> Updates the lifecontainer according to how many lives are left </summary>
    public void UpdateLifeContainer(){
        lifeContainer.ClearContainer();
        for (int i = originalLives + NumberOfExtraLives() - 1; i >= 0; i--){
            if (i >= lives){
                lifeContainer.AddEntity(
                    new Entity(new DynamicShape(nextHeartPos(i), heartSize), emptyImage));}
            else
            {   lifeContainer.AddEntity(
                    new Entity(new DynamicShape(nextHeartPos(i), heartSize), fullImage));}}
    }

    /// <summary> 
    /// Calculates how many extra lives should be rendered in event of powerup when life is full.
    /// </summary>
    private int NumberOfExtraLives() {
        // Ekstra lives picked up
        if (lives > originalLives) {
            return (int)lives - originalLives;
        } else {
            return 0;
        }
    }
    
    /// <summary> Decrement lives by one </summary>
    public void LoseLife(){
        if (lives>0)
        {lives--;}
    }

    /// <summary> Reset lives to 0 </summary>
    public void ResetLife() {
        lives = 0;
    }

    /// <summary> Adds one life unless maximum lives has been reached </summary>
    public void AddLife() {
        if (lives + 1 < MAX_LIVES) {
            lives += 1;
        }
    }

    /// <summary> Processes events. </summary>
    void IGameEventProcessor.ProcessEvent(GameEvent gameEvent) {
        if (gameEvent.Message == "EFFECT") {
            switch (EffectTransformer.TransformStringToEffect(gameEvent.StringArg1)) {
                case Effects.LifeUp:
                    AddLife();
                break;
                case Effects.LifeDown:
                    LoseLife();
                break;
            }
        }
    }
}