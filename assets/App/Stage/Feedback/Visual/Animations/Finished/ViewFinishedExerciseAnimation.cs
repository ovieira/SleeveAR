using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class ViewFinishedExerciseAnimation : MonoBehaviour {

	#region LifeCycle
	void Start () {

        for (int i = 0; i < 3; i++) {
            var ob = Utils.AddChildren(this.transform, whitecircleprefab);
            ob.transform.localScale = Vector3.zero;
            whitecircles.Add(ob);

        }
	}
	
	void Update () {
	    for (int i = 0; i < whitecircles.Count; i++)
	    {
	        var ob = whitecircles[i];
            var sr = ob.GetComponent<SpriteRenderer>();
	        sr.color = Color.Lerp(sr.color, _colors[i], Time.deltaTime);
	    }
	}
	#endregion

    #region Sprites

    public GameObject whitecircleprefab;
    public List<GameObject> whitecircles =  new List<GameObject>();
    protected Color[] _colors = new Color[3];
    #endregion

    public void startAnimation()
    {
        Hashtable hashValueTo = Utils.HashValueTo(this.name + 1, 0, 0.05f, 5f, 0f, iTween.EaseType.easeInOutSine,
            "onUpdateCircleScale", "onCompleteCircleScale");

        hashValueTo.Add("looptype", iTween.LoopType.pingPong);

        iTween.ValueTo(this.gameObject, hashValueTo);
        
    }

    protected void onUpdateCircleScale(float progress)
    {
        foreach (var whitecircle in whitecircles)
        {
            whitecircle.transform.localScale = Vector3.one * progress;            
        }
    }

    protected void onCompleteCircleScale()
    {
        for (int i = 0; i < _colors.Length; i++)
        {
            _colors[i] = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }

    public List<Vector3> positions = new List<Vector3>();
}
