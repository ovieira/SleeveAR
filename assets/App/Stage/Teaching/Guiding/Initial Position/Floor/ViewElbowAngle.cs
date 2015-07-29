using System;
using UnityEngine;
using System.Collections;

public class ViewElbowAngle : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {

        circleSpriteRenderer.color = Color.red;

    }

    // Update is called once per frame
    void Update() {

        Color c = Color.Lerp(Color.green, Color.red, computeLerp());

        circleSpriteRenderer.color = c;


        Vector2 a = new Vector2(armDirection.x, armDirection.z);
        float _extraAngle = Vector2.Angle(Vector2.up, a);
        currentBar.localEulerAngles = new Vector3(0, -180 - current, 0);
        targetBar.localEulerAngles = new Vector3(0, -180 - target, 0);

        //this.transform.eulerAngles = new Vector3(0, -90 + _extraAngle, 0);
        this.container.localEulerAngles = new Vector3(0, -90 + _extraAngle, 0);

    } 
    #endregion

    #region Current/Target

    [NonSerialized]
    public float current, target;

    #endregion

    #region Arm Direction

    [NonSerialized]
    public Vector3 armDirection;

    #endregion

    #region Circle/Bars

    public Transform circle;
    public Transform currentBar, targetBar;

    #endregion

    #region SpriteRenderer

    public SpriteRenderer circleSpriteRenderer, targetBarSpriteRenderer, currentBarSpriteRenderer;

    #endregion

    protected float computeLerp() {
        float diff = Mathf.Abs(target - current);
        return Utils.map(diff, 0, target, 0, 1);
    }

    #region Container

    public Transform container;

    #endregion
}
