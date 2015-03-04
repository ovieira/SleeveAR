using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot("MovementLog")]
public class MovementLog {



    [XmlArray("TimeCoords")]
    [XmlArrayItem("Joint")]
    public List<Joint> _log = new List<Joint>();

    public void Add(Joint t) {
        _log.Add(t);
    }

    public Joint Get(int index)
    {
       return _log[index];
    }
}

