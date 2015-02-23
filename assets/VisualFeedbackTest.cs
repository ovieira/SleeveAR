using UnityEngine;
using System.Collections;

public class VisualFeedbackTest : MonoBehaviour {

    public Transform cube, sphere,projector, shadowSphere;

    public Vector3 offset;
 
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
        Vector3 posTarget = new Vector3(sphere.position.x, cube.position.y, sphere.position.z);
	    Vector3 dir = posTarget - projector.position;
        Ray r = new Ray(projector.position, dir);
	    RaycastHit hit;
        if(Physics.Raycast(r,out hit))
	    {
	        Vector3 v = hit.point;
	        shadowSphere.position = v+offset;
            //Debug.DrawLine(r.origin,hit.point,Color.white);
	    }
        //_Sprite.position = cube.position + offset;


	}
}
