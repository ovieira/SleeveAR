using System;
using UnityEngine;
using System.Collections;

public class Utils {

    public static void LaunchEvent(object _sender, EventHandler<EventArgs> _event) {
        if (_event != null) _event(_sender, new EventArgs());
    }

}
