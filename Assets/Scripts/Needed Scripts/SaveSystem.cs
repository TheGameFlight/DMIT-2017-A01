using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    public string filePath;
    public List<SaveData> saveDataList = new List<SaveData>();

    public void CreateSave(string profileName_, int highScore_, GhostData ghostData_)
    {
        SaveData saveData = new SaveData(profileName_, highScore_, ghostData_);
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            if (!fileExists)
            {
                writer.WriteLine("Profile Name, Score, GhostDataJson");
            }

            string ghostJson = JsonUtility.ToJson(ghostData_);
            writer.WriteLine($"{saveData.profileName}, {saveData.highScore}, {ghostJson}");
            saveDataList.Add(saveData);
        }
    }

    public void UpdateSave(SaveData saveData_)
    {

    }

    public void DeleteSave(SaveData saveData_)
    {

    }

    public void DeleteProfile(string profileName)
    {
        if (!File.Exists(filePath))
            return;

        List<string> lines = new List<string>(File.ReadAllLines(filePath));

        for (int i = lines.Count - 1; i >= 1; i--)
        {
            string[] columns = Regex.Split(lines[i],",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            if (columns[0] == profileName)
            {
                lines.RemoveAt(i);
                break;
            }
        }

        File.WriteAllLines(filePath, lines);
        Debug.Log("Profile deleted: " + profileName);
    }


    public SaveData LoadProfile(string profileName)
    {
        if (!File.Exists(filePath))
            return null;

        string[] lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = Regex.Split(
                lines[i],
                ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"
            );

            if (columns[0] == profileName)
            {
                int highScore = int.Parse(columns[1]);

                GhostData ghostData = null;
                if (columns.Length > 2 && !string.IsNullOrEmpty(columns[2]))
                {
                    ghostData = JsonUtility.FromJson<GhostData>(columns[2]);
                }

                return new SaveData(profileName, highScore)
                {
                    ghostData = ghostData
                };
            }
        }

        return null;
    }

    public void SaveGhost(string profileName, GhostData ghostData, int newHighScore)
    {
        if (!File.Exists(filePath))
            return;

        string[] lines = File.ReadAllLines(filePath);
        bool updated = false;

        for (int i = 1; i < lines.Length; i++)
        {
            string[] columns = Regex.Split(
                lines[i],
                ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"
            );

            if (columns[0] == profileName)
            {
                string ghostJson = JsonUtility.ToJson(ghostData);
                lines[i] = $"{profileName}, {newHighScore}, {ghostJson}";
                updated = true;
                break;
            }
        }

        if (updated)
        {
            File.WriteAllLines(filePath, lines);
            Debug.Log("Ghost data saved");
        }
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