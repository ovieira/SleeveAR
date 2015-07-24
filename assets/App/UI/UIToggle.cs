using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Toggle))]

public class UIToggle : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {
        this.toggle.onValueChanged.AddListener(this._valueChanged);
        value = this.toggle.isOn;
    }
    #endregion

    #region On Value Changed

    public event EventHandler<EventArgs> onValueChanged;

    protected void _valueChanged(bool v)
    {
        value = v;
        Utils.LaunchEvent(this, onValueChanged);
    }
    #endregion

    #region Value

    public bool value;

    #endregion

    #region Toggle
    protected Toggle toggle {
        get { return this.GetComponent<Toggle>(); }
    }
    #endregion
}
