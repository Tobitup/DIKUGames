using DIKUArcade.Entities;
using DIKUArcade.Math;
using DIKUArcade.Graphics;
using Breakout.Blocks;
using Breakout.BreakoutStates;

namespace Breakout.Levels;
public class Level
{
    private Dictionary<string, string> metaData;
    private Dictionary<string, string> legendData;
    private EntityContainer<Block> blockContainer = new EntityContainer<Block>();
    public EntityContainer<Block> BlockContainer { get { return blockContainer; } }
    private string[,] levelMap;

    /// <summary> Initializes a new instance of the Level class with the specified metadata, 
    ///           legend data, and level map. </summary>
    /// <param name="metaData"> A Dictionary object that represents the metadata linked with 
    ///                         the level. </param>
    /// <param name="legendData"> A Dictionary object that represents the legend data linked with 
    ///                           the level. </param>
    /// <param name="levelMap"> A string array that represents the level map. </param>
    public Level(Dictionary<string, string> metaData, Dictionary<string, string> legendData,
                                                                            string[,] levelMap) {
        this.metaData = metaData;
        this.legendData = legendData;
        this.levelMap = levelMap;
        GenerateEntityContainer();
    }

    /// <summary> Generates an entity container from the level map. </summary>
    /// <returns> Void. </returns>
    private void GenerateEntityContainer() {
        for (int i = 0; i < levelMap.GetLength(0); i++) {
            for (int j = 0; j < levelMap.GetLength(1); j++) {

                string character = levelMap[i, j].ToString();

                if (levelMap[i, j] != "-") {
                    string imagePath = Path.Combine(LevelLoader.MAIN_PATH, "Assets", "Images",  
                                                                            legendData[character]);
                    blockContainer.AddEntity(
                        new Block(new Vec2I(i, j), new Image(imagePath)));
                }

            }
        }
    }
}