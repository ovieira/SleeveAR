using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIViewTeachingText : UIView {

    public override void show() {
        show(this.canvasGroup.alpha, 1f, 0.5f, 0f);
    }

    public override void hide() {
        hide(this.canvasGroup.alpha, 0f, 0.5f, 0f);
    }

    public void hide(float delay)
    {
        hide(this.canvasGroup.alpha, 0f, .5f, delay);
    }

    public override void onShowCompleted() {
        //throw new System.NotImplementedException();
    }

    public override void onHideCompleted() {
        //throw new System.NotImplementedException();
    }

    public override void onUpdate(float progress) {
        this.canvasGroup.alpha = progress;
    }

    #region Text

    public Text text;

    #endregion
}
