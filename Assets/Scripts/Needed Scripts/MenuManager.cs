using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Analytics.IAnalytic;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject profileSelectPanel;
    public GameObject confirmDeletePanel;

    public SaveSystem saveSystem;

    private string pendingDeleteProfile;

    private void Start()
    {
        ShowMainMenu();
    }


    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        profileSelectPanel.SetActive(false);
        confirmDeletePanel.SetActive(false);
    }

    public void ShowProfileSelect()
    {
        mainMenuPanel.SetActive(false);
        profileSelectPanel.SetActive(true);
        confirmDeletePanel.SetActive(false);
    }

    public void StartButton()
    {
        ShowProfileSelect();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void PickProfile(string profileName)
    {
        ActiveProfile.profileName = profileName;

        SaveData data = saveSystem.LoadProfile(profileName);

        if (data == null)
        {
            Debug.Log($"[PROFILE PICKED] {profileName} — NO SAVE DATA");
        }
        else if (data.ghostData == null || data.ghostData.ghostDataFrames.Count == 0)
        {
            Debug.Log($"[PROFILE PICKED] {profileName} — Best Time: {data.highScore} (NO GHOST)");
        }
        else
        {
            Debug.Log($"[PROFILE PICKED] {profileName} — Best Time: {data.highScore} (GHOST FOUND, Frames: {data.ghostData.ghostDataFrames.Count})");
        }

        profileSelectPanel.SetActive(false);

        SceneManager.LoadScene("Game");
    }

    public void RequestDeleteProfile(string profileName)
    {
        pendingDeleteProfile = profileName;
        confirmDeletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        if (!string.IsNullOrEmpty(pendingDeleteProfile))
        {
            saveSystem.DeleteProfile(pendingDeleteProfile);
        }

        pendingDeleteProfile = null;
        confirmDeletePanel.SetActive(false);
    }
}
