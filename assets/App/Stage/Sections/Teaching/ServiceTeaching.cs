using System;
using UnityEngine;
using System.Collections;

public class ServiceTeaching {

    #region Initial Position

    public event EventHandler<EventArgs> onInitialPositionCompleted;

    protected bool _initialPositionCompleted;

    public bool initialPositionCompleted {
        get { return this._initialPositionCompleted; }
        set {
            this._initialPositionCompleted = value;
            if (value)
                Utils.LaunchEvent(this, onInitialPositionCompleted);
        }
    }

    public event EventHandler<EventArgs> onReachedInitialPosition;

    protected bool _isOnInitialPosition;

    public bool isOnInitialPosition
    {
        get { return this._isOnInitialPosition; }
        set
        {
            if (this._isOnInitialPosition == value) return;
            
                this._isOnInitialPosition = value;
                Utils.LaunchEvent(this, onReachedInitialPosition);
            
        }
    }

    #endregion

    #region Guidance



    #endregion

    #region Failing

    public event EventHandler<EventArgs> onFailingExerciseChanged; 

    protected bool _failingExercise;

    public bool failingExercise
    {
        get { return this._failingExercise; }

        set
        {
            if (this._failingExercise == value) return;
            this._failingExercise = value;
            Utils.LaunchEvent(this, onFailingExerciseChanged);
        }
    }

    #endregion

    #region Counter

    /// <summary>
    /// Number of repetitions already made
    /// </summary>
    public int count;

    #endregion

    #region Start Over
    public event EventHandler<EventArgs> onStartOver;

    public void startOver()
    {
        Utils.LaunchEvent(this, onStartOver);
    }

    #endregion

    #region Singleton

    private static ServiceTeaching _instance;


    public static ServiceTeaching instance {
        get {
            if (_instance == null) {
                _instance = new ServiceTeaching();
            }
            return _instance;
        }
    }

    #endregion
}
