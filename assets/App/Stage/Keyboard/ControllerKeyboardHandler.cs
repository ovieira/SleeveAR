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
    }

    #endregion



    #region Keys

    public KeyCode enableTracking;
    public KeyCode projectFloorJoints;

    #endregion
}
