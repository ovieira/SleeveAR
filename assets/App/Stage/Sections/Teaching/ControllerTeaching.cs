using UnityEngine;
using System.Collections;

public class ControllerTeaching : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        serviceTeaching.onStartOver += this._onStartOver;

        serviceTeaching.initialPositionCompleted = false;

        serviceTeaching.onInitialPositionCompleted += this._onInitialPositionCompleted;

        serviceExercise.onSelectedExerciseChanged += this._onSelectedExerciseChanged;

        serviceExercise.onStart += this._onStart;

        serviceExercise.onCurrentIndexChanged += this._onCurrentIndexChanged;

        serviceTeaching.onFailingExerciseChanged += this._onFailingExerciseChanged;

        serviceExercise.onFinishedExercise += this._onFinishedExercise;
        Utils.DestroyAllChildren(this.transform);

        if (serviceExercise.selected.parts.Count == 0)
        {
            serviceExercise.selected.addPart(0, serviceExercise.count-1);
        }

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
        Invoke("FailingExercise", 10f);
    }

    #endregion

    protected void instantiatePrefab(GameObject prefab)
    {
        GameObject ob = Instantiate(prefab);
        ob.transform.SetParent(this.transform);
    }

    #region Service Teaching

    private void _onStartOver(object sender, System.EventArgs e) {
        CancelInvoke("FailingExercise");
        CancelInvoke("ResetMovement");
        serviceTeaching.initialPositionCompleted = false;
        Utils.DestroyAllChildren(this.transform);
        Utils.AddChildren(this.transform, initialPositionGuidance);
    }

    private void _onFinishedExercise(object sender, System.EventArgs e)
    {
        CancelInvoke("ResetMovement");
        Utils.DestroyAllChildren(this.transform);
        serviceTeaching.count++;
        switch (serviceTeaching.count) {
            case 0:
                break;
            case 1:
                serviceDifficulty.selected = ServiceDifficulty.Difficulty.MEDIUM;
                break;
            case 2:
                serviceDifficulty.selected = ServiceDifficulty.Difficulty.HARD;
                break;
            default:
                break;
        }
        serviceTeaching.startOver();
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
            Invoke("ResetMovement", 3f);
        }
        else
        {
            CancelInvoke("ResetMovement");    
            serviceAudio.StopAudio();
        }
    }

    protected void ResetMovement()
    {
        serviceExercise.index = 0;
        CancelInvoke("FailingExercise");
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
