using UnityEngine;
using System.Collections;

public class ExerciseSimulator : MonoBehaviour {

    public enum ExerciseDemo
    {
        Demo1,
        Demo2,
        Demo3
    }

    public ExerciseDemo Demo;
    public Transform target;
    public Vector3 pA, pB;
    public float Speed;
    private float lerpV = 0f;
	// Use this for initialization
	void Start () {
	}
	// Update is called once per frame
	void Update () {
        switch (Demo) {
            case ExerciseDemo.Demo1:
                _demo1();
                break;
            case ExerciseDemo.Demo2:
                break;
            case ExerciseDemo.Demo3:
                break;
            default:
                break;
        }
	}

    #region Demo 1
    private void _demo1()
    {
        
        target.position = Vector3.Lerp(pA, pB, Mathf.Abs(Mathf.Sin(lerpV)));
        lerpV += Time.deltaTime*Speed;
        
    } 
    #endregion
}
