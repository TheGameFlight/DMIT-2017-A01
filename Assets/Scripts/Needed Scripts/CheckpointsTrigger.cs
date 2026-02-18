using UnityEngine;

public class CheckpointsTrigger : MonoBehaviour
{
    public int checkpointIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        raceManager.Instance.checkpointPassed(checkpointIndex);
    }
}
