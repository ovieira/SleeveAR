using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GravityDemoScript : MonoBehaviour {

    public GameObject[] _balls;
    public Transform Target;
    public float factor = 1;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < _balls.Length; i++) {
            _balls[i].rigidbody.AddForce(Random.Range(0, 3), Random.Range(0, 3), Random.Range(0, 3));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        for (int i = 0; i < _balls.Length; i++) {
            _balls[i].rigidbody.AddForce((Target.position - _balls[i].transform.position) * factor,ForceMode.Force);
        }
    }
}
