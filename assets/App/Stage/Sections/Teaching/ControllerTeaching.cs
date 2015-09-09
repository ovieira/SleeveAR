using System;
using UnityEngine;

public class ControllerTeaching : Controller
{
    protected void instantiatePrefab(GameObject prefab)
    {
        var ob = Instantiate(prefab);
        ob.transform.SetParent(transform);
    }

    protected void FailingExercise()
    {
        serviceTeaching.failingExercise = true;
    }

    #region LifeCycle

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

        serviceTeaching.onStartOver += _onStartOver;
        serviceTeaching.initialPositionCompleted = false;
        serviceTeaching.onInitialPositionCompleted += _onInitialPositionCompleted;
        serviceExercise.onSelectedExerciseChanged += _onSelectedExerciseChanged;
        serviceExercise.onStart += _onStart;
        serviceExercise.onCurrentIndexChanged += _onCurrentIndexChanged;
        serviceTeaching.onFailingExerciseChanged += _onFailingExerciseChanged;
        serviceExercise.onFinishedExercise += _onFinishedExercise;
        serviceTeaching.onFinishedRepetitions += _onFinishedRepetitions;
        Utils.DestroyAllChildren(transform);
        if (serviceExercise.selected.parts.Count == 0)
        {
            serviceExercise.selected.addPart(0, serviceExercise.count - 1);
        }
        Utils.AddChildren(transform, initialPositionGuidance);

        
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        serviceExercise.onFinishedExercise -= _onFinishedExercise;
        serviceTeaching.onFailingExerciseChanged -= _onFailingExerciseChanged;
        serviceExercise.onCurrentIndexChanged -= _onCurrentIndexChanged;
        serviceExercise.onStart -= _onStart;
        serviceExercise.onSelectedExerciseChanged -= _onSelectedExerciseChanged;
        serviceTeaching.onInitialPositionCompleted -= _onInitialPositionCompleted;
        serviceTeaching.onFinishedRepetitions -= this._onFinishedRepetitions;

    }

    #endregion

    #region Service Exercise

    private void _onSelectedExerciseChanged(object sender, EventArgs e)
    {
        ServiceTeaching.instance.initialPositionCompleted = false;
    }

    private void _onStart(object sender, EventArgs e)
    {
        serviceTeaching.currentLog = new Log();
        //instantiatePrefab(initialPositionGuidance);
        // Utils.AddChildren(this.transform, initialPositionGuidance);
    }

    private void _onCurrentIndexChanged(object sender, EventArgs e)
    {
        if (serviceExercise.index == 0)
        {
            CancelInvoke("FailingExercise");
            return;
        }
        serviceTeaching.failingExercise = false;
        CancelInvoke("FailingExercise");
        Invoke("FailingExercise", 10f);
    }

    #endregion

    #region Service Teaching

    private void _onStartOver(object sender, EventArgs e)
    {
        CancelInvoke("FailingExercise");
        CancelInvoke("ResetMovement");
        serviceTeaching.initialPositionCompleted = false;
        serviceTeaching.session.Add(serviceTeaching.currentLog);
        serviceTeaching.currentLog = new Log();
        Utils.DestroyAllChildren(transform);
        Utils.AddChildren(transform, initialPositionGuidance);
        serviceExercise.start = true;
    }

    private void _onFinishedExercise(object sender, EventArgs e)
    {
        CancelInvoke("ResetMovement");
        Utils.DestroyAllChildren(transform);
        serviceTeaching.count++;

        if (serviceTeaching.count >= 1)
        {
            return;
        }

        switch (serviceTeaching.count)
        {
            case 0:
                break;
            case 1:
                serviceDifficulty.selected = ServiceDifficulty.Difficulty.MEDIUM;
                break;
            case 2:
                //serviceDifficulty.selected = ServiceDifficulty.Difficulty.HARD;
                break;
            default:
                break;
        }
        serviceTeaching.startOver();
    }

    private void _onInitialPositionCompleted(object sender, EventArgs e)
    {
        Debug.Log("Start guiding");
        Utils.DestroyAllChildren(transform);
        Utils.AddChildren(transform, MovementGuidance);
    }

    private void _onFailingExerciseChanged(object sender, EventArgs e)
    {
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
        //todo: count number of reset movements?
        serviceExercise.index = 0;
        CancelInvoke("FailingExercise");
    }

    private void _onFinishedRepetitions(object sender, EventArgs e)
    {
        Debug.Log("Finished Repetitions");
        CancelInvoke("FailingExercise");
        CancelInvoke("ResetMovement");
        serviceTeaching.session.Add(serviceTeaching.currentLog);
        serviceTeaching.session.print();
        Utils.DestroyAllChildren(transform);

        Utils.AddChildren(this.transform, SessionReviewPrefab);
    }

    #endregion

    #region Guiding Prefabs

    public GameObject initialPositionGuidance;
    public GameObject MovementGuidance;
    public GameObject SessionReviewPrefab;
    #endregion

    #region History

    public Session _session = new Session();

    #endregion

    [ContextMenu("printSession")]
    public void print()
    {
        serviceTeaching.session.print();
    }

    [ContextMenu("createLog")]
    public void createLog() {
        var log = new Log();

        var jg = serviceTracking.getCurrentJointGroup();

        for (int i = 0; i < 100; i++)
        {
            var pos = new Vector3(i,i,i);
            log.AddEntry(jg,pos);
        }
        //log.print();
        serviceTeaching.currentLog = log;
        serviceTeaching.currentLog.print();
    }

    [ContextMenu("createSession")]
    public void createSession() {
        var log = new Log();
        var session = new Session();
        var jg = serviceTracking.getCurrentJointGroup();

        for (int i = 0; i < 2000; i++) {
            var pos = new Vector3(i, i, i);
            serviceTeaching.currentLog.AddEntry(jg, pos);
        }
        //serviceTeaching.currentLog.print();
        //serviceTeaching.currentLog = log;

        //serviceTeaching.currentLog.print();
        //session.Add(log);
        serviceTeaching.session.Add(serviceTeaching.currentLog);
        serviceTeaching.currentLog = new Log();
        serviceTeaching.session.print();
    }
}