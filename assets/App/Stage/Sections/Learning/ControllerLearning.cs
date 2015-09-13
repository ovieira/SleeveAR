using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;
using FullSerializer;

public class ControllerLearning : Controller {

    #region Variables
    public ExerciseModel exerciseModel = new ExerciseModel();

    public GameObject JointRepresentationPrefab;
    //private int entry_no;
    public string _FileToLoad { get; set; }
    public Material greenMat;
    private List<Transform> _replayPrefabs = new List<Transform>();
    //private JointsGroup currentJointgroup;


    //Time

    private float startTime;
    [Header("Time Variables")]

    public float _CountdownTime;

    public int capturesPerSecond = 24;

    [Space(10)]
    public AudioClip OneTwoThree;

    #endregion

    #region LifeCycle
    protected override void Awake() {
        base.Awake();
        ServiceExercise.instance.onSelectedExerciseChanged += this._onSelectedExerciseChanged;
        ServiceExercise.instance.onCurrentIndexChanged += this._onCurrentIndexChanged;
        CreateServiceMedia();
    }

    protected override void Start() {
        base.Start();
        for (int i = 0; i < ServiceTracking.instance.count; i++) {
            GameObject ob = (GameObject)Instantiate(JointRepresentationPrefab, Vector3.zero, Quaternion.identity);
            ob.transform.parent = this.transform;
            ob.GetComponent<Renderer>().material = greenMat;
            ob.SetActive(false);
            _replayPrefabs.Add(ob.transform);
        }

        if (serviceExercise.selected == null) return;
        exerciseModel = serviceExercise.selected;
        foreach (Transform replayPrefab in _replayPrefabs) {
            replayPrefab.gameObject.SetActive(true);
        }
        StopAllCoroutines();
        StartCoroutine(UpdateReplayJoints());

        
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        DestroyServiceMedia();

        ServiceExercise.instance.onSelectedExerciseChanged -= this._onSelectedExerciseChanged;

    }
    #endregion

    #region Service Exercise

    private void _onSelectedExerciseChanged(object sender, EventArgs e) {
        exerciseModel = serviceExercise.selected;
        
        StopAllCoroutines();
        StartCoroutine(UpdateReplayJoints());

        foreach (Transform replayPrefab in _replayPrefabs) {
            replayPrefab.gameObject.SetActive(true);
        }
    }

    private void _onCurrentIndexChanged(object sender, EventArgs e)
    {

    }



    #endregion

    #region Service Media
    private void CreateServiceMedia() {
        ServiceMedia.instance.onStartPlaying += this._onStartPlaying;
        ServiceMedia.instance.onStartRecording += this._onStartRecording;
        ServiceMedia.instance.onStopRecording += this._onStopRecording;
    }

    private void DestroyServiceMedia() {
        ServiceMedia.instance.onStopRecording -= this._onStopRecording;
        ServiceMedia.instance.onStartRecording -= this._onStartRecording;
        ServiceMedia.instance.onStartPlaying -= this._onStartPlaying;
    }

    private void _onStopRecording(object sender, EventArgs e) {
        StopRecording();
    }

    private void _onStartRecording(object sender, EventArgs e) {
        if (ServiceTracking.instance.tracking == false) {
            Debug.LogWarning("Tracking is not enabled");
        }
        StartRecording();
    }

    private void _onStartPlaying(object sender, EventArgs e) {
        if (ServiceExercise.instance.selected == null) {
            Debug.LogError("No exercise loaded to play :(");
            return;
        }
        StartPlaying();
    }

    #endregion

    #region KeyboardHandler
    private void KeyboardHandler() {
        if (Input.GetKeyDown(KeyCode.R)) {
            StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            StopRecording();
        }
    }
    #endregion

    #region Recording

    public float recordingTime;

    private void Record() {
        if (recordingTime != 0f)
        {
            if (Time.time - startTime >= recordingTime)
            {
                StopRecording();
            }
        }
        exerciseModel.Add(ServiceTracking.instance.getCurrentJointGroup());
        Debug.Log("Recording Data");
    }

    private void StartRecording() {
        print("Started Recording");

        Invoke("playWarningSound", _CountdownTime - 3f);
        Invoke("setUpTimer", _CountdownTime);
        InvokeRepeating("Record", _CountdownTime, 1f / capturesPerSecond);
    }

    private void playWarningSound() {
        GetComponent<AudioSource>().PlayOneShot(OneTwoThree);
    }

    private void setUpTimer() {
        startTime = Time.time;
    }

    private void StopRecording() {
        serviceAudio.PlayCorrect();
        CancelInvoke("Record");
        ServiceExercise.instance.selected = exerciseModel;

        print("Stopped Recording");
        print("Entries Saved: " + exerciseModel.exerciseModel.Count);
        print("Time:" + (Time.time - startTime));
    }
    #endregion

    #region Playing
    public void StartPlaying() {

        if (serviceExercise.index == serviceExercise.count - 1)
        {
            serviceExercise.index = 0;
        }

        ServiceTracking.instance.setTracking(false);
        print("playing exercise: " + exerciseModel.exerciseID);
        //IterateExercise();
        //CancelInvoke("IterateExercise");
        //InvokeRepeating("IterateExercise", 0f, 1f / capturesPerSecond);
       // canPlay = true;
       
    }

    IEnumerator UpdateReplayJoints()
    {
        while (true)
        {
            JointsGroup jg = serviceExercise.currentJointsGroup;
            for (int i = 0; i < _replayPrefabs.Count; i++)
            {
                SingleJoint j = jg.jointsList[i];
                _replayPrefabs[i].position = j.position;
                _replayPrefabs[i].rotation = j.rotation;
            }
            yield return null;
        }
    }


    public void IterateExercise() {

        
        //if (entry_no < exerciseModel.exerciseModel.Count) {
        //    currentJointgroup = exerciseModel.Get(entry_no);
        //    entry_no++;
        //}
        
        //currentJointgroup = serviceExercise.currentJointsGroup;
        serviceExercise.index++;
        //entry_no = serviceExercise.index;
        
    }
    #endregion


}
