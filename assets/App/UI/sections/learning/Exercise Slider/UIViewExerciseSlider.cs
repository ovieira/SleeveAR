using UnityEngine;
using System.Collections.Generic;

public class UIViewExerciseSlider : UIView {

    #region Show/Hide

    public override void show()
    {
        show(this.canvasGroup.alpha, 1,0.5f,0);
    }

    public override void onShowCompleted()
    {
    }

    public override void onHideCompleted()
    {
    }

    public override void hide() {
        hide(this.canvasGroup.alpha, 0, 0.5f, 0);
    }

    public override void onUpdate(float progress)
    {
        this.canvasGroup.alpha = progress;
    }

    #endregion

    #region Divider

    public List<Vector2> parts;

    public GameObject fillArea;
    public GameObject divider;

    #endregion

    public float maxValue;

    internal void showDividers()
    {
        var rectTransform = this.GetComponent<RectTransform>();

        var width = rectTransform.sizeDelta;

        foreach (var part in parts)
        {
            GameObject ob = Utils.AddChildren(this.fillArea.transform, divider);
            var v = new Vector2(Utils.Map(part.y, 0, maxValue, -250, 250), 0);
            ob.GetComponent<RectTransform>().anchoredPosition = v;
        }
    }

    public void updateDividers() {
        throw new System.NotImplementedException();
    }
}
