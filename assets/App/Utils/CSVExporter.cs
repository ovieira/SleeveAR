using System;
using UnityEngine;
using System.Collections.Generic;

public class CSVExporter : MonoBehaviour
{

    public int maxID;

    #region Filenames
    public string[] Sessionfilenames;
    public string[] VideoFilenames;
    public string[] ExerciseFilenames; 
    #endregion

    #region Objects List

    protected List<ExerciseModel> exercises = new List<ExerciseModel>();
    protected List<Session> sessions = new List<Session>();
    protected List<ExerciseModel> videos = new List<ExerciseModel>();
    

    #endregion

	#region LifeCycle
	void Start () {

        LoadExerciseModels(ExerciseFilenames,exercises);
        Debug.Log("Loaded Original Exercises " + exercises.Count);
        LoadSessions(Sessionfilenames, sessions);
        Debug.Log("Loaded Sessions " + sessions.Count);
        LoadExerciseModels(VideoFilenames, videos);
        Debug.Log("Loaded Original Exercises " + videos.Count);
	}
	
	void Update () {
	
	}
	#endregion

    #region Loading
    protected void LoadSessions(string[] paths, List<Session> list) {
        foreach (var path in paths) {
            var session = ServiceFileManager.instance.LoadSession(path);
            list.Add(session);
        }
    }

    protected void LoadExerciseModels(string[] paths, List<ExerciseModel> list) {
        foreach (var path in paths) {
            var exercisemodel = ServiceFileManager.instance.LoadExerciseModel(path);
            list.Add(exercisemodel);
        }
    }

    private static void WriteToFile(string path, Func<string> _action) {
        string newpath = Application.dataPath +
               path;
        string s = _action();

        System.IO.File.WriteAllText(newpath, s);

    } 
    #endregion


    #region Visualize

    public LineRenderer original;
    public LineRenderer session;
    public LineRenderer video;

    [ContextMenu("draworiginal")]
    public void drawOriginal()
    {
        var em = exercises[0];
        original.SetVertexCount(em.exerciseModel.Count);
        for (int i = 0; i < em.exerciseModel.Count; i++)
        {
            var jg = em.exerciseModel[i];
            var upperDir = jg.getUpperArmDirection();

            original.SetPosition(i, upperDir*5);
        }
    }

    [ContextMenu("drawsession")]
    public void drawSession() {
        var sess = sessions[0].logs[2].entries;
        session.SetVertexCount(sess.Count);
        for (int i = 0; i < sess.Count; i++) {
            var jg = sess[i].jointsGroup;
            var upperDir = jg.getUpperArmDirection();

            session.SetPosition(i, upperDir * 5);
        }
    }

    [ContextMenu("drawvideo")]
    public void drawVideo() {
        var em = videos[0];
        video.SetVertexCount(em.exerciseModel.Count);
        for (int i = 0; i < em.exerciseModel.Count; i++) {
            var jg = em.exerciseModel[i];
            var upperDir = jg.getUpperArmDirection();

            video.SetPosition(i, upperDir * 5);
        }
    }
    #endregion

    [ContextMenu("sumdistOriginalSession")]
    public void sumdistorisess()
    {
        var sessionlst = sessions[0].logs[2].LogToJointsGroupsList();
        comparePaths(exercises[0].exerciseModel,sessionlst);
    }

    [ContextMenu("sumdistOriginalVideo")]
    public void sumdistorivideo() {
        comparePaths(exercises[0].exerciseModel, videos[0].exerciseModel);
    }

    #region Sum of Minimum Distances
    public float comparePaths(List<JointsGroup> original, List<JointsGroup> copy) {
        float distSum = 0f;
        for (int i = 0; i < copy.Count; i++) {
            var copyUpperDir = copy[i].getUpperArmDirection();
            float dist = float.MaxValue;
            for (int j = 0; j < original.Count; j++) {
                var originalUpperDir = original[j].getUpperArmDirection();
                var d = Vector3.Distance(copyUpperDir, originalUpperDir);
                if (d < dist) dist = d;
            }
            distSum += dist;
        }
        Debug.Log("Sum of minimum distances: " + distSum);
        return distSum;
    } 
    #endregion
}
