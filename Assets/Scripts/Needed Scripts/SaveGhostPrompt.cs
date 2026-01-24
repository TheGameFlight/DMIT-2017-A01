using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGhostPrompt : MonoBehaviour
{
    public SaveSystem saveSystem;
    public GhostDataRecorder ghostRecorder;
    public raceManager raceManager;

    public void YES()
    {
        string profile = ActiveProfile.profileName;
        if (string.IsNullOrEmpty(profile))
        {
            return;
        }

        GhostData ghostData = ghostRecorder.GetGhostData();
        int finalTime = raceManager.GetFinalTime();

        saveSystem.SaveGhost(profile, ghostData, finalTime);
        SceneManager.LoadScene("MainMenu");
    }
    public void NO()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
