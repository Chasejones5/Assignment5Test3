using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    private static string highScoreFilePath;

    static HighScoreManager()
    {
        highScoreFilePath = Path.Combine(Application.persistentDataPath, "highscore.dat");
        Debug.Log("High score file path: " + highScoreFilePath);
    }


    public static int GetHighScore()
    {
        if (File.Exists(highScoreFilePath))
        {
            return LoadHighScore();
        }
        else
        {
            return 0; // Return 0 if the high score file doesn't exist
        }
    }

    public static void UpdateHighScore(int newHighScore)
    {
        SaveHighScore(newHighScore);
    }

    private static void SaveHighScore(int highScore)
    {
        try
        {
            using (FileStream fileStream = new FileStream(highScoreFilePath, FileMode.Create))
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(highScore);
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error saving high score: {e.Message}");
        }
    }

    private static int LoadHighScore()
    {
        try
        {
            using (FileStream fileStream = new FileStream(highScoreFilePath, FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fileStream))
            {
                return reader.ReadInt32();
            }
        }
        catch (IOException e)
        {
            Debug.LogError($"Error loading high score: {e.Message}");
            return 0;
        }
    }
}
