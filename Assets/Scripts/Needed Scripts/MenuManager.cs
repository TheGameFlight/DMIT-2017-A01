using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject profileSelectPanel;
    public GameObject confirmDeletePanel;

    public SaveSystem saveSystem;
    public raceManager raceManager;

    private string pendingDeleteProfile;
    private string activeProfile;

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

    public void ShowDeleteConfirm(string profileName)
    {
        pendingDeleteProfile = profileName;
        confirmDeletePanel.SetActive(true);
    }

    public void CancelDelete()
    {
        pendingDeleteProfile = null;
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
        activeProfile = profileName;

        SaveData data = saveSystem.LoadProfile(profileName);

        if (data != null && data.ghostData != null)
        {
            raceManager.StartRaceWithGhost(data.ghostData);
        }

        raceManager.SetActiveProfile(profileName);
        profileSelectPanel.SetActive(false);
    }

    public void RequestDeleteProfile(string profileName)
    {
        ShowDeleteConfirm(profileName);
    }

    public void ConfirmDelete()
    {
        if (!string.IsNullOrEmpty(pendingDeleteProfile))
        {
            saveSystem.DeleteProfile(pendingDeleteProfile);
        }

        CancelDelete();
    }
}
