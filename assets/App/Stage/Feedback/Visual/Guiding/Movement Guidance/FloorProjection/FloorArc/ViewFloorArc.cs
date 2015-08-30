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

        for (int i = 0; i < upperArmDirectionsList.Count; i++)
        {
            var pos = upperArmDirectionsList[i]*distance;
            fullMovementLineRenderer.SetPosition(i, pos);
            path.Add(pos+Vector3.up);
        }

        //this.guideline.transform.localPosition = path[0];

        for (int i = 0; i < guideline.Length; i++)
        {
            var ob = guideline[i];
            ob.transform.localPosition = path[0];
            Hashtable hashMoveTo = Utils.HashMoveTo(this.name + i, path.ToArray(), true, 15f, i/10f, iTween.EaseType.easeInSine, true);
            hashMoveTo.Add("looptype", iTween.LoopType.loop);
            hashMoveTo.Add("movetopath", false);
            iTween.MoveTo(ob, hashMoveTo);
        }

        

        //Hashtable hashValueTo = Utils.HashValueTo(this.name, 0, path.Count-2, 5f, 0, iTween.EaseType.linear, "onupdate",
        //    "oncomplete");
        //iTween.ValueTo(this.gameObject, hashValueTo);
    }

    //protected void onupdate(int progress)
    //{
    //    //this.guideline.transform.localPosition = path[progress];
    //    //this.guideline.transform.LookAt(path[progress+20]);
    //}

    protected void onstarttarget(GameObject ob)
    {
        ob.transform.localPosition = path[0];
    }

    protected void oncomplete(GameObject ob) {

        ob.transform.localPosition = path[0];
    }

    // Update is called once per frame
    void Update() {
        fullMovementLineRenderer.transform.position = currentLineRenderer.transform.position = basePosition;
        currentLineRenderer.transform.Translate(Vector3.up);
        currentLineRenderer.SetVertexCount(this.progress);
        for (int i = 0; i < this.progress; i++) {
            currentLineRenderer.SetPosition(i, upperArmDirectionsList[i] * distance);
        }
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
}
