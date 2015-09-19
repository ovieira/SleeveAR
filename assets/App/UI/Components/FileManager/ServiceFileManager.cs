using System;
using UnityEngine;
using System.Collections;
using System.IO;
using FullSerializer;

public class ServiceFileManager {

    #region ExerciseModel
    public void SaveExerciseModel(string _fileName, ExerciseModel exerciseModel) {
        string fileName = Path.Combine(Application.dataPath + "/Recordings", _fileName);
        try {
            if (File.Exists(fileName + ".json")) {
                int i = 1;
                while (File.Exists(fileName + i + ".json")) {
                    i++;
                }
                fileName = fileName + i;
            }
            JSONSave(fileName,exerciseModel);
        }
        catch (IOException e) {
            Debug.Log(e.Message);
        }
    }

    public ExerciseModel LoadExerciseModel(string _fileName) {
        string fileName = Path.Combine(Application.dataPath + "/Recordings", _fileName);
        ExerciseModel exerciseModel;
        Debug.Log("Loading file : " + fileName);
        exerciseModel = JSONLoad<ExerciseModel>(fileName);
        return exerciseModel;

    }
    #endregion

    #region Sessions

    public void SaveSession(Session session)
    {
        string filename = session.sessionID + "_" + session.exerciseID;
        SaveSession(filename,session);
    }

    public void SaveSession(string _fileName, Session session) {
        string fileName = Path.Combine(Application.dataPath + "/Sessions", _fileName);
        try {
            if (File.Exists(fileName + ".json")) {
                int i = 1;
                while (File.Exists(fileName + i + ".json")) {
                    i++;
                }
                fileName = fileName + i;
            }
            JSONSave(fileName, session);
        }
        catch (IOException e) {
            Debug.Log(e.Message);
        }
    }

    public Session LoadSession(string _fileName) {
        string fileName = Path.Combine(Application.dataPath + "/Sessions", _fileName);
        Session session;
        Debug.Log("Loading file : " + fileName);
        session = JSONLoad<Session>(fileName);
        return session;
    }

    #endregion

    #region Generic JSON Save/Load

    public static T JSONLoad<T>(string filename)
    {
        using (FileStream stream = new FileStream(filename + ".json", FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                fsSerializer _serializer = new fsSerializer();

                // step 1: parse the JSON data
                fsData data = fsJsonParser.Parse(reader.ReadToEnd());

                // step 2: deserialize the data
                object deserialized = null;
                _serializer.TryDeserialize(data, typeof (T), ref deserialized).AssertSuccessWithoutWarnings();

                return (T) deserialized;
            }
        }
    }

    public static void JSONSave<T>(string filename, T obj)
    {
        using (FileStream stream = new FileStream(filename + ".json", FileMode.CreateNew)) {
            using (StreamWriter writer = new StreamWriter(stream)) {
                fsSerializer _serializer = new fsSerializer();
                fsData data;
                _serializer.TrySerialize(typeof(T), obj, out data).AssertSuccessWithoutWarnings();
                writer.Write(data.ToString());
                Debug.Log("Saved! : " + filename);
                writer.Flush();
            }
        }
    }

    #endregion

    #region Generic Text Save

    public void WriteToFile(string filename, string content) {
        string newpath = Application.dataPath + "/CSV/" + 
               filename;

        System.IO.File.WriteAllText(newpath, content);

        Debug.Log(filename + " has been created.");
    }

    #endregion

    #region Singleton

    private static ServiceFileManager _instance;


    public static ServiceFileManager instance {
        get {
            if (_instance == null) {
                _instance = new ServiceFileManager();
            }
            return _instance;
        }
    }

    #endregion

}
