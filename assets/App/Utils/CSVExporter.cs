using System;
using UnityEngine;
using System.Collections.Generic;

public class CSVExporter : MonoBehaviour
{

    public int maxID;

    #region Filenames
    //public string[] Sessionfilenames;
    //public string[] VideoFilenames;
    public string[] ExerciseFilenames; 
    #endregion

    #region Objects List

    protected List<ExerciseModel> exercises = new List<ExerciseModel>();
    //protected List<Session> sessions = new List<Session>();
    //protected List<ExerciseModel> videos = new List<ExerciseModel>();
    

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


    //#region Visualize

    //public LineRenderer original;
    //public LineRenderer session;
    //public LineRenderer video;

    //[ContextMenu("draworiginal")]
    //public void drawOriginal()
    //{
    //    var em = exercises[0];
    //    original.SetVertexCount(em.exerciseModel.Count);
    //    for (int i = 0; i < em.exerciseModel.Count; i++)
    //    {
    //        var jg = em.exerciseModel[i];
    //        var upperDir = jg.getUpperArmDirection();

    //        original.SetPosition(i, upperDir*5);
    //    }
    //}

    //[ContextMenu("drawsession")]
    //public void drawSession() {
    //    var sess = sessions[0].logs[2].entries;
    //    session.SetVertexCount(sess.Count);
    //    for (int i = 0; i < sess.Count; i++) {
    //        var jg = sess[i].jointsGroup;
    //        var upperDir = jg.getUpperArmDirection();

    //        session.SetPosition(i, upperDir * 5);
    //    }
    //}

    //[ContextMenu("drawvideo")]
    //public void drawVideo() {
    //    var em = videos[0];
    //    video.SetVertexCount(em.exerciseModel.Count);
    //    for (int i = 0; i < em.exerciseModel.Count; i++) {
    //        var jg = em.exerciseModel[i];
    //        var upperDir = jg.getUpperArmDirection();

    //        video.SetPosition(i, upperDir * 5);
    //    }
    //}
    //#endregion

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


    #region Session Table
    [ContextMenu("Session Table")]
    public void generateSumofMinDistSession() {
        string csvString =
            "id;ex1_try1;ex1_try2;ex1_try3;ex2_try1;ex2_try2;ex2_try3;ex3_try1;ex3_try2;ex3_try3;ex4_try1;ex4_try2;ex4_try3;ex5_try1;ex5_try2;ex5_try3\n";

        for (int i = 1; i <= maxID; i++) {
            print("Writing session " + i);
            string entry = LogEntrySOMDSession(i);
            csvString += (entry + "\n");

        }

        ServiceFileManager.instance.WriteToFile("Sessions.csv", csvString);
    }

    private string LogEntrySOMDSession(int i) {
        string s = "";
        List<Session> listsessions = new List<Session>();
        getSessionsFromID(listsessions, i);
        s += (i + ";");

        foreach (var listsession in listsessions) {
            int exID = Int32.Parse(listsession.exerciseID);
            foreach (var log in listsession.logs) {
                var result = SumOfMinimumDistances(exercises[exID - 1].exerciseModel, log.LogToJointsGroupsList());
                s += (result + ";");
            }
        }

        return s;
    }

    private void getSessionsFromID(List<Session> listsessions, int i) {

        for (int j = 1; j <= 5; j++) {
            var s = ServiceFileManager.instance.LoadSession("" + i + "_" + j);
            listsessions.Add(s);
        }

    } 
    #endregion

    #region AVG Session Table

    [ContextMenu("Average Session Table")]
    public void AVGgenerateSumofMinDistSession() {
        string csvString =
            "id;ex1;ex2;ex3;ex4;ex5\n";
        for (int i = 1; i <= maxID; i++) {
            string entry = AVGLogEntrySOMDSession(i);
            csvString += (entry + "\n");

        }

        ServiceFileManager.instance.WriteToFile("AVGSessions.csv", csvString);
    }

    private string AVGLogEntrySOMDSession(int i) {
        string s = "";
        List<Session> listsessions = new List<Session>();
        getSessionsFromID(listsessions, i);
        s += (i + ";");

        foreach (var listsession in listsessions) {
            int exID = Int32.Parse(listsession.exerciseID);
            float f = avgSOMD(listsession, exercises[exID - 1]);
            //foreach (var log in listsession.logs) {
            //    var result = SumOfMinimumDistances(exercises[exID - 1].exerciseModel, log.LogToJointsGroupsList());
            //    f += result;
            //}
            //f = f/3f;
            s += (f + ";");
        }

        return s;
    }

    #endregion

    #region Video Table
    [ContextMenu("Video Table")]

    public void generateSumOfMinDistVideo() {
        string csvString = "id;ex1;ex2;ex3;ex4;ex5\n";

        for (int i = 1; i <= maxID; i++) {
            string entry = VideoSession(i);
            csvString += (entry + "\n");
        }

        ServiceFileManager.instance.WriteToFile("Videos.csv", csvString);
    }

    private string VideoSession(int i) {
        string s = "";
        List<ExerciseModel> videoslist = new List<ExerciseModel>();
        getVideosFromID(videoslist, i);

        s += (i + ";");

        for (int j = 0; j < videoslist.Count; j++) {
            var sum = SumOfMinimumDistances(exercises[j].exerciseModel, videoslist[j].exerciseModel);
            s += (sum + ";");
        }

        return s;
    }

    private void getVideosFromID(List<ExerciseModel> videoslist, int i) {
        for (int j = 1; j <= 5; j++) {
            var v = ServiceFileManager.instance.LoadExerciseModel("video" + i + "_" + j);
            videoslist.Add(v);
        }
    } 
    #endregion

    #region Individual SOMD(SUM OF MINIMUM DISTANCES) Table
    //ID:avg1:video1:avg2:video2:avg3:video3:avg4:video4:avg5:video5

    public void IndividualTable(int id)
    {
        string s = "id;avg1;video1;avg2;video2;avg3;video3;avg4;video4;avg5;video5\n";

        List<Session> _sessionList = new List<Session>();
        List<ExerciseModel> _videos = new List<ExerciseModel>();

        getSessionsFromID(_sessionList,id);
        getVideosFromID(_videos,id);

        s += (id + ";");
        for (int i = 0; i < _sessionList.Count; i++)
        {
            var _session = _sessionList[i];
            var _video = _videos[i];

            var avgSession = avgSOMD(_session, exercises[i]);
            var videosodm = SumOfMinimumDistances(exercises[i].exerciseModel, _videos[i].exerciseModel);

            s+=(avgSession+";"+videosodm+";");
        }

        ServiceFileManager.instance.WriteToFile("user"+id+".csv", s);
    }

    [ContextMenu("avg and video")]
    public void avgandvideo()
    {
        string s = "id;avg1;video1;avg2;video2;avg3;video3;avg4;video4;avg5;video5\n";

        for (int id = 1; id <= maxID; id++)
        {
            List<Session> _sessionList = new List<Session>();
            List<ExerciseModel> _videos = new List<ExerciseModel>();

            getSessionsFromID(_sessionList, id);
            getVideosFromID(_videos, id);

            s += (id + ";");
            for (int i = 0; i < _sessionList.Count; i++) {
                var _session = _sessionList[i];
                var _video = _videos[i];

                var avgSession = avgSOMD(_session, exercises[i]);
                var videosodm = SumOfMinimumDistances(exercises[i].exerciseModel, _videos[i].exerciseModel);

                s += (avgSession + ";" + videosodm + ";");
            }
            s += "\n";
        }
        ServiceFileManager.instance.WriteToFile("SessionsAndVideo.csv",s);
    }

    private float avgSOMD(Session _session, ExerciseModel exerciseModel) {
        float f = 0;
        foreach (var log in _session.logs) {
            var result = SumOfMinimumDistances(exerciseModel.exerciseModel, log.LogToJointsGroupsList());
            f += result;
            //s += (result + ";");
        }
        f = f / 3f;
        return f;
    }

    #endregion

    [ContextMenu("individual")]
    public void individual()
    {
        for (int i = 1; i <= maxID; i++)
        {
            IndividualTable(i);
        }
    }
}
