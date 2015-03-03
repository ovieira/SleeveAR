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
        
        //Debug.Log(splited);
        //string joints_aux = splited[2];
        string[] joints = message.Split(';');

        for (int i = 0; i < joints.Length; i++) {
            string[] _s = joints[i].Split('=');
            if (JointInfo.ContainsKey(_s[0])) {
                if (_s.Length > 1)
                JointInfo[_s[0]] = _s[1];
            }
            else {
                if (_s.Length>1)
                JointInfo.Add(_s[0], _s[1]);
            }
        }
        //Debug.Log("lol");
    }

    void OnGUI()
    {
        
        int i = 0;
        foreach (KeyValuePair<string, string> keyValuePair in JointInfo)
        {
            GUI.Label(new Rect(Screen.width - 400, i * 20 + 15, 400, 100), "" + keyValuePair.Key + " : " + keyValuePair.Value);
            i++;
        }
    }
}