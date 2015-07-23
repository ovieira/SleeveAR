using UnityEngine;
using System.Collections;



public class UIViewGoSectionButton : UIView {

    #region Show/Hide

    //public void show() {
    //    show(this.rectTransform.anchoredPosition.x, 0f, 1f, 0f, iTween.EaseType.easeInOutSine);
    //}

    //public void hide() {
    //    hide(this.rectTransform.anchoredPosition.x, -60f, 1f, 0f, iTween.EaseType.easeInOutSine);
    //}

    public override void show()
    {
        show(this.rectTransform.anchoredPosition.x, 0f, 1f, 0f, iTween.EaseType.easeInOutSine);
    }

    public override void hide()
    {
       hide(this.rectTransform.anchoredPosition.x, -60f, 1f, 0f, iTween.EaseType.easeInOutSine);
    }


    protected override void onUpdate(float progress)
    {
        base.onUpdate(progress);
        this.rectTransform.anchoredPosition = new Vector2(progress, 0);
    }

    #endregion

}
