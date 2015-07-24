using System;
using UnityEngine;
using System.Collections;

public class UIControllerHoldPositionText : Controller {


    #region LifeCycle
    protected override void Start() {
        base.Start();
        serviceTeaching.onReachedInitialPosition += this._onReachedInitialPosition;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        serviceTeaching.onReachedInitialPosition -= this._onReachedInitialPosition;

    }
    #endregion

    #region Service Teaching

    protected void _onReachedInitialPosition(object sender, EventArgs e)
    {
        if (serviceTeaching.isOnInitialPosition)
        {
            this.view.show();
        }
        else
        {
            //serviceAudio.StopAudio();
            this.view.hide();
        }
    }

    #endregion


    #region View

    public UIViewHoldPositionText view;

    #endregion
}
