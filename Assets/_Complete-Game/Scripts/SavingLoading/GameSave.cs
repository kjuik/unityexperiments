using System;
using UnityEngine;

[Serializable]
public class GameSave
{
    [Serializable]
    public struct SavedTransform
    {
        public Vector3 Position;
        public Quaternion Rotation;
    }
}
