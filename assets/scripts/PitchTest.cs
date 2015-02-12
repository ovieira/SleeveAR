using System;
using UnityEngine;
using System.Collections;

public class PitchTest : MonoBehaviour
{

    public GameObject target;

    public AudioClip audioclip;
    private bool _play = true;

	// Use this for initialization
	void Start ()
	{

	}

   
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        if (_play)
	        {
                audio.PlayOneShot(audioclip);
                //StartCoroutine (WaitMethod ());
	        }
	        else
	        {
                //audio.Pause();
	        }
            //_play ^= true;

	    }

        changePitch();

	}

    IEnumerator WaitMethod() {
        yield return new WaitForSeconds(2f);
        audio.PlayOneShot(audioclip);
    }

    private void changePitch() {
        //Debug.Log(target.transform.position.y);
        audio.pitch = map(target.transform.position.y, 0, 10, 0, 1);
    }

    float map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}
