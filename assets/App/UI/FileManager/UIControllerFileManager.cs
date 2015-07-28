using UnityEngine;
using System.Collections;

public class UIControllerFileManager : Controller {
    #region LifeCycle
    protected override void Start() {
        base.Start();
        serviceSection.onSectionChanged += this._onSectionChanged;
    }

    private void _onSectionChanged(object sender, System.EventArgs e)
    {
        if (serviceSection.selected == ServiceSection.Section.LEARNING)
            this.view.show();
        else
            this.view.hide();

    }

    protected override void OnDestroy() {
        base.OnDestroy();
        serviceSection.onSectionChanged -= this._onSectionChanged;

    } 
    #endregion


    #region View

    public UIViewFileManager view;

    #endregion
}
