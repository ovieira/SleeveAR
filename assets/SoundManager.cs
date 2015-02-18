using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private MovementLog _log;
    public RadarTest Radar;
    public PitchTest1 Pitch;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLog() {
        print("Loaded");
        _log = XMLHandler.instance._CurrentLog;
        Radar.enabled = true;
    }
}
