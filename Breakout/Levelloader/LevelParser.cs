namespace Breakout.Levels;
public class LevelParser {
    private string[] rawLinesFromFile;

    public LevelParser(string[] textLines) {
        rawLinesFromFile = textLines;
    }

    // Returns the beginning and end location of given tag
    private (int, int) findTag(string tag) {
        int tagBeginsAt = Array.IndexOf(rawLinesFromFile, $"{tag}:") + 1;
        int tagEndsAt = Array.IndexOf(rawLinesFromFile, $"{tag}/");

        return (tagBeginsAt,tagEndsAt);
    }

    private string[,] findLevelMap() {
        string[,] levelMap = new string[25,12];

        int mapBegins = findTag("Map").Item1;

        if (mapBegins != 0) {
            for (int i = 0; i < 25; i++) {
                for (int j = 0; j < 12; j++) {
                    levelMap[i,j] = rawLinesFromFile[mapBegins+i][j].ToString();
                }
            }
        }
        

        return levelMap;
    }

    private Dictionary<string, string> findMetaData() {
        (int,int) metaTagLocation = findTag("Meta");

        Dictionary<string, string> metaDataDictionary = new Dictionary<string, string> {};

        for (int i = metaTagLocation.Item1; i < metaTagLocation.Item2; i++) {
            string[] lineOfMetaData = rawLinesFromFile[i].Split(": ");
            metaDataDictionary.Add(lineOfMetaData[0],lineOfMetaData[1]);
        }

        return metaDataDictionary;
    }

    private Dictionary<string, string> findLegendData() {
        (int,int) legendTagLocation = findTag("Legend");

        Dictionary<string, string> legendDataDictionary = new Dictionary<string, string> {};

        for (int i = legendTagLocation.Item1; i < legendTagLocation.Item2; i++) {
            string[] lineOfLegendData = rawLinesFromFile[i].Split(") ");
            legendDataDictionary.Add(lineOfLegendData[0],lineOfLegendData[1]);
        }

        return legendDataDictionary;
    }

    public Level GenerateLevel() {
        return new Level(findMetaData(), findLegendData(), findLevelMap());

    }
}