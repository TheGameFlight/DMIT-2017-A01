using UnityEngine;
using UnityEngine.SceneManagement;

public class CarSelectManager : MonoBehaviour
{
    public SaveSystem saveSystem;

    private SaveData currentData;

    private void Start()
    {
        currentData = saveSystem.LoadProfile(ActiveProfile.profileName);
    }

    public void SelectCarBody(int index)
    {
        currentData.carBodyIndex = index;
    }

    public void SelectBodyColor(Color color)
    {
        currentData.bodyColor = color;
    }

    public void ConfirmSelection()
    {
        saveSystem.SaveProfile(currentData);
        SceneManager.LoadScene("Game");
    }
    public void GoBack()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
