namespace Breakout.Levels;

public static class FileReader {

    /// <summary> Reads the lines in a file and returns the contents as a string array. </summary>
    /// <param name="inputPath"> The path to the file to read. </param>
    /// <returns> An array of strings containing the lines of the file. </returns>
    public static string[] ReadFile(string inputPath) {
        try {
            string[] lines = File.ReadAllLines(inputPath);
            return lines;
        } catch(FileNotFoundException) {
            System.Console.WriteLine($"Could not open file {inputPath}");
            return new string[0];
        }  
    }

    /// <summary> Checks if the data is in a valid map format. </summary>
    /// <param name="rawData"> An array of strings containing the lines of the file. </param>
    /// <returns> True if the data is valid false if it is invalid. </returns>
    public static bool IsDataValid(string[] rawData) {
        bool isValid = true;
        // Checking if every tag exists
        if ((Array.IndexOf(rawData, "Map:") == -1) ||(Array.IndexOf(rawData, "Map/") == -1)) {
            isValid = false;
        }
        if ((Array.IndexOf(rawData, "Legend:") == -1) ||(Array.IndexOf(rawData, "Legend/") == -1)) {
            isValid = false;
        }
        if ((Array.IndexOf(rawData, "Meta:") == -1) ||(Array.IndexOf(rawData, "Meta/") == -1)) {
            isValid = false;
        }
        return isValid;
    }
}