using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIViewScore : UIView {

    #region Score

    public float score;
    #endregion

    #region Show/Hide
    public override void show()
    {
        this.scoreValueText.text = "";

        this.scoreBar.sizeDelta = new Vector2(0, this.scoreBar.sizeDelta.y);
        this.show(this.canvasGroup.alpha, 1f, 1f, 0f);
    }

    public override void hide()
    {
        this.scoreValueText.text = "";
        this.hide(this.canvasGroup.alpha, 0, 1f, 0f);
    }

    public override void onShowCompleted()
    {
        this.animateScore();
    }

    public override void onHideCompleted() {
        //Debug.Log("hide completed");
    }

    public override void onUpdate(float progress) {
        this.canvasGroup.alpha = progress;
    }
    #endregion

    #region ScoreBar

    public RectTransform scoreBar;
    public Image scoreBarImage;
    public Text scoreValueText;
    private void animateScore()
    {
        Hashtable valueto = Utils.HashValueTo("scoreanimate", 0, score, 2f, 0f, iTween.EaseType.easeInOutSine,
            "updateScoreBoard", "completeScoreBoard");

        iTween.ValueTo(this.gameObject, valueto);
    }

    protected void updateScoreBoard(float progress)
    {
        this.scoreValueText.text = ((int)progress).ToString();

        var newDelta = new Vector2(progress*3, scoreBar.sizeDelta.y);
        scoreBar.sizeDelta = newDelta;
        scoreBarImage.color = Color.Lerp(Color.red, Color.green, progress/100f);
    }

    public event EventHandler<EventArgs> onCompletedScoreBoard;
 
    protected void completeScoreBoard() {
        Utils.LaunchEvent(this, onCompletedScoreBoard);
    }
    #endregion


    internal void hide(bool p)
    {
        this.canvasGroup.alpha = 0;
    }
}
