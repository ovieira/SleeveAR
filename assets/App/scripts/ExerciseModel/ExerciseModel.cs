using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

[XmlRoot("ExerciseModel")]
public class ExerciseModel
{
    public List<JointsGroup> _exerciseModel = new List<JointsGroup>();
    //public Dictionary<int, singleJointLi> ();


    /// <summary>
    ///     Adds JointGroup to current exerciseModel
    /// </summary>
    /// <param name="jointsGroup"></param>
    public void Add(JointsGroup jointsGroup)
    {
        _exerciseModel.Add(jointsGroup);
    }

    /// <summary>
    ///     Return JointGroup at index position
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public JointsGroup Get(int index)
    {
        return _exerciseModel[index];
    }

    public void testPopulate()
    {
        Debug.Log("Started Populate");
        for (var i = 0; i < 10000; i++)
        {
            var j = new SingleJoint(new Vector3(i, i, i), new Quaternion(i*5, i*5, i*5, 1));
            var jg = new JointsGroup(j, j, j);
            _exerciseModel.Add(jg);
        }
    }
}