using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIButton))]
public class UIControllerGoSectionButton : MonoBehaviour {

    #region Section

    public ServiceSection.Section _section;
    #endregion


	// Use this for initialization
	void Start ()
	{
	    button.onClicked += this._onClicked;
	}

    private void _onClicked(object sender, System.EventArgs e)
    {
        ServiceSection.instance.selected = _section;
    }

    #region Button

    public UIButton button
    {
        get { return this.GetComponent<UIButton>(); }
    }

    #endregion
}
