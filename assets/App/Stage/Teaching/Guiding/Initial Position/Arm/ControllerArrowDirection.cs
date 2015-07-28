using UnityEngine;
using System.Collections;

public class ControllerArrowDirection : Controller {

    #region LifeCycle
    protected override void Start() {
        base.Start();

        if (serviceExercise.selected != null)
        {
            JointsGroup jg = serviceExercise.currentJointsGroup;
            this.view.target = jg.getUpperArmDirection();
        }

    }

    public void Update()
    {
        this.view.current = serviceTracking.getCurrentJointGroup().getUpperArmDirection();

        this.view.currentArmPosition = serviceTracking.PositionProjectedWithOffset;
    }


    protected override void OnDestroy() {
        base.OnDestroy();
    } 
    #endregion


    #region View

    public ViewArrowDirection view;

    #endregion
}
