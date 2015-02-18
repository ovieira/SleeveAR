using UnityEngine;
using System.Collections;
using System.Security.Cryptography;

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

    public void LoadAudioPhase()
    {
        Application.LoadLevel("Sound");        
    }

    public void LoadVisualPhase() {
        Application.LoadLevel("Visual");
    }
}
