using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewArrowDirection : MonoBehaviour {
    

	// Use this for initialization
	void Start () {
	    for (int i = 0; i < arrowsCount; i++)
	    {
            GameObject go = Utils.AddChildren(this.transform, arrowPrefab);
            go.transform.localPosition = Vector3.zero;
            arrowsTransform.Add(go.transform);
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 middleUpperArmPosition = (currentArmPosition[1] + currentArmPosition[0])/2f;
        Vector2 a = new Vector2(target.x, target.z);
        float _angle = Vector2.Angle(Vector2.up, a);
	    foreach (var t in arrowsTransform)
	    {
	        t.position = middleUpperArmPosition;
            t.localEulerAngles = new Vector3(0,_angle,0);
	    }
	}

    #region Tracking

    public Vector3[] currentArmPosition;
    

    #endregion

    #region Current/Target

    public Vector3 target;
    public Vector3 current;

    #endregion

    #region Arrow

    public int arrowsCount = 1;

    protected List<Transform> arrowsTransform = new List<Transform>(); 

    public SpriteRenderer arrowSprite;
    public GameObject arrowPrefab;

    #endregion
}
