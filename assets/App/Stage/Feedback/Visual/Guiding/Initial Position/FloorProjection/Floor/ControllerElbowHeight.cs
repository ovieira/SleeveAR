using System;
using UnityEngine;
using System.Collections;

public class ControllerElbowHeight : Controller {

    #region LifeCycle

    protected override void Start() {
        base.Start();

        serviceExercise.onCurrentJointGroupChanged += this._onCurrentJointGroupChanged;

        if (serviceExercise.selected != null)
        {
            JointsGroup jg = serviceExercise.currentJointsGroup;
            this.view.targetHeight = jg.getUpperArmDirection().y;
        }
    }

    public void Update()
    {
        this.view.currentHeight = serviceTracking.getCurrentJointGroup().getUpperArmDirection().y;
        //this.view.current = serviceTracking.getCurrentJointGroup().angle;
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

    public ViewElbowHeight view;

    #endregion

}
