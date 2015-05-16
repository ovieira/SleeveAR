using UnityEngine;
using System.Collections;

public class LineProjection : MonoBehaviour {

    #region Prefabs
    public GameObject LineRendererPrefab; 
    #endregion

    #region LineRenderers

    protected LineRenderer lineRenderer, lineRendererOffset; 

    #endregion

    #region Lifecycle
    // Use this for initialization
    public virtual void Start() {

        /** /
       var go = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
       go.transform.parent = this.transform;
       lineRenderer = go.GetComponent<LineRenderer>();

       var go2 = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
       go2.transform.parent = this.transform;
       lineRendererWithOffset = go2.GetComponent<LineRenderer>();

       lineRendererWithOffset.material = red;
       /**/
        var go = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = this.transform;
        lineRenderer = go.GetComponent<LineRenderer>();
        lineRenderer.SetColors(Color.white, Color.white);

        var go2 = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
        go2.transform.parent = this.transform;
        lineRendererOffset = go2.GetComponent<LineRenderer>();
        lineRendererOffset.SetColors(Color.red, Color.red);
        /**/

    }

    // Update is called once per frame
    public virtual void Update() {

    }
    #endregion

    #region Update Position
    /// <summary>
    /// Updates LineRenderer Vertex position
    /// </summary>
    /// <param name="lr"></param>
    protected void UpdateLineRendererPosition(LineRenderer lr) {
        for (var i = 0; i < ManagerTracking.instance.count; i++) {
            lr.SetPosition(i, ManagerTracking.instance.PositionProjectedWithOffset[i]);
        }
    } 
    #endregion




    #region Update Color
    /// <summary>
    /// Changes full line renderer color
    /// </summary>
    /// <param name="lr"></param>
    /// <param name="c"></param>
    protected void UpdateLineRendererColor(LineRenderer lr, Color c) {
        lr.SetColors(c,c);
    }

    protected void UpdateLineRendererColor(LineRenderer lr, Color c1, Color c2)
    {
        lr.SetColors(c1, c2);
    }
    
    #endregion

}
