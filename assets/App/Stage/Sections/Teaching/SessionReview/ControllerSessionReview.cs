using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControllerSessionReview : Controller {

    #region Exercise Model

    protected ExerciseModel _exerciseModel;

    #endregion

    #region LifeCycle
    protected override void Awake() {
        base.Awake();
        if (serviceExercise.selected != null)
            _exerciseModel = serviceExercise.selected;
        this.view.onFinishedDrawing += this._onFinishedDrawing;
        //serviceTeaching.onStartOver += this._onStartOver;
    }

   
    protected override void Start() {
        base.Start();
        if (serviceExercise.selected == null) return;
        this.view.distance =/*Vector3.Distance(serviceTracking.PositionFloor[0], serviceTracking.PositionFloor[1]) *8;*/ 1.5f; //TODO: use dynamic distance

        updateFloorArc();

    }

    private void updateFloorArc() {
        //var _list = new List<Vector3>();
        //for (int i = 0; i < serviceExercise.count; i++) {
        //    _list.Add(_exerciseModel.exerciseModel[i].getUpperArmDirection());
        //}
        this.view.upperArmDirectionsList = new List<Vector3>(_exerciseModel.GetUpperArmDirectionList());
        this.view.session = serviceTeaching.session;
        this.view.logid = serviceTeaching.count - 1;
        this.view.StartCoroutine("updateViewFloorArc",.01f);
    }

    private void _onFinishedDrawing(object sender, System.EventArgs e)
    {
        var log = serviceTeaching.session.logs[serviceTeaching.count - 1];
        var score = Utils.Map(log.validCount, 0, log.validCount+(log.invalidCount*2), 0, 100);
        serviceTeaching.currentScore = score;
        Debug.Log("SCORE: " + score);
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        this.view.onFinishedDrawing -= this._onFinishedDrawing;

        //serviceTeaching.onStartOver -= this._onStartOver;
      
    }
    #endregion

    #region View

    public ViewSessionReview view;

    #endregion
}
