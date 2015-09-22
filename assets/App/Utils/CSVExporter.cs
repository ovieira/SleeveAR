using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class CSVExporter : MonoBehaviour
{

    public int maxID;

    #region Exercise names
    public string[] ExerciseFilenames; 
    #endregion

    #region Lists

    protected List<ExerciseModel> exercises = new List<ExerciseModel>();
    protected Dictionary<string, ExerciseModel> videos = new Dictionary<string, ExerciseModel>();
    protected Dictionary<string, Session> sessions = new Dictionary<string, Session>();

    #endregion

	#region LifeCycle
	void Start () {
        LoadExerciseModels(ExerciseFilenames, exercises);
        Debug.Log("Loaded Original Exercises " + exercises.Count);
        LoadSessions();
        Debug.Log("Loaded Sessions " + sessions.Count);
	    LoadVideos();
        Debug.Log("Loaded Videos " + videos.Count);
	}
	#endregion

    #region Loading
    private void LoadVideos() {
        for (int i = 1; i <= maxID; i++) {
            for (int j = 1; j <= 5; j++) {
                var name = "video" + i + "_" + j;
                var _video = ServiceFileManager.instance.LoadExerciseModel(name, false);
                videos.Add(name, _video);
            }
        }
    }

    private void LoadSessions() {
        for (int i = 1; i <= maxID; i++) {
            for (int j = 1; j <= 5; j++) {
                var name = i + "_" + j;
                var _session = ServiceFileManager.instance.LoadSession(name, false);
                sessions.Add(name, _session);
            }
        }
    }

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

    #region UpperArm Sum of Minimum Distances
    public float UpperArmSOMD(List<JointsGroup> original, List<JointsGroup> copy) {
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

    public float UpperArmSOMD(List<JointsGroup> original, Log copy) {
        float distSum = 0f;
        for (int i = 0; i < copy.entries.Count; i++) {
            var copyUpperDir = copy.entries[i].jointsGroup.getUpperArmDirection();
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

    #region ForeArm Sum of Minimum Distances
    public float ForeArmSOMD(List<JointsGroup> original, List<JointsGroup> copy) {
        float distSum = 0f;
        for (int i = 0; i < copy.Count; i++) {
            var copyforearmdir = copy[i].getForeArmDirection();
            float dist = float.MaxValue;
            for (int j = 0; j < original.Count; j++) {
                var originalUpperDir = original[j].getForeArmDirection();
                var d = Vector3.Distance(copyforearmdir, originalUpperDir);
                if (d < dist) dist = d;
            }
            distSum += dist;
        }
        return distSum;
    }

    public float ForeArmSOMD(List<JointsGroup> original, Log copy) {
        float distSum = 0f;
        for (int i = 0; i < copy.entries.Count; i++) {
            var copyForeDir = copy.entries[i].jointsGroup.getForeArmDirection();
            float dist = float.MaxValue;
            for (int j = 0; j < original.Count; j++) {
                var originalForeDir = original[j].getForeArmDirection();
                var d = Vector3.Distance(copyForeDir, originalForeDir);
                if (d < dist) dist = d;
            }
            distSum += dist;
        }
        return distSum;
    }
    #endregion

    #region Session Table
    [ContextMenu("Session Table (all tries with sleeveAR")]
    public void generateSumofMinDistSession() {
        string csvString ="upperArm\n\n"+
            "id;ex1_try1;ex1_try2;ex1_try3;ex2_try1;ex2_try2;ex2_try3;ex3_try1;ex3_try2;ex3_try3;ex4_try1;ex4_try2;ex4_try3;ex5_try1;ex5_try2;ex5_try3\n";

        for (int i = 1; i <= maxID; i++) {
            print("Writing session " + i);
            string entry = UpperArmLogEntrySOMDSession(i);
            csvString += (entry + "\n");
        }

        csvString += ("foreArm\n\n" +
                      "id;ex1_try1;ex1_try2;ex1_try3;ex2_try1;ex2_try2;ex2_try3;ex3_try1;ex3_try2;ex3_try3;ex4_try1;ex4_try2;ex4_try3;ex5_try1;ex5_try2;ex5_try3\n");

        for (int i = 1; i <= maxID; i++) {
            print("Writing session " + i);
            string entry = ForeArmLogEntrySOMDSession(i);
            csvString += (entry + "\n");
        }

        ServiceFileManager.instance.WriteToFile("Sessions.csv", csvString);
    }

    private string UpperArmLogEntrySOMDSession(int i) {
        string s = "";
        List<Session> listsessions = new List<Session>();
        getSessionsFromID(listsessions, i);
        s += (i + ";");

        foreach (var listsession in listsessions) {
            int exID = Int32.Parse(listsession.exerciseID);
            foreach (var log in listsession.logs) {
                var result = UpperArmSOMD(exercises[exID - 1].exerciseModel, log.LogToJointsGroupsList());
                s += (result + ";");
            }
        }

        return s;
    }

    private string ForeArmLogEntrySOMDSession(int i) {
        string s = "";
        List<Session> listsessions = new List<Session>();
        getSessionsFromID(listsessions, i);
        s += (i + ";");

        foreach (var listsession in listsessions) {
            int exID = Int32.Parse(listsession.exerciseID);
            foreach (var log in listsession.logs) {
                var result = ForeArmSOMD(exercises[exID - 1].exerciseModel, log.LogToJointsGroupsList());
                s += (result + ";");
            }
        }

        return s;
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
            float f = UpperAVGSOMD(listsession, exercises[exID - 1]);
            //foreach (var log in listsession.logs) {
            //    var result = UpperArmSOMD(exercises[exID - 1].exerciseModel, log.LogToJointsGroupsList());
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
            var sum = UpperArmSOMD(exercises[j].exerciseModel, videoslist[j].exerciseModel);
            s += (sum + ";");
        }

        return s;
    }

   
    #endregion

    #region Individual SOMD(SUM OF MINIMUM DISTANCES) Table
    //ID:avg1:video1:avg2:video2:avg3:video3:avg4:video4:avg5:video5

    

    [ContextMenu("upper avg and video")]
    public string upperavgandvideo()
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

                var avgSession = UpperAVGSOMD(_session, exercises[i]);
                var videosodm = UpperArmSOMD(exercises[i].exerciseModel, _video.exerciseModel);

                s += (avgSession + ";" + videosodm + ";");
            }
            s += "\n";

        }

        s += "\n\n\n\n\n\n\nid;avg1;video1;avg2;video2;avg3;video3;avg4;video4;avg5;video5\n";

        for (int id = 1; id <= maxID; id++) {
            List<Session> _sessionList = new List<Session>();
            List<ExerciseModel> _videos = new List<ExerciseModel>();

            getSessionsFromID(_sessionList, id);
            getVideosFromID(_videos, id);

            s += (id + ";");
            for (int i = 0; i < _sessionList.Count; i++) {
                var _session = _sessionList[i];
                var _video = _videos[i];

                var avgSession = Upper_DTW_AVG_SOMD(_session, exercises[i]);
                var videosodm = Upper_DTW_SOMD(exercises[i].exerciseModel, _video.exerciseModel);

                s += (avgSession + ";" + videosodm + ";");
            }
            s += "\n";

        }

        ServiceFileManager.instance.WriteToFile("UpperArmAVGSession_VS_Video.csv", s);
        return s;
    }

    [ContextMenu("fore avg and video")]
    public string foreavgandvideo() {
        string s = "id;avg1;video1;avg2;video2;avg3;video3;avg4;video4;avg5;video5\n";

        for (int id = 1; id <= maxID; id++) {
            List<Session> _sessionList = new List<Session>();
            List<ExerciseModel> _videos = new List<ExerciseModel>();

            getSessionsFromID(_sessionList, id);
            getVideosFromID(_videos, id);

            s += (id + ";");
            for (int i = 0; i < _sessionList.Count; i++) {
                var _session = _sessionList[i];
                var _video = _videos[i];

                var avgSession = ForeAVGSOMD(_session, exercises[i]);
                var videosodm = ForeArmSOMD(exercises[i].exerciseModel, _videos[i].exerciseModel);

                s += (avgSession + ";" + videosodm + ";");
            }
            s += "\n";
        }
        ServiceFileManager.instance.WriteToFile("ForeArmAVGSession_VS_Video.csv", s);
        return s;
    }

    private float UpperAVGSOMD(Session _session, ExerciseModel exerciseModel) {
        float f = 0;
        foreach (var log in _session.logs) {
            var result = UpperArmSOMD(exerciseModel.exerciseModel, log.LogToJointsGroupsList());
            f += result;
            //s += (result + ";");
        }
        f = f / 3f;
        return f;
    }



    private float ForeAVGSOMD(Session _session, ExerciseModel exerciseModel) {
        float f = 0;
        foreach (var log in _session.logs) {
            var result = ForeArmSOMD(exerciseModel.exerciseModel, log.LogToJointsGroupsList());
            f += result;
            //s += (result + ";");
        }
        f = f / 3f;
        return f;
    }

    private float Upper_DTW_AVG_SOMD(Session _session, ExerciseModel exerciseModel) {
        float f = 0;
        foreach (var log in _session.logs) {
            var result = Upper_DTW_SOMD(exerciseModel.exerciseModel, log.LogToJointsGroupsList());
            f += result;
            //s += (result + ";");
        }
        f = f / 3f;
        return f;
    }

    #endregion

    
    #region Individual
    public void IndividualTable(int id) {
        string s = "id;avg1;video1;avg2;video2;avg3;video3;avg4;video4;avg5;video5\n";

        List<Session> _sessionList = new List<Session>();
        List<ExerciseModel> _videos = new List<ExerciseModel>();

        getSessionsFromID(_sessionList, id);
        getVideosFromID(_videos, id);

        s += (id + ";");
        for (int i = 0; i < _sessionList.Count; i++) {
            var _session = _sessionList[i];
            var _video = _videos[i];

            var avgSession = UpperAVGSOMD(_session, exercises[i]);
            var videosodm = UpperArmSOMD(exercises[i].exerciseModel, _videos[i].exerciseModel);

            s += (avgSession + ";" + videosodm + ";");
        }

        ServiceFileManager.instance.WriteToFile("user" + id + ".csv", s);
    }

    [ContextMenu("individual")]
    public void individual() {
        for (int i = 1; i <= maxID; i++) {
            IndividualTable(i);
        }
    } 
    #endregion

    private void getSessionsFromID(List<Session> listsessions, int i) {

        for (int j = 1; j <= 5; j++) {
            //var s = ServiceFileManager.instance.LoadSession("" + i + "_" + j);
            var s = sessions[i + "_" + j];
            listsessions.Add(s);
        }
    }

    private void getVideosFromID(List<ExerciseModel> videoslist, int i) {
        for (int j = 1; j <= 5; j++) {
            var v = videos["video" + i + "_" + j];
            videoslist.Add(v);
        }
    }

    protected float Upper_DTW_SOMD(List<JointsGroup> original, List<JointsGroup> copy )
    {
        float result = 0f;
        int copyMaxPosition = copy.Count-1;
        int originalMaxPosition = original.Count - 1;
        for (int i = 0; i < original.Count; i++)
        {
            var copyPosition = (int) Utils.Map(i, 0, originalMaxPosition, 0, copyMaxPosition);

            var jgOriginal = original[i];
            var jgCopy = copy[i];

            var dist = Vector3.Distance(jgOriginal.getUpperArmDirection(), jgCopy.getUpperArmDirection());
            result += dist;
        }

        return result;
    }


    [ContextMenu("dtw Sleeve")]
    public void DTWTestSleeve()
    {
        JointsGroup[] vec1;
        JointsGroup[] svec;
        JointsGroup[] vvec;
        //
        var ex = exercises[0].exerciseModel;
        var sleeve = sessions["2_1"];
        var video = videos["video2_1"];
        //
        vec1 = ex.ToArray();
        svec = sleeve.logs[2].LogToJointsGroupsList().ToArray();
        vvec = video.exerciseModel.ToArray();
        //

        int start = System.DateTime.Now.Millisecond;
        Debug.Log("Start at " + start);
        DTW dtw = new DTW(new mIndex[] { new mIndex(1, 1), new mIndex(1, 2), new mIndex(2, 1) });
        List<mIndex> wres = dtw.warpSubsequenceDTW(vec1, svec,false);
        Debug.Log("-- Time " + (System.DateTime.Now.Millisecond - start));

        //mDebug.printMatrix(dtw.CostMatrix);
        //mDebug.printMatrix(dtw.AccumCostMatrix);

        //foreach (mIndex step in wres) {
        //    print(step + " " + dtw.AccumCostMatrix[step.y][step.x]);
        //}

        Debug.Log("Warp Path Cost" + dtw.getWarpPathCost());

    }

    [ContextMenu("dtw Video")]
    public void DTWTestVideo() {
        JointsGroup[] jg1;
        JointsGroup[] jg2;
        //
        var ex = exercises[0].exerciseModel;
        var sleeve = sessions["2_1"];
        var video = videos["video2_1"];
        //
        jg1 = ex.ToArray();
        //svec = sleeve.logs[2].LogToJointsGroupsList().ToArray();
        jg2 = video.exerciseModel.ToArray();
        //

        int start = System.DateTime.Now.Millisecond;
        Debug.Log("Start at " + start);
        DTW dtw = new DTW(new mIndex[] { new mIndex(1, 1), new mIndex(1, 2), new mIndex(2, 1) });
        List<mIndex> wres = dtw.warpSubsequenceDTW(jg1, jg2,true);
        Debug.Log("-- Time " + (System.DateTime.Now.Millisecond - start));

        //mDebug.printMatrix(dtw.CostMatrix);
        //mDebug.printMatrix(dtw.AccumCostMatrix);

        //foreach (mIndex step in wres) {
        //    print(step + " " + dtw.AccumCostMatrix[step.y][step.x]);
        //}

        Debug.Log("Warp Path Cost" + dtw.getWarpPathCost());

    }

    [ContextMenu("dtw ex1")]
    public void dtwex1()
    {
        DTWExercise(0);
    }

    [ContextMenu("DTW ALL EXERCISES")]
    public void dtwAllExercises() {
        for (int i = 0; i < 5; i++)
        {
            DTWExercise(i);
        }
    }

    public void DTWExercise(int i)
    {
        var exercise = exercises[i];

        string s = "id;upperTry1;upperTry2;upperTry3;upperVideo;foreTry1;foreTry2;foreTry3;foreVideo\n";

        for (int j = 1; j <= maxID; j++)
        {
            //get sessions from id j
            List<Session> _sessions = new List<Session>();
            getSessionsFromID(_sessions, j);

            //get videos from id j
            List<ExerciseModel> _videos = new List<ExerciseModel>();
            getVideosFromID(_videos, j);

            //get session from exercise i 
            var _session = _sessions[i];

            //get video from exercise i 
            var _video = _videos[i];

            s += (j + ";");

            foreach (var log in _session.logs)
            {
                var uppersleeveardtwcost = DTWCost(exercise.exerciseModel.ToArray(), log.LogToJointsGroupsList().ToArray(), true);
                s += (uppersleeveardtwcost + ";");
            }

            var uppervideodtwcost = DTWCost(exercise.exerciseModel.ToArray(), _video.exerciseModel.ToArray(), true);
            s += (uppervideodtwcost + ";");

            foreach (var log in _session.logs) {
                var foresleeveardtwcost = DTWCost(exercise.exerciseModel.ToArray(), log.LogToJointsGroupsList().ToArray(), false);
                s += (foresleeveardtwcost + ";");
            }

            var forevideodtwcost = DTWCost(exercise.exerciseModel.ToArray(), _video.exerciseModel.ToArray(), false);
            s += (forevideodtwcost + ";");

            s += ("\n");
        }

        ServiceFileManager.instance.WriteToFile("DTW_Exercise"+(i+1)+".csv",s);

    }

    private float DTWCost(JointsGroup[] jg1, JointsGroup[] jg2, bool upper) {
        //int start = System.DateTime.Now.Millisecond;
        //Debug.Log("Start at " + start);
        DTW dtw = new DTW(new mIndex[] { new mIndex(1, 1), new mIndex(1, 2), new mIndex(2, 1) });
        List<mIndex> wres = dtw.warpSubsequenceDTW(jg1, jg2, upper);
        //Debug.Log("-- Time " + (System.DateTime.Now.Millisecond - start));

        //mDebug.printMatrix(dtw.CostMatrix);
        //mDebug.printMatrix(dtw.AccumCostMatrix);

        //foreach (mIndex step in wres) {
        //    print(step + " " + dtw.AccumCostMatrix[step.y][step.x]);
        //}

        Debug.Log("Warp Path Cost" + dtw.getWarpPathCost());
        return dtw.getWarpPathCost();
    }
}
