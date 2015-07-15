using System;
using UnityEngine;

public class ManagerExercise {

    #region ExerciseModel

    private ExerciseModel _loadedExerciseModel;

    public ExerciseModel loadedExerciseModel {
        get {
            if (_loadedExerciseModel == null) {
                Debug.LogWarning("No exercise loaded");
                return null;
            }
            return _loadedExerciseModel;
        }
        set {
            if (value != null && value != _loadedExerciseModel) {
                _loadedExerciseModel = value;
            }
        }
    }

    #endregion

    #region Iterator

    public event EventHandler<EventArgs> onCurrentIndexChanged, onFinishedExercise; 

    protected int _currentIndex;

    public int currentIndex
    {
        get { return this._currentIndex; }
        set
        {
            if (this._currentIndex == value) return;
            if (value >= _loadedExerciseModel.exerciseModel.Count) {
                Utils.LaunchEvent(this, onFinishedExercise);
                return;
            }
            this._currentIndex = value;
            currentJointsGroup = _loadedExerciseModel.exerciseModel[currentIndex];
            Utils.LaunchEvent(this, onCurrentIndexChanged);
        }
    }

    public event EventHandler<EventArgs> onCurrentJointGroupChanged; 
    
    protected JointsGroup _currentJointsGroup;

    public JointsGroup currentJointsGroup
    {
        get { return this._currentJointsGroup; }
        set
        {
            this._currentJointsGroup = value;
            Utils.LaunchEvent(this, onCurrentJointGroupChanged);
        }
    }
    #endregion

    #region Singleton

    private static ManagerExercise _instance;

    public static ManagerExercise instance {
        get {
            if (_instance == null) {
                _instance = new ManagerExercise();
            }
            return _instance;
        }
    }

    #endregion

}
