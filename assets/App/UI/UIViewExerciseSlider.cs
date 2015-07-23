using UnityEngine;
using System.Collections;

public class UIViewExerciseSlider : UIView {

    #region Show/Hide

    public override void show()
    {
        show(this.canvasGroup.alpha, 1,0.5f,0);
    }

    public override void hide() {
        hide(this.canvasGroup.alpha, 0, 0.5f, 0);
    }

    protected override void onUpdate(float progress)
    {
        base.onUpdate(progress);
        this.canvasGroup.alpha = progress;
    }

    #endregion

}
