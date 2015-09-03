using UnityEngine;
using System.Collections;

public class UIControllerJointsDebugger : Controller {
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    void Update()
    {

        var upper = Vector3.Angle(serviceExercise.currentJointsGroup.getUpperArmDirection(), serviceTracking.getCurrentJointGroup().getUpperArmDirection());
        var fore = serviceExercise.currentJointsGroup.angle - serviceTracking.getCurrentJointGroup().angle;

        this.view.upper = upper;
        this.view.fore = fore;
    }

    public UIViewJointsDebugger view;

}

