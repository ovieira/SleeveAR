using UnityEngine;
using System.Collections;
using UnityEditor;

public class UIViewStartExerciseButton : UIView {

    #region Show/Hide
    public void show() {
        this.show(this.canvasGroup.alpha, 1f, 1f, 0f);
    }

    public void hide() {
        this.hide(this.canvasGroup.alpha, 0, 1f, 0f);
    }

    protected override void onShowCompleted() {
        base.onShowCompleted();
        //Debug.Log("Show Completed");
    }


    protected override void onHideCompleted()
    {
        base.onHideCompleted();
        //Debug.Log("hide completed");
    }

    protected override void onUpdate(float progress) {
        base.onUpdate(progress);
        this.canvasGroup.alpha = progress;
    }

    #endregion
}
