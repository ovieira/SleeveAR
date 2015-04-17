using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.UI;
using FullSerializer;

[XmlRoot("ExerciseModel")]

public class MovementRecorder : MonoBehaviour {

    public ExerciseModel exerciseModel = new ExerciseModel();

    public enum FileFormatEnum {
        XML,
        JSON
    }

    public FileFormatEnum FileFormat;

    #region variables
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

    // Use this for initialization
    void Start() {
        //if (Target == null) {
        //    Debug.Log("Nothing to Record");
        //    canRecord = false;
        //}
        //Positions = new List<Joint>();

        for (int i = 0; i < ManagerTracking.instance.count; i++) {
            GameObject ob = (GameObject)Instantiate(JointRepresentationPrefab, Vector3.zero, Quaternion.identity);
            ob.GetComponent<Renderer>().material = greenMat;
            ob.SetActive(false);
            _replayPrefabs.Add(ob.transform);
        }
    }

    void Update() {
        if (canPlay) {
            for (int i = 0; i < _replayPrefabs.Count; i++)
            {
                Joint j = currentJointgroup.jointsList[i];
                _replayPrefabs[i].position = j.position;
                _replayPrefabs[i].rotation = j.rotation;
            }
        }
    }

    // Update is called once per frame
    void LateUpdate() {
        KeyboardHandler();
    }

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
    }

    public void OnClickRecordButton() {
        StartRecording();
    }

    private void StartRecording() {
        //canRecord = true;
        print("Started Recording");
        startTime = Time.time;
        InvokeRepeating("Record", 0f, 1f / FPS);

    }

    public void OnClickStopButton() {
        StopRecording();
    }

    private void StopRecording() {
        //canRecord = false;
        CancelInvoke("Record");
        print("Stopped Recording");
        print("Entries Saved: " + exerciseModel._exerciseModel.Count);
        print("Time:" + (Time.time - startTime));
    }
    #endregion

    public void OnClickPlayButton() {
        //throw new NotImplementedException();

        foreach (Transform replayPrefab in _replayPrefabs)
        {
            replayPrefab.gameObject.SetActive(true);
        }

        print("play");
        entry_no = 0;
        ManagerTracking.instance.setTracking(false);
        //Target.position = exerciseModel.Get(0).position;
        //Target.rotation = exerciseModel.Get(0).rotation;
        //GameObject.Find("Optitrack").SendMessage("setTracking", false);
        StartPlaying();
        InvokeRepeating("StartPlaying", 0f, 1f / FPS);
        canPlay = true;

    }

    public void StartPlaying() {
        //throw new NotImplementedException();

        if (entry_no < exerciseModel._exerciseModel.Count)
        {
           currentJointgroup =  exerciseModel.Get(entry_no);
            entry_no++;
        }

        //if (entry_no < exerciseModel.LogList.Count)
        //{
        //    Joint tc = exerciseModel.Get(entry_no++);
        //    auxPos = tc.position;
        //    auxRot = tc.rotation;
        //}
        //else {
        //    canPlay = false;
        //    CancelInvoke("StartPlaying");
        //}
    }


    public void OnClickSaveButton() {
        switch (FileFormat) {
            case FileFormatEnum.XML:
                XMLSave(Path.Combine(Application.dataPath + "/Recordings", _FileToLoad));
                break;
            case FileFormatEnum.JSON:
                JSONSave(Path.Combine(Application.dataPath + "/Recordings", _FileToLoad));
                break;
        }
    }

    public void OnClickLoadButton() {
        switch (FileFormat) {
            case FileFormatEnum.XML:
                //exerciseModel = XMLLoadFromFile(Path.Combine(Application.dataPath + "/Recordings", _FileToLoad));
                exerciseModel = XMLHandler.Load(Path.Combine(Application.dataPath + "/Recordings", _FileToLoad));
                break;
            case FileFormatEnum.JSON:
                exerciseModel = JSONLoad(Path.Combine(Application.dataPath + "/Recordings", _FileToLoad));
                break;
        }

        _InputField.text = "";
    }

    #region XML

    public void XMLSave(string fileName) {
        print("Saving file to: " + fileName);
        try {
            using (FileStream stream = new FileStream(fileName, FileMode.CreateNew)) {
                XmlSerializer XML = new XmlSerializer(typeof(ExerciseModel));
                XML.Serialize(stream, exerciseModel);
                print("Done!");
            }
        }
        catch (IOException e) {
            Debug.Log(e.Message);
        }
    }

    public ExerciseModel XMLLoadFromFile(string fileName) {
        print("Loading file : " + _FileToLoad);

        using (FileStream stream = new FileStream(fileName, FileMode.Open)) {
            XmlSerializer XML = new XmlSerializer(typeof(ExerciseModel));
            print("Done!");
            return (ExerciseModel)XML.Deserialize(stream);
        }

    }
    #endregion

    #region JSON

    private void JSONSave(string fileName) {
        try
        {
            if (File.Exists(fileName + ".json"))
            {
                int i = 1;
                while (File.Exists(fileName + i + ".json"))
                {
                    i++;
                }
                fileName = fileName + i;
            }

            using (FileStream stream = new FileStream(fileName+".json", FileMode.CreateNew)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                    fsSerializer _serializer = new fsSerializer();
                    fsData data;
                    _serializer.TrySerialize(typeof(ExerciseModel), exerciseModel, out data).AssertSuccessWithoutWarnings();
                    writer.Write(data.ToString());
                    print("Saved! : " + fileName);
                    writer.Flush();
                }
            }
        }
        catch (IOException e) {
            Debug.Log(e.Message);
        }
    }

    private ExerciseModel JSONLoad(string fileName) {
        print("Loading file : " + fileName);
        using (FileStream stream = new FileStream(fileName + ".json", FileMode.Open)) {
            using (StreamReader reader = new StreamReader(stream)) {
                fsSerializer _serializer = new fsSerializer();

                // step 1: parse the JSON data
                fsData data = fsJsonParser.Parse(reader.ReadToEnd());

                // step 2: deserialize the data
                object deserialized = null;
                _serializer.TryDeserialize(data, typeof(ExerciseModel), ref deserialized).AssertSuccessWithoutWarnings();

                return (ExerciseModel)deserialized;
            }
        }
    }

    #endregion

    [ContextMenu("TestJson")]
    public void testJson() {
        exerciseModel.testPopulate();
        JSONSave(Path.Combine(Application.dataPath + "/Recordings", "loool"));
    }

    [ContextMenu("TestJsonLoad")]
    public void testJsonLoad() {
        exerciseModel = JSONLoad(Path.Combine(Application.dataPath + "/Recordings", "loool"));
        Debug.Log("Done");
    }
}
