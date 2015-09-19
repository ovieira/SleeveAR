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

        LoadExerciseModels(ExerciseFilenames, exercises);
        Debug.Log("Loaded Original Exercises " + exercises.Count);
        /* LoadSessions(Sessionfilenames, sessions);
         Debug.Log("Loaded Sessions " + sessions.Count);
         LoadExerciseModels(VideoFilenames, videos);
         Debug.Log("Loaded Original Exercises " + videos.Count);*/
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
        SumOfMinimumDistances(exercises[0].exerciseModel,sessionlst);
    }

    [ContextMenu("sumdistOriginalVideo")]
    public void sumdistorivideo() {
        SumOfMinimumDistances(exercises[0].exerciseModel, videos[0].exerciseModel);
    }

    #region Sum of Minimum Distances
    public float SumOfMinimumDistances(List<JointsGroup> original, List<JointsGroup> copy) {
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
       // Debug.Log("Sum of minimum distances: " + distSum);
        return distSum;
    } 
    #endregion


    [ContextMenu("test")]
    public void generateSumofMinDistSession()
    {
        string csvString =
            "id;ex1_try1;ex1_try2;ex1_try3;ex2_try1;ex2_try2;ex2_try3;ex3_try1;ex3_try2;ex3_try3;ex4_try1;ex4_try2;ex4_try3;ex5_try1;ex5_try2;ex5_try3\n";

        for (int i = 1; i <= maxID; i++)
        {
            Debug.Log("Writing session " + i);
            string entry = sumofmindistentry(i);
            csvString += (entry + "\n");

        }

        ServiceFileManager.instance.WriteToFile("test.csv",csvString);
    }

    private string sumofmindistentry(int i)
    {
        string s = "";
        List<Session> listsessions = new List<Session>();
        getSessionsFromID(listsessions, i);
        s += (i+";");

        foreach (var listsession in listsessions)
        {
            int exID = Int32.Parse(listsession.exerciseID);
            foreach (var log in listsession.logs)
            {
                var result = SumOfMinimumDistances(exercises[exID - 1].exerciseModel, log.LogToJointsGroupsList());
                s += (result+";");
            }
        }

        return s;
    }

    private void getSessionsFromID(List<Session> listsessions, int i) {

        for (int j = 1; j <= 5; j++)
        {
            var s = ServiceFileManager.instance.LoadSession("" + i + "_" + j);
            listsessions.Add(s);
        }

    }

    

    
}
