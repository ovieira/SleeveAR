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

        serviceExercise.onCurrentIndexChanged += this._onCurrentIndexChanged;

        serviceTeaching.onFailingExerciseChanged += this._onFailingExerciseChanged;

        serviceExercise.onFinishedExercise += this._onFinishedExercise;
        Utils.DestroyAllChildren(this.transform);

        Utils.AddChildren(this.transform, initialPositionGuidance);
    }

   

    

    protected override void OnDestroy()
    {
        base.OnDestroy();

        serviceExercise.onFinishedExercise -= this._onFinishedExercise;


        serviceTeaching.onFailingExerciseChanged -= this._onFailingExerciseChanged;


        serviceExercise.onCurrentIndexChanged -= this._onCurrentIndexChanged;

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

    private void _onCurrentIndexChanged(object sender, System.EventArgs e)
    {
        serviceTeaching.failingExercise = false;
        CancelInvoke("FailingExercise");
        Invoke("FailingExercise", 5f);
    }

    #endregion

    protected void instantiatePrefab(GameObject prefab)
    {
        GameObject ob = Instantiate(prefab);
        ob.transform.SetParent(this.transform);
    }

    #region Service Teaching

    private void _onFinishedExercise(object sender, System.EventArgs e)
    {
        CancelInvoke("ResetExercise");
        Utils.DestroyAllChildren(this.transform);
    }

    private void _onInitialPositionCompleted(object sender, System.EventArgs e) {
        Debug.Log("Start guiding");
        Utils.DestroyAllChildren(this.transform);
        Utils.AddChildren(this.transform, MovementGuidance);

    }

    private void _onFailingExerciseChanged(object sender, System.EventArgs e) {
        if (serviceTeaching.failingExercise)
        {
            serviceAudio.PlayCountDown();
            Invoke("ResetExercise", 3f);
        }
        else
        {
            CancelInvoke("ResetExercise");    
            serviceAudio.StopAudio();
        }
    }

    protected void ResetExercise()
    {
        serviceExercise.index = 0;
    }

    #endregion

    #region Guiding Prefabs

    public GameObject initialPositionGuidance;
    public GameObject MovementGuidance;
    #endregion

    protected void FailingExercise()
    {
        serviceTeaching.failingExercise = true;
    }
   
}
