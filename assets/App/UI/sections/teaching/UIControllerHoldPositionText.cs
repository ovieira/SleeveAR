using System;
using UnityEngine;
using System.Collections;

public class UIControllerHoldPositionText : Controller {


    #region LifeCycle
    protected override void Start() {
        base.Start();
        serviceTeaching.onReachedInitialPosition += this._onReachedInitialPosition;
        serviceTeaching.onInitialPositionCompleted += this._onInitialPositionCompleted;
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

    private void _onInitialPositionCompleted(object sender, EventArgs e)
    {
        this.view.text.text = "Begin!";
        this.view.hide(3f);
    }

    #endregion


    #region View

    public UIViewHoldPositionText view;

    #endregion
}
