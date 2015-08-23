using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewFloorArc : MonoBehaviour
{

    public List<Vector3> upperArmDirectionsList = new List<Vector3>(); 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        fullMovementLineRenderer.SetVertexCount(upperArmDirectionsList.Count);
	    for (int i = 0; i < upperArmDirectionsList.Count; i++)
	    {
	        fullMovementLineRenderer.SetPosition(i, basePosition + upperArmDirectionsList[i]);
	    }
	}

    #region LineRenderer

    public LineRenderer fullMovementLineRenderer;
    public LineRenderer currentLineRenderer;
    public Vector3 basePosition;

    #endregion
}
