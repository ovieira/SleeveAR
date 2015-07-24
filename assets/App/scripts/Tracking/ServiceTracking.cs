using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ServiceTracking : MonoBehaviour
{
    #region Vars and Refs
    public enum TrackingDevice {
        OPTITRACK,
        KINECT
    }

    [SerializeField]
    private GameObject[] _assignedRigidBodies;
    [SerializeField]
    protected GameObject _Optitrack, _Kinect;
    [SerializeField]
    protected TrackingDevice _TrackingDevice;
    public int count { get; set; }

    #endregion

    #region LifeCycle
    public void Awake() {
        RenderSettings.ambientLight = Color.white;

        count = _assignedRigidBodies.Length;
        InitializeTrackingDevice();
        InitializeProperties();
    }

    public void Start()
    {
        StartCoroutine(updateFloorPositionsRoutine());
        StartCoroutine(updateLightProjectionPositionRoutine());
    }

    private void InitializeTrackingDevice() {
        if (_Optitrack != null)
            _Optitrack.SetActive(_TrackingDevice == TrackingDevice.OPTITRACK);
        if (_Kinect != null)
            _Kinect.SetActive(_TrackingDevice == TrackingDevice.KINECT);
    }


    public void Update() {
        //updateFloorPositions();
        //updateLightProjectionPosition();
    }

    public void setTracking(bool p) {
        _Optitrack.SetActive(p);
        //_Kinect.SetActive(p);
    }
    #endregion

    #region Positions Update
    //this one uses current joint group
    private void updateLightProjectionPosition() {
        var _jointGroup = getCurrentJointGroup();
        for (var i = 0; i < count; i++) {
            var rigidbodyPosition = _jointGroup.jointsList[i].position;
            var floorPosition = PositionFloor[i];
            var posTarget = new Vector3(floorPosition.x, rigidbodyPosition.y, floorPosition.z);
            var dir = posTarget - Projector.position;
            var r = new Ray(Projector.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit)) {
                PositionProjected[i] = hit.point;
            }

            var _joint = _jointGroup.jointsList[i];
            var rot = _joint.rotation * Vector3.up;
            var rb_posWithOffset = _joint.position - rot * ProjectionOffset;
            posTarget = new Vector3(floorPosition.x, rb_posWithOffset.y, floorPosition.z);
            dir = posTarget - Projector.position;
            r = new Ray(Projector.position, dir);
            if (Physics.Raycast(r, out hit)) {
                PositionProjectedWithOffset[i] = hit.point;
            }
        }
    }

    private void updateFloorPositions() {
        Transform[] _positions = getRigidBodyTransforms();

        for (var i = 0; i < count; i++) {
            var pX = _positions[i].position.x * factorX + offset.x;
            var pZ = _positions[i].position.z * factorZ + offset.z;

            PositionFloor[i] = new Vector3(pX, 0, pZ);
        }
    }

    IEnumerator updateFloorPositionsRoutine()
    {
        while (true)
        {
            updateFloorPositions();
            yield return null;
        }
    }
    IEnumerator updateLightProjectionPositionRoutine() {
        while (true) {
            updateLightProjectionPosition();
            yield return null;
        }
    }
    #endregion

    #region JointsGroup

    /// <summary>
    ///     Returns a JointGroup representing the current state of the rigid bodies being tracked
    /// </summary>
    /// <returns></returns>
    public JointsGroup getCurrentJointGroup()
    {

        var jg = new JointsGroup(_transforms[0], _transforms[1], _transforms[2]);

        //jg.Print();

        return jg;
    }

    #endregion

    #region Properties

    protected Transform[] _transforms;
    public Vector3[] _floorTransforms;

    public Vector3[] PositionFloor, PositionProjected, PositionProjectedWithOffset;
    public Transform Projector;

    private JointsGroup _currentJointsGroup;

    public Transform getRigidBodyTransform(int index)
    {
        if (index >= count)
        {
            Debug.Log("ServiceTracking: index out of bounds");
            return null;
        }

        return _transforms[index];
    }

    public Transform[] getRigidBodyTransforms()
    {
        return _transforms;
    }

    private void InitializeProperties()
    {
        _transforms = new Transform[count];
        _floorTransforms = new Vector3[count];
        PositionFloor = new Vector3[count];
        PositionProjected = new Vector3[count];
        PositionProjectedWithOffset = new Vector3[count];
        for (var i = 0; i < count; i++)
        {
            _transforms[i] = _assignedRigidBodies[i].transform;
        }
    }

    #endregion

    #region Enable Tracking

    public event EventHandler<EventArgs> onTrackingToggleChanged;

    protected bool _tracking;

    public bool tracking
    {
        get
        {
            return this._tracking;
        }
        set
        {
            if (this._tracking == value) return;
            this._tracking = value;
            Utils.LaunchEvent(this, onTrackingToggleChanged);
        }
    }

    #endregion

    #region SINGLETON

    private static ServiceTracking _instance;
    public Vector3 offset = new Vector3(0.2f,0,-0.075f);
    public float factorX = 1f;
    public float factorZ = 1.05f;

    
    public float ProjectionOffset = 0.05f;

    public static ServiceTracking instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ServiceTracking>();
            }
            return _instance;
        }
    }

    #endregion

}