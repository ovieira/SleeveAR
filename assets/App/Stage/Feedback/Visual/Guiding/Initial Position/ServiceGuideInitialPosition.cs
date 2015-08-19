using System;
using UnityEngine;
using System.Collections;

public class ServiceGuideInitialPosition {

    #region Mode

    public enum Mode {
        Unidirectional,
        Bidirectional,
        ArrowsDirection
    }
    public event EventHandler<EventArgs> onModeChanged; 

    protected Mode _selected;

    public Mode selected {
        get { return this._selected; }
        set {
            if (this._selected != value) {
                this._selected = value;
                Utils.LaunchEvent(this, onModeChanged);
            }
        }
    }
    #endregion

    #region Singleton

    private static ServiceGuideInitialPosition _instance;

    public static ServiceGuideInitialPosition instance {
        get {
            if (_instance == null) {
                _instance = new ServiceGuideInitialPosition();
            }
            return _instance;
        }
    }

    #endregion
}
