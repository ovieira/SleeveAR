﻿using UnityEngine;
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



    protected override void OnDestroy() {
        base.OnDestroy();
        //StopAllCoroutines();
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
            yield return null;
        }
    }

    protected void checkInitialPosition() {
        //get arm angle
        JointsGroup jg = serviceTracking.getCurrentJointGroup();
        JointsGroup goal = serviceExercise.currentJointsGroup;

        if (checkJointAngle(jg, goal) && checkHeight(jg, goal) && checkDirection(jg, goal)) {
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
    #endregion

    #region MovementGuidance

    IEnumerator MovementGuidance() {
        while (true) {
            checkCurrentPosition();
            yield return null;
        }
    }

    private void checkCurrentPosition() {
        //get arm angle
        JointsGroup jg = serviceTracking.getCurrentJointGroup();
        JointsGroup goal = serviceExercise.currentJointsGroup;

        //jg.Print();
        //goal.Print();
        if (/*checkJointAngle(jg, goal) && checkHeight(jg, goal) &&*/ checkDirection(jg, goal, 2f)) {
            //Debug.Log("TA PARECIDO");
            //serviceTeaching.isOnInitialPosition = true;
            //if (initialPositionTimer <= 0)
            //    serviceTeaching.initialPositionCompleted = true;
            //else {
            //    initialPositionTimer -= Time.deltaTime;
            //}
            Debug.Log("index++");
            serviceExercise.index++;
        }
        else {
            //initialPositionTimer = 3f;
            //serviceTeaching.isOnInitialPosition = false;

        }
        //Debug.Log(initialPositionTimer);
        //get single joints angle

        //if close enough to initial position, launch initialPositionCompleted Event and stop coroutine

    }

    #endregion
    private bool checkDirection(JointsGroup jg, JointsGroup goal , float t) {
        //return Utils.IsApproximately(jg.getUpperArmDirection().x - goal.getUpperArmDirection().x, 0, directionComparisonThreshold);
        //bool b = Utils.IsApproximately(jg.getUpperArmDirection(), goal.getUpperArmDirection(), directionComparisonThreshold);
        bool b = //Utils.isEqual(jg.getUpperArmDirection(), goal.getUpperArmDirection(), t);
            Utils.isEqualByAngle(jg.getUpperArmDirection(), goal.getUpperArmDirection(), t);
      //  Debug.Log("Direction: " + b);
        return b;
    }

    private bool checkDirection(JointsGroup jg, JointsGroup goal ) {
        //return Utils.IsApproximately(jg.getUpperArmDirection().x - goal.getUpperArmDirection().x, 0, directionComparisonThreshold);
        //bool b = Utils.IsApproximately(jg.getUpperArmDirection(), goal.getUpperArmDirection(), directionComparisonThreshold);
        bool b = Utils.isEqual(jg.getUpperArmDirection(), goal.getUpperArmDirection(), directionComparisonThreshold);
     //   Debug.Log("Direction: " + b);
        return b;
    }

    private bool checkHeight(JointsGroup jg, JointsGroup goal) {
        bool b = Utils.IsApproximately(jg.getHeight(), goal.getHeight(), heightComparisonThreshold);
       // Debug.Log("Height: " + b);
        return b;
    }

    private bool checkJointAngle(JointsGroup jg, JointsGroup goal) {
        bool b = Utils.IsApproximately(jg.angle, goal.angle, angleComparisonThreshold);
      //  Debug.Log("Angle: " + b);
        return b;
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

    #endregion

    #region Timers

    public float initialPositionTimer;

    #endregion

    #region Thresholds

    [Header("Thresholds")]
    public float angleComparisonThreshold;

    public float heightComparisonThreshold;

    public float directionComparisonThreshold;

    #endregion
}
