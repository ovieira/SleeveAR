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

        createServiceTeaching();
        createServiceExercise();
        serviceTeaching.count = 0;
        Utils.DestroyAllChildren(transform);
        if (serviceExercise.selected.parts.Count == 0)
        {
            serviceExercise.selected.addPart(0, serviceExercise.count - 1);
        }
        Utils.AddChildren(transform, initialPositionGuidance);

        
    }

    private void createServiceExercise() {
        serviceExercise.onSelectedExerciseChanged += _onSelectedExerciseChanged;
        serviceExercise.onStart += _onStart;
        serviceExercise.onCurrentIndexChanged += _onCurrentIndexChanged;
        serviceExercise.onFinishedExercise += _onFinishedExercise;
    }

    private void createServiceTeaching() {
        serviceTeaching.onStartOver += _onStartOver;
        serviceTeaching.initialPositionCompleted = false;
        serviceTeaching.onInitialPositionCompleted += _onInitialPositionCompleted;
        serviceTeaching.onFailingExerciseChanged += _onFailingExerciseChanged;
        serviceTeaching.onFinishedRepetitions += _onFinishedRepetitions;
        serviceTeaching.session = new Session();
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        destroyServiceTeaching();
        destroyServiceExercise();
        

    }

    private void destroyServiceExercise() {
        serviceExercise.onFinishedExercise -= _onFinishedExercise;
        serviceExercise.onCurrentIndexChanged -= _onCurrentIndexChanged;
        serviceExercise.onStart -= _onStart;
        serviceExercise.onSelectedExerciseChanged -= _onSelectedExerciseChanged;
    }

    private void destroyServiceTeaching() {
        serviceTeaching.onFailingExerciseChanged -= _onFailingExerciseChanged;
        serviceTeaching.onInitialPositionCompleted -= _onInitialPositionCompleted;
        serviceTeaching.onFinishedRepetitions -= this._onFinishedRepetitions;
        serviceTeaching.onStartOver -= _onStartOver;
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
        serviceTeaching.initialLogTime = Time.time;
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

    public int exerciseRepetitions;
    private void _onStartOver(object sender, EventArgs e)
    {
        CancelInvoke("FailingExercise");
        CancelInvoke("ResetMovement");
        Utils.DestroyAllChildren(transform);
       

        if (serviceTeaching.count >= this.exerciseRepetitions)
        {
            Debug.Log("Requesitar ID");
            Debug.Log("Gravar session");
            serviceExercise.start = false;
            serviceTeaching.session.calculateSessionScore();
            serviceTeaching.session.sessionID = sessionID;
            serviceTeaching.session.exerciseID = serviceExercise.selected.exerciseID;
            ServiceFileManager.instance.SaveSession(serviceTeaching.session);
            Debug.Log("logs count"+serviceTeaching.session.logs.Count);
            serviceSection.selected = ServiceSection.Section.LEARNING;
        }
        else
        {

            serviceTeaching.currentLog = new Log();
            serviceTeaching.initialLogTime = Time.time;
            serviceExercise.start = true;
            serviceTeaching.initialPositionCompleted = false;
            Utils.AddChildren(transform, initialPositionGuidance);
        }
    }

    private void _onFinishedExercise(object sender, EventArgs e)
    {
        Debug.Log("Finished Exercise");
        CancelInvoke("FailingExercise");
        CancelInvoke("ResetMovement");
        Utils.DestroyAllChildren(transform);
        serviceTeaching.count++;
        float totalTime = Time.time - serviceTeaching.initialLogTime;
        serviceTeaching.currentLog.totaltime = totalTime;
        Debug.Log("Total Time: " + totalTime);
        serviceTeaching.session.Add(serviceTeaching.currentLog);
        if (serviceTeaching.count < 3)
        {
            Utils.AddChildren(this.transform, SessionReviewPrefab);
        }
        else
        {
            //TODO: review of all recent tries?
        }
        

        //serviceTeaching.startOver();
    }

    private void _onInitialPositionCompleted(object sender, EventArgs e)
    {
        serviceTeaching.currentLog.initialPositionTime = Time.time - serviceTeaching.initialLogTime;
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
        serviceTeaching.failingExercise = false;
        //serviceTeaching.session.Add(serviceTeaching.currentLog);
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

    #region Session

    public Session _session = new Session();


    [Header("SESSION ID")] 
    public string sessionID;

    #endregion

    #region Context menu debug

    //[ContextMenu("printSession")]
    //public void print() {
    //    serviceTeaching.session.print();
    //}

    //[ContextMenu("createLog")]
    //public void createLog() {
    //    var log = new Log();

    //    var jg = serviceTracking.getCurrentJointGroup();

    //    for (int i = 0; i < 100; i++) {
    //        log.AddEntry(jg);
    //    }
    //    //log.print();
    //    serviceTeaching.currentLog = log;
    //    serviceTeaching.currentLog.print();
    //}

    //[ContextMenu("createSession")]
    //public void createSession() {
    //    var log = new Log();
    //    var session = new Session();
    //    var jg = serviceTracking.getCurrentJointGroup();

    //    for (int i = 0; i < 10; i++) {
    //        var pos = new Vector3(i, i, i);
    //        serviceTeaching.currentLog.AddEntry(jg);
    //    }
    //    serviceTeaching.currentLog.logID = "LOL69";
    //    serviceTeaching.currentLog.invalidCount = 44;
    //    serviceTeaching.currentLog.validCount = 99;
    //    serviceTeaching.currentLog.totaltime = 1.23f;
    //    //serviceTeaching.currentLog.print();
    //    //serviceTeaching.currentLog = log;

    //    //serviceTeaching.currentLog.print();
    //    //session.Add(log);
    //    serviceTeaching.session.Add(serviceTeaching.currentLog);
    //    serviceTeaching.currentLog = new Log();
    //    serviceTeaching.session.print();
    //}

    //[ContextMenu("savesession")]
    //public void saveSession()
    //{
    //    createSession();
    //    ServiceFileManager.instance.SaveSession("testeSession", serviceTeaching.session);
    //}

    #endregion
}