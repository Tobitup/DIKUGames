using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Levels;

namespace Breakout.BallClass;
public static class BallFactory {
    public static Random rnd = new Random();
    private const float WIDTH = 0.03f;
    private const float HEIGTH = 0.03f;
    private const float DIRX = 0.005f;
    private const float DIRY = 0.012f;
    private const float POSX = 0.45f;
    private const float POSY = 0.22f;


    public static Ball GenerateNormalBall() {
        Image ballImage = new Image(Path.Combine(LevelLoader.MAIN_PATH, 
                                                            "Assets", "Images", "ball.png"));
        Ball newBall = new Ball(
            new DynamicShape(new Vec2F(POSX, POSY), 
                            new Vec2F(WIDTH, HEIGTH), new Vec2F(DIRX, DIRY) ), ballImage);

        return newBall;
    }

    public static Ball GenerateRandomDirBall(Vec2F pos) {
        Image ballImage = new Image(Path.Combine(LevelLoader.MAIN_PATH,
                                                                "Assets", "Images", "ball.png"));
        Random rnd = new Random();
        int rndDirX = rnd.Next(-10,10);
        int rndDirY = rnd.Next(-10,10);
        Ball newBall = new Ball(
            new DynamicShape(pos, 
                            new Vec2F(WIDTH, HEIGTH),
                             new Vec2F((float)rndDirX/100f, (float)rndDirY/100f) ), ballImage);
        return newBall;
    }
}