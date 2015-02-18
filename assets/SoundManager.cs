using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    private MovementLog _log;
    public RadarTest Radar;
    public PitchTest1 Pitch;

    public enum AudioFeedback
    {
        PITCH,
        RADAR
    }

    public AudioFeedback _Feedback;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void LoadLog() {
        print("Loaded");
        _log = XMLHandler.instance._CurrentLog;

        switch (_Feedback) {
            case AudioFeedback.PITCH:
                Pitch.enabled = true;
                Radar.enabled = false;
                break;
            case AudioFeedback.RADAR:
                Pitch.enabled = false;
                Radar.enabled = true;
                break;
            default:
                break;
        }

    }
}
