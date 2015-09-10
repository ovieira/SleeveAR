using System;
using UnityEngine;
using System.Collections.Generic;

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

    public bool isOnInitialPosition {
        get { return this._isOnInitialPosition; }
        set {
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

    public bool failingExercise {
        get { return this._failingExercise; }

        set {
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

    public event EventHandler<EventArgs> onFinishedRepetitions;

    protected int _count;

    public int count {
        get { return this._count; }
        set {
            this._count = value;
            Debug.Log("COUNT " + _count);
            if (this._count >= 3) Utils.LaunchEvent(this, onFinishedRepetitions);
        }
    }

    #endregion

    #region Start Over
    public event EventHandler<EventArgs> onStartOver;

    public void startOver() {
        Utils.LaunchEvent(this, onStartOver);
    }

    #endregion

    #region History

    public Session session = new Session();

    public Log currentLog = new Log();

    #endregion

    #region Score

    public event EventHandler<EventArgs> onChangedCurrentScore; 
    protected float _currentScore;
    public float currentScore
    {
        get { return this._currentScore; }
        set
        {
            this._currentScore = value;
            this.scores.Add(value);
            Utils.LaunchEvent(this, onChangedCurrentScore);
        }
    }


    public List<float> scores = new List<float>(3);

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



//public class Log {
//    public List<Vector3> positions { get; set; }
//}

//public class Session : List<Log> {
//}

