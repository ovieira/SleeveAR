using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectListener : MonoBehaviour {
    private Dictionary<string, string> JointInfo;

    // Use this for initialization
    private void Start() {
        JointInfo = new Dictionary<string, string>();
    }

    // Update is called once per frame
    private void Update() {
    }

    public void Parse(string message) {
        string[] splited = message.Split('/');
        string joints_aux = splited[2];
        string[] joints = joints_aux.Split(';');

        for (int i = 0; i < joints.Length; i++) {
            string[] _s = joints[i].Split('=');
            if (JointInfo.ContainsKey(_s[0])) {
                JointInfo[_s[0]] = _s[1];
            }
            else {
                JointInfo.Add(_s[0], _s[1]);
            }
        }
        Debug.Log("lol");
    }
}