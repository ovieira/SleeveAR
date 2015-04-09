using UnityEngine;
using System.Collections;

public class RadarTest : MonoBehaviour {

    public Transform Model, Tracked;

    public AudioClip RadarClip;
    public AudioClip CorrectClip;
    
    private float lastPlay;

    public float Interval;
    public float Threshold;
    private bool stop = false;
    private int _modelIndex;
    public float IntervalFactor;

    
     
    // Use this for initialization
    public void Start() {
        Interval = 1;
        lastPlay = 0;
        _modelIndex = 0;
        Model.position = XMLHandler.instance._CurrentLog.Get(_modelIndex).position;
    }

    // Update is called once per frame
    public void Update() {
        lastPlay += Time.deltaTime;

        if (!stop) {
            if (lastPlay >= Interval) {
                GetComponent<AudioSource>().PlayOneShot(RadarClip);
                lastPlay = 0;
            }
        }
        setInterval();
    }

    private void setInterval() {
        //Interval = Mathf.Clamp(Mathf.Abs(Model.position.y - Tracked.position.y), 0.1f, 2f);
        Interval = Mathf.Abs(XMLHandler.instance._CurrentLog.Get(_modelIndex).position.y - Tracked.position.y) / IntervalFactor;
        Model.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, Interval);
        if (Interval < Threshold ) {
            if (stop == false) {
                stop = true;
                if(!GetComponent<AudioSource>().isPlaying)
                    GetComponent<AudioSource>().PlayOneShot(CorrectClip); 
            }
            //_modelIndex+=1;
            Model.position = XMLHandler.instance._CurrentLog.Get(_modelIndex).position;
            return;
        }
        if (Interval>Threshold)
        {
            stop = false;            
        }
    }

    float map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
}
