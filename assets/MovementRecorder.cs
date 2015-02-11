using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine.UI;

[XmlRoot("MovementLog")]
public class MovementRecorder : MonoBehaviour
{


    public MovementLog _MovementLog = new MovementLog();

   

    public Transform Target;

    public int FPS = 24;
    private bool canRecord = false;
    private float startTime;
    private Vector3 auxPos;
    private bool canPlay = false;
    private int entry_no;
    public InputField _InputField;
    public string _FileToLoad { get; set; }

    // Use this for initialization
    void Start() {
        if (Target == null) {
            Debug.Log("Nothing to Record");
            canRecord = false;
        }
        //Positions = new List<TimeCoord>();
    }

    void Update()
    {
        if (canPlay) {
            Target.position = Vector3.Lerp(Target.position, auxPos, 1f/FPS);
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
        if (Time.time-startTime>=10)
        {
            StopRecording();
        }
        _MovementLog.Add(new TimeCoord(DateTime.Now, Target.position));
    }

    public void OnClickRecordButton() {
        StartRecording();
    }

    private void StartRecording() {
        //canRecord = true;
        print("Started Recording");
        startTime = Time.time;
        InvokeRepeating("Record", 0f, 1f/FPS);

    }

    public void OnClickStopButton() {
        StopRecording();
    }

    private void StopRecording() {
        //canRecord = false;
        CancelInvoke("Record");
        print("Stopped Recording");
        print("Entries Saved: " + _MovementLog._log.Count);
        print("Time:" + (Time.time - startTime));
    } 
    #endregion

    public void OnClickPlayButton()
    {
        print("play");
        canPlay = true;
        entry_no = 0;
        Target.position = _MovementLog.Get(0)._position;
        InvokeRepeating("StartPlaying", 0f, 1f/FPS);


    }

    public void StartPlaying()
    {
        if (entry_no < _MovementLog._log.Count) {
            auxPos = _MovementLog.Get(entry_no++)._position;
        }
        else
        {
            canPlay = false;
            CancelInvoke("StartPlaying");
        }
    }

    #region Save
    public void OnClickSaveButton()
    {
        Save(Path.Combine(Application.dataPath,"teste.xml"));
    }

    
    public void Save(string fileName) {
        print("Saving file to: "+fileName);
        using (FileStream stream = new FileStream(fileName, FileMode.CreateNew)) {
            XmlSerializer XML = new XmlSerializer(typeof(MovementLog));
            XML.Serialize(stream, _MovementLog);
        }
        print("Done!");
    } 
    #endregion

    #region Load

    public void OnClickLoadButton()
    {

        LoadFromFile(Path.Combine(Application.dataPath, _FileToLoad));
        _InputField.text = "";
    }

    public MovementLog LoadFromFile(string fileName)
    {
        print("Loading file : " + _FileToLoad);

        using (FileStream stream = new FileStream(fileName, FileMode.Open)) {
            XmlSerializer XML = new XmlSerializer(typeof(MovementLog));
            print("Done!");
            return (MovementLog)XML.Deserialize(stream);
        }

    }

    #endregion
}
