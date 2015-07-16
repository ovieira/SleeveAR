using UnityEngine;
using System.Collections;
using System.IO;
using FullSerializer;

public class ServiceFileManager {

    #region Save
    public void Save(string _fileName, ExerciseModel exerciseModel) {
        string fileName = Path.Combine(Application.dataPath + "/Recordings", _fileName);
        try {
            if (File.Exists(fileName + ".json")) {
                int i = 1;
                while (File.Exists(fileName + i + ".json")) {
                    i++;
                }
                fileName = fileName + i;
            }

            using (FileStream stream = new FileStream(fileName + ".json", FileMode.CreateNew)) {
                using (StreamWriter writer = new StreamWriter(stream)) {
                    fsSerializer _serializer = new fsSerializer();
                    fsData data;
                    _serializer.TrySerialize(typeof(ExerciseModel), exerciseModel, out data).AssertSuccessWithoutWarnings();
                    writer.Write(data.ToString());
                    Debug.Log("Saved! : " + fileName);
                    writer.Flush();
                }
            }
        }
        catch (IOException e) {
            Debug.Log(e.Message);
        }
    } 
    #endregion

    #region Load
    public ExerciseModel Load(string _fileName) {
        string fileName = Path.Combine(Application.dataPath + "/Recordings", _fileName);
        ExerciseModel exerciseModel;
        Debug.Log("Loading file : " + fileName);
        using (FileStream stream = new FileStream(fileName + ".json", FileMode.Open)) {
            using (StreamReader reader = new StreamReader(stream)) {
                fsSerializer _serializer = new fsSerializer();

                // step 1: parse the JSON data
                fsData data = fsJsonParser.Parse(reader.ReadToEnd());

                // step 2: deserialize the data
                object deserialized = null;
                _serializer.TryDeserialize(data, typeof(ExerciseModel), ref deserialized).AssertSuccessWithoutWarnings();

                exerciseModel =  (ExerciseModel)deserialized;
            }
        }

        //ServiceExercise.instance.selected = exerciseModel;
        return exerciseModel;

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
