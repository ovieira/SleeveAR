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
            ServiceFileManager.instance.Load("front2");
           // serviceSection.selected = ServiceSection.Section.TEACHING;
            
        }
    }

    #endregion



    #region Keys

    public KeyCode enableTracking;
    public KeyCode projectFloorJoints;
    public KeyCode test;

    #endregion
}
