using UnityEngine;
using System.Collections;

public class ControllerKeyboardHandler : Controller {
    

    #region LifeCycle

    void Update()
    {
        if (Input.GetKeyDown(enableTracking))
        {
            serviceTracking.tracking ^= true;
        }

        if (Input.GetKeyDown(projectFloorJoints))
        {
            Debug.Log("not implemented");
        }

        if (Input.GetKeyDown(test))
        {
            

            ExerciseModel exerciseModel = ServiceFileManager.instance.LoadExerciseModel("front2");
            if (exerciseModel != null) {
                ServiceExercise.instance.selected = exerciseModel;
                exerciseModel.print();
            }
            
        }
    }

    #endregion



    #region Keys

    public KeyCode enableTracking;
    public KeyCode projectFloorJoints;
    public KeyCode test;

    #endregion
}
