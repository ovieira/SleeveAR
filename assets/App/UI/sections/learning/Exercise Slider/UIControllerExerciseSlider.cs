using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            setMinMax(0, serviceExercise.count);
            slider.value = serviceExercise.index;

            this.view.show();
        }
        else this.view.hide();

        addPartButton.onClicked += this._onAddPartButtonClicked;
    }

    

    protected override void OnDestroy() {
        base.OnDestroy();
        addPartButton.onClicked -= this._onAddPartButtonClicked;

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
        this.setMinMax(0,serviceExercise.count); 
        this.view.show();
        this.view.parts = new List<Vector2>(serviceExercise.selected.parts);
        this.view.showDividers();
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
        this.view.maxValue = slider.maxValue = max;
    }
    #endregion

    #region Add Part

    public UIButton addPartButton;

    private void _onAddPartButtonClicked(object sender, EventArgs e) {
        if (serviceExercise.selected.parts.Count == 0)
        {
            serviceExercise.selected.addPart(0, serviceExercise.index);
        }
        else
        {
            var lastPart = serviceExercise.selected.parts[serviceExercise.selected.parts.Count - 1];
            serviceExercise.selected.addPart((int)lastPart.y, serviceExercise.index);
        }
        this.view.parts = new List<Vector2>(serviceExercise.selected.parts);
        this.view.updateDividers();
    }

    #endregion
}
