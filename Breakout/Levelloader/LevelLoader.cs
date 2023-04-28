using DIKUArcade.Utilities;

namespace Breakout.Levels;
public class LevelLoader {
    private LevelParser levelParser;

    // Static Path to insure breakoutTests always pulls assets from the correct directory.
    public static readonly string MAIN_PATH = 
                    Path.Combine(Directory.GetParent(FileIO.GetProjectPath())!.FullName,"Breakout");

    private Level level;
    public Level Level {get {return level;}}
    public LevelLoader(SelectLevel levelToBeLoaded) {
        level = AttemptLevelLoad(ReadRawFileData(levelToBeLoaded));
    }

    private string[] ReadRawFileData(SelectLevel levelToBeLoaded) {
        return FileReader.ReadFile(
                                    Path.Combine(MAIN_PATH, "Assets", "Levels",
                                    LevelTransformer.TransformLevelToString(levelToBeLoaded)));
    }

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