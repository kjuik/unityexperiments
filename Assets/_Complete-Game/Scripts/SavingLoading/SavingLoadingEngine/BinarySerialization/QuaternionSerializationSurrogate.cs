﻿using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// Source: https://answers.unity.com/questions/956047/serialize-quaternion-or-vector3.html
/// </summary>
sealed class QuaternionSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj,
                              SerializationInfo info, StreamingContext context)
    {
        Quaternion q = (Quaternion)obj;
        info.AddValue("x", q.x);
        info.AddValue("y", q.y);
        info.AddValue("z", q.z);
        info.AddValue("w", q.w);
    }

    public object SetObjectData(object obj,
                                       SerializationInfo info, StreamingContext context,
                                       ISurrogateSelector selector)
    {
        Quaternion q = (Quaternion)obj;
        q.x = (float)info.GetValue("x", typeof(float));
        q.y = (float)info.GetValue("y", typeof(float));
        q.z = (float)info.GetValue("z", typeof(float));
        q.w = (float)info.GetValue("w", typeof(float));
        obj = q;
        return obj;
    }
}