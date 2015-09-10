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
        serviceTeaching.onStartOver += this._onStartOver;
        serviceTeaching.onFinishedRepetitions += this._onFinishedRepetitions;
        serviceExercise.onFinishedExercise += this._onFinishedExercise;
    }

    private void _onFinishedExercise(object sender, EventArgs e) {
        StopAllCoroutines();
    }

 

    protected override void OnDestroy() {
        base.OnDestroy();
        //StopAllCoroutines();
        serviceExercise.onFinishedExercise -= this._onFinishedExercise;

        serviceTeaching.onFinishedRepetitions -= this._onFinishedRepetitions;

        serviceTeaching.onStartOver -= this._onStartOver;

        serviceTeaching.onReachedInitialPosition -= this._onReachedInitialPosition;
        serviceTeaching.onInitialPositionCompleted -= this._onInitialPositionCompleted;

        serviceExercise.onStart -= this._onStart;

    }
    #endregion

    #region Service Exercise
    private void _onStart(object sender, EventArgs e) {
        ServiceExercise.instance.index = 0;

        StartCoroutine("InitialPositionCoroutine");
    }
    #endregion

    #region Position Comparisons

    #region Initial Position
    IEnumerator InitialPositionCoroutine() {
        while (true) {
            checkInitialPosition();
            yield return new WaitForSeconds(1/24f);
        }
    }

    protected void checkInitialPosition() {
        //get arm angle
        JointsGroup jg = serviceTracking.getCurrentJointGroup();
        JointsGroup goal = serviceExercise.currentJointsGroup;

        if (checkForeArmAngle(jg,goal, serviceDifficulty.angleThreshold) && CheckUpperArmDirection(jg, goal, serviceDifficulty.directionThreshold)) {
            serviceTeaching.isOnInitialPosition = true;
            if (initialPositionTimer <= 0)
                serviceTeaching.initialPositionCompleted = true;
            else {
                initialPositionTimer -= (1/24f);
            }
        }
        else {
            initialPositionTimer = 3f;
            serviceTeaching.isOnInitialPosition = false;

        }
    }
    #endregion

    #region MovementGuidance

    IEnumerator MovementGuidance() {
        while (true) {
            checkCurrentPosition();
            yield return new WaitForSeconds(1 / 24f);
        }
    }

    private void checkCurrentPosition() {
        //get arm angle
        JointsGroup jg = serviceTracking.getCurrentJointGroup();
        JointsGroup goal = serviceExercise.currentJointsGroup;

        if (serviceExercise.index > serviceExercise.count) return;

        if (checkForeArmAngle(jg, goal, serviceDifficulty.angleThreshold) && CheckUpperArmDirection(jg, goal, serviceDifficulty.directionThreshold)) {
            //Debug.Log("index++");
            serviceExercise.index++;
        }
    }

    #endregion

    private bool checkForeArmAngle(JointsGroup a, JointsGroup b, float t)
    {
        return Utils.IsApproximately(a.angle, b.angle, t);
    }

    private bool CheckUpperArmDirection(JointsGroup jg, JointsGroup goal , float t) {
        return Utils.isEqualByAngle(jg.getUpperArmDirection(), goal.getUpperArmDirection(), t);
    }

    #endregion

    #region Service Teaching

    private void _onInitialPositionCompleted(object sender, EventArgs e) {
        StopCoroutine("InitialPositionCoroutine");
        serviceAudio.PlayCorrect();
        StartCoroutine("MovementGuidance");
    }

    private void _onReachedInitialPosition(object sender, EventArgs e) {
        if (serviceTeaching.isOnInitialPosition) {
            serviceAudio.PlayCountDown();
        }
        else {
            serviceAudio.StopAudio();
        }
    }

    private void _onStartOver(object sender, EventArgs e) {
        StopAllCoroutines();
        //serviceExercise.start = true;
    }



    private void _onFinishedRepetitions(object sender, EventArgs e) {
        StopAllCoroutines();
    }
    #endregion

    #region Timers

    public float initialPositionTimer;

    #endregion
    
    
}
