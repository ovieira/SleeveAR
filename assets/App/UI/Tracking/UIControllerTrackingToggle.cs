using System;
using UnityEngine;
using System.Collections;

public class UIControllerTrackingToggle : Controller {

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
        this.toggle.onValueChanged += this._onValueChanged;

    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.toggle.onValueChanged -= this._onValueChanged;

    }

    protected void _onValueChanged(object sender, EventArgs e)
    {
        serviceTracking.tracking = toggle.value;
    }

    protected UIToggle toggle
    {
        get { return this.GetComponent<UIToggle>(); }
    }
}
