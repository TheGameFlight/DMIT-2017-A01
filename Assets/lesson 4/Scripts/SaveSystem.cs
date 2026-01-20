using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{

    public string filePath;
    public List<SaveData> saveDataList = new List<SaveData>();
    
    private void Start()
    {
        //CreateSave("sujan", 1000);
        Debug.Log(LoadData("sujan"));
    }
    public void CreateSave(string profileName_, int highScore_)
    {
        SaveData saveData = new SaveData(profileName_, highScore_);
        bool fileExists = File.Exists(filePath);

        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            if (!fileExists)
            {
                writer.WriteLine("Profile Name, Score");
            }

            writer.WriteLine($"{saveData.profileName}, {saveData.highScore}");
            saveDataList.Add(saveData);
        }
    }

    public void UpdateSave(SaveData saveData_)
    {

    }

    public void DeleteSave(SaveData saveData_)
    {

    }

    public int LoadData(string profileName)
    {
        int highScore = 0;
        string[] lines = File.ReadAllLines(filePath);
        for (int i = 0; i < lines.Length; i++)
        {
            string[] columns = Regex.Split(lines[i], ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)");

            if (columns[0] == profileName)
            {
                highScore = int.Parse(columns[1]);
                break;
            }
        }
        SaveData saveData = new SaveData(profileName, highScore);
        saveDataList.Add(saveData);
        return highScore;
    }
}

[Serializable]
public class SaveData
{
    public string profileName;
    public int highScore;

    public SaveData(string profileName_, int highScore_)
    {
        profileName = profileName_;
        highScore = highScore_;
    }
}
