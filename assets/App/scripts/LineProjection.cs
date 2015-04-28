using UnityEngine;

public class LineProjection : MonoBehaviour
{
    public LineRenderer lineRenderer;
    //public Color c1, c2;

    public void Start() {
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetColors(c1, c2);
    }

    // Update is called once per frame
    private void Update()
    {
        //_Sprite.position = cube.position + offset;
        //lineRenderer.SetColors(c1, c2);

        for (var i = 0; i < ManagerTracking.instance.count; i++)
        {
            lineRenderer.SetPosition(i, ManagerTracking.instance.PositionProjected[i]);
        }
    }
}