using UnityEngine;
using System.Collections;

public class Calibration : MonoBehaviour {

    #region Variables

    //Projection physical size
    public float _projectedArea_X = 425f; //cm
    public float _projectedArea_Z = 328.5f; //cm
    public Vector3 offset;

    [Range(0.5f, 1.5f)]
    public float factorX, factorY;

    //
    public GameObject[] _calibrationSprite;
    #endregion


    #region LifeCycle

    public void Start()
    {
        _mt = ManagerTracking.instance;
        _dw = UIDebugWindow.instance;
        _dw.debuggedObject = _calibrationSprite[0].gameObject;

    }

    public void Update()
    {
        //Vector3 position = _mt.getRigidBodyTransform(0).position;
        //Vector3 positionTimesFactor = new Vector3(position.x * factorX, position.y, position.z * factorY);
        //_calibrationSprite.transform.position = positionTimesFactor + offset;

        for (int i = 0; i < _calibrationSprite.Length; i++) {
            Vector3 position = _mt.getRigidBodyTransform(i).position;
            Vector3 positionTimesFactor = new Vector3(position.x * factorX, position.y, position.z * factorY);
            _calibrationSprite[i].transform.position = positionTimesFactor + offset;
        }
    }

    #endregion

   


    #region Manager Tracking

    protected ManagerTracking _mt;
    protected UIDebugWindow _dw;

    #endregion
}
