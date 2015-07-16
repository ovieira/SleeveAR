using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControllerSaveButton : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {

        this.button.onClick.AddListener(this._onButtonClicked);

    } 
    #endregion

    #region Click Listener
    private void _onButtonClicked() {
        ExerciseModel _exercice = ServiceExercise.instance.selected;
        String _filename = InputText.text;

        ServiceFileManager.instance.Save(_filename, _exercice);
    } 
    #endregion
	
    #region Button

    public Button button;

    #endregion

    #region Input Text

    public Text InputText;

    #endregion
}
