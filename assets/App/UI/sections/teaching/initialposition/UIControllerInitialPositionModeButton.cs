using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIButton))]
public class UIControllerInitialPositionModeButton : MonoBehaviour {

    #region Mode

    public ServiceGuideInitialPosition.Mode _mode;

    #endregion

    #region LifeCycle
    // Use this for initialization
    private void Start() {
        button.onClicked += this._onClicked;
    }

    public void OnDestroy() {
        button.onClicked -= this._onClicked;
    }


    #endregion

    #region Button

    public UIButton button {
        get { return this.GetComponent<UIButton>(); }
    }

    private void _onClicked(object sender, EventArgs e) {

        ServiceGuideInitialPosition.instance.selected = _mode;
    }
    #endregion
}
