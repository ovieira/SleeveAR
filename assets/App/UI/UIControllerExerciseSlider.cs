using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIControllerExerciseSlider : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Start() {
        base.Start();
        //ServiceMedia.instance.onStartPlaying += _onStartPlaying;
        serviceExercise.onFinishedExercise += _onFinishedExercise;
        serviceExercise.onCurrentIndexChanged += _onIndexChanged;
        serviceExercise.onSelectedExerciseChanged += _onExercisedLoaded;

        slider.onValueChanged.AddListener(_onValueChanged);

        if (serviceExercise.selected != null)
        {
            setMinMax(0,serviceExercise.count);
            slider.value = serviceExercise.index;

            this.view.show();
        }
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        slider.onValueChanged.RemoveListener(_onValueChanged);
        serviceExercise.onSelectedExerciseChanged -= _onExercisedLoaded;
        serviceExercise.onCurrentIndexChanged -= _onIndexChanged;
        serviceExercise.onFinishedExercise -= _onFinishedExercise;

    }

    #endregion


    protected void _onStartPlaying(object sender, EventArgs e) {
        this.view.show();
    }

    protected void _onFinishedExercise(object sender, EventArgs e) {
        //this.view.hide();
    }

    protected void _onIndexChanged(object sender, EventArgs e) {
        slider.value = serviceExercise.index;
    }

    protected void _onExercisedLoaded(object sender, EventArgs e) {
        this.setMinMax(0,serviceExercise.count); //TODO: serviceExercise must have total entries saved in property
        this.view.show();

    }
    #region View

    public UIViewExerciseSlider view;

    #endregion

    #region Slider

    public Slider slider;


    protected void _onValueChanged(float value) {
        serviceExercise.index = (int)value;
    }

    protected void setMinMax(int min, int max) {
        slider.minValue = min;
        slider.maxValue = max;
    }
    #endregion
}
