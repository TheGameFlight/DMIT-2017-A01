using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public string profilesCsvFileName = "profiles.csv";
    public List<SaveData> saveDataList = new List<SaveData>();

    private string ProfilesFolder => Path.Combine(Application.persistentDataPath, "Saves");
    private string CsvPath => Path.Combine(ProfilesFolder, profilesCsvFileName);

    private void Awake()
    {
        if (!Directory.Exists(ProfilesFolder))
            Directory.CreateDirectory(ProfilesFolder);

        if (!File.Exists(CsvPath))
            File.WriteAllText(CsvPath, "ProfileName,HighScore,GhostFile");
    }

    public void CreateSave(string profileName, int highScore)
    {
        string ghostFile = $"{profileName}_ghost.json";
        string ghostPath = Path.Combine(ProfilesFolder, ghostFile);

        SaveData saveData = new SaveData(profileName, highScore);
        saveDataList.Add(saveData);

        File.WriteAllText(ghostPath, JsonUtility.ToJson(new GhostData()));

        using (StreamWriter writer = new StreamWriter(CsvPath, true))
        {
            writer.WriteLine($"{profileName},{highScore},{ghostFile}");
        }

        Debug.Log($"[CreateSave] Created profile {profileName} with ghost file {ghostFile}");
    }

    public List<SaveData> GetAllProfiles()
    {
        List<SaveData> profiles = new();

        if (!File.Exists(CsvPath))
            return profiles;

        string[] lines = File.ReadAllLines(CsvPath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');
            if (columns.Length < 2) continue;

            profiles.Add(new SaveData(columns[0], int.Parse(columns[1])));
        }

        return profiles;
    }

    public void RenameProfile(string oldName, string newName)
    {
        if (!File.Exists(CsvPath))
            return;

        string[] lines = File.ReadAllLines(CsvPath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');
            if (columns[0] == oldName)
            {
                string oldGhost = columns[2];
                string newGhost = $"{newName}_ghost.json";

                string oldPath = Path.Combine(ProfilesFolder, oldGhost);
                string newPath = Path.Combine(ProfilesFolder, newGhost);

                if (File.Exists(oldPath))
                    File.Move(oldPath, newPath);

                lines[i] = $"{newName},{columns[1]},{newGhost}";
                break;
            }
        }

        File.WriteAllLines(CsvPath, lines);
        Debug.Log($"[RenameProfile] {oldName} {newName}");
    }


    public SaveData LoadProfile(string profileName)
    {
        if (!File.Exists(CsvPath))
        {
            Debug.LogWarning($"[LoadProfile] CSV file does NOT exist: {CsvPath}");
            return null;
        }

        string[] lines = File.ReadAllLines(CsvPath);
        Debug.Log($"[LoadProfile] Total profiles in file: {lines.Length - 1}");

        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');

            if (columns[0] == profileName)
            {
                int highScore = int.Parse(columns[1]);
                GhostData ghostData = null;

                if (columns.Length > 2)
                {
                    string ghostPath = Path.Combine(ProfilesFolder, columns[2]);
                    if (File.Exists(ghostPath))
                    {
                        string json = File.ReadAllText(ghostPath);
                        ghostData = JsonUtility.FromJson<GhostData>(json);
                    }
                    else
                    {
                        Debug.LogWarning($"[LoadProfile] Ghost file not found: {ghostPath}");
                    }
                }

                Debug.Log($"[LoadProfile] Loaded profile: {profileName}, HighScore: {highScore}, GhostFrames: {(ghostData != null ? ghostData.ghostDataFrames.Count : 0)}");
                return new SaveData(profileName, highScore, ghostData);
            }
        }

        Debug.LogWarning($"[LoadProfile] Profile {profileName} not found in CSV");
        return null;
    }

    public void SaveGhost(string profileName, GhostData ghostData, int newHighScore)
    {
        if (!File.Exists(CsvPath))
        {
            Debug.LogWarning($"[SaveGhost] CSV file does NOT exist: {CsvPath}");
            return;
        }

        string[] lines = File.ReadAllLines(CsvPath);
        bool updated = false;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = lines[i].Split(',');

            if (columns[0] == profileName)
            {
                string ghostFile = $"{profileName}_ghost.json";
                string ghostPath = Path.Combine(ProfilesFolder, ghostFile);

                File.WriteAllText(ghostPath, JsonUtility.ToJson(ghostData));

                lines[i] = $"{profileName},{newHighScore},{ghostFile}";
                updated = true;
                break;
            }
        }

        if (updated)
        {
            File.WriteAllLines(CsvPath, lines);
            Debug.Log($"[SaveGhost] Saved ghost for profile {profileName}, Frames: {ghostData.ghostDataFrames.Count}, Score: {newHighScore}");
        }
        else
        {
            Debug.LogWarning($"[SaveGhost] Profile {profileName} not found, cannot save ghost");
        }
    }

    public void DeleteProfile(string profileName)
    {
        if (!File.Exists(CsvPath))
        {
            Debug.LogWarning($"[DeleteProfile] CSV file does NOT exist: {CsvPath}");
            return;
        }

        List<string> lines = new List<string>(File.ReadAllLines(CsvPath));
        string ghostFile = $"{profileName}_ghost.json";
        string ghostPath = Path.Combine(ProfilesFolder, ghostFile);

        for (int i = lines.Count - 1; i >= 1; i--)
        {
            string[] columns = lines[i].Split(',');
            if (columns[0] == profileName)
            {
                lines.RemoveAt(i);
                break;
            }
        }

        File.WriteAllLines(CsvPath, lines);

        if (File.Exists(ghostPath))
        {
            File.Delete(ghostPath);
        }

        Debug.Log($"[DeleteProfile] Deleted profile {profileName} and ghost file {ghostFile}");
    }
}

[Serializable]
public class SaveData
{
    public string profileName;
    public int highScore;
    public GhostData ghostData;

    public SaveData(string profileName_, int highScore_, GhostData ghostData_ = null)
    {
        profileName = profileName_;
        highScore = highScore_;
        ghostData = ghostData_;
    }
}
//You are doing a great job you can do this!