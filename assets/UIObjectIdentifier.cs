using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIObjectIdentifier : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
        //_name = this.GetComponent<Text>();
        //if (_name == null) _name = this.gameObject.AddComponent<Text>();
	    _name = this.name;
	}
	
	// Update is called once per frame
	void Update ()
	{
	   // _name.rectTransform.position = Camera.main.WorldToScreenPoint(this.transform.position);
	}

    public void OnGUI()
    {
        Vector3 p = Camera.main.WorldToScreenPoint(this.transform.position);

        GUI.Label(new Rect(p.x, Screen.height - p.y +30 ,20,20),this.name);
    }

    

    protected String _name;
}
