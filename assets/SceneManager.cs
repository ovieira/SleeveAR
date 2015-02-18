using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void LoadLearningPhase()
    {
        Application.LoadLevel("Learning");
    }

    public void LoadTeachingPhase()
    {
        Application.LoadLevel("Sound");        
    }
}
