using System;
using UnityEngine;
using System.Collections;

public class PitchTest1 : MonoBehaviour
{

    public Transform Model, Tracked;

    
    public AudioClip audioclip;

    private float lastPlay;

    public float Interval;
    public float Threshold;
    private bool stop = false;
    private int _modelIndex;
    public float IntervalFactor;
    private float dist;



    // Use this for initialization
    public void Start() {
        Interval = 1;
        lastPlay = 0;
        _modelIndex = 0;
        Model.position = XMLHandler.instance._CurrentLog.Get(_modelIndex)._position;
        GetComponent<AudioSource>().PlayOneShot(audioclip);
    }

    // Update is called once per frame
    public void Update() {
        lastPlay += Time.deltaTime;

        //if (!stop) {
        //    if (lastPlay >= Interval) {
        //        audio.PlayOneShot(RadarClip);
        //        lastPlay = 0;
        //    }
        //}
        setInterval();
        changePitch();

    }

    private void setInterval() {
        //Interval = Mathf.Clamp(Mathf.Abs(Model.position.y - Tracked.position.y), 0.1f, 2f);
        dist = (XMLHandler.instance._CurrentLog.Get(_modelIndex)._position.y - Tracked.position.y) / IntervalFactor;
        Interval = Mathf.Abs(dist) ;
        Model.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, Interval);
        if (Interval < Threshold) {
            if (stop == false) {
                stop = true;
                GetComponent<AudioSource>().pitch = 1;
            }
            _modelIndex += 1;
            Model.position = XMLHandler.instance._CurrentLog.Get(_modelIndex)._position;
            return;
        }
        if (Interval > Threshold) {
            stop = false;
        }
    }

    private void changePitch() {
        //Debug.Log(target.transform.position.y);
        GetComponent<AudioSource>().pitch = Mathf.Clamp(1 - dist, 0.5f, 2f);
    }

    float map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}
