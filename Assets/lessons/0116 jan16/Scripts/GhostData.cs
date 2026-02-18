using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]

public class GhostData
{
    public List<GhostDataFrame> ghostDataFrames = new List<GhostDataFrame>();
    // index
    // position - posX, posY, posZ
    // rotation  - rotX, rotY, rotZ
    public GhostDataRecorder ghostRecorder;
    public void AddFrame(Vector3 position_, Vector3 rotation_, float time_)
    {
        ghostDataFrames.Add(new GhostDataFrame(position_, rotation_, time_));
    }
}

[Serializable]
public class GhostDataFrame
{
    public Vector3 position;
    public Vector3 rotation;
    public float time;

    public GhostDataFrame(Vector3 position_, Vector3 rotation_, float time_)
    {
        position = position_;
        rotation = rotation_;
        time = time_;
    }
}
