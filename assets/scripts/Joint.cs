using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

public class Joint {

    //public Joint(DateTime t, Vector3 v) {
    //    _time = t;
    //    _position = v;
    //}

    internal Joint() { }

    public Joint(DateTime d, Vector3 p, Quaternion q) {
        _time = d;
        _position = p;
        _rotation = q;
    }

    public Joint(DateTime d, float x, float y, float z, float qx, float qy, float qz, float qw) {
        _time = d;
        _position = new Vector3(x, y, z);
        _rotation = new Quaternion(qx, qy, qz, qw);
    }

    #region TimeStamp Attributes
    [XmlAttribute("Time")]
    public DateTime _time; 
    #endregion

    #region Position Attributes
    [XmlAttribute("X")]
    public float _x;

    [XmlAttribute("Y")]
    public float _y;

    [XmlAttribute("Z")]
    public float _z; 
    #endregion

    #region Rotation Attributes
    
    [XmlAttribute("QX")]
    public float _qx;

    [XmlAttribute("QY")]
    public float _qy;

    [XmlAttribute("QZ")]
    public float _qz;

    [XmlAttribute("QW")]
    public float _qw;

    #endregion

    public Vector3 _position;
    public Quaternion _rotation;
    
    public void Add(DateTime d,float x, float y, float z,float qx, float qy, float qz, float qw) {
        _time = d;
        _position = new Vector3(x, y, z);
        _rotation = new Quaternion(qx, qy, qz, qw);
    }

    public void UpdateJoint(string iType, string value) {
        if (iType == "P") {
            _position = ParsePosition(value);;
        }
        else if (iType == "O")
        {
            _rotation = ParseRotation(value);
        }
    }

    private Quaternion ParseRotation(string _string) {
        string[] s = _string.Split(':');
        float x = float.Parse(s[0]);
        float y = float.Parse(s[1]);
        float z = float.Parse(s[2]);
        float w = float.Parse(s[3]);
        return new Quaternion(x,y,z,w);
    }

    private Vector3 ParsePosition(string _string) {
        string[] s = _string.Split(':');
        float x = float.Parse(s[0]);
        float y = float.Parse(s[1]);
        float z = float.Parse(s[2]);
        return new Vector3(x, y, z);
    }
}
