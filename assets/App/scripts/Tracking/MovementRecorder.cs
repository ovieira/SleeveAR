using System;
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

    public Transform Target;

    public int FPS = 24;
    private bool canRecord = false;
    private float startTime;
    private Vector3 auxPos;
    private bool canPlay = false;
    private int entry_no;
    public InputField _InputField;
    public Quaternion auxRot;
    public string _FileToLoad { get; set; }

    // Use this for initialization
    void Start() {
        if (Target == null) {
            Debug.Log("Nothing to Record");
            canRecord = false;
        }
        //Positions = new List<Joint>();
    }

    void Update() {
        if (canPlay) {
            Target.position = Vector3.Lerp(Target.position, auxPos, 1f / FPS);
            Target.rotation = Quaternion.Lerp(Target.rotation, auxRot, 1f/FPS);
        }
    }

    // Update is called once per frame
    void LateUpdate() {
        //KeyboardHandler();
        //if (canRecord) Record();
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
        //exerciseModel.Add(new Joint(Target.position,Target.rotation));
        for (int i = 0; i < ManagerTracking.instance.count; i++)
        {
            
        }
    }

    public void OnClickRecordButton() {
        StartRecording();
    }

    private void StartRecording() {
        //canRecord = true;
        print("Started Recording");
        startTime = Time.time;
        GameObject.Find("Optitrack").SendMessage("setTracking", true);
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

    public void OnClickPlayButton()
    {
        throw new NotImplementedException();
        //print("play");
        //canPlay = true;
        //entry_no = 0;
        //Target.position = exerciseModel.Get(0).position;
        //Target.rotation = exerciseModel.Get(0).rotation;
        //GameObject.Find("Optitrack").SendMessage("setTracking", false);
        //InvokeRepeating("StartPlaying", 0f, 1f / FPS);   
    }
     
    public void StartPlaying() {
        throw new NotImplementedException();

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
                XMLSave(Path.Combine(Application.dataPath + "/Recordings", _FileToLoad+".xml"));
                break;
            case FileFormatEnum.JSON:
                JSONSave(Path.Combine(Application.dataPath + "/Recordings", _FileToLoad+".json"));
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
        try {
            using (FileStream stream = new FileStream(fileName, FileMode.CreateNew)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                    fsSerializer _serializer = new fsSerializer();
                    fsData data;
                    _serializer.TrySerialize(typeof(ExerciseModel), exerciseModel, out data).AssertSuccessWithoutWarnings();
                    writer.Write(data.ToString());
                    print("Done!");
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
        using (FileStream stream = new FileStream(fileName, FileMode.Open)) {
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
    public void testJson()
    {
        exerciseModel.testPopulate();
        JSONSave(Path.Combine(Application.dataPath + "/Recordings","loool.json"));
    }

    [ContextMenu("TestJsonLoad")]
    public void testJsonLoad()
    {
      exerciseModel = JSONLoad(Path.Combine(Application.dataPath + "/Recordings", "loool.json"));
        Debug.Log("Done");
    }
}
