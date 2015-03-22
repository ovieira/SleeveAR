using UnityEngine;
using System.Collections;

public class OffsetScript : MonoBehaviour {

    //public Transform cube;

    public GameObject[] _offsetObjects;

    public float pX, pY, pZ;

    public float unity_world_width = 2.8f;
    public float unity_world_depth = 2f;
    public float ratio;
    public float offX, offY;
    public float projection_width;
    public float projection_depth;
    public float mX, mY,X,Y;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update ()
	{

        //ratio = unity_world_width/unity_world_depth;

	    for (int i = 0; i < ManagerTracking.instance.count; i++)
	    {

	        _offsetObjects[i].transform.position = ManagerTracking.instance.PositionFloor[i];
	    }
	}

    void OnGUI()
    {
        Vector3 p = Input.mousePosition;
      
        //Debug.Log(p);
        //GUI.Label(new Rect(p.x - 50, Screen.height - p.y - 20, 150f, 30f), p.x + "," + p.y + " , " + p.z));
        Vector3 pp = Camera.main.ScreenToWorldPoint(p);
        mX = pp.x;
        mY = pp.y;
        GUI.Label(new Rect(p.x - 50, Screen.height - p.y - 30, 150f, 40f), pp.x + "," + pp.y + " , " + pp.z);

        GUI.Label(new Rect(p.x - 50, Screen.height - p.y + 30, 150f, 40f),  pp.x + "," + pp.y + " , " + pp.z);
    }
}
