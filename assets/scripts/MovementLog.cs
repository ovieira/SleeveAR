using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using FullSerializer;

[XmlRoot("MovementLog")]
public class MovementLog
{

    //public int Field = 1;

    [fsProperty]
    public List<Joint> LogList = new List<Joint>();

    //public List<Vector3> JSONTEST = new List<Vector3>();

    public void Add(Joint t) {
        LogList.Add(t);
    }

    public Joint Get(int index)
    {
       return LogList[index];
    }

    public void testPopulate() {
        for (int i = 0; i < 10000; i++) {
            Joint j = new Joint(new Vector3(i, i, i), Quaternion.identity);
            LogList.Add(j);
        }
    }
}

