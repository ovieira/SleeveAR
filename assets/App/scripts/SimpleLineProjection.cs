
public class SimpleLineProjection : LineProjection {
    //private LineRenderer lineRenderer, lineRendererWithOffset;

    //public Material white, red;
    //public GameObject LineRendererPrefab;
    ////public Color c1, c2;

    //public void Start() {
    //    //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
    //    //lineRenderer.SetColors(c1, c2);


    //    /** /
    //    var go = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
    //    go.transform.parent = this.transform;
    //    lineRenderer = go.GetComponent<LineRenderer>();

    //    var go2 = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
    //    go2.transform.parent = this.transform;
    //    lineRendererWithOffset = go2.GetComponent<LineRenderer>();

    //    lineRendererWithOffset.material = red;
    //    /**/
    //    var go = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
    //    go.transform.parent = this.transform;
    //    lineRenderer = go.GetComponent<LineRenderer>();
    //    lineRenderer.SetColors(Color.white, Color.white);

    //    var go2 = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
    //    go2.transform.parent = this.transform;
    //    lineRendererWithOffset = go2.GetComponent<LineRenderer>();
    //    lineRendererWithOffset.SetColors(Color.red, Color.red);
    //    /**/


    //}

    //// Update is called once per frame
    //private void Update() {
    //    //_Sprite.position = cube.position + offset;
    //    //lineRenderer.SetColors(c1, c2);

    //    for (var i = 0; i < ManagerTracking.instance.count; i++) {
    //        lineRenderer.SetPosition(i, ManagerTracking.instance.PositionProjected[i]);
    //        lineRendererWithOffset.SetPosition(i, ManagerTracking.instance.PositionProjectedWithOffset[i]);
    //    }
    //}

    public override void Update() {
        base.Update();
        UpdateLineRendererPosition(lineRenderer);
    }
}