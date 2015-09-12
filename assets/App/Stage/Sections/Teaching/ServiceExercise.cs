using System;
using UnityEngine;

public class ServiceExercise {

    #region ExerciseModel

    protected ExerciseModel _loadedExerciseModel;
    public event EventHandler<EventArgs> onSelectedExerciseChanged;

    public ExerciseModel selected {
        get {
            if (_loadedExerciseModel == null) {
                Debug.LogWarning("No exercise loaded");
                return null;
            }
            return _loadedExerciseModel;
        }
        set {
            if (value != null && value != _loadedExerciseModel)
            {
                _loadedExerciseModel = value;
                this.count = value.exerciseModel.Count;
                this.index = 0;
                currentJointsGroup = _loadedExerciseModel.exerciseModel[index];
                this.partIndex = 0;

                if (_loadedExerciseModel.parts.Count > 0)
                {
                    currentPart = _loadedExerciseModel.parts[this.partIndex];
                }
                Utils.LaunchEvent(this, onSelectedExerciseChanged);
            }
            else
            {
                if(value == null)
                _loadedExerciseModel = null;

            }
        }
    }

    protected int PartIndex;

    public int partIndex {
        get { return this.PartIndex; }
        set { this.PartIndex = value; }
    }
    #endregion

    #region Count

    protected int _count;

    public int count
    {
        get
        {
            if (this.selected == null) return 0;
            else
            {
                return _count;
            }
        }
        private set { _count = value; }
    }

    #endregion

    #region Iterator

    public event EventHandler<EventArgs> onCurrentIndexChanged, onFinishedExercise;

    protected int _index;

    public int index {
        get { return this._index; }
        set {
            if (value >= this.count) {
                Utils.LaunchEvent(this, onFinishedExercise);
                return;
            }

            if (value >= this.currentPart.y || value <= this.currentPart.x)
            {
                for (int i = 0; i < selected.parts.Count; i++)
                {
                    var part = selected.parts[i];
                    if (value >= part.x && value <= part.y)
                    {
                        this.partIndex = i;
                        this.currentPart = part;
                    }
                }
            }

            if (this._index == value) return;
            this._index = value;
            currentJointsGroup = _loadedExerciseModel.exerciseModel[index];
            Utils.LaunchEvent(this, onCurrentIndexChanged);
        }
    }

    public event EventHandler<EventArgs> onCurrentJointGroupChanged;

    protected JointsGroup _currentJointsGroup;

    public JointsGroup currentJointsGroup {
        get { return this._currentJointsGroup; }
        set {
            this._currentJointsGroup = value;
            Utils.LaunchEvent(this, onCurrentJointGroupChanged);
        }
    }

    public event EventHandler<EventArgs> onCurrentPartChanged;

    protected Vector2 _currentPart;

    public Vector2 currentPart
    {
        get
        {
            return this._currentPart;
        }
        set
        {
            this._currentPart = value;
            Utils.LaunchEvent(this, onCurrentPartChanged);
        }
    }

    #endregion

    #region Start Exercise

    public event EventHandler<EventArgs> onStart;

    private bool _start;

    public bool start {
        get { return _start; }
        set {
            _start = value;
            if (_start)
                Utils.LaunchEvent(this, onStart);
        }
    }
    #endregion

    #region Singleton

    private static ServiceExercise _instance;


    public static ServiceExercise instance {
        get {
            if (_instance == null) {
                _instance = new ServiceExercise();
            }
            return _instance;
        }
    }

    #endregion
}
