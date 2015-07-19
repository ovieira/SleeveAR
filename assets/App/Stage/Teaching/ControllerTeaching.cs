using UnityEngine;
using System.Collections;

public class ControllerTeaching : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {
        ServiceTeaching.instance.reachedInitialPosition = false;

        ServiceTeaching.instance.onReachedInitialPosition += this._onReachedInitialPosition; 

        ServiceExercise.instance.onSelectedExerciseChanged += this._onSelectedExerciseChanged;
    }


    // Update is called once per frame

    void Update() {

    }

    #endregion

    #region Service Exercise

    private void _onSelectedExerciseChanged(object sender, System.EventArgs e)
    {
        ServiceTeaching.instance.reachedInitialPosition = false;
    }

    #endregion

    #region Service Teaching

    private void _onReachedInitialPosition(object sender, System.EventArgs e) {
        Debug.Log("Start guiding");
    }

    #endregion
}
