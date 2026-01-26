using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject profileSelectPanel;
    public GameObject confirmDeletePanel;

    public TMP_Dropdown profileDropdown;
    public TMP_InputField renameInput;

    public SaveSystem saveSystem;

    private string pendingDeleteProfile;

    private void Start()
    {
        ShowMainMenu();
        RefreshDropdown();
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

        RefreshDropdown();
    }

    public void StartButton()
    {
        ShowProfileSelect();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    private string SelectedProfile
    {
        get
        {
            if (profileDropdown.options.Count == 0)
            {
                return null;
            }
            return profileDropdown.options[profileDropdown.value].text;
        }
    }

    private void RefreshDropdown()
    {
        profileDropdown.ClearOptions();

        List<string> options = new();
        foreach (SaveData save in saveSystem.GetAllProfiles()) options.Add(save.profileName);

        profileDropdown.AddOptions(options);

        if (options.Count > 0)
        {
            profileDropdown.value = 0;

        }
    }

    public void CreateNewSave()
    {
        string name = $"Save {saveSystem.GetAllProfiles().Count + 1}";
        saveSystem.CreateSave(name, 0);
        RefreshDropdown();

        Debug.Log($"[Create] {name}");
    }

    public void PickProfile(string profileName)
    {
        if (string.IsNullOrEmpty(SelectedProfile))
        {
            return;
        }

        ActiveProfile.profileName = SelectedProfile;
        Debug.Log($"[PROFILE PICKED] {SelectedProfile}");

        SceneManager.LoadScene("Game");
    }

    public void RequestDeleteProfile(string profileName)
    {
        if (string.IsNullOrEmpty(SelectedProfile))
        {
            Debug.LogWarning("[DeleteProfile] No profile selected");
            return;
        }

        pendingDeleteProfile = SelectedProfile;
        confirmDeletePanel.SetActive(true);
    }

    public void ConfirmDelete()
    {
        if (!string.IsNullOrEmpty(pendingDeleteProfile))
        {
            saveSystem.DeleteProfile(pendingDeleteProfile);
            Debug.Log($"[DeleteProfile] Deleted profile {pendingDeleteProfile}");
        }

        pendingDeleteProfile = null;
        confirmDeletePanel.SetActive(false);
        RefreshDropdown();
    }

    public void RenameSelected()
    {
        if (string.IsNullOrEmpty(SelectedProfile))
        {
            return;
        }

        if (string.IsNullOrEmpty(renameInput.text))
        {
            return;
        }

        saveSystem.RenameProfile(SelectedProfile, renameInput.text);
        Debug.Log($"[Rename] {SelectedProfile} {renameInput.text}");

        renameInput.text = string.Empty;
        RefreshDropdown();
    }
}
