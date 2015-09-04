﻿using System;
using UnityEngine;

public class ControllerTeaching : Controller
{
    protected void instantiatePrefab(GameObject prefab)
    {
        var ob = Instantiate(prefab);
        ob.transform.SetParent(transform);
    }

    protected void FailingExercise()
    {
        serviceTeaching.failingExercise = true;
    }

    #region LifeCycle

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        serviceTeaching.onStartOver += _onStartOver;
        serviceTeaching.initialPositionCompleted = false;
        serviceTeaching.onInitialPositionCompleted += _onInitialPositionCompleted;
        serviceExercise.onSelectedExerciseChanged += _onSelectedExerciseChanged;
        serviceExercise.onStart += _onStart;
        serviceExercise.onCurrentIndexChanged += _onCurrentIndexChanged;
        serviceTeaching.onFailingExerciseChanged += _onFailingExerciseChanged;
        serviceExercise.onFinishedExercise += _onFinishedExercise;
        serviceTeaching.onFinishedRepetitions += _onFinishedRepetitions;
        Utils.DestroyAllChildren(transform);
        if (serviceExercise.selected.parts.Count == 0)
        {
            serviceExercise.selected.addPart(0, serviceExercise.count - 1);
        }
        Utils.AddChildren(transform, initialPositionGuidance);
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        serviceExercise.onFinishedExercise -= _onFinishedExercise;
        serviceTeaching.onFailingExerciseChanged -= _onFailingExerciseChanged;
        serviceExercise.onCurrentIndexChanged -= _onCurrentIndexChanged;
        serviceExercise.onStart -= _onStart;
        serviceExercise.onSelectedExerciseChanged -= _onSelectedExerciseChanged;
        serviceTeaching.onInitialPositionCompleted -= _onInitialPositionCompleted;
        serviceTeaching.onFinishedRepetitions -= this._onFinishedRepetitions;

    }

    #endregion

    #region Service Exercise

    private void _onSelectedExerciseChanged(object sender, EventArgs e)
    {
        ServiceTeaching.instance.initialPositionCompleted = false;
    }

    private void _onStart(object sender, EventArgs e)
    {
        //instantiatePrefab(initialPositionGuidance);
        // Utils.AddChildren(this.transform, initialPositionGuidance);
    }

    private void _onCurrentIndexChanged(object sender, EventArgs e)
    {
        serviceTeaching.failingExercise = false;
        CancelInvoke("FailingExercise");
        Invoke("FailingExercise", 10f);
    }

    #endregion

    #region Service Teaching

    private void _onStartOver(object sender, EventArgs e)
    {
        CancelInvoke("FailingExercise");
        CancelInvoke("ResetMovement");
        serviceTeaching.initialPositionCompleted = false;
        Utils.DestroyAllChildren(transform);
        Utils.AddChildren(transform, initialPositionGuidance);
    }

    private void _onFinishedExercise(object sender, EventArgs e)
    {
        CancelInvoke("ResetMovement");
        Utils.DestroyAllChildren(transform);
        serviceTeaching.count++;

        if (serviceTeaching.count >= 3)
        {
            return;
        }

        switch (serviceTeaching.count)
        {
            case 0:
                break;
            case 1:
                serviceDifficulty.selected = ServiceDifficulty.Difficulty.MEDIUM;
                break;
            case 2:
                serviceDifficulty.selected = ServiceDifficulty.Difficulty.HARD;
                break;
            default:
                break;
        }
        serviceTeaching.startOver();
    }

    private void _onInitialPositionCompleted(object sender, EventArgs e)
    {
        Debug.Log("Start guiding");
        Utils.DestroyAllChildren(transform);
        Utils.AddChildren(transform, MovementGuidance);
    }

    private void _onFailingExerciseChanged(object sender, EventArgs e)
    {
        if (serviceTeaching.failingExercise)
        {
            serviceAudio.PlayCountDown();
            Invoke("ResetMovement", 3f);
        }
        else
        {
            CancelInvoke("ResetMovement");
            serviceAudio.StopAudio();
        }
    }

    protected void ResetMovement()
    {
        serviceExercise.index = 0;
        CancelInvoke("FailingExercise");
    }

    private void _onFinishedRepetitions(object sender, EventArgs e)
    {
        Debug.Log("Finished Repetitions");
        CancelInvoke("FailingExercise");
        CancelInvoke("ResetMovement");
        Utils.DestroyAllChildren(transform);
    }

    #endregion

    #region Guiding Prefabs

    public GameObject initialPositionGuidance;
    public GameObject MovementGuidance;

    #endregion
}