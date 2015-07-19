using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIButton))]
public class UIControllerGoSectionButton : MonoBehaviour {

    #region Section

    public ServiceSection.Section _section;
    #endregion

    #region LifeCycle
    // Use this for initialization
    private void Start()
    {
        button.onClicked += this._onClicked;
    }
#endregion

    #region Button

    public UIButton button
    {
        get { return this.GetComponent<UIButton>(); }
    }

    private void _onClicked(object sender, System.EventArgs e) {
        ServiceSection.instance.selected = _section;
    }
    #endregion
}
