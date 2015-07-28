using UnityEngine;
using System.Collections;

public class ViewDirectionHeight : MonoBehaviour {

    #region LifeCycle
    public void Awake() {
        GameObject go = Instantiate(dottedCirclePrefab);
        dottedCircle = go.transform;
        dottedCircle.SetParent(this.transform);
        dottedCircle.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 diff = current - target;
        float directiontDiff = current.x - target.x;
        float heightDiff = current.y - target.y;

        this.dottedCircle.localPosition = new Vector3(directiontDiff, this.dottedCircle.localPosition.y, heightDiff);
        this.dottedCircle.localScale = new Vector3(0.032f, 0.032f, 0.032f) / ((this.dottedCircle.localPosition.magnitude) + 1);
    
    } 
    #endregion


    #region Current/Target

    public Vector3 current, target;

    #endregion

    #region Dotted Circle

    protected Transform dottedCircle;
    public GameObject dottedCirclePrefab;

    #endregion
}
