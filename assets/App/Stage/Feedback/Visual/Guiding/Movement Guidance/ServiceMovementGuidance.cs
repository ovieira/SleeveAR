using System;
using UnityEngine;
using System.Collections;

public class ServiceMovementGuidance {

    #region Floor Arc

    public event EventHandler<EventArgs> onFloorArcChanged;
    protected bool _floorArc = true;

    public bool floorArc
    {
        get { return this._floorArc; }
        set
        {
            this._floorArc = value;
            Utils.LaunchEvent(this, onFloorArcChanged);
        }
    }

    #endregion

    #region Singleton

    private static ServiceMovementGuidance _instance;

    public static ServiceMovementGuidance instance {
        get {
            if (_instance == null) {
                _instance = new ServiceMovementGuidance();
            }
            return _instance;
        }
    }

    #endregion
}
