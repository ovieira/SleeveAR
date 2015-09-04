
using UnityEngine;

public class ControllerDifficulty : Controller {
    #region LifeCycle

    protected override void Awake()
    {
        base.Awake();

        serviceDifficulty.onDifficultyChanged += this._onDifficultyChanged;

    }

    

    protected override void Start()
    {
        base.Start();
        serviceDifficulty.selected = ServiceDifficulty.Difficulty.EASY;
        setTresholds(EasyAngle, EasyDirection);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        serviceDifficulty.onDifficultyChanged -= this._onDifficultyChanged;

    }

    #endregion

    #region Service Difficulty

    private void _onDifficultyChanged(object sender, System.EventArgs e) {

        switch (serviceDifficulty.selected) {
            case ServiceDifficulty.Difficulty.EASY:
                Debug.Log("Easy Thresholds");
                setTresholds(EasyAngle, EasyDirection);
                break;
            case ServiceDifficulty.Difficulty.MEDIUM:
                Debug.Log("Medium Thresholds");
                setTresholds(MediumAngle,MediumDirection);
                break;
            case ServiceDifficulty.Difficulty.HARD:
                Debug.Log("Hard Thresholds");
                setTresholds(HardAngle,HardDirection);
                break;
            default:
                break;
        }

    }

    private void setTresholds(float angle, float direction)
    {
        serviceDifficulty.angleThreshold = angle;
        serviceDifficulty.directionThreshold = direction;
    }

    #endregion


    #region Thresholds
    [Header("Easy")]
    public float EasyAngle;
    public float EasyDirection;

    [Header("Medium")]
    public float MediumAngle;
    public float MediumDirection;

    [Header("Hard")]
    public float HardAngle;
    public float HardDirection; 
    #endregion
}
