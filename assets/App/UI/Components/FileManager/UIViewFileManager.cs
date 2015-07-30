using UnityEngine;
using System.Collections;

public class UIViewFileManager : UIView {
    #region Show/Hide

    public override void show() {
        show(this.canvasGroup.alpha, 1, 0.5f, 0);
    }

    public override void onShowCompleted()
    {
        this.canvasGroup.interactable = true;
        this.canvasGroup.blocksRaycasts = true;
    }

    public override void onHideCompleted() {
        this.canvasGroup.interactable = false;
        this.canvasGroup.blocksRaycasts = false;
    }

    public override void hide() {
        hide(this.canvasGroup.alpha, 0, 0.5f, 0);
    }

    public override void onUpdate(float progress) {
        this.canvasGroup.alpha = progress;
    }

    #endregion
}
