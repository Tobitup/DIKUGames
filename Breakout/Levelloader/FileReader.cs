namespace Breakout.Levels;

public static class FileReader {
    public static string[] ReadFile(string inputPath) {
        try{
            string[] lines = File.ReadAllLines(inputPath);

            return lines;
        } catch(FileNotFoundException) {
            System.Console.WriteLine($"Could not open file {inputPath}");
            return new string[0];
        }
        
    }

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