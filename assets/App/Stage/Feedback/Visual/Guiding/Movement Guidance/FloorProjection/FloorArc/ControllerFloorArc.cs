using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControllerFloorArc : Controller {

    #region Exercise Model

    protected ExerciseModel _exerciseModel;

    #endregion

    protected override void Awake() {
        base.Awake();
        if (serviceExercise.selected != null)
            _exerciseModel = serviceExercise.selected;
        serviceExercise.onCurrentIndexChanged += this._onIndexChanged;
    }



    protected override void Start() {
        base.Start();
        var _list = new List<Vector3>();
        for (int i = boundaryLeft; i < boundaryRight; i++) {
            _list.Add(_exerciseModel.exerciseModel[i].getUpperArmDirection());
        }
        this.view.upperArmDirectionsList = new List<Vector3>(_list);
        this.view.distance = Vector3.Distance(serviceTracking.PositionFloor[0], serviceTracking.PositionFloor[1]);
    }

    void Update() {
        this.view.basePosition = serviceTracking.PositionFloor[0];
        //this.view.progress = this.progress;
    }

    protected override void OnDestroy() {
        base.OnDestroy();

    }

    #region Boundaries

    public int boundaryLeft, boundaryRight;

    #endregion

    #region View

    public ViewFloorArc view;

    #endregion

    [Range(0, 100)]
    public int progress;

    private void _onIndexChanged(object sender, System.EventArgs e) {
        //this.view.progress = (int)Utils.Map(serviceExercise.index, boundaryLeft, boundaryRight, 0, 100);
        int a =  (int)Utils.Map(serviceExercise.index, boundaryLeft, boundaryRight, 0, boundaryRight - boundaryLeft);
        Debug.Log("A" + a);
        this.view.progress = a;
        //this.view.progress = progress;
    }
}
