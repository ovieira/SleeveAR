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
    }

    public void updateViewFloorArc() {
        currentLineRenderer.SetVertexCount(0);
        fullMovementLineRenderer.SetVertexCount(upperArmDirectionsList.Count);
        //path.nodes = new List<Vector3>(upperArmDirectionsList.Count);
        path.Clear();
        for (int i = 0; i < upperArmDirectionsList.Count; i++)
        {
            var pos = upperArmDirectionsList[i]*distance;
            fullMovementLineRenderer.SetPosition(i, pos);
            path.Add(pos+Vector3.up);
        }

        //this.guideline.transform.localPosition = path[0];

        for (int i = 0; i < guideline.Length; i++)
        {
            foreach (var o in guideline)
            {
                iTween.Stop(o);
            }
            var ob = guideline[i];
            ob.transform.localPosition = path[0];
            Hashtable hashMoveTo = Utils.HashMoveTo(this.name + i, path.ToArray(), true, 15f, i/10f, iTween.EaseType.easeInSine, true);
            hashMoveTo.Add("looptype", iTween.LoopType.loop);
            hashMoveTo.Add("movetopath", false);
            iTween.MoveTo(ob, hashMoveTo);
        }
    }

    // Update is called once per frame
    void Update() {
        fullMovementLineRenderer.transform.position = currentLineRenderer.transform.position = basePosition;

        currentLineRenderer.transform.Translate(Vector3.up);

        currentLineRenderer.SetVertexCount(this.progress);
        for (int i = 0; i < this.progress; i++) {
            currentLineRenderer.SetPosition(i, upperArmDirectionsList[i] * distance);
        }

        //heighguideline
        var currentupperarmdir = currentJointsGroup.getUpperArmDirection();
        var goalupperarmdir = upperArmDirectionsList[progress];

        float heightDiff = currentupperarmdir.y - goalupperarmdir.y;
        this.heightGuideLine.transform.position = currentLineRenderer.transform.position + upperArmDirectionsList[progress] * (distance + heightDiff);


    } 
    #endregion

    #region Full Movement Line Renderer
    public LineRenderer fullMovementLineRenderer;

    #endregion

    #region Current Movement Line Renderer

    public LineRenderer currentLineRenderer;

    protected int _progress;

    public int progress {
        get { return this._progress; }
        set {
            //int _value = (int)Utils.Map(value, 0, 100, 0, upperArmDirectionsList.Count);
            Debug.Log("Progress: " + value);
            _progress = value;
            _progress = Mathf.Clamp(_progress, 0, upperArmDirectionsList.Count-1);
            //updateCurrentLineRenderer();
        }
    }

    private void updateCurrentLineRenderer() {

        currentLineRenderer.SetVertexCount(this.progress);
        for (int i = 0; i < this.progress; i++) {
            currentLineRenderer.SetPosition(i, upperArmDirectionsList[i] * distance);
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

    public GameObject heightGuideLine;

    #endregion

    #region Current Joints Group

    public JointsGroup currentJointsGroup;

    #endregion
}
