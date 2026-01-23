using System.Threading;
using UnityEngine;

public class GhostDataRecorder : MonoBehaviour
{
    public GhostData ghostData = new GhostData();
    private bool recording;
    private float timer;

    public void StartRecording()
    {
        ghostData.ghostDataFrames.Clear();
        timer = 0f;
        recording = true;
        Debug.Log("StartRecording");
    }

    public void StopRecording()
    {
        recording = false;
        Debug.Log("StopRecording");
    }

    public void FixedUpdate()
    {
        if (!recording)
        {
            return;
        }

        timer += Time.fixedDeltaTime;
        ghostData.AddFrame(transform.position, transform.eulerAngles, timer);
    }
}
