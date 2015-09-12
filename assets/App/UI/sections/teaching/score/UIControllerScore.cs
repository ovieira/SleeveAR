using UnityEngine;
using System.Collections;

public class UIControllerScore : Controller {

	#region LifeCycle

    protected override void Awake()
    {
        base.Awake();
        serviceTeaching.onChangedCurrentScore += this._onChangedCurrentScore;
    }

    protected override void Start()
    {
        base.Start();
        this.view.hide(false);
        this.view.onCompletedScoreBoard += this._onCompletedScoreBoard;
    }

    

    protected override void OnDestroy()
    {
        base.OnDestroy();
        serviceTeaching.onChangedCurrentScore -= this._onChangedCurrentScore;

    }

    #endregion

    #region Service Teaching

    private void _onChangedCurrentScore(object sender, System.EventArgs e)
    {
        this.view.score = serviceTeaching.currentScore;
        this.view.show();
    }

    private void _onCompletedScoreBoard(object sender, System.EventArgs e) {
        Invoke("startOver", 2f);
    }

    protected void startOver()
    {
        this.view.hide();
        serviceTeaching.startOver();
    }
    #endregion

    #region View

    public UIViewScore view;

    #endregion
}
