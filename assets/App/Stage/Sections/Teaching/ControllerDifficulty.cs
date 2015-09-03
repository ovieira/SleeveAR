
using UnityEngine;

public class ControllerDifficulty : Controller {
    #region LifeCycle

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    #endregion

    #region Thresholds
    [Header("Easy")]
    public float EasyAngle;
    public float EasyHeight;
    public float EasyDirection;

    [Header("Medium")]
    public float MediumAngle;
    public float MediumHeight;
    public float MediumDirection;

    [Header("Hard")]
    public float HardAngle;
    public float HardHeight;
    public float HardDirection; 
    #endregion
}
