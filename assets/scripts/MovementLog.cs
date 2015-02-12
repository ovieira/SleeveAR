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
    [XmlArrayItem("TimeCoord")]
    public List<TimeCoord> _log = new List<TimeCoord>();

    public void Add(TimeCoord t) {
        _log.Add(t);
    }

    public TimeCoord Get(int index)
    {
       return _log[index];
    }
}

