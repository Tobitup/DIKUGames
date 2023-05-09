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
    private EntityContainer<Entity> blockContainer = new EntityContainer<Entity>();
    public EntityContainer<Entity> BlockContainer { get { return blockContainer; } }
    private string[,] levelMap;

    /// <summary> Initializes a new instance of the Level class with the specified metadata, 
    ///           legend data, and level map. </summary>
    /// <param name="metaData"> A Dictionary object that represents the metadata linked with 
    ///                         the level. </param>
    /// <param name="legendData"> A Dictionary object that represents the legend data linked with 
    ///                           the level. </param>
    /// <param name="levelMap"> A string array that represents the level map. </param>
    public Level(Dictionary<string, string> metaData, Dictionary<string, string> legendData,
                                                                            string[,] levelMap)
    {
        this.metaData = metaData;
        this.legendData = legendData;
        this.levelMap = levelMap;
        GenerateEntityContainer();
    }
    /// <summary> Loops through metadata to find out which type of block a character is
    /// connected to  </summary>
    /// <param name="character"> The character as a string </param>
    /// <returns> The type of the block as a string  </returns>
    private string GetBlockType(string character)
    {
        string blocktype = "Normal";
        foreach (string value in metaData.Values)
        {
            if (character == value)
            {
                blocktype = metaData.FirstOrDefault(x => x.Value == value).Key;
            }
        }
        return blocktype;
    }
    /// <summary> Generates an entity container from the level map. </summary>
    /// <returns> Void. </returns>
    private void GenerateEntityContainer()
    {
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                string character = levelMap[i, j].ToString();

                if (levelMap[i, j] != "-")
                {
                    string imagePath = Path.Combine
                    (LevelLoader.MAIN_PATH, "Assets", "Images", legendData[character]);
                    string blocktype = GetBlockType(character);
                    blockContainer.AddEntity(BlockFactory.CreateNewBlock
                            (blocktype, new Vec2I(i, j), new Image(imagePath)));
                }
            }
        }
    }
}