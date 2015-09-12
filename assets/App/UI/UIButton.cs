using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButton : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {
        this.button.onClick.AddListener(this.onClick);
    }

    #endregion
    
    #region OnClick

    public event EventHandler<EventArgs> onClicked;

    protected void onClick() {
        Utils.LaunchEvent(this, onClicked);   
    }
    #endregion

    #region Button

    protected Button button
    {
        get { return this.GetComponent<Button>(); }
    }

    #endregion

    #region Text

    public Text text
    {
        get { return this.GetComponentInChildren<Text>(); }
    } 

    #endregion
}
