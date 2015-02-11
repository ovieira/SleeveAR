using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;

public class TimeCoord {

    //public TimeCoord(DateTime t, Vector3 v) {
    //    _time = t;
    //    _position = v;
    //}

    internal TimeCoord() { }

    public TimeCoord(DateTime d, Vector3 p) {
        _time = d;
        _position = p;
    }

    [XmlAttribute("Time")]
    public DateTime _time /*{ get; set; }*/;

    [XmlAttribute("Position")]
    public Vector3 _position /*{ get; set; }*/;

    public void Add(DateTime d, Vector3 p)
    {
        _time = d;
        _position = p;
    }
}
