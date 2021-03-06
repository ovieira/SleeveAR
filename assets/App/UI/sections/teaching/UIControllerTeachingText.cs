﻿using System;
using UnityEngine;
using System.Collections;

public class UIControllerTeachingText : Controller {


    #region LifeCycle
    protected override void Start() {
        base.Start();
        serviceTeaching.onReachedInitialPosition += this._onReachedInitialPosition;
        serviceTeaching.onInitialPositionCompleted += this._onInitialPositionCompleted;
        serviceTeaching.onFailingExerciseChanged += this._onFailingExerciseChanged;
        serviceExercise.onFinishedExercise += this._onFinishedExercise;
        serviceTeaching.onFinishedRepetitions += this._onFinishedRepetitions;
    }



    

    protected override void OnDestroy() {
        base.OnDestroy();

        serviceTeaching.onFinishedRepetitions -= this._onFinishedRepetitions;

        serviceTeaching.onFailingExerciseChanged -= this._onFailingExerciseChanged;
        serviceExercise.onFinishedExercise -= this._onFinishedExercise;
        serviceTeaching.onInitialPositionCompleted -= this._onInitialPositionCompleted;

        serviceTeaching.onReachedInitialPosition -= this._onReachedInitialPosition;

    }

    #endregion

    #region Service Teaching

    private void _onFailingExerciseChanged(object sender, EventArgs e) {
        if (serviceTeaching.failingExercise)
        {
            this.view.text.text = "Restarting Exercise in 3 Seconds";
            this.view.show();
        }
        else
        {
            this.view.hide();
        }
    }

    protected void _onReachedInitialPosition(object sender, EventArgs e)
    {
        if (serviceTeaching.isOnInitialPosition)
        {
            this.view.text.text = "Hold this position";
            this.view.show();
        }
        else
        {
            //serviceAudio.StopAudio();
            this.view.hide();
        }
    }

    private void _onInitialPositionCompleted(object sender, EventArgs e)
    {
        this.view.text.text = "Begin!";
        this.view.hide(2f);
    }

    private void _onFinishedRepetitions(object sender, EventArgs e)
    {

    }

    #endregion

    #region Service Exercise

    private void _onFinishedExercise(object sender, EventArgs e) {
        this.view.text.text = "Exercise Finished!";

    }

    #endregion

    #region View

    public UIViewTeachingText view;

    #endregion
}
