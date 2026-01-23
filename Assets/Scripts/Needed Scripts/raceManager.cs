using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class raceManager : MonoBehaviour
{
    public static raceManager Instance;

    public List<CheckpointsTrigger> checkpoints = new List<CheckpointsTrigger>();

    private bool raceStarted = false;
    private bool raceFinished = false;
    private int nextCheckpointIndex = 0;

    private float raceTimer = 0f;

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

    public void StartFinishPassed()
    {
        if (!raceStarted)
        {
            raceStarted = true;
            raceTimer = 0f;
            nextCheckpointIndex = 0;

            Debug.Log("Race Started");
            Debug.Log($"Checkpoint progress: 0 / {checkpoints.Count}");
            return;
        }

        if (raceStarted && !raceFinished && AllCheckpointsPassed())
        {
            raceFinished = true;
            Debug.Log("RACE FINISHED");
            Debug.Log($"Race Finished! Time: {raceTimer}");
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
}
