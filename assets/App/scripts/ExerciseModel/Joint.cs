using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using FullSerializer;

/// <summary>
/// Represents a single instant of a human joint with its position and rotation.
/// </summary>
public class Joint {

    public Joint() {
        position = new Vector3();
        rotation = Quaternion.identity;
    }

    public Joint(Vector3 p, Quaternion q) {
        position = p;
        rotation = q;
    }

    public Joint(Transform t)
    {
        position = t.position;
        rotation = t.rotation;
    }

    public Joint(GameObject g)
    {
        position = g.transform.position;
        rotation = g.transform.rotation;
    }

    /// <summary>
    /// Joint position
    /// </summary>
    [fsProperty]
    public Vector3 position { get; set; }

    /// <summary>
    /// Joint rotation
    /// </summary>
    [fsProperty]
    public Quaternion rotation { get; set; }

    public void Add(float x, float y, float z, float qx, float qy, float qz, float qw) {
        position = new Vector3(x, y, z);
        rotation = new Quaternion(qx, qy, qz, qw);
    }

    //public void UpdateJoint(string iType, string value) {
    //    if (iType == "P") {
    //        position = ParsePosition(value);;
    //    }
    //    else if (iType == "O")
    //    {
    //        rotation = ParseRotation(value);
    //    }
    //}

    //private Quaternion ParseRotation(string _string) {
    //    string[] s = _string.Split(':');
    //    float x = float.Parse(s[0]);
    //    float y = float.Parse(s[1]);
    //    float z = float.Parse(s[2]);
    //    float w = float.Parse(s[3]);
    //    return new Quaternion(x,y,z,w);
    //}

    //private Vector3 ParsePosition(string _string) {
    //    string[] s = _string.Split(':');
    //    float x = float.Parse(s[0]);
    //    float y = float.Parse(s[1]);
    //    float z = float.Parse(s[2]);
    //    return new Vector3(x, y, z);
    //}
}
