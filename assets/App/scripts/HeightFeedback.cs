using UnityEngine;
using System.Collections;

public class HeightFeedback : MonoBehaviour {

    public enum HeightFeedbackMode
    {
        Color,
        Shapes
    }

    public HeightFeedbackMode _mode;
    private LineRenderer lineRenderer, lineRendererWithOffset;
    public GameObject LineRendererPrefab;
    public Material white;
    private Color targetColor;

    public Camera _camera;
    private Color targetColor2;

	// Use this for initialization
	void Start () {
        white.color = Color.white;
        var go = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
        go.transform.parent = this.transform;
        lineRenderer = go.GetComponent<LineRenderer>();

        InvokeRepeating("randomColor", 0, 5);
	}
	
	// Update is called once per frame
	void Update ()
	{
	    white.color = Color.Lerp(white.color, targetColor, Time.deltaTime);
        _camera.backgroundColor = Color.Lerp(_camera.backgroundColor, targetColor2, Time.deltaTime);
        for (var i = 0; i < ManagerTracking.instance.count; i++) {
            lineRenderer.SetPosition(i, ManagerTracking.instance.PositionProjectedWithOffset[i]);
        }
	}

    public void randomColor()
    {
        targetColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        targetColor2 = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

    }
}
