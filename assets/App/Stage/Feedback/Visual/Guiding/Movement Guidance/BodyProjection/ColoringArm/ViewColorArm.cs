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
        updateForeArmColor();
        updateUpperArmColor();
    }

    private void updateUpperArmColor()
    {
        //var anglediff = Vector3.Angle(current.getUpperArmDirection(), target.getUpperArmDirection());
        //upperArmColor = Color.Lerp(correctColor, wrongColor, anglediff);

        var diff = Mathf.Abs(current.getUpperArmDirection().y - target.getUpperArmDirection().y);

        //diff = Utils.Map(diff, 0, 5, 0, 1);
        Debug.Log(diff);
        upperArmColor = Color.Lerp(correctColor, wrongColor, diff);

        upperArm.SetColors(upperArmColor, upperArmColor);

    }

    private void updateForeArmColor()
    {
        float anglediff = current.angle - target.angle;
        foreArmColor = Color.Lerp(correctColor, wrongColor, computeLerp());

        foreArm.SetColors(foreArmColor, foreArmColor);
    }

    protected float computeLerp() {
        float diff = Mathf.Abs(target.angle - current.angle);
        var lerp = Utils.Map(diff, 0, target.angle, 0, 1);
        return lerp;
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
