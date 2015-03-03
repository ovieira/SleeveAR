using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectListener : MonoBehaviour {
    private Dictionary<string, string> JointInfoString;
    private Dictionary<string, Joint> JointInfo;

    public string[] _jointsRequired;

    private List<string> _JointsRequired;

    public struct Joint
    {
        public Vector3 position, rotation;

        public Joint(Vector3 p, Vector3 r) {
            this.position = p;
            this.rotation = r;
        }

        

        public Joint(string p, string r)
        {
            this.position = ParseVector3(p);
            this.rotation = ParseVector3(r);
        }

        

        //public override string ToString()
        //{
        //    string 
        //}
    }

    public static Vector3 ParseVector3(string _string) {
        string[] s = _string.Split(':');
        float x = float.Parse(s[0]);
        float y = float.Parse(s[1]);
        float z = float.Parse(s[2]);
        return new Vector3(x,y,z);
    }

    // Use this for initialization
    private void Start() {
        JointInfoString = new Dictionary<string, string>();
        JointInfo = new Dictionary<string, Joint>();
        _JointsRequired = new List<string>(_jointsRequired);
        foreach (string s in _JointsRequired)
        {
            JointInfoString.Add(s, "");
            JointInfo.Add(s, new Joint());
        }
    }

    // Update is called once per frame
    private void Update() {
    }

    public void Parse(string message) {
        
        //Debug.Log(splited);
        //string joints_aux = splited[2];
        string[] joints = message.Split(';');

        for (int i = 0; i < joints.Length; i++) {
            string[] _s = joints[i].Split('=');
            if (_JointsRequired.Contains(_s[0]))
            {
                string jointID = _s[0].Substring(0, _s[0].Length - 1);
                JointInfoString[_s[0]] = _s[1];

            }
        }
        //Debug.Log("lol");
    }

    void OnGUI()
    {
        
        int i = 0;
        foreach (KeyValuePair<string, string> keyValuePair in JointInfoString)
        {
            if (_JointsRequired.Contains(keyValuePair.Key)) {
                GUI.Label(new Rect(Screen.width - 400, i * 20 + 15, 400, 100), "" + keyValuePair.Key + " : " + keyValuePair.Value);
                i++;
            }
        }
    }
}