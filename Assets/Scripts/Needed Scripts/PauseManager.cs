using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePanel;
    private bool isPaused;
    public GameObject CarMenu;

    private InputAction escapeAction;

    private void Awake()
    {
        escapeAction = new InputAction("Escape", InputActionType.Button, "<Keyboard>/escape");
    }

    private void OnEnable()
    {
        escapeAction.Enable();
    }

    private void OnDisable()
    {
        escapeAction.Disable();
    }

    private void Start()
    {
        PausePanel.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        if (escapeAction.WasPressedThisFrame())
        {
            Debug.Log("Pause was pressed");
            PauseMenu();
        }
    }

    public void PauseMenu()
    {
        isPaused = !isPaused;

        PausePanel.SetActive(isPaused);
        CarMenu.SetActive(false);
        Time.timeScale = isPaused ? 0f : 1f;

        Debug.Log("isPaused = " + isPaused);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void Resume()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}