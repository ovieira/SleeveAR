using UnityEngine;
using System.Collections;

public class UIViewHoldPositionText : UIView {



    public override void show() {
        show(this.canvasGroup.alpha, 1f, 0.5f, 0f);
    }

    public override void hide() {
        hide(this.canvasGroup.alpha, 0f, 0.5f, 0f);
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
}
