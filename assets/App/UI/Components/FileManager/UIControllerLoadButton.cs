using UnityEngine;
using System.Collections;
using System.IO;
using FullSerializer;
using UnityEngine.UI;

public class UIControllerLoadButton : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Start() {

        this.button.onClick.AddListener(this._onButtonClicked);

    }
    #endregion

    #region Click Listener
    private void _onButtonClicked()
    {
        string filename = InputText.text;

        if (filename == "") {
            Debug.LogWarning("Please write a filename to load");
            return;
        }

        ExerciseModel exerciseModel = ServiceFileManager.instance.Load(filename);
        if (exerciseModel != null)
        {
            ServiceExercise.instance.selected = exerciseModel;
            exerciseModel.print();
        }
    }
    #endregion

    #region Button

    public Button button;

    #endregion

    #region Input Text

    public Text InputText;

    #endregion
}
