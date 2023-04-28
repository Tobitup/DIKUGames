using DIKUArcade.Utilities;

namespace Breakout.Levels;
public class LevelLoader {
    private LevelParser levelParser;

    // Static Path to insure breakoutTests always pulls Assets from the correct directory.
    public static readonly string MAIN_PATH = 
                    Path.Combine(Directory.GetParent(FileIO.GetProjectPath())!.FullName,"Breakout");

    private Level level;
    public Level Level {get {return level;}}

    /// <summary> Constructs a new LevelLoader object, which loads the specified level file. 
    ///          </summary>
    /// <param name="levelToBeLoaded"> The SelectLevel enum value of the level to be loaded.</param>
    /// <return> Returns a Level that has been loaded. </returns>
    public LevelLoader(SelectLevel levelToBeLoaded) {
        level = AttemptLevelLoad(ReadRawFileData(levelToBeLoaded));
    }

    /// <summary> Reads the raw file data of the specified level file. </summary>
    /// <param name="levelToBeLoaded"> The SelectLevel enum value of the level to be loaded.</param>
    /// <returns> The raw file data of the specified level file. </returns>
    private string[] ReadRawFileData(SelectLevel levelToBeLoaded) {
        return FileReader.ReadFile(
                                    Path.Combine(MAIN_PATH, "Assets", "Levels",
                                    LevelTransformer.TransformLevelToString(levelToBeLoaded)));
    }

    /// <summary> Attempts to load a level from the specified raw file data. </summary>
    /// <param name="rawFileData"> The raw file data to be used for level loading. </param>
    /// <returns> A loaded Level object if the raw file data is valid, an empty Level object 
    ///           otherwise.</returns>
    private Level AttemptLevelLoad(string[] rawFileData) {
        // Load level if data else load empty level
        if ((rawFileData.Length != 0) && (FileReader.IsDataValid(rawFileData))) {
            levelParser = new LevelParser(rawFileData);
            return levelParser.GenerateLevel(); 
        }
        return new Level(new Dictionary<string, string>{}, new Dictionary<string, string>{},
                                                                                new string[0,0]);
    }
}