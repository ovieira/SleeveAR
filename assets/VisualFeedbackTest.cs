using UnityEngine;
using System.Collections;

public class VisualFeedbackTest : MonoBehaviour {

    public Transform cube, sphere,projector, shadowSphere;

    public float offset;
    public LineRenderer _Line;
 
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
        
        //_Sprite.position = cube.position + offset;

	    for (int i = 0; i < ManagerTracking.instance.count; i++)
	    {
            //Vector3 rb_pos = ManagerTracking.instance.getRigidBodyTransform(i).transform.position;
            //Vector3 floor_pos = ManagerTracking.instance._floorTransforms[i];
            //Vector3 posTarget = new Vector3(floor_pos.x, rb_pos.y, floor_pos.z);
            //Vector3 dir = posTarget - projector.position;
            //Ray r = new Ray(projector.position, dir);
            //RaycastHit hit;
            //if (Physics.Raycast(r, out hit)) {
            //    Vector3 v = hit.point;
            //    _Line.SetPosition(i, v*offset);
            //    //shadowSphere.position = v + (cube.transform.up * -1 * offset);
            //    Debug.DrawLine(r.origin,hit.point,Color.white);
            //}
	        _Line.SetPosition(i, ManagerTracking.instance.PositionProjected[i]);
	    }

	}
}
