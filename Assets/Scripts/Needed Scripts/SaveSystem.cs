using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private string ProfilesFolder =>
        Path.Combine(Application.persistentDataPath, "Saves");

    private void Awake()
    {
        if (!Directory.Exists(ProfilesFolder))
            Directory.CreateDirectory(ProfilesFolder);
    }

    public void CreateSave(string profileName, int highScore)
    {
        SaveData data = new SaveData
        {
            profileName = profileName,
            highScore = highScore,
            ghostData = new GhostData(),
            carBodyIndex = 0,
            bodyColor = Color.white
        };

        SaveProfile(data);

        Debug.Log($"[CreateSave] Created profile {profileName}");
    }

    public void SaveProfile(SaveData data)
    {
        string path = GetProfilePath(data.profileName);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public SaveData LoadProfile(string profileName)
    {
        string path = GetProfilePath(profileName);

        if (!File.Exists(path))
        {
            Debug.LogWarning($"[LoadProfile] Profile not found: {profileName}");
            return null;
        }

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public List<SaveData> GetAllProfiles()
    {
        List<SaveData> profiles = new();

        string[] lines = File.ReadAllLines(CsvPath);

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
            {
                continue;
            }


            string[] columns = lines[i].Split(',');

            if (columns.Length < 2)
            {
                continue;
            }

            string profileName = columns[0].Trim();

            if (string.IsNullOrEmpty(profileName))
            {
                continue;
            }

            if (!int.TryParse(columns[1], out int highScore))
            {
                highScore = 0;
            }

                profiles.Add(new SaveData(profileName, highScore));
        }

        return profiles;
    }

    public void RenameProfile(string oldName, string newName)
    {
        string oldPath = GetProfilePath(oldName);
        string newPath = GetProfilePath(newName);

        if (!File.Exists(oldPath))
            return;

        SaveData data = LoadProfile(oldName);
        data.profileName = newName;

        File.Delete(oldPath);
        SaveProfile(data);

        Debug.Log($"[RenameProfile] {oldName} {newName}");
    }

    public void DeleteProfile(string profileName)
    {
        string path = GetProfilePath(profileName);

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log($"[DeleteProfile] Deleted {profileName}");
        }
        else
        {
            Debug.LogWarning($"[DeleteProfile] Profile not found: {profileName}");
        }
    }

    public void SaveGhost(string profileName, GhostData ghostData, int newHighScore)
    {
        SaveData data = LoadProfile(profileName);
        if (data == null)
            return;

        data.ghostData = ghostData;
        data.highScore = newHighScore;

        SaveProfile(data);

        Debug.Log($"[SaveGhost] Saved ghost for {profileName}");
    }

    private string GetProfilePath(string profileName)
    {
        return Path.Combine(ProfilesFolder, $"{profileName}.json");
    }
}
