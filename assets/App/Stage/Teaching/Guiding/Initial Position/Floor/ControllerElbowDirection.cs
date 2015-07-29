using System;
using UnityEngine;
using System.Collections;

public class ControllerElbowDirection : Controller {

    #region LifeCycle

    protected override void Start() {
        base.Start();

        serviceExercise.onCurrentJointGroupChanged += this._onCurrentJointGroupChanged;

        if (serviceExercise.selected != null) {
            JointsGroup jg = serviceExercise.currentJointsGroup;
            this.view.targetDirection = jg.getUpperArmDirection();
        }
    }

    public void Update()
    {
        this.view.currentDirection = serviceTracking.getCurrentJointGroup().getUpperArmDirection();
    }

    protected override void OnDestroy() {
        base.OnDestroy();

        serviceExercise.onCurrentJointGroupChanged -= this._onCurrentJointGroupChanged;
    }

    #endregion

    #region Service Exercise

    protected void _onCurrentJointGroupChanged(object sender, EventArgs e) {
        //this.view.target = serviceExercise.currentJointsGroup.angle;
    }

    #endregion

    #region View

    public ViewElbowDirection view;

    #endregion

}
