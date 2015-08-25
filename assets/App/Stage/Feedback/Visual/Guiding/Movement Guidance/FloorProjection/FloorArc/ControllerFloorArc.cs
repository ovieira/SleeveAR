using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControllerFloorArc : Controller {

    #region Exercise Model

    protected ExerciseModel _exerciseModel;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        if (serviceExercise.selected != null)
            _exerciseModel = serviceExercise.selected;
    }

    protected override void Start()
    {
        base.Start();
        var _list = new List<Vector3>();
        for (int i = boundaryLeft; i < boundaryRight; i++)
        {
            _list.Add(_exerciseModel.exerciseModel[i].getUpperArmDirection());
        }
        this.view.upperArmDirectionsList = new List<Vector3>(_list);
    }

    void Update()
    {
        this.view.basePosition = serviceTracking.PositionFloor[0];
        this.view.progress = this.progress;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
    }

    #region Boundaries

    public int boundaryLeft, boundaryRight;

    #endregion

    #region View

    public ViewFloorArc view;

    #endregion

    [Range(0,100)]
    public int progress;
}
