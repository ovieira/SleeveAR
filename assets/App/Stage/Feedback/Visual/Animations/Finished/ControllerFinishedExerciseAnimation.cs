using UnityEngine;
using System.Collections;
using System.Linq;

public class ControllerFinishedExerciseAnimation : Controller {

	#region LifeCycle

    protected override void Awake()
    {
        base.Awake();

        serviceTeaching.onFinishedRepetitions += this._onFinishedRepetitions;
    }


    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        serviceTeaching.onFinishedRepetitions -= this._onFinishedRepetitions;

    }

    void Update()
    {
        for (int i = 0; i < this.view.whitecircles.Count; i++)
        {
            this.view.whitecircles[i].transform.position = serviceTracking.PositionFloor[i];
        }
    }
    #endregion

    #region View

    public ViewFinishedExerciseAnimation view;

    #endregion

    private void _onFinishedRepetitions(object sender, System.EventArgs e)
    {
        this.view.startAnimation();
    }
}
