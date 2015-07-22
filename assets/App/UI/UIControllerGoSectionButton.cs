using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIButton))]
public class UIControllerGoSectionButton : MonoBehaviour {

    #region Section

    public ServiceSection.Section _section;
    #endregion

    #region LifeCycle
    // Use this for initialization
    private void Start() {
        button.onClicked += this._onClicked;
        ServiceSection.instance.onSectionChanged += this._onSectionChanged;
    }
    #endregion

    #region Button

    public UIButton button {
        get { return this.GetComponent<UIButton>(); }
    }

    private void _onClicked(object sender, System.EventArgs e) {
        ServiceSection.instance.selected = _section;
    }
    #endregion

    #region Section Service

    protected void _onSectionChanged(object sender, EventArgs e) {
        //if (ServiceSection.instance.selected == this._section) this.view.hide();
        //else
        //    this.view.show();
    }

    #endregion

    #region View

    public UIViewGoSectionButton view;

    #endregion
}
