using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerJointsFloorProjection : Controller {

    //public Transform cube;

    public GameObject _Prefab;

    private List<Transform> _floorObjects = new List<Transform>(); 

	// Use this for initialization
	protected override void Start ()
	{
        base.Start();
	    InstantiateFloorObjects();
	}

    private void InstantiateFloorObjects() {
        for (int i = 0; i < serviceTracking.count; i++)
        {
            GameObject obj = (GameObject) Instantiate(_Prefab, Vector3.zero, Quaternion.identity);
            obj.name = i + "";
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
        for (int i = 0; i < serviceTracking.count; i++)
        {
            _floorObjects[i].position = serviceTracking.PositionFloor[i];
        }
    }
}
