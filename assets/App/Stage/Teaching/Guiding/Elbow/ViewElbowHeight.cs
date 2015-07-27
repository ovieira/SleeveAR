using System;
using UnityEngine;
using System.Collections;

public class ViewElbowHeight : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start()
    {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float heightDiff = currentHeight.y - targetHeight.y;

        this.dottedCircle.localPosition = new Vector3(0,0, heightDiff);
    } 
    #endregion

    #region Current/Target

    public Vector3 currentHeight, targetHeight;

    protected Vector3 defaultPosition;

    #endregion

    #region Arm Direction

    public Transform dottedCircle;

    #endregion

}
