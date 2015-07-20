using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CanvasGroup))]
public class UIView : MonoBehaviour {

    #region Show/Hide

    public void show()
    {
        Hashtable hashValueTo = Utils.HashValueTo(this.gameObject.name + "show", this.canvasGroup.alpha, 1f, 1f, 0f,
            iTween.EaseType.easeInOutSine, "onUpdate", "onComplete");

        iTween.ValueTo(this.gameObject, hashValueTo);

        this.canvasGroup.interactable = true;
        this.canvasGroup.blocksRaycasts = true;
    }

    public void hide() {
        Hashtable hashValueTo = Utils.HashValueTo(this.gameObject.name + "hide", this.canvasGroup.alpha, 0f, 1f, 0f,
            iTween.EaseType.easeInOutSine, "onUpdate", "onComplete");

        iTween.ValueTo(this.gameObject, hashValueTo);

        this.canvasGroup.interactable = false;
        this.canvasGroup.blocksRaycasts = false;
    }

    #endregion


    #region iTween Callback Functions

    protected void onUpdate(float progress)
    {
        this.canvasGroup.alpha = progress;
    }

    protected void onComplete()
    {
        
    }
    #endregion

    #region CanvasGroup

    protected CanvasGroup canvasGroup
    {
        get { return this.GetComponent<CanvasGroup>(); }
    }

    #endregion
}
