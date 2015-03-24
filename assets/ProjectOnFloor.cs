using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[DisallowMultipleComponent]
public class ProjectOnFloor : MonoBehaviour {

    //public Transform cube;

    public GameObject _Prefab;

    private List<Transform> _floorObjects = new List<Transform>(); 

    public float mX, mY;
	// Use this for initialization
	void Start ()
	{
	    InstantiateFloorObjects();
	}

    private void InstantiateFloorObjects() {
        for (int i = 0; i < ManagerTracking.instance.count; i++)
        {
            GameObject obj = (GameObject) Instantiate(_Prefab, Vector3.zero, Quaternion.identity);
            obj.transform.parent = this.transform;
            _floorObjects.Add(obj.transform);
        }
    }
	
	// Update is called once per frame
	void Update ()
	{
	    UpdatePositions();
	}

    private void UpdatePositions()
    {
        for (int i = 0; i < ManagerTracking.instance.count; i++)
        {
            _floorObjects[i].position = ManagerTracking.instance.PositionFloor[i];
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
