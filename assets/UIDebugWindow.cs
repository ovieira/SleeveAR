using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDebugWindow : MonoBehaviour
{
    protected GameObject _debuggedObject;
    public GameObject debuggedObject
    {
        get { return this._debuggedObject; }
        set
        {
            if (value == null) return;
            this._debuggedObject = value;
            debuggedObjectTransform = _debuggedObject.transform;
        }
        
    }
    protected Transform debuggedObjectTransform;

    public Text _XPosition;
    public Text _YPosition;
    public Text _ZPosition;



	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{

	    if (debuggedObjectTransform == null) return;

        _XPosition.text = "X: " + debuggedObjectTransform.position.x;
        _YPosition.text = "Y: " + debuggedObjectTransform.position.y;
        _ZPosition.text = "Z: " + debuggedObjectTransform.position.z;

	}

    private static UIDebugWindow _instance;

    public static UIDebugWindow instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<UIDebugWindow>();
            }
            return _instance;
        }
    }
}
