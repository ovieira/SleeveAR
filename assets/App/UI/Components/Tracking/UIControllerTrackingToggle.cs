using System;
using UnityEngine;
using System.Collections;

public class UIControllerTrackingToggle : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Start() {
        base.Start();
        this.toggle.onValueChanged += this._onValueChanged;
        serviceTracking.onTrackingToggleChanged += this._onTrackingChanged;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        this.toggle.onValueChanged -= this._onValueChanged;
        serviceTracking.onTrackingToggleChanged -= this._onTrackingChanged;

    }

    #endregion

    #region Service Tracking
    private void _onTrackingChanged(object sender, EventArgs e)
    {
        toggle.value = serviceTracking.tracking;
    } 
    #endregion

    #region Toggle
    protected void _onValueChanged(object sender, EventArgs e) {
        serviceTracking.tracking = toggle.value;
    }

    protected UIToggle toggle {
        get { return this.GetComponent<UIToggle>(); }
    } 
    #endregion
}
