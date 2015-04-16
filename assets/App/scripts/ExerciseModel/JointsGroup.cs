using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;


/// <summary>
/// Represents a single instant of a group of Joints.
/// </summary>
public class JointsGroup
{

    [fsProperty] public List<Joint> jointsList = new List<Joint>(3);

    /// <summary>
    /// Constructor with Joints
    /// </summary>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    /// <param name="j3"></param>
    public JointsGroup(Joint j1, Joint j2, Joint j3)
    {
        jointsList.Add(j1);
        jointsList.Add(j2);
        jointsList.Add(j3);
    }

    /// <summary>
    /// Constructor with GameObjects
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    /// <param name="g3"></param>
    public JointsGroup(GameObject g1, GameObject g2, GameObject g3)
    {
        jointsList.Add(new Joint(g1));
        jointsList.Add(new Joint(g2));
        jointsList.Add(new Joint(g3));
    }

    /// <summary>
    /// Constructor with Transforms
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    public JointsGroup(Transform t1, Transform t2, Transform t3)
    {
        jointsList.Add(new Joint(t1));
        jointsList.Add(new Joint(t2));
        jointsList.Add(new Joint(t3));
    }

   public void Print() {
        foreach (Joint joint in jointsList)
        {
            joint.Print();
        }
    }
}
