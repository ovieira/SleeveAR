using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewFloorArc : MonoBehaviour {

    public List<Vector3> upperArmDirectionsList = new List<Vector3>();

    #region LifeCycle
    // Use this for initialization
    void Start() {
        fullMovementLineRenderer.useWorldSpace = currentLineRenderer.useWorldSpace = false;
        // updateViewFloorArc();
        StartCoroutine("updateHistory");
    }

    public void updateViewFloorArc() {
        currentLineRenderer.SetVertexCount(0);
        fullMovementLineRenderer.SetVertexCount(upperArmDirectionsList.Count);
        //path.nodes = new List<Vector3>(upperArmDirectionsList.Count);
        path.Clear();
        for (int i = 0; i < upperArmDirectionsList.Count; i++) {
            var pos = upperArmDirectionsList[i] * distance ;
            fullMovementLineRenderer.SetPosition(i, pos);
            path.Add(pos + Vector3.up);
        }

        //this.guideline.transform.localPosition = path[0];

        //startGuidelineAnimation();
    }

    //private void startGuidelineAnimation() {
    //    foreach (var o in guideline) {
    //        iTween.Stop(o);
    //    }
    //    for (int i = 0; i < guideline.Length; i++) {
    //        var ob = guideline[i];
    //        ob.transform.localPosition = path[0];
    //        Hashtable hashMoveTo = Utils.HashMoveTo(this.name + i, path.ToArray(), true, 15f, i / 10f,
    //            iTween.EaseType.easeInSine, true);
    //        hashMoveTo.Add("looptype", iTween.LoopType.loop);
    //        hashMoveTo.Add("movetopath", false);
    //        iTween.MoveTo(ob, hashMoveTo);
    //    }
    //}

    // Update is called once per frame
    void Update() {
        fullMovementLineRenderer.transform.position = currentLineRenderer.transform.position = basePosition;

        currentLineRenderer.transform.Translate(Vector3.up);

        currentLineRenderer.SetVertexCount(this.progress);
        for (int i = 0; i < this.progress; i++) {
            currentLineRenderer.SetPosition(i, path[i]);
        }

        
        var currentupperarmdir = currentJointsGroup.getUpperArmDirection();
        var goalupperarmdir = upperArmDirectionsList[progress];
        float directiontDiff = (currentupperarmdir.x - goalupperarmdir.x)*2;
        float heightDiff = (currentupperarmdir.y - goalupperarmdir.y)*2;
        var circlenextPos = currentLineRenderer.transform.position + Vector3.up + path[progress];

        /** /
        var dottednextPos = currentLineRenderer.transform.position + Vector3.up + upperArmDirectionsList[progress] * (distance+heightDiff) + directiontDiff*Vector3.Cross(upperArmDirectionsList[progress],Vector3.down);
        /** /
        var dottednextPos = currentLineRenderer.transform.position + Vector3.up + currentupperarmdir * (distance + heightDiff) + directiontDiff * Vector3.Cross(currentupperarmdir, Vector3.down);
        /**/
        var dottednextPos = currentLineRenderer.transform.position + Vector3.up + currentupperarmdir * (distance + heightDiff);
        /**/
        this.circleGuideLine.transform.position = Vector3.Lerp(this.circleGuideLine.transform.position, circlenextPos, Time.deltaTime * 2);
        this.dottedCircleGuideLine.transform.position = Vector3.Lerp(this.dottedCircleGuideLine.transform.position,dottednextPos, Time.deltaTime*5);
        historyList.Add(dottednextPos);
        
    }

   
    #endregion

    #region Full Movement Line Renderer
    public LineRenderer fullMovementLineRenderer;

    #endregion

    #region Current Movement Line Renderer

    public LineRenderer currentLineRenderer;
    public LineRenderer history;
    public List<Vector3> historyList = new List<Vector3>();

    IEnumerator updateHistory() {
        while (true)
        {
            history.SetVertexCount(historyList.Count);
            for (int i = 0; i < historyList.Count; i++) {
                history.SetPosition(i, historyList[i]);
            }
            yield return null;
        }
    }

    protected int _progress;

    public int progress {
        get { return this._progress; }
        set {
            //int _value = (int)Utils.Map(value, 0, 100, 0, upperArmDirectionsList.Count);
            //Debug.Log("Progress: " + value);
            _progress = value;
            _progress = Mathf.Clamp(_progress, 0, upperArmDirectionsList.Count - 1);
            //updateCurrentLineRenderer();
        }
    }

    private void updateCurrentLineRenderer() {

        currentLineRenderer.SetVertexCount(this.progress);
        for (int i = 0; i < this.progress; i++) {
            currentLineRenderer.SetPosition(i, path[progress] );
        }
    }

    #endregion

    #region Tracking
    public Vector3 basePosition;

    public float distance;
    #endregion

    #region Path

    public List<Vector3> path = new List<Vector3>();

    #endregion

    #region GuideLines

    public GameObject[] guideline;

    #endregion

    #region Current Height

    public GameObject circleGuideLine;
    public GameObject dottedCircleGuideLine;

    #endregion

    #region Current Joints Group

    public JointsGroup currentJointsGroup;

    #endregion
}
