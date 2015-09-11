using System;
using UnityEngine;
using System.Collections;

public class ViewElbowAngle : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {

        circleSpriteRenderer.color = Color.red;
        setAlpha(rotateLeftSprite, 0f);
        setAlpha(rotateRightSprite, 0f);
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

        this.rotateLeft.transform.Rotate(Vector3.up, -Time.deltaTime*15);
        this.rotateRight.transform.Rotate(Vector3.up, Time.deltaTime*15);
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

    private bool showingLeft, showingRight;

    protected float computeLerp() {
        float diff = target - current;
        float absDiff = Mathf.Abs(diff);

        if (diff > 5f ) {
            setAlpha(rotateLeftSprite, 1f);
            setAlpha(rotateRightSprite, 0f);
        }
        else if (diff < -5f) {
            setAlpha(rotateLeftSprite, 0f);
            setAlpha(rotateRightSprite, 1f);
        }
        else
        {
            setAlpha(rotateLeftSprite, 0f);
            setAlpha(rotateRightSprite, 0f);
        }
        var map = Utils.Map(absDiff, 0, target, 0, 1);
        return map;
    }

    private void setAlpha(SpriteRenderer sr, float alpha) {
        sr.color = new Color(sr.color.r,sr.color.g, sr.color.b, alpha);
    }

    private void hideBoth()
    {
        showingRight = showingLeft = false;
        this.hideRight();
        this.hideLeft();
    }

    #region Container

    public Transform container;

    #endregion

    #region Rotate Arrows

    public GameObject rotateLeft, rotateRight;
    public SpriteRenderer rotateLeftSprite, rotateRightSprite;

    protected void showLeft()
    {
        showingRight = false;
        this.hideRight();
        Hashtable hash = Utils.HashValueTo(this.name + "showleft", rotateLeftSprite.color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateLeft",
            "oncomplete");

        iTween.ValueTo(this.gameObject,hash);
    }

    protected void hideLeft() {

        Hashtable hash = Utils.HashValueTo(this.name + "hideleft", rotateLeftSprite.color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateLeft",
            "oncomplete");
        iTween.ValueTo(this.gameObject, hash);

    }

    protected void onUpdateLeft(float progress)
    {
        var colorsr = rotateLeftSprite.color;
        rotateLeftSprite.color = new Color(colorsr.r, colorsr.g, colorsr.b, progress);
    }

    protected void showRight()
    {
        showingLeft = false;
        this.hideLeft();
        Hashtable hash = Utils.HashValueTo(this.name + "showright", rotateRightSprite.color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateRight",
            "oncomplete");
        iTween.ValueTo(this.gameObject, hash);

    }

    protected void hideRight() {

        Hashtable hash = Utils.HashValueTo(this.name + "hideright", rotateRightSprite.color.a, 1f, .5f, 0, iTween.EaseType.easeInOutSine, "onUpdateRight",
            "oncomplete");
        iTween.ValueTo(this.gameObject, hash);

    }

    protected void onUpdateRight(float progress) {
        var colorsr = rotateRightSprite.color;
        rotateRightSprite.color = new Color(colorsr.r, colorsr.g, colorsr.b, progress);
    }
    #endregion
}
