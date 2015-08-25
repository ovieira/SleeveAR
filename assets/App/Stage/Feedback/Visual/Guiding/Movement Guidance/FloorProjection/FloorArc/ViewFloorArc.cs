using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewFloorArc : MonoBehaviour
{

    public List<Vector3> upperArmDirectionsList = new List<Vector3>(); 

	// Use this for initialization
	void Start ()
	{

	    fullMovementLineRenderer.useWorldSpace = currentLineRenderer.useWorldSpace = false;
        currentLineRenderer.SetVertexCount(0);
        fullMovementLineRenderer.SetVertexCount(upperArmDirectionsList.Count);
        for (int i = 0; i < upperArmDirectionsList.Count; i++) {
            fullMovementLineRenderer.SetPosition(i, upperArmDirectionsList[i]);
        }

	}
	
	// Update is called once per frame
	void Update ()
	{
        fullMovementLineRenderer.transform.position = currentLineRenderer.transform.position = basePosition;
        currentLineRenderer.transform.Translate(Vector3.up);
	}

    #region Full Movement Line Renderer
    public LineRenderer fullMovementLineRenderer;

    #endregion

    #region Current Movement Line Renderer

    public LineRenderer currentLineRenderer;

    protected int _progress;

    public int progress
    {
        get { return this._progress; }
        set
        {
            int _value = (int) Utils.Map(value, 0, 100, 0, upperArmDirectionsList.Count);
            _progress = _value;
            updateCurrentLineRenderer();
        }
    }

    private void updateCurrentLineRenderer() {
        currentLineRenderer.SetVertexCount(this.progress);
        for (int i = 0; i < this.progress; i++)
        {
            currentLineRenderer.SetPosition(i, upperArmDirectionsList[i]);
        }
    }

    #endregion

    #region Tracking
    public Vector3 basePosition;
    #endregion
}
