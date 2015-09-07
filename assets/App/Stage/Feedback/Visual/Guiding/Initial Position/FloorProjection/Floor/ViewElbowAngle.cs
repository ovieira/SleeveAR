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
        currentBar.localEulerAngles = new Vector3(0,  - current - 180 , 0);
        targetBar.localEulerAngles = new Vector3(0, - target - 180 , 0);

        //this.transform.eulerAngles = new Vector3(0, -90 + _extraAngle, 0);
        this.container.localEulerAngles = new Vector3(0, -90 + _extraAngle, 0);

        //this.rotateLeft.transform.Rotate(Vector3.up, -Time.deltaTime);
        //this.rotateLeft.transform.Rotate(Vector3.up, Time.deltaTime);
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
        return Utils.Map(diff, 0, target, 0, 1);
    }

    #region Container

    public Transform container;

    #endregion

    #region Rotate Arrows

    public GameObject rotateLeft, rotateRight;

    protected void showLeft()
    {
        Hashtable hash = Utils.HashValueTo(this.name + "showleft", this.rotateLeft.GetComponent<SpriteRenderer>().color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateLeft",
            "oncomplete");

        iTween.ValueTo(this.gameObject,hash);
    }

    protected void hideLeft() {
        Hashtable hash = Utils.HashValueTo(this.name + "hideleft", this.rotateLeft.GetComponent<SpriteRenderer>().color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateLeft",
            "oncomplete");
        iTween.ValueTo(this.gameObject, hash);

    }

    protected void onUpdateLeft(float progress)
    {
        var sr = this.rotateLeft.GetComponent<SpriteRenderer>();
        var colorsr = sr.color;
        sr.color = new Color(colorsr.r,colorsr.g,colorsr.b,progress);
    }

    protected void showRight() {
        Hashtable hash = Utils.HashValueTo(this.name + "showright", this.rotateLeft.GetComponent<SpriteRenderer>().color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateRight",
            "oncomplete");
        iTween.ValueTo(this.gameObject, hash);

    }

    protected void hideRight() {
        Hashtable hash = Utils.HashValueTo(this.name + "hideright", this.rotateLeft.GetComponent<SpriteRenderer>().color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateRight",
            "oncomplete");
        iTween.ValueTo(this.gameObject, hash);

    }

    protected void onUpdateRight(float progress) {
        var sr = this.rotateRight.GetComponent<SpriteRenderer>();
        var colorsr = sr.color;
        sr.color = new Color(colorsr.r, colorsr.g, colorsr.b, progress);
    }
    #endregion
}
