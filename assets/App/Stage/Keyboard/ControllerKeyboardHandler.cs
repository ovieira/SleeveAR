using UnityEngine;
using System.Collections;

public class ControllerKeyboardHandler : Controller {


    #region LifeCycle

    void Update() {
        if (Input.GetKeyDown(enableTracking)) {
            serviceTracking.tracking ^= true;
        }

        if (Input.GetKeyDown(startRec)) {
            ServiceMedia.instance.Record();
        }
        if (Input.GetKeyDown(stopRec)) {
            ServiceMedia.instance.Stop();
        }

    }

    #endregion



    #region Keys

    public KeyCode enableTracking;
    public KeyCode startRec;
    public KeyCode stopRec;

    #endregion
}
