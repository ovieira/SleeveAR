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

    public void Awake() {
        count = _assignedRigidBodies.Length;
        InitializeTrackingDevice();
        InitializeProperties();
    }

	// Use this for initialization
	void Start () {
        
	}

    private void InitializeTrackingDevice() {
        if (_Optitrack != null) 
            _Optitrack.SetActive(_TrackingDevice == TrackingDevice.OPTITRACK);
        if (_Kinect != null) 
            _Kinect.SetActive(_TrackingDevice == TrackingDevice.KINECT);
    }

    #region Properties

    protected Transform[] _transforms;
    public Vector3 [] _floorTransforms;

    public Vector3[] PositionFloor, PositionProjected;
    public Transform Projector;

    private JointsGroup _currentJointsGroup;

    public Transform getRigidBodyTransform(int index) {
        if (index >= count) {
            Debug.Log("ManagerTracking: index out of bounds");
            return null;
        }

        return _transforms[index];
    }

    private void InitializeProperties()
    {
        _transforms = new Transform[count];
        _floorTransforms = new Vector3[count];
        PositionFloor = new Vector3[count];
        PositionProjected = new Vector3[count];
        for (int i = 0; i < count; i++) {
            _transforms[i] = _assignedRigidBodies[i].transform;
        }
    }

    #endregion

    void Update()
    {
        //updateJointGroup();
        updateFloorPositions();
        updateProjectionPosition();
    }

    

    private void updateProjectionPosition() {
        for (int i = 0; i < count; i++) {
            Vector3 rb_pos = getRigidBodyTransform(i).position;
            Vector3 floor_pos = PositionFloor[i];
            Vector3 posTarget = new Vector3(floor_pos.x, rb_pos.y, floor_pos.z);
            Vector3 dir = posTarget - Projector.position;
            Ray r = new Ray(Projector.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit)) {
                PositionProjected[i] = hit.point;
            }
        }
    }

    private void updateFloorPositions() {
        for (int i = 0; i < count; i++) {
            float pX = getRigidBodyTransform(i).position.x * offX + X;
            float pZ = getRigidBodyTransform(i).position.z * offY + Y;

            PositionFloor[i] = new Vector3(pX, 0, pZ);
        }
    }

    #region JointsGroup

    public JointsGroup getCurrentJointGroup()
    {
        JointsGroup jg = new JointsGroup(_transforms[0], _transforms[1], _transforms[2]);

        jg.Print();

        return jg;
    }

    #endregion


    #region SINGLETON

    private static ManagerTracking _instance;
    private float offX = 0.64f;
    private float offY = 0.635f;
    private float X = 0.16f;
    private float Y = -0.04f;

    public static ManagerTracking instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<ManagerTracking>();
            }
            return _instance;
        }
    }

    #endregion


    public void setTracking(bool p) {
        _Optitrack.SetActive(p);
        //_Kinect.SetActive(p);
    }
}
