using UnityEngine;
using System.Collections;

public class ControllerColorArm : Controller {

    #region LifeCycle
    protected override void Awake() {
        base.Awake();
        serviceExercise.onCurrentJointGroupChanged += this._onCurrentJointGroupChanged;

    }

    protected override void OnDestroy() {
        base.OnDestroy();

        serviceExercise.onCurrentJointGroupChanged -= this._onCurrentJointGroupChanged;

    }

    protected override void Start() {
        base.Start();
    }

    protected void Update()
    {
        this.view.current = serviceTracking.getCurrentJointGroup();
        this.view.currentArmPosition = serviceTracking.PositionProjectedWithOffset;
    }
    #endregion


    #region Service Exercise
    private void _onCurrentJointGroupChanged(object sender, System.EventArgs e)
    {
        this.view.target = serviceExercise.currentJointsGroup;
    }
    #endregion

    #region View

    public ViewColorArm view;

    #endregion
}
