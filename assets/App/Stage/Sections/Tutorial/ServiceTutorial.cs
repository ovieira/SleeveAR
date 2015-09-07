using System;
using UnityEngine;
using System.Collections;

public class ServiceTutorial {

    #region Tutorial Type

    public enum TutorialType
    {
        FOREARM,
        UPPERARM,
        BOTH
    }

    public event EventHandler<EventArgs> onSelectedChanged;

    protected TutorialType _selected;

    public TutorialType selected
    {
        get { return this._selected; }
        set
        {
            if (this._selected == value) return;
            this._selected = value;
            Utils.LaunchEvent(this, onSelectedChanged);
        }
    }

    #endregion

    #region Tutorial Counter

    public event EventHandler<EventArgs> onCountChanged; 

    protected int _count;

    public int count
    {
        get { return this._count; }
        set
        {
            this._count = value;
            Utils.LaunchEvent(this, onCountChanged);
        }
    }


    #endregion

    #region Goal JG

    public JointsGroup goalJointsGroup;

    #endregion
    #region Singleton

    private static ServiceTutorial _instance;


    public static ServiceTutorial instance {
        get {
            if (_instance == null) {
                _instance = new ServiceTutorial();
            }
            return _instance;
        }
    }

    #endregion
}
