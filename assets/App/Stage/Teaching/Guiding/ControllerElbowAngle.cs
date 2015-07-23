using System;
using UnityEngine;
using System.Collections;

public class ControllerElbowAngle : Controller {

    #region LifeCycle

    protected override void Start() {
        base.Start();

        serviceExercise.onCurrentJointGroupChanged += this._onCurrentJointGroupChanged;

        if (serviceExercise.selected != null) {
            this.view.targetAngle = serviceExercise.currentJointsGroup.angle;
        }
    }

    public void Update()
    {
        this.view.currentAngle = serviceTracking.getCurrentJointGroup().angle;
    }

    protected override void OnDestroy() {
        base.OnDestroy();

        serviceExercise.onCurrentJointGroupChanged -= this._onCurrentJointGroupChanged;
    }

    #endregion

    #region Service Exercise

    protected void _onCurrentJointGroupChanged(object sender, EventArgs e) {
        this.view.targetAngle = serviceExercise.currentJointsGroup.angle;
    }

    #endregion



    #region View

    public ViewElbowAngle view;

    #endregion
}
