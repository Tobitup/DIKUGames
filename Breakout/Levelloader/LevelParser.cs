namespace Breakout.Levels;
public class LevelParser {
    private string[] rawLinesFromFile;
    public LevelParser(string[] textLines) {
        rawLinesFromFile = textLines;
    }

    /// <summary> 
    /// Generates a Level object from the findMetaData(), findLegendData() and 
    /// findLevelMap() data. 
    /// </summary>
    /// <returns> A new Level object. </returns>
    public Level GenerateLevel() {
        return new Level(parseMetaData(), parseLegendData(), parseLevelMap());
    }

    /// <summary> Parses the level map data from the raw text lines. </summary>
    /// <returns> A 2D array of strings representing the level map. </returns>
    public string[,] parseLevelMap() {
        (int, int) mapLocation = findTag("Map");

        // Default empty map
        string[,] levelMap = initEmptyMap(12, 25);

        if (mapLocation.Item1 != 0) {

            int mapHeight = mapLocation.Item2-mapLocation.Item1;
            int mapWidth = rawLinesFromFile[mapLocation.Item1].Count();

            for (int i = 0; i < mapHeight; i++) {
                for (int j = 0; j < mapWidth; j++) {
                    if (j >= rawLinesFromFile[mapLocation.Item1+i].Count()) {
                        levelMap[i,j] = "-";
                    } else {
                        levelMap[i,j] = rawLinesFromFile[mapLocation.Item1+i][j].ToString();
                    }
                }
            }
        }
        return levelMap;
    }

    /// <summary> Parses the metadata from the raw text lines. </summary>
    /// <returns> A dictionary of key-value pairs representing the metadata. </returns>
    public Dictionary<string, string> parseMetaData() {
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
    public Dictionary<string, string> parseLegendData() {
        (int,int) legendTagLocation = findTag("Legend");
        Dictionary<string, string> legendDataDictionary = new Dictionary<string, string> {};

        for (int i = legendTagLocation.Item1; i < legendTagLocation.Item2; i++) {
            string[] lineOfLegendData = rawLinesFromFile[i].Split(") ");
            legendDataDictionary.Add(lineOfLegendData[0],lineOfLegendData[1]);
        }
        return legendDataDictionary;
    }


    /// <summary> 
    /// Finds the beginning and end location of a given tag in an array of raw lines from a file. 
    /// </summary>
    /// <param name="tag"> The tag to search for. </param>
    /// <returns> 
    /// A tuple of two integers representing the beginning and end location of the tag. 
    /// </returns>
    public (int, int) findTag(string tag) {
        int tagBeginsAt = Array.IndexOf(rawLinesFromFile, $"{tag}:") + 1;
        int tagEndsAt = Array.IndexOf(rawLinesFromFile, $"{tag}/");
        return (tagBeginsAt,tagEndsAt);
    }

    /// <summary> Initializes an empty map with the specified width and height. </summary>
    /// <param name="width"> The width of the map. </param>
    /// <param name="height"> The height of the map. </param>
    /// <returns> A 2D array representing the initialized empty map. </returns>
    public string[,] initEmptyMap(int width, int height) {
        string[,] map = new string [height,width];
        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                map[i,j] = "-";
            }
        }
        return map;
    }
}