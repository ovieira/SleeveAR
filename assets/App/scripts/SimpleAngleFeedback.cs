using System;
using UnityEngine;
using System.Collections;

public class SimpleAngleFeedback : LineProjection
{


    [Range(0, 360f)]
    public float targetAngle;

	
	// Update is called once per frame
	void Update () {
        base.Update();
	    UpdateLineRendererPosition(lineRenderer);

	    float diff = computeLerp();
       // Debug.Log(diff);
	    Color c = Color.Lerp(Color.green, Color.red, diff);
	    UpdateLineRendererColor(lineRenderer, c);
	}

    protected float computeLerp()
    {
        float currentAngle = ManagerTracking.instance.getCurrentJointGroup().angle;
        float diff = Mathf.Abs(targetAngle - currentAngle);
        return map(diff, 0, targetAngle, 0, 1);
    }
}
