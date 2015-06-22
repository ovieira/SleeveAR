using FullSerializer;
using UnityEngine;

/// <summary>
///     Represents a single instant of a human joint with its position and rotation.
/// </summary>
public class SingleJoint
{
    protected Vector3 _position;
    public float OffsetMagnitude = 0.05f;

    public SingleJoint()
    {
        position = new Vector3();
        rotation = Quaternion.identity;
    }

    public SingleJoint(Vector3 p, Quaternion q)
    {
        position = p;
        rotation = q;
    }

    public SingleJoint(Transform t)
    {
        position = t.position;
        rotation = t.rotation;
    }

    public SingleJoint(GameObject g)
    {
        position = g.transform.position;
        rotation = g.transform.rotation;
    }

    /// <summary>
    ///     SingleJoint position
    /// </summary>
    [fsProperty]
    public Vector3 position
    {
        get { return _position; }
        set
        {
            _position = value;
            var _up = rotation*Vector3.up;
            positionWithOffset = _position - _up*OffsetMagnitude;
        }
    }

    /// <summary>
    ///     joint position with up vector offset applied
    /// </summary>
    [fsProperty]
    public Vector3 positionWithOffset { get; set; }

    /// <summary>
    ///     SingleJoint rotation
    /// </summary>
    [fsProperty]
    public Quaternion rotation { get; set; }

    public void Add(float x, float y, float z, float qx, float qy, float qz, float qw)
    {
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

    public void Print()
    {
       Debug.Log("P: " + position + "|| R: " + rotation);
    }
}