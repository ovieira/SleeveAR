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
        serviceExercise.onCurrentPartChanged += this._onCurrentPartChanged;
    }

    public float distanceFactor;
    protected override void Start() {
        base.Start();
        if (serviceExercise.selected == null) return;
        this.view.distance = Vector3.Distance(serviceTracking.PositionFloor[0], serviceTracking.PositionFloor[1]) *
                             8;

        updateFloorArc();
    }

    private void updateFloorArc()
    {
        boundaryLeft = (int)serviceExercise.currentPart.x;
        boundaryRight = (int)serviceExercise.currentPart.y;
        var _list = new List<Vector3>();
        var currentPart = serviceExercise.currentPart;
        for (int i = (int)boundaryLeft; i <= boundaryRight; i++)
        {
            _list.Add(_exerciseModel.exerciseModel[i].getUpperArmDirection());
        }
        this.view.upperArmDirectionsList = new List<Vector3>(_list);
        this.view.updateViewFloorArc();
    }

    void Update() {
        this.view.basePosition = serviceTracking.PositionFloor[0];
        this.view.currentJointsGroup = serviceTracking.getCurrentJointGroup();
        //this.view.distance = Vector3.Distance(serviceTracking.PositionFloor[0], serviceTracking.PositionFloor[1]) *
        //                     distanceFactor;
        //this.view.progress = this.progress;
    }

    protected override void OnDestroy() {
        base.OnDestroy();

        serviceExercise.onCurrentIndexChanged -= this._onIndexChanged;
        serviceExercise.onCurrentPartChanged -= this._onCurrentPartChanged;
    }

    #region Boundaries

    public int boundaryLeft, boundaryRight;

    #endregion

    #region View

    public ViewFloorArc view;

    #endregion

    #region Service Exercise
    private void _onIndexChanged(object sender, System.EventArgs e) {
        //this.view.progress = (int)Utils.Map(serviceExercise.index, boundaryLeft, boundaryRight, 0, 100);
        int a = (int)Utils.Map(serviceExercise.index, boundaryLeft, boundaryRight, 0, boundaryRight - boundaryLeft);
        Debug.Log("A" + a);
        this.view.progress = a;
        //this.view.progress = progress;
    }

    private void _onCurrentPartChanged(object sender, System.EventArgs e)
    {
        
        updateFloorArc();
    } 
    #endregion
}
