namespace Breakout.Levels;
public class LevelParser {
    private string[] rawLinesFromFile;
    public LevelParser(string[] textLines) {
        rawLinesFromFile = textLines;
    }

    /// <summary> Generates a Level object from the findMetaData(), findLegendData() and 
    ///           findLevelMap() data. </summary>
    /// <returns> A new Level object. </returns>
    public Level GenerateLevel() {
        return new Level(findMetaData(), findLegendData(), findLevelMap());
    }

    /// <summary> Parses the level map data from the raw text lines. </summary>
    /// <returns> A 2D array of strings representing the level map. </returns>
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

    /// <summary> Parses the metadata from the raw text lines. </summary>
    /// <returns> A dictionary of key-value pairs representing the metadata. </returns>
    private Dictionary<string, string> findMetaData() {
        (int,int) metaTagLocation = findTag("Meta");
        Dictionary<string, string> metaDataDictionary = new Dictionary<string, string> {};

        for (int i = metaTagLocation.Item1; i < metaTagLocation.Item2; i++) {
            string[] lineOfMetaData = rawLinesFromFile[i].Split(": ");
            metaDataDictionary.Add(lineOfMetaData[0],lineOfMetaData[1]);
        }
        return metaDataDictionary;
    }

    /// <summary> Parses the legend data from the raw text lines. </summary>
    /// <returns> A dictionary of key-value pairs representing the legend data. </returns>
    private Dictionary<string, string> findLegendData() {
        (int,int) legendTagLocation = findTag("Legend");
        Dictionary<string, string> legendDataDictionary = new Dictionary<string, string> {};

        for (int i = legendTagLocation.Item1; i < legendTagLocation.Item2; i++) {
            string[] lineOfLegendData = rawLinesFromFile[i].Split(") ");
            legendDataDictionary.Add(lineOfLegendData[0],lineOfLegendData[1]);
        }
        return legendDataDictionary;
    }


    /// <summary> Finds the beginning and end location of a given tag in an array of raw lines
    ///           from a file. </summary>
    /// <param name="tag"> The tag to search for. </param>
    /// <returns> A tuple of two integers representing the beginning and end location of the tag. 
    /// </returns>
    private (int, int) findTag(string tag) {
        int tagBeginsAt = Array.IndexOf(rawLinesFromFile, $"{tag}:") + 1;
        int tagEndsAt = Array.IndexOf(rawLinesFromFile, $"{tag}/");
        return (tagBeginsAt,tagEndsAt);
    }

}