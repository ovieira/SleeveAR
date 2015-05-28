
public class SimpleLineProjection : LineProjection {
    
    public override void Update() {
        base.Update();
        UpdateLineRendererPosition(lineRenderer);
    }
}