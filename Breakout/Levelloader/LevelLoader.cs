namespace Breakout.Levels;
public class LevelLoader {
    private LevelParser levelParser;
    private Level level;
    public Level Level {get {return level;}}
    public LevelLoader(SelectLevel levelToBeLoaded) {

        string[] rawFileData = FileReader.ReadFile(
                                        Path.Combine("Assets", "Levels",
                                        LevelTransformer.TransformLevelToString(levelToBeLoaded)));

        // Load level if data else load empty level
        if ((rawFileData.Length != 0) && (FileReader.IsDataValid(rawFileData))) {
            levelParser = new LevelParser(rawFileData);
            level = levelParser.GenerateLevel();
        } else {
            level = new Level(new Dictionary<string, string>{}, new Dictionary<string, string>{},
                                new string[0,0]);
        }
    }
}