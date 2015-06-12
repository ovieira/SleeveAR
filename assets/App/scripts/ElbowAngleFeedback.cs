using UnityEngine;

public class ElbowAngleFeedback : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {

        targetAngle = angle;
        currentAngle = 0;
        circleSpriteRenderer.color = Color.red;
    }

    // Update is called once per frame
    void Update() {
        currentAngle = ManagerTracking.instance.getCurrentJointGroup().angle;

        if (_projectOnBody)
            this.transform.position = ManagerTracking.instance.PositionProjectedWithOffset[1];

        Color c = Color.Lerp(Color.green, Color.red, computeLerp());

        circleSpriteRenderer.color = c;
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

    #region ProjectOnBody

    public bool _projectOnBody;

    #endregion

    protected float computeLerp() {
        float diff = Mathf.Abs(targetAngle - currentAngle);
        return map(diff, 0, targetAngle, 0, 1);
    }

    protected float map(float s, float a1, float a2, float b1, float b2) {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

    #region SpriteRenderer

    public SpriteRenderer circleSpriteRenderer, targetBarSpriteRenderer, currentBarSpriteRenderer;

    #endregion
}
