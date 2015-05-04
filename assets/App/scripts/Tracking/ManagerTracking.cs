using UnityEngine;
using UnityEngine.UI;

public class ManagerTracking : MonoBehaviour
{
    public enum TrackingDevice
    {
        OPTITRACK,
        KINECT
    }

    [SerializeField] private GameObject[] _assignedRigidBodies;
    [SerializeField] protected GameObject _Optitrack, _Kinect;
    [SerializeField] protected TrackingDevice _TrackingDevice;
    [SerializeField] protected Text _FpsText;
    public int count { get; set; }
    private int fpsCount;
    private float timerCount = 0;
    private float timeaux;
    public Text _jointAngle;


    public void Awake()
    {
        count = _assignedRigidBodies.Length;
        InitializeTrackingDevice();
        InitializeProperties();
    }

    private void InitializeTrackingDevice()
    {
        if (_Optitrack != null)
            _Optitrack.SetActive(_TrackingDevice == TrackingDevice.OPTITRACK);
        if (_Kinect != null)
            _Kinect.SetActive(_TrackingDevice == TrackingDevice.KINECT);
    }

    private void Update()
    {
        //updateJointGroup();
        timeaux = Time.time;
        updateFloorPositions();
        updateLightProjectionPosition();
        timeaux = Time.time - timeaux;
        updateFPSCount();
        _jointAngle.text = ""+getCurrentJointGroup().angle;
    }

    /// <summary>
    /// Time spent calculating tracking information on each frame, Average Time Calculation Tracking Info (ATCTI)
    /// </summary>
    private void updateFPSCount()
    {
        timerCount += timeaux;

        float _fps = timerCount / (float)Time.frameCount;
        Debug.Log(_fps.ToString());
        _FpsText.text = "ATCTI: " + _fps.ToString();
    }

    private void updateLightProjectionPosition()
    {
        for (var i = 0; i < count; i++)
        {
            var rb_pos = getRigidBodyTransform(i).position;
            var floor_pos = PositionFloor[i];
            var posTarget = new Vector3(floor_pos.x, rb_pos.y, floor_pos.z);
            var dir = posTarget - Projector.position;
            var r = new Ray(Projector.position, dir);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit))
            {
                PositionProjected[i] = hit.point;
            }
        }
    }

    private void updateFloorPositions()
    {
        for (var i = 0; i < count; i++)
        {
            var pX = getRigidBodyTransform(i).position.x*offX + X;
            var pZ = getRigidBodyTransform(i).position.z*offY + Y;

            PositionFloor[i] = new Vector3(pX, 0, pZ);
        }
    }

    #region JointsGroup

    /// <summary>
    ///     Returns a JointGroup representing the current state of the rigid bodies being tracked
    /// </summary>
    /// <returns></returns>
    public JointsGroup getCurrentJointGroup()
    {

        var jg = new JointsGroup(_transforms[0], _transforms[1], _transforms[2]);

        jg.Print();

        return jg;
    }

    #endregion

    public void setTracking(bool p)
    {
        _Optitrack.SetActive(p);
        //_Kinect.SetActive(p);
    }

    #region Properties

    protected Transform[] _transforms;
    public Vector3[] _floorTransforms;

    public Vector3[] PositionFloor, PositionProjected;
    public Transform Projector;

    private JointsGroup _currentJointsGroup;

    public Transform getRigidBodyTransform(int index)
    {
        if (index >= count)
        {
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
        for (var i = 0; i < count; i++)
        {
            _transforms[i] = _assignedRigidBodies[i].transform;
        }
    }

    #endregion

    #region SINGLETON

    private static ManagerTracking _instance;
    private readonly float offX = 0.64f;
    private readonly float offY = 0.635f;
    private readonly float X = 0.16f;
    private readonly float Y = -0.04f;

    public static ManagerTracking instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ManagerTracking>();
            }
            return _instance;
        }
    }

    #endregion
}