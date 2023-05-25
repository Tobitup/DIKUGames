using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Entities;
using Breakout.Levels;

namespace Breakout.PlayerLives;

public class Lives {
    private uint lives = 0;
    private int originalLives = 0;
    private Vec2F lastHeartPos = new Vec2F(0.9f,0.88f);
    private Vec2F heartSize = new Vec2F(0.05f,0.05f);
    private IBaseImage fullImage = new Image (Path.Combine
                    (LevelLoader.MAIN_PATH, "Assets", "Images", "heart_filled.png"));
    private IBaseImage emptyImage = new Image(Path.Combine
                    (LevelLoader.MAIN_PATH, "Assets", "Images", "heart_empty.png"));
    private EntityContainer<Entity> lifeContainer = new EntityContainer<Entity>();
    public EntityContainer<Entity> LifeContainer { get { return lifeContainer; } }

    public uint GetCurrentLives { get {return lives;}}

    public Lives(uint playerLives){
        lives = playerLives;
        originalLives = (int)playerLives;
        UpdateLifeContainer();
    }

    private Vec2F nextHeartPos(int heartIndex){
        float distance = (float)originalLives-1-heartIndex;
        float newHeartX = (lastHeartPos.X-(heartSize.X*distance)-0.01f);
        return new Vec2F(newHeartX,lastHeartPos.Y);
    } 

    public void UpdateLifeContainer(){
        lifeContainer.ClearContainer();
        for (int i = originalLives - 1; i >= 0; i--){
            if (i >= lives){
                lifeContainer.AddEntity(
                    new Entity(new DynamicShape(nextHeartPos(i), heartSize), emptyImage));}
            else
            {   lifeContainer.AddEntity(
                    new Entity(new DynamicShape(nextHeartPos(i), heartSize), fullImage));}}
    }
    
    public void LoseLife(){
        if (lives>0)
        {lives--;}
    }

    public void ResetLife() {
        lives = 0;
    }
}