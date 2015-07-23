using UnityEngine;
using System.Collections;

public class ControllerTeaching : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        serviceTeaching.reachedInitialPosition = false;

        serviceTeaching.onReachedInitialPosition += this._onReachedInitialPosition;

        serviceExercise.onSelectedExerciseChanged += this._onSelectedExerciseChanged;

        serviceExercise.onStart += this._onStart;
    }


   

    #endregion

    #region Service Exercise

    private void _onSelectedExerciseChanged(object sender, System.EventArgs e)
    {
        ServiceTeaching.instance.reachedInitialPosition = false;
    }

    private void _onStart(object sender, System.EventArgs e) {
        instantiatePrefab(AngleFeedback);
    }

    protected void instantiatePrefab(GameObject prefab)
    {
        GameObject ob = Instantiate(prefab);
        ob.transform.SetParent(this.transform);
    }

    #endregion

    #region Service Teaching

    private void _onReachedInitialPosition(object sender, System.EventArgs e) {
        Debug.Log("Start guiding");
    }

    #endregion

    #region Guiding Prefabs

    public GameObject AngleFeedback;
    public GameObject HeightFeedback;
    public GameObject FloorFeedback;

    #endregion
}
