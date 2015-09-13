using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewSessionReview : MonoBehaviour {
    public List<Vector3> upperArmDirectionsList = new List<Vector3>();

    #region Tracking
    public Vector3 basePosition;

    public float distance;
    #endregion

    #region Path

    public List<Vector3> path = new List<Vector3>();

    #endregion

    //public void updateViewFloorArc() {
    //    fullMovementLineRenderer.SetVertexCount(upperArmDirectionsList.Count);
    //    //path.nodes = new List<Vector3>(upperArmDirectionsList.Count);
    //    path.Clear();
    //    for (int i = 0; i < upperArmDirectionsList.Count; i++) {
    //        var pos = upperArmDirectionsList[i] * distance;
    //        fullMovementLineRenderer.SetPosition(i, pos);
    //        path.Add(pos + Vector3.up);
    //    }

    //    //this.guideline.transform.localPosition = path[0];

    //    //startGuidelineAnimation();
    //}

   public IEnumerator updateViewFloorArc(float wait)
   {
       int i = 0;
       while (true)
       {
           i++;
           if (i>=upperArmDirectionsList.Count)
           {
               StartCoroutine("updateSessionsFloorArc",.02f);
               StopCoroutine("updateViewFloorArc");
               yield return null;
           }
           fullMovementLineRenderer.SetVertexCount(i);
           for (int j = 0; j < i; j++) {
               var pos = upperArmDirectionsList[j] * distance;
               fullMovementLineRenderer.SetPosition(j, pos);
           }
           yield return null;
       }  
    }

    public int logid;
    public event EventHandler<EventArgs> onFinishedDrawing ;
   public IEnumerator updateSessionsFloorArc(float wait) {
       int i = 0;
       //int logid = 0;
       while (true) {
           i++;
           if (i >= session.logs[logid].entries.Count) {
               Utils.LaunchEvent(this, onFinishedDrawing);
               StopCoroutine("updateSessionsFloorArc");
               yield return null;
           }
           currentLineRenderer.SetVertexCount(i);
           for (int j = 0; j < i; j++)
           {
               var pos = session.logs[logid].entries[j].jointsGroup.getUpperArmDirection() * distance;
               currentLineRenderer.SetPosition(j, pos);
           }
           
           yield return null;
       }
   }

    #region Full Movement Line Renderer
    public LineRenderer fullMovementLineRenderer;

    #endregion

    #region Current Movement Line Renderer
    public LineRenderer currentLineRenderer;
    #endregion

    public Session session;
}
