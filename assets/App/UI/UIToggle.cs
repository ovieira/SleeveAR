using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof (Toggle))]
public class UIToggle : MonoBehaviour
{
    #region Toggle

    protected Toggle toggle
    {
        get { return GetComponent<Toggle>(); }
    }

    #endregion

    #region LifeCycle

    // Use this for initialization
    private void Start()
    {
        toggle.onValueChanged.AddListener(_valueChanged);
        value = toggle.isOn;
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

    private bool _value;

    public bool value
    {
        get { return _value; }
        set
        {
            _value = value;
            toggle.isOn = value;
        }
    }

    #endregion
}