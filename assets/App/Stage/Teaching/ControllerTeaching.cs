using UnityEngine;
using System.Collections;

public class ControllerTeaching : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        serviceTeaching.initialPositionCompleted = false;

        serviceTeaching.onInitialPositionCompleted += this._onInitialPositionCompleted;

        serviceExercise.onSelectedExerciseChanged += this._onSelectedExerciseChanged;

        serviceExercise.onStart += this._onStart;

        Utils.DestroyAllChildren(this.transform);

        Utils.AddChildren(this.transform, initialPositionGuidance);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        serviceExercise.onStart -= this._onStart;
        serviceExercise.onSelectedExerciseChanged -= this._onSelectedExerciseChanged;

        serviceTeaching.onInitialPositionCompleted -= this._onInitialPositionCompleted;


    }

    #endregion

    #region Service Exercise

    private void _onSelectedExerciseChanged(object sender, System.EventArgs e)
    {
        ServiceTeaching.instance.initialPositionCompleted = false;
    }

    private void _onStart(object sender, System.EventArgs e) {
        //instantiatePrefab(initialPositionGuidance);
       // Utils.AddChildren(this.transform, initialPositionGuidance);
    }

    protected void instantiatePrefab(GameObject prefab)
    {
        GameObject ob = Instantiate(prefab);
        ob.transform.SetParent(this.transform);
    }

    #endregion

    #region Service Teaching

    private void _onInitialPositionCompleted(object sender, System.EventArgs e) {
        Debug.Log("Start guiding");
    }

    #endregion

    #region Guiding Prefabs

    public GameObject initialPositionGuidance;
    public GameObject MovementGuidance;

    public GameObject AngleFeedback;
    public GameObject HeightFeedback;
    public GameObject FloorFeedback;

    #endregion

   
}
