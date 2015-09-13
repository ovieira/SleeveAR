using System;
using UnityEngine;
using System.Collections;

public class UIControllerShortcuts : Controller {

	#region LifeCycle

    protected override void Awake()
    {
        base.Awake();
        foreach (var uiButton in buttons)
        {
            uiButton.onClicked += this._onClicked;
        }
    }

    

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        foreach (var uiButton in buttons) {
            uiButton.onClicked -= this._onClicked;
        }
    }

    #endregion

    #region Onclick

    private void _onClicked(object sender, System.EventArgs e)
    {
        var button = (UIButton) sender;
        int i = Int32.Parse(button.text.text);
        var filename = filenames[i - 1];
        ExerciseModel exerciseModel = ServiceFileManager.instance.LoadExerciseModel(filename);
        if (exerciseModel != null) {
            ServiceExercise.instance.selected = exerciseModel;
            exerciseModel.print();
        }
    }

    #endregion

    #region FileNames

    public string[] filenames;

    #endregion

    #region View

    public UIViewShortcuts view;

    #endregion

    #region MyRegion

    public UIButton[] buttons;

    #endregion
}
