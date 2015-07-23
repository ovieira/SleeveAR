using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CanvasGroup))]
public abstract class UIView : MonoBehaviour, IUIView {

    #region Show/Hide

    public abstract void show();
    public abstract void hide();

    #region Show
    public void show(float from, float to, float time, float delay, iTween.EaseType easetype, string onupdate,
       string oncomplete) {
        Hashtable hashValueTo = Utils.HashValueTo(this.gameObject.name + "show", from, to, time, delay, easetype, onupdate, oncomplete);

        iTween.ValueTo(this.gameObject, hashValueTo);
    }

    public void show(float from, float to, float time, float delay)
    {
        show(from,to,time,delay,iTween.EaseType.linear, "onUpdate", "onShowCompleted");
    }

    public void show(float from, float to, float time, float delay, iTween.EaseType easetype) {
        show(from, to, time, delay, easetype, "onUpdate", "onShowCompleted");
    }

    protected virtual void onShowCompleted() {

    }
    #endregion


    #region Hide
    public void hide(float from, float to, float time, float delay, iTween.EaseType easetype, string onupdate,
       string oncomplete) {
        Hashtable hashValueTo = Utils.HashValueTo(this.gameObject.name + "show", from, to, time, delay, easetype, onupdate, oncomplete);

        iTween.ValueTo(this.gameObject, hashValueTo);

    }

    public void hide(float from, float to, float time, float delay) {
        hide(from, to, time, delay, iTween.EaseType.linear, "onUpdate", "onHideCompleted");
    }

    public void hide(float from, float to, float time, float delay, iTween.EaseType easetype) {
        hide(from, to, time, delay, easetype, "onUpdate", "onHideCompleted");
    }

    protected virtual void onHideCompleted() {

    }
    #endregion


    #endregion

    #region iTween Callback Functions

    protected virtual void onUpdate(float progress) {
        //this.canvasGroup.alpha = progress;
    }

   
    #endregion

    #region CanvasGroup

    protected CanvasGroup canvasGroup {
        get { return this.GetComponent<CanvasGroup>(); }
    }

    #endregion

    #region RectTransform

    protected RectTransform rectTransform
    {
        get { return this.GetComponent<RectTransform>(); }
    }
    #endregion
}
