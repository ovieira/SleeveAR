using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControllerExerciseSlider : Controller {

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
	    //ServiceMedia.instance.onStartPlaying += _onStartPlaying;
        serviceExercise.onFinishedExercise += _onFinishedExercise;
	    serviceExercise.onCurrentIndexChanged += _onIndexChanged;
	    serviceExercise.onSelectedExerciseChanged += _onExercisedLoaded;

        slider.onValueChanged.AddListener(_onValueChanged);
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    protected void _onStartPlaying(object sender, EventArgs e)
    {
        this.view.show();
    }

    protected void _onFinishedExercise(object sender, EventArgs e) {
        this.view.hide();
    }

    protected void _onIndexChanged(object sender, EventArgs e)
    {
        slider.value = serviceExercise.index;
    }

    protected void _onExercisedLoaded(object sender, EventArgs e)
    {
        slider.minValue = 0;
        slider.maxValue = serviceExercise.selected.exerciseModel.Count; //TODO: serviceExercise must have total entries saved in property
        this.view.show();

    }
    #region View

    public UIViewExerciseSlider view;

    #endregion

    #region Slider

    public Slider slider;


    protected void _onValueChanged(float value)
    {
        serviceExercise.index = (int)value;
    }

    #endregion
}
