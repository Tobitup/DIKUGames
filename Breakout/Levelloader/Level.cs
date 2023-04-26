using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Blocks;

namespace Breakout.Levels;
public class Level {
    private Dictionary<string,string> meteData;
    private Dictionary<string,string> legendData;
    private EntityContainer<Block> blockContainer = new EntityContainer<Block>(); 
    public EntityContainer<Block> BlockContainer {get {return blockContainer;}}
    private string[,] levelMap;

    public Level(Dictionary<string,string> metaData, Dictionary<string,string> legendData,
     string[,] levelMap) {
        this.meteData = metaData;
        this.legendData = legendData;
        this.levelMap = levelMap;

        GenerateEntityContainer();
    }

    private void GenerateEntityContainer() {
        for (int i = 0; i < levelMap.GetLength(0); i++) {
            for (int j = 0; j < levelMap.GetLength(1); j++) {

                string character = levelMap[i,j].ToString();

                if (levelMap[i,j] != "-") {
                    string imagePath = Path.Combine("Assets", "Images", legendData[character]);
                    blockContainer.AddEntity(
                        new Block(new Vec2I(i,j), new Image(imagePath)));
                }

            }
        }
    }
}