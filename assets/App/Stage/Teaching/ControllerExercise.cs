using UnityEngine;
using System.Collections;
using System;

public class ControllerExercise : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Start() {
        base.Start();
        serviceExercise.onStart += this._onStart;
        serviceTeaching.onInitialPositionCompleted += this._onInitialPositionCompleted;
        serviceTeaching.onReachedInitialPosition += this._onReachedInitialPosition;
    }

    // Update is called once per frame
    private void Update() {

    }

    public void OnDestroy() {
        //StopAllCoroutines();

        serviceTeaching.onInitialPositionCompleted -= this._onInitialPositionCompleted;

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

        if (Utils.IsApproximately(jg.angle, goal.angle, angleComparisonThreshold)) {
            //Debug.Log("TA PARECIDO");
            serviceTeaching.isOnInitialPosition = true;
            if (initialPositionTimer <= 0)
                serviceTeaching.initialPositionCompleted = true;
            else {
                initialPositionTimer -= Time.deltaTime;
            }
        }
        else {
            initialPositionTimer = 3f;
            serviceTeaching.isOnInitialPosition = false;

        }
        //Debug.Log(initialPositionTimer);
        //get single joints angle

        //if close enough to initial position, launch initialPositionCompleted Event and stop coroutine
    }

    #region Service Teaching

    private void _onInitialPositionCompleted(object sender, EventArgs e) {
        StopCoroutine("initialPosition");
        serviceAudio.PlayCorrect();
    }

    private void _onReachedInitialPosition(object sender, EventArgs e) {
        if (serviceTeaching.isOnInitialPosition)
        {
            serviceAudio.PlayCountDown();
        }
        else
        {
            serviceAudio.StopAudio();
        }
    }

    #endregion

    #region Timers

    public float initialPositionTimer;

    #endregion

    #region Thresholds

    [Header("Thresholds")]
    public float angleComparisonThreshold;

    #endregion
}
