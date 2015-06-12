using UnityEngine;

public class ElbowAngleFeedback : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {

        targetAngle = angle;
        currentAngle = 0;
    }

    // Update is called once per frame
    void Update() {
        currentAngle = ManagerTracking.instance.getCurrentJointGroup().angle;

        if (_projectOnBody)
            this.transform.position = ManagerTracking.instance.PositionProjectedWithOffset[1];
    }
    #endregion

    #region Bars

    public Transform currentBar, targetBar;

    #endregion

    #region Angles

    public float angle;

    protected float _currentAngle, _targetAngle;

    public float currentAngle {
        get { return _currentAngle; }
        set {
            _currentAngle = value;
            currentBar.localRotation = Quaternion.Euler(0, 0, _currentAngle);
        }
    }

    public float targetAngle {
        get { return _targetAngle; }
        set {
            _targetAngle = value;
            targetBar.localRotation = Quaternion.Euler(0, 0, _targetAngle);
        }
    }

    #endregion

    [ContextMenu("lol")]
    public void lol() {
        currentAngle = 0;
        targetAngle = 90;
    }

    #region Project on body

    public bool _projectOnBody;

    #endregion
}
