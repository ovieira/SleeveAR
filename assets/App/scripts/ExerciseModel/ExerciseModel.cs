using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("ExerciseModel")]
public class ExerciseModel
{
    #region Label
    public String exerciseID; 
    #endregion

    #region Exercise Model List
    public List<JointsGroup> exerciseModel = new List<JointsGroup>();
    //public Dictionary<int, singleJointLi> ();
    
    #region Getters/Setters
    /// <summary>
    ///     Adds JointGroup to current exerciseModel
    /// </summary>
    /// <param name="jointsGroup"></param>
    public void Add(JointsGroup jointsGroup) {
        exerciseModel.Add(jointsGroup);
    }

    /// <summary>
    ///     Return JointGroup at index position
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public JointsGroup Get(int index) {
        return exerciseModel[index];
    }
    #endregion
    #endregion

    #region Exercise Parts

    public List<Vector2> parts = new List<Vector2>();

    public void addPart(int leftBound, int rightBound)
    {
        if (exerciseModel.Count == 0) return;
        if (leftBound < 0 || rightBound < 0 || leftBound >= rightBound)
        {
            Debug.LogError("Invalid Exercise Bounds");
            return;
        }

        parts.Add(new Vector2(leftBound,rightBound));
        Debug.Log("Added Part:");
        foreach (var part in parts)
        {
            Debug.Log(part);
        }
    }


    #endregion

    public List<Vector3> GetUpperArmDirectionList()
    {
        var _list = new List<Vector3>();

        foreach (JointsGroup jointsGroup in exerciseModel)
        {
            _list.Add(jointsGroup.getUpperArmDirection());
        }
        return _list;
    } 

    public void print()
    {
        Debug.Log("Name: " + this.exerciseID);
        Debug.Log("Entries: " + this.exerciseModel.Count );
        Debug.Log("Exercise Parts: ");
        for (int i = 0; i < parts.Count; i++)
        {
            Debug.Log(parts[i]);
        }
    }

    public void testPopulate()
    {
        Debug.Log("Started Populate");
        for (var i = 0; i < 10; i++)
        {
            var j = new SingleJoint(new Vector3(i, i, i), new Quaternion(i*5, i*5, i*5, 1));
            var jg = new JointsGroup(j, j, j);
            exerciseModel.Add(jg);
        }
    }
}