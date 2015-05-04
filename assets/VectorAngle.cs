using UnityEngine;
using System.Collections;

public class VectorAngle : MonoBehaviour {

    public Transform[] joints;

    public Vector3[] dirs;
    public LineRenderer _line;
    public float angle;
	// Use this for initialization
	void Start () {
	    dirs = new Vector3[2];
	    _line = gameObject.AddComponent<LineRenderer>();
        _line.SetVertexCount(3);
	    _line.SetWidth(0.05f, 0.05f);
	}
	
	// Update is called once per frame
	void Update ()
	{
        dirs[0] = joints[1].position - joints[0].position;
        dirs[1] = joints[2].position - joints[1].position;


        _line.SetPosition(0, joints[0].position);
        _line.SetPosition(1, joints[1].position);
        _line.SetPosition(2, joints[2].position);

	    angle = Vector3.Angle(dirs[0], dirs[1]);
	}
}
