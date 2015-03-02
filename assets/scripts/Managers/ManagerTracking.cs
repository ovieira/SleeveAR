using UnityEngine;
using System.Collections;

public class ManagerTracking : MonoBehaviour {

    public enum TrackingDevice
    {
        OPTITRACK,
        KINECT
    }

    [SerializeField]
    protected GameObject _Optitrack, _Kinect;

    [SerializeField]
    protected TrackingDevice _TrackingDevice;

    [SerializeField]
    private GameObject[] _assignedRigidBodies;

    public int count { get; set; }

	// Use this for initialization
	void Start () {
        count = _assignedRigidBodies.Length;
	    InitializeTrackingDevice();
	    InitializeProperties();
	}

    private void InitializeTrackingDevice() {
        if (_Optitrack != null) 
            _Optitrack.SetActive(_TrackingDevice == TrackingDevice.OPTITRACK);
        if (_Kinect != null) 
            _Kinect.SetActive(_TrackingDevice == TrackingDevice.KINECT);
    }

    #region Properties

    protected Transform[] _transforms;

    public Transform getRigidBody(int index) {
        if (index >= count) {
            Debug.Log("ManagerTracking: index out of bounds");
            return null;
        }

        return _transforms[index];
    }

    private void InitializeProperties()
    {
        _transforms = new Transform[count];
        for (int i = 0; i < count; i++) {
            _transforms[i] = _assignedRigidBodies[i].transform;
        }
    }

    #endregion


    #region SINGLETON

    private static ManagerTracking _instance;

    public static ManagerTracking instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<ManagerTracking>();
            }
            return _instance;
        }
    }

    #endregion

}
