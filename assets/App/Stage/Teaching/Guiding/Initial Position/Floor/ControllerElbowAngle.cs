﻿using System;
using UnityEngine;
using System.Collections;


public class ControllerElbowAngle : Controller {

    #region LifeCycle

    protected override void Start() {
        base.Start();

        serviceExercise.onCurrentJointGroupChanged += this._onCurrentJointGroupChanged;

        if (serviceExercise.selected != null) {
            this.view.target = serviceExercise.currentJointsGroup.angle;
        }
    }

    public void Update()
    {
        this.view.armDirection = (serviceTracking.PositionProjectedWithOffset[1] -
                         serviceTracking.PositionProjectedWithOffset[0]).normalized;
        this.view.current = serviceTracking.getCurrentJointGroup().angle;
    }

    protected override void OnDestroy() {
        base.OnDestroy();

        serviceExercise.onCurrentJointGroupChanged -= this._onCurrentJointGroupChanged;
    }

    #endregion

    #region Service Exercise

    protected void _onCurrentJointGroupChanged(object sender, EventArgs e) {
        this.view.target = serviceExercise.currentJointsGroup.angle;
    }

    #endregion

    #region View

    public ViewElbowAngle view;

    #endregion
}