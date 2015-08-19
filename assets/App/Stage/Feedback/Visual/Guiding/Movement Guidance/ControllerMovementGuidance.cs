using UnityEngine;
using System.Collections;

public class ControllerMovementGuidance : Controller {

    #region LifeCycle
    protected override void Awake() {
        base.Awake();
        serviceExercise.onCurrentIndexChanged += this._onCurrentIndexChanged;

        if (serviceExercise.index != 0) serviceExercise.index = 0;
    }

    protected override void Start() {
        base.Start();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        serviceExercise.onCurrentIndexChanged -= this._onCurrentIndexChanged;
    } 
    #endregion

    #region Service Exercise
    protected void _onCurrentIndexChanged(object sender, System.EventArgs e) {
    
    } 
    #endregion
}
