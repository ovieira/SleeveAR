using UnityEngine;
using System.Collections;

public class ViewElbowDirection : MonoBehaviour {

    #region LifeCycle
    public void Awake() {
        GameObject go = Instantiate(dottedCirclePrefab);
        dottedCircle = go.transform;
        dottedCircle.SetParent(this.transform);
        dottedCircle.localPosition = Vector3.zero;
    }


    // Use this for initialization
    void Start() {
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
        float directiontDiff = currentDirection.x - targetDirection.x;
        Debug.Log(directiontDiff);
        this.dottedCircle.localPosition = new Vector3(directiontDiff, this.dottedCircle.localPosition.y, this.dottedCircle.localPosition.z);
        this.dottedCircle.localScale = new Vector3(0.032f, 0.032f, 0.032f) / ((this.dottedCircle.localPosition.magnitude) + 1);
    }
    #endregion

    #region Current/Target

    public Vector3 currentDirection, targetDirection;

    protected Vector3 defaultPosition;

    #endregion

    #region Dotted Circle

    protected Transform dottedCircle;
    public GameObject dottedCirclePrefab;

    #endregion
}
