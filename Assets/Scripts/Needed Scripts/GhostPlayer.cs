using System.Collections.Generic;
using UnityEngine;

public class GhostPlayer : MonoBehaviour
{
    public GhostData ghostData;

    private int currentFrame = 0;
    private float timer = 0f;

    private void OnEnable()
    {
        currentFrame = 0;
        timer = 0f;

        if (ghostData == null || ghostData.ghostDataFrames.Count == 0)
        {
            enabled = true;
        }
    }

    private void Update()
    {
        if (ghostData == null || currentFrame >= ghostData.ghostDataFrames.Count)
        {
            return;
        }

        timer += Time.deltaTime;

        while (currentFrame < ghostData.ghostDataFrames.Count && ghostData.ghostDataFrames[currentFrame].time <= timer)
        {
            GhostDataFrame frame = ghostData.ghostDataFrames[currentFrame];

            transform.position = frame.position;
            transform.rotation = Quaternion.Euler(frame.rotation);

            currentFrame++;

        }

    }
}