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

    public int Field = 1;

    [XmlArray("Joint1")]
    [XmlArrayItem("Joint")]
    [fsProperty]
    public List<Joint> _log = new List<Joint>();


    public void Add(Joint t) {
        _log.Add(t);
    }

    public Joint Get(int index)
    {
       return _log[index];
    }
}

