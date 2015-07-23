using UnityEngine;
using System.Collections;
using System;

public class ControllerExercise : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Start() {
        base.Start();
        serviceExercise.onStart += this._onStart;
        serviceTeaching.onReachedInitialPosition += this._onReachedInitialPosition;
    }

    // Update is called once per frame
    private void Update() {

    }

    public void OnDestroy() {
        //StopAllCoroutines();

        serviceTeaching.onReachedInitialPosition -= this._onReachedInitialPosition;

        serviceExercise.onStart -= this._onStart;

    }
    #endregion

    private void _onStart(object sender, EventArgs e) {
        ServiceExercise.instance.index = 0;

        StartCoroutine("initialPosition");
    }

    IEnumerator initialPosition() {
        while (true) {
            checkInitialPosition();
            yield return null;
        }
    }

    protected void checkInitialPosition() {
        //get arm angle
        JointsGroup jg = serviceTracking.getCurrentJointGroup();
        JointsGroup goal = serviceExercise.currentJointsGroup;

        Debug.Log(jg.angle + " : " + goal.angle);

        if (Utils.IsApproximately(jg.angle, goal.angle, 1f)) {
            Debug.Log("TA PARECIDO");
            serviceTeaching.reachedInitialPosition = true;
        }
        //get single joints angle

        //if close enough to initial position, launch reachedInitialPosition Event and stop coroutine
    }

    #region Service Teaching

    private void _onReachedInitialPosition(object sender, EventArgs e) {
        StopCoroutine("initialPosition");
        serviceAudio.PlayCorrect();
    }



    #endregion
}
