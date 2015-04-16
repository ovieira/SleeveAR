using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using FullSerializer;

[XmlRoot("ExerciseModel")]
public class ExerciseModel {

    public List<JointsGroup> _exerciseModel = new List<JointsGroup>();

    //public Dictionary<int, singleJointLi> ();


    /// <summary>
    /// Adds JointGroup to current exerciseModel
    /// </summary>
    /// <param name="jointsGroup"></param>
    public void Add(JointsGroup jointsGroup) {
        _exerciseModel.Add(jointsGroup);
    }

    /// <summary>
    /// Return JointGroup at index position
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public JointsGroup Get(int index) {
        return _exerciseModel[index];
    }

    public void testPopulate() {
        Debug.Log("Started Populate");
        for (int i = 0; i < 10000; i++) {
            Joint j = new Joint(new Vector3(i, i, i), new Quaternion(i,i,i,1));
            JointsGroup jg = new JointsGroup(j, j, j);
            _exerciseModel.Add(jg);
        }
    }
}

