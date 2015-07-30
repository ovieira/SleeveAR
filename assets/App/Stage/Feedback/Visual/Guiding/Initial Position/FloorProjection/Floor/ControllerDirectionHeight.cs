using System;
using UnityEngine;
using System.Collections;

public class ControllerDirectionHeight : Controller {
    protected override void Start()
    {
        base.Start();

        serviceExercise.onCurrentJointGroupChanged += this._onCurrentJointGroupChanged;

        if (serviceExercise.selected != null) {
            JointsGroup jg = serviceExercise.currentJointsGroup;
            this.view.target = jg.getUpperArmDirection();
        }
    }

    public void Update()
    {
        this.view.current = serviceTracking.getCurrentJointGroup().getUpperArmDirection();

    }

    

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }


    #region Service Exercise

    protected void _onCurrentJointGroupChanged(object sender, EventArgs e) {
        //this.view.target = serviceExercise.currentJointsGroup.angle;
    }

    #endregion

    #region View

    public ViewDirectionHeight view;

    #endregion
}
