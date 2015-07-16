using UnityEngine;

public class SimpleHeightFeedback : LineProjection {
    public override void Start()
    {
        base.Start();
    }

    public enum HeightFeedbackMode {
        Color,
        Shapes
    }

    public HeightFeedbackMode _mode;

    [Range(0f, 3f)]
    public float targetHeight;
    
    //private Color targetColor;

    public override void Update() {
        base.Update();
        UpdateLineRendererPosition(lineRenderer);
        float diff = computeLerp();
            
        Color c = Color.Lerp(Color.green, Color.red, diff);
        UpdateLineRendererColor(lineRenderer, c);
    }

    private float computeLerp() {
        float diff = Mathf.Abs((targetHeight - ServiceTracking.instance.getCurrentJointGroup().jointsList[0].positionWithOffset.y));
        return map(diff, 0, targetHeight, 0, 1);
    }

}
