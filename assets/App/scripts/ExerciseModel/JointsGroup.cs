﻿using System;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

/// <summary>
///     Represents a single instant of a group of Joints.
/// </summary>
public class JointsGroup :mDistanceMeasurable
{
    #region Public Variables

    /// <summary>
    /// List of individual joints that represent the JointGroup
    /// </summary>
    [fsProperty]
    public List<SingleJoint> jointsList = new List<SingleJoint>(3);

    [fsProperty]
    public float angle; 

    #endregion

    #region Private Variables

    [NonSerialized]
    public Vector3 upperArmDirection, foreArmDirection;
    
    #endregion


    /// <summary>
    ///     Constructor with Joints
    /// </summary>
    /// <param name="j1"></param>
    /// <param name="j2"></param>
    /// <param name="j3"></param>
    public JointsGroup(SingleJoint j1, SingleJoint j2, SingleJoint j3)
    {
        jointsList.Add(j1);
        jointsList.Add(j2);
        jointsList.Add(j3);
        CalculateJointGroupAngle();
    }

    /// <summary>
    ///     Constructor with GameObjects
    /// </summary>
    /// <param name="g1"></param>
    /// <param name="g2"></param>
    /// <param name="g3"></param>
    public JointsGroup(GameObject g1, GameObject g2, GameObject g3)
    {
        jointsList.Add(new SingleJoint(g1));
        jointsList.Add(new SingleJoint(g2));
        jointsList.Add(new SingleJoint(g3));
        CalculateJointGroupAngle();
    }

    /// <summary>
    ///     Constructor with Transforms
    /// </summary>
    /// <param name="t1"></param>
    /// <param name="t2"></param>
    /// <param name="t3"></param>
    public JointsGroup(Transform t1, Transform t2, Transform t3)
    {
        jointsList.Add(new SingleJoint(t1));
        jointsList.Add(new SingleJoint(t2));
        jointsList.Add(new SingleJoint(t3));
        CalculateJointGroupAngle();
    }

    public void Print()
    {
        foreach (var joint in jointsList)
        {
            joint.Print();
        }
        Debug.Log("Angle: " + angle);
        Debug.Log("UpperArmDir: " + this.getUpperArmDirection());
        Debug.Log("ForeArm: " + this.getForeArmDirection());
    }

    private void CalculateJointGroupAngle()
    {
        upperArmDirection = jointsList[1].position - jointsList[0].position;
        foreArmDirection = jointsList[2].position - jointsList[1].position;
        upperArmDirection.Normalize();
        foreArmDirection.Normalize();
        angle = Vector3.Angle(upperArmDirection, foreArmDirection);
        //angle = Utils.Angle(upperArmDirection, foreArmDirection);
    }

    public float getHeight()
    {
        return upperArmDirection.y;
    }

    public Vector3 getUpperArmDirection() {
        if(upperArmDirection == Vector3.zero)
            upperArmDirection = (jointsList[1].position - jointsList[0].position);
        //Debug.Log("LOL"+upperArmDirection);
        upperArmDirection.Normalize();
        //Debug.Log(upperArmDirection);
        return upperArmDirection;
    }

    public Vector3 getForeArmDirection() {
        if (foreArmDirection == Vector3.zero)
            foreArmDirection = (jointsList[2].position - jointsList[1].position).normalized;
        return foreArmDirection;
    }

    public float getDistance(mDistanceMeasurable other, bool upper)
    {
        JointsGroup otherJG = (JointsGroup) other;

        if (upper)
        {
            var thisUpperDir = getUpperArmDirection();
            var otherUpperDir = otherJG.getUpperArmDirection();
            return Vector3.Distance(thisUpperDir, otherUpperDir);
        }
        else
        {
            var thisUpperDir = getForeArmDirection();
            var otherUpperDir = otherJG.getForeArmDirection();
            return Vector3.Distance(thisUpperDir, otherUpperDir);
        }
    }
}