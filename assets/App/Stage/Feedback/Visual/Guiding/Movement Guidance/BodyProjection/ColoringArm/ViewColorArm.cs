using UnityEngine;
using System.Collections;

public class ViewColorArm : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        upperArm = Utils.AddChildren(this.transform, lineRendererPrefab).GetComponent<LineRenderer>();
        foreArm = Utils.AddChildren(this.transform, lineRendererPrefab).GetComponent<LineRenderer>();

        upperArm.SetColors(Color.white, Color.white);
        upperArm.SetVertexCount(2);
        upperArm.SetWidth(0.25f, 0.25f);

        foreArm.SetColors(Color.white, Color.white);
        foreArm.SetVertexCount(2);
        foreArm.SetWidth(0.25f, 0.25f);

	}
	
	// Update is called once per frame
	void Update ()
	{
	    updatePositions();
	    updateColors();
	}

    private void updateColors()
    {
        float anglediff = current.angle - target.angle;
        foreArmColor = Color.Lerp(correctColor,wrongColor,anglediff);

        foreArm.SetColors(foreArmColor, foreArmColor);
    }

    private void updatePositions() {
        updateLineRendererPosition(upperArm, currentArmPosition[0], currentArmPosition[1]);
        updateLineRendererPosition(foreArm, currentArmPosition[1], currentArmPosition[2]);
    }

    #region Current/Target

    public JointsGroup current, target;
    #endregion


    #region Colors

    public Color correctColor;
    public Color wrongColor;

    protected Color upperArmColor, foreArmColor;
    #endregion

    #region LineRenderers

    public GameObject lineRendererPrefab;

    protected LineRenderer upperArm, foreArm;


    void updateLineRendererPosition(LineRenderer lineRenderer, Vector3 posa, Vector3 posb)
    {
        lineRenderer.SetPosition(0, posa);
        lineRenderer.SetPosition(1, posb);
    }

    #endregion

    #region Tracking

    public Vector3[] currentArmPosition;

    #endregion
}
