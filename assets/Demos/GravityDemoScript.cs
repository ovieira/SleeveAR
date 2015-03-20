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
            _balls[i].GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(0, 3), 0, Random.Range(0, 3));
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        for (int i = 0; i < _balls.Length; i++) {
            //_balls[i].rigidbody.AddForce((Target.position - _balls[i].transform.position) * factor,ForceMode.Force);
            _balls[i].GetComponent<Rigidbody>().AddForce(PhysicsAttraction(_balls[i].GetComponent<Rigidbody>(), Target.GetComponent<Rigidbody>()));
        }
    }


    Vector3 PhysicsAttraction(Rigidbody a, Rigidbody b)
    {
        return (a.mass*b.mass)/Vector3.Distance(a.transform.position, b.transform.position)*
               (b.transform.position - a.transform.position).normalized;
    }
}
