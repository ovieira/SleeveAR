using System;
using UnityEngine;
using System.Collections;

public class ServiceTeaching {

    #region Initial Position

    public EventHandler<EventArgs> onReachedInitialPosition;

    protected bool _reachedInitialPosition = false;

    public bool reachedInitialPosition {
        get { return this._reachedInitialPosition; }
        set {
            if (this._reachedInitialPosition == value) return;
            this._reachedInitialPosition = value;
            if (value)
                Utils.LaunchEvent(this, onReachedInitialPosition);
        }
    }

    #endregion

    #region Singleton

    private static ServiceTeaching _instance;


    public static ServiceTeaching instance {
        get {
            if (_instance == null) {
                _instance = new ServiceTeaching();
            }
            return _instance;
        }
    }

    #endregion
}
