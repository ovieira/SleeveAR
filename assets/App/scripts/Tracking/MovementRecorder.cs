using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;
using FullSerializer;

[XmlRoot("ExerciseModel")]

public class MovementRecorder : MonoBehaviour {



    #region variables
    public ExerciseModel exerciseModel = new ExerciseModel();

    public GameObject JointRepresentationPrefab;
    public int FPS = 24;
    private bool canRecord = false;
    private float startTime;
    private Vector3 auxPos;
    private bool canPlay = false;
    private int entry_no;
    public InputField _InputField;
    public Quaternion auxRot;
    public string _FileToLoad { get; set; }
    public Material greenMat;
    private List<Transform> _replayPrefabs = new List<Transform>();
    private JointsGroup currentJointgroup;
    #endregion

    #region LifeCycle
    void Awake() {
        ServiceExercise.instance.onSelectedExerciseChanged += this._onSelectedExerciseChanged;
    }

    private void _onSelectedExerciseChanged(object sender, EventArgs e) {
        exerciseModel = ServiceExercise.instance.selected;
    }

    // Use this for initialization
    void Start() {
        for (int i = 0; i < ManagerTracking.instance.count; i++) {
            GameObject ob = (GameObject)Instantiate(JointRepresentationPrefab, Vector3.zero, Quaternion.identity);
            ob.GetComponent<Renderer>().material = greenMat;
            ob.SetActive(false);
            _replayPrefabs.Add(ob.transform);
        }
    }

    void Update() {
        if (canPlay) {
            for (int i = 0; i < _replayPrefabs.Count; i++) {
                SingleJoint j = currentJointgroup.jointsList[i];
                _replayPrefabs[i].position = j.position;
                _replayPrefabs[i].rotation = j.rotation;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate() {
        //KeyboardHandler();
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
    private void Record() {
        if (Time.time - startTime >= 10) {
            StopRecording();
        }
        exerciseModel.Add(ManagerTracking.instance.getCurrentJointGroup());
        Debug.Log("Recording Data");
    }

    public void OnClickRecordButton() {
        StartRecording();
    }

    private void StartRecording() {
        //canRecord = true;
        print("Started Recording");
        
        Invoke("playWarningSound", _CountdownTime-3f);
        Invoke("setUpTimer", _CountdownTime);
        InvokeRepeating("Record", _CountdownTime, 1f / FPS);
    }

    private void playWarningSound()
    {
        GetComponent<AudioSource>().PlayOneShot(OneTwoThree);
    }

    private void setUpTimer()
    {
        startTime = Time.time;
    }

    public void OnClickStopButton() {
        StopRecording();
    }

    private void StopRecording() {
        //canRecord = false;
        CancelInvoke("Record");
        ServiceExercise.instance.selected = exerciseModel;

        print("Stopped Recording");
        print("Entries Saved: " + exerciseModel.exerciseModel.Count);
        print("Time:" + (Time.time - startTime));
    }
    #endregion

    #region Playing
    public void OnClickPlayButton() {
        //throw new NotImplementedException();

        foreach (Transform replayPrefab in _replayPrefabs) {
            replayPrefab.gameObject.SetActive(true);
        }

        entry_no = 0;
        ManagerTracking.instance.setTracking(false);
        print("playing exercise: " + exerciseModel.label);

        //Target.position = exerciseModel.Get(0).position;
        //Target.rotation = exerciseModel.Get(0).rotation;
        //GameObject.Find("Optitrack").SendMessage("setTracking", false);
        StartPlaying();
        InvokeRepeating("StartPlaying", 0f, 1f / FPS);
        canPlay = true;

    }

    public void StartPlaying() {

        if (entry_no < exerciseModel.exerciseModel.Count) {
            currentJointgroup = exerciseModel.Get(entry_no);
            entry_no++;
        }

    } 
    #endregion

    public AudioClip OneTwoThree;
    public float _CountdownTime;
}
