using System;
using UnityEngine;

public class ManagerExercise : MonoBehaviour {

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
            if (value >= _loadedExerciseModel._exerciseModel.Count) Utils.LaunchEvent(this, onFinishedExercise);
            this._currentIndex = value;
            Utils.LaunchEvent(this, onCurrentIndexChanged);
        }
    }

    #endregion

    #region Singleton

    private static ManagerExercise _instance;

    public static ManagerExercise instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<ManagerExercise>();
            }
            return _instance;
        }
    }

    #endregion

}
