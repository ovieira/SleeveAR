using UnityEngine;
using System.Collections;
using UnityEditor;

public class UIViewStartExerciseButton : UIView {

    #region Show/Hide
    public override void show() {
        this.show(this.canvasGroup.alpha, 1f, 1f, 0f);
    }

    public override void hide() {
        this.hide(this.canvasGroup.alpha, 0, 1f, 0f);
    }

    public override void onShowCompleted() {
        //Debug.Log("Show Completed");
    }


    public override void onHideCompleted()
    {
        //Debug.Log("hide completed");
    }

    public override void onUpdate(float progress) {
        this.canvasGroup.alpha = progress;
    }

    #endregion
}
