using UnityEngine;
using System.Collections;

public class ControllerArrowForeArmDirection : Controller {
    protected override void Start()
    {
        base.Start();

        if (serviceExercise.selected != null) {
            JointsGroup jg = serviceExercise.currentJointsGroup;
            this.view.target = jg.getForeArmDirection();
        }
    }

    public void Update()
    {
        this.view.current = serviceTracking.getCurrentJointGroup().getForeArmDirection();

        this.view.currentArmPosition = serviceTracking.PositionProjectedWithOffset;

    }

    #region View

    public ViewArrowForeArmDirection view;

    #endregion
}
