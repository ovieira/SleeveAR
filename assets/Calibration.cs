using UnityEngine;
using System.Collections;

public class Calibration : MonoBehaviour {

    #region Variables

    //Projection physical size
    public float _projectedArea_X = 425f; //cm
    public float _projectedArea_Z = 328.5f; //cm
   


    //
    public GameObject _calibrationSprite;
    #endregion


    #region LifeCycle

    public void Start()
    {
        _mt = ManagerTracking.instance;
        _dw = UIDebugWindow.instance;
        _dw.debuggedObject = _calibrationSprite.gameObject;

    }

    public void Update()
    {
        _calibrationSprite.transform.position = _mt.getRigidBodyTransform(0).position;
    }

    #endregion

   


    #region Manager Tracking

    protected ManagerTracking _mt;
    protected UIDebugWindow _dw;

    #endregion
}
