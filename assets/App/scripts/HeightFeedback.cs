﻿using UnityEngine;
using System.Collections;

public class HeightFeedback : LineProjection {

    public enum HeightFeedbackMode {
        Color,
        Shapes
    }

    public HeightFeedbackMode _mode;
    public float targetHeight;
    private Color targetColor;

    public override void Update()
    {
        base.Update();
        UpdateLineRendererPosition(lineRenderer);
        float diff =
            Mathf.Abs((targetHeight - ManagerTracking.instance.getCurrentJointGroup().jointsList[0].positionWithOffset.y));
    }

    //// Use this for initialization
    //void Start () {
    //    white.color = Color.white;
    //    var go = (GameObject)Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity);
    //    go.transform.parent = this.transform;
    //    lineRenderer = go.GetComponent<LineRenderer>();

    //    InvokeRepeating("randomColor", 0, 5);
    //}

    //// Update is called once per frame
    //void Update ()
    //{
    //    white.color = Color.Lerp(white.color, targetColor, Time.deltaTime);
    //    _camera.backgroundColor = Color.Lerp(_camera.backgroundColor, targetColor2, Time.deltaTime);
    //    for (var i = 0; i < ManagerTracking.instance.count; i++) {
    //        lineRenderer.SetPosition(i, ManagerTracking.instance.PositionProjectedWithOffset[i]);
    //    }
    //}


}
