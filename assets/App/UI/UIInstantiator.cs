using UnityEngine;
using System.Collections;

public class UIInstantiator : MonoBehaviour {


    #region Prefab

    public GameObject[] prefabs;

    #endregion

	// Use this for initialization
	void Start ()
	{

	    if (prefabs == null)
	    {
	        Debug.LogError("prefab is null");
	        return;
	    }

	    foreach (var prefab in prefabs)
	    {
            GameObject ob = Instantiate(prefab);
            ob.transform.SetParent(this.transform, false);
	    }

	}
}
