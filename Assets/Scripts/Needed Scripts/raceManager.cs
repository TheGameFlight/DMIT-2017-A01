using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class raceManager : MonoBehaviour
{
    public static raceManager Instance;

    public List<CheckpointsTrigger> checkpoints = new List<CheckpointsTrigger>();
    public GhostDataRecorder ghostRecorder;

    public GhostPlayer ghostPrefab;
    private GhostPlayer ghostInstance;

    public GameObject savePromptPanel;
    public TMPro.TextMeshProUGUI finalTimeText;

    [SerializeField] private SaveSystem saveSystem;

    private bool raceStarted = false;
    private bool raceFinished = false;
    private int nextCheckpointIndex = 0;
    private float raceTimer = 0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        savePromptPanel.SetActive(false);
        if (string.IsNullOrEmpty(ActiveProfile.profileName))
        {
            return;
        }

        SaveData data = saveSystem.LoadProfile(ActiveProfile.profileName);
        if (data != null && data.ghostData != null)
        {
            StartRaceWithGhost(data.ghostData);
        }
    }

    private void Update()
    {
        if (raceStarted && !raceFinished)
        {
            raceTimer += Time.deltaTime;
        }
    }

    public void StartFinishPassed()
    {
        if (!raceStarted)
        {
            raceStarted = true;
            raceTimer = 0f;
            nextCheckpointIndex = 0;
            ghostRecorder.StartRecording();
            Debug.Log("Race Started");
            Debug.Log($"Checkpoint progress: 0 / {checkpoints.Count}");

            SaveData data = saveSystem.LoadProfile(ActiveProfile.profileName);
            if (data != null && data.ghostData != null)
            {
                StartRaceWithGhost(data.ghostData);
            }

            return;
        }

        if (raceStarted && !raceFinished && AllCheckpointsPassed())
        {
            FinishRace();

        }
        else
        {
            Debug.Log($"Finish line reached, but checkpoints incomplete " + $"({nextCheckpointIndex} / {checkpoints.Count})");
        }
    }

    private void FinishRace()
    {
        raceFinished = true;
        ghostRecorder.StopRecording();
        finalTimeText.text = $"Final Time: {raceTimer:F2}";
        savePromptPanel.SetActive(true);

        Debug.Log("RACE FINISHED");
        Debug.Log($"Race Finished! Time: {raceTimer}");
    }

    public void checkpointPassed(int checkpointIndex)
    {
        if (!raceStarted || raceFinished)
        {
            return;
        }

        if (checkpointIndex != nextCheckpointIndex)
        {
            return;
        }

        nextCheckpointIndex++;
        Debug.Log($"Checkpoint {checkpointIndex} passed");
    }

    private bool AllCheckpointsPassed()
    {

        return nextCheckpointIndex >= checkpoints.Count;

    }

    public void StartRaceWithGhost(GhostData previousGhost)
    {
        if (previousGhost == null || previousGhost.ghostDataFrames.Count == 0)
        {
            return;

        }

        if (ghostInstance == null)
        {
            ghostInstance = Instantiate(ghostPrefab);
        }
        ghostInstance.ghostData = previousGhost;
        ghostInstance.gameObject.SetActive(true);
    }

    public int GetFinalTime()
    {
        return Mathf.RoundToInt(raceTimer);
    }

}
