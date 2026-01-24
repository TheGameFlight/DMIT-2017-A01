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

    private bool raceStarted = false;
    private bool raceFinished = false;
    private int nextCheckpointIndex = 0;

    private float raceTimer = 0f;

    [SerializeField] private SaveSystem saveSystem;
    [SerializeField] private string activeProfileName;

    

    //delete after testing
    public Transform ghostSpawnPoint;
    //delete after testing

    private void Start()
    {
        LoadGhostForActiveProfile();
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (raceStarted && !raceFinished)
        {
            raceTimer += Time.deltaTime;
        }
    }

    public void SetActiveProfile(string profileName)
    {
        activeProfileName = profileName;
    }

    private void LoadGhostForActiveProfile()
    {
        if (saveSystem == null)
            return;

        SaveData data = saveSystem.LoadProfile(activeProfileName);

        if (data != null && data.ghostData != null)
        {
            StartRaceWithGhost(data.ghostData);
        }

    }

    public void StartRaceWithGhost(GhostData previousGhost)
    {
        if (previousGhost == null || previousGhost.ghostDataFrames.Count == 0)
            return;

        if (ghostInstance == null)
            ghostInstance = Instantiate(ghostPrefab);

        ghostInstance.ghostData = previousGhost;
        ghostInstance.gameObject.SetActive(true);
    }

    public void StartFinishPassed()
    {
        if (!raceStarted)
        {
            raceStarted = true;
            raceTimer = 0f;
            nextCheckpointIndex = 0;

            Debug.Log("Race Started");
            Debug.Log($"Checkpoint progress: 0 / {checkpoints.Count}");

            ghostRecorder.StartRecording();
            return;
        }

        if (raceStarted && !raceFinished && AllCheckpointsPassed())
        {
            raceFinished = true;
            Debug.Log("RACE FINISHED");
            Debug.Log($"Race Finished! Time: {raceTimer}");

            ghostRecorder.StopRecording();
            saveSystem.SaveGhost(activeProfileName, ghostRecorder.ghostData, Mathf.RoundToInt(raceTimer));

            //delete after testing
            SpawnTestGhost();
            //delete after testing
        }
        else
        {
            Debug.Log($"Finish line reached, but checkpoints incomplete " + $"({nextCheckpointIndex} / {checkpoints.Count})");
        }
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

    //delete after testing
    private void SpawnTestGhost()
    {
        Debug.Log("SpawnTestGhost() called");
        if (ghostRecorder == null ||
            ghostRecorder.ghostData == null ||
            ghostRecorder.ghostData.ghostDataFrames.Count == 0)
        {
            Debug.Log("No ghost data to play");
            return;
        }

        if (ghostInstance != null)
        {
            Destroy(ghostInstance.gameObject);
        }

        ghostInstance = Instantiate(
            ghostPrefab,
            ghostSpawnPoint.position,
            ghostSpawnPoint.rotation
        );

        ghostInstance.ghostData = ghostRecorder.ghostData;
        ghostInstance.gameObject.SetActive(true);

        Debug.Log("Ghost spawned for testing");
    }
    //delete after testing
}
