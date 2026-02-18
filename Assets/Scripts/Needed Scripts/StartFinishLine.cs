using UnityEngine;

public class StartFinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        raceManager.Instance.StartFinishPassed();
    }
}
