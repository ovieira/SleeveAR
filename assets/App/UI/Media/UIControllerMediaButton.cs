using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIControllerMediaButton : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {

        button = this.GetComponent<Button>();

        this.button.onClick.AddListener(this._onButtonClicked);

    }
    #endregion

    #region On Click
    private void _onButtonClicked() {

        switch (buttonType) {
            case MediaButtonType.RECORD:
                ServiceMedia.instance.Record();
                break;
            case MediaButtonType.STOP:
                ServiceMedia.instance.Stop();
                break;
            case MediaButtonType.PLAY:
                ServiceMedia.instance.Play();
                break;
            default:
                break;
        }

    }
    #endregion

    #region MediaType

    public enum MediaButtonType {
        RECORD,
        STOP,
        PLAY
    }


    public MediaButtonType buttonType;

    #endregion

    #region Button

    protected Button button;

    #endregion

    #region View

    protected UIViewMediaButton view {
        get { return this.GetComponent<UIViewMediaButton>(); }
    }

    #endregion
}
