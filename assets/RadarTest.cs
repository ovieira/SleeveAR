using UnityEngine;
using System.Collections;

public class RadarTest : MonoBehaviour {

    public Transform Source, Target;

    public AudioClip RadarClip;
    public AudioClip CorrectClip;

    private float lastPlay;

    public float Interval;
    public float Threshold;
    private bool stop = false;

    // Use this for initialization
    void Start() {
        Interval = 1;
        lastPlay = 0;
    }

    // Update is called once per frame
    void Update() {
        lastPlay += Time.deltaTime;

        if (!stop) {
            if (lastPlay >= Interval) {
                audio.PlayOneShot(RadarClip);
                lastPlay = 0;
            }
        }
        setInterval();
    }

    private void setInterval() {
        //Interval = Mathf.Clamp(Mathf.Abs(Source.position.y - Target.position.y), 0.1f, 2f);
        Interval = Mathf.Abs(Source.position.y - Target.position.y) / 4;
        Source.renderer.material.color = Color.Lerp(Color.green, Color.red, Interval);
        if (Interval < Threshold && stop == false) {
            stop = true;
            audio.PlayOneShot(CorrectClip);
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
