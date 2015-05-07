using UnityEngine;
using System.Collections;

public class ManagerExercise : MonoBehaviour {

    #region ExerciseModel

    private ExerciseModel _loadedExerciseModel;

    public ExerciseModel loadedExerciseModel
    {
        get
        {
            if (_loadedExerciseModel == null)
            {
                Debug.LogWarning("No exercise loaded");
                return null;
            }
            return _loadedExerciseModel;
        }
        set
        {
            if (value != null && value != _loadedExerciseModel)
            {
                _loadedExerciseModel = value;
            }
        }
    }

    #endregion


    #region Singleton

    private static ManagerExercise _instance;

    public static ManagerExercise instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ManagerExercise>();
            }
            return _instance;
        }
    }

    #endregion

}
