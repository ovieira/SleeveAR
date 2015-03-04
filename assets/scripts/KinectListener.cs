using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinectListener : MonoBehaviour {
    private Dictionary<string, string> JointInfoString;
    private Dictionary<string, Joint> JointInfo;

    public string[] _jointsRequired;

    private List<string> _JointsRequired;

    //public struct Joint {
    //    public Vector3 position, rotation;

    //    public Joint(Vector3 p, Vector3 r) {
    //        this.position = p;
    //        this.rotation = r;
    //    }

    //    public Joint(string p, string r) {
    //        this.position = ParsePosition(p);
    //        this.rotation = ParsePosition(r);
    //    }

    //    public void UpdateJoint(string iType, string value) {
    //        Vector3 v = ParsePosition(value);
    //        if (iType == "P") {
    //            this.position = v;
    //        }
    //        else if (iType == "O") {
    //            this.rotation = v;
    //        }
    //    }

    //    //public override string ToString()
    //    //{
    //    //    string 
    //    //}
    //}

   

    // Use this for initialization
    private void Start() {
        JointInfoString = new Dictionary<string, string>();
        JointInfo = new Dictionary<string, Joint>();
        _JointsRequired = new List<string>(_jointsRequired);
        foreach (string s in _JointsRequired) {
            JointInfoString.Add(s, "");
            string js = s.Substring(0, s.Length - 1);
            if (!JointInfo.ContainsKey(js))
                JointInfo.Add(s.Substring(0, s.Length-1), new Joint());
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
            if (_JointsRequired.Contains(_s[0])) {
                UpdateJoint(_s[0], _s[1]);
            }
        }
        //Debug.Log("lol");
    }

    public void UpdateJoint(string jID, string iType, string value) {
        JointInfo[jID].UpdateJoint(iType, value);
    }

    public void UpdateJoint(string ID, string value)
    {
        string jointID = ID.Substring(0, ID.Length - 1);
        string infoType = ID.Substring(ID.Length - 1, 1);
        JointInfoString[ID] = value;

        UpdateJoint(jointID, infoType, value);
    }

    void OnGUI() {

        int i = 0;
        foreach (KeyValuePair<string, Joint> keyValuePair in JointInfo) {
            GUI.Label(new Rect(Screen.width - 400, i * 40 + 15, 400, 100), "" + keyValuePair.Key + ":");
            GUI.Label(new Rect(Screen.width - 300, i * 40 + 15, 400, 100), "P: " + keyValuePair.Value._position);
            GUI.Label(new Rect(Screen.width - 300, i * 40 + 30, 400, 100), "O: " + keyValuePair.Value._rotation);
            i++;
        }
    }

    private string _testString =
        "HandLeftS=Unknown;HandLeftC=Low;HandRightS=Unknown;HandRightC=Low;Lean=0,8758649:0,5484357;LeanSTracked;HeadP=0.4654921:-0.570684:2.54236;HeadO=0:0:0:0;NeckP=0.3948015:-0.6561701:2.580424;NeckO=0.1670709:0.1274583:0.9551386:-0.2086912;SpineBaseP=0.2473954:-1.052225:2.709532;SpineBaseO=0.1259917:0.1558652:0.9669392:-0.1576732;SpineMidP=0.3223307:-0.8541669:2.651183;SpineMidO=0.1520128:0.151558:0.9631913:-0.1618177;ShoulderLeftP=0.2382424:-0.6669062:2.545516;ShoulderLeftO=-0.01541323:-0.5643243:0.7909305:-0.2360707;ElbowLeftP=0.1411756:-0.8159271:2.443533;ElbowLeftO=-0.2959315:0.9226919:-0.221312:-0.1099323;WristLeftP=0.1504416:-0.9697894:2.328408;WristLeftO=-0.2267693:0.624565:-0.2206833:0.713998;HandLeftP=0.1570076:-0.9641067:2.270011;HandLeftO=0.5558995:-0.4519687:0.4889807:-0.497592;ShoulderRightP=0.5616466:-0.7760696:2.704201;ShoulderRightO=0.2719452:0.8106255:0.5180601:0.02336042;ElbowRightP=0.5503948:-0.9114395:2.560738;ElbowRightO=-0.3115782:0.763102:-0.2456178:0.5101629;WristRightP=0.4959866:-0.927136:2.370693;WristRightO=0.6128554:-0.5510417:-0.2912579:0.4857264;HandRightP=0.4379477:-0.9738909:2.307336;HandRightO=0.4939632:-0.4646151:-0.1304423:0.7232689;HipLeftP=0.1851055:-1.021028:2.657459;HipLeftO=0.09969288:-0.4751945:0.8181674:-0.3079831;KneeLeftP=0.08525138:-0.9607603:2.278743;KneeLeftO=0.6479138:-0.4510132:-0.3952876:0.4696196;AnkleLeftP=0.02243579:-0.9090995:1.850968;AnkleLeftO=0.6198006:-0.4903421:-0.4185305:0.4474864;FootLeftP=0.04914136:-0.9195393:1.714829;FootLeftO=0:0:0:0;HipRightP=0.3034733:-1.058489:2.696569;HipRightO=-0.01509364:0.728857:0.6675859:-0.1512237;KneeRightP=0.4105481:-0.873898:2.358241;KneeRightO=0.314141:-0.03618985:0.7950163:-0.5176435;AnkleRightP=0.1447073:-0.9597967:2.080803;AnkleRightO=0.2374083:0.2855407:-0.578406:0.7263266;FootRightP=0.152406:-0.9821132:1.944617;FootRightO=0:0:0:0;SpineShoulderP=0.377164:-0.7057469:2.600475;SpineShoulderO=0.1735742:0.1388959:0.9573458:-0.1845779;HandTipLeftP=0.1768923:-0.9719676:2.208328;HandTipLeftO=0:0:0:0;ThumbLeftP=0.2178839:-0.9527963:2.266778;ThumbLeftO=0:0:0:0;HandTipRightP=0.4300965:-0.9767722:2.228052;HandTipRightO=0:0:0:0;ThumbRightP=0.3873569:-0.9360727:2.292379;ThumbRightO=0:0:0:0";
    [ContextMenu("parsetest")]
    public void testParse()
    {
        Parse(_testString);
        Debug.Log(JointInfo.Count);
        
    }
}