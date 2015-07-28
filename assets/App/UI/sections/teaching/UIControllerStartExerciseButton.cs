using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIButton))]
public class UIControllerStartExerciseButton : MonoBehaviour {
   
    #region LifeCycle
    // Use this for initialization
    private void Start() {
        button.onClicked += this._onClicked;
	     ServiceExercise.instance.onSelectedExerciseChanged += this._onSelectedExerciseChanged;

        if (ServiceExercise.instance.selected == null) this.view.hide();
        else
        {
            this.view.show();
        }
    }


    public void OnDestroy() {
        ServiceExercise.instance.onSelectedExerciseChanged -= this._onSelectedExerciseChanged;
        button.onClicked -= this._onClicked;
    }

   
    #endregion

    #region Service Exercise

    private void _onSelectedExerciseChanged(object sender, System.EventArgs e) {
        if (ServiceExercise.instance.selected != null) {
            this.view.show();
        }
    }

    #endregion

    #region View

    public UIViewStartExerciseButton view;

    #endregion

    #region Button

    public UIButton button {
        get { return this.GetComponent<UIButton>(); }
    }

    private void _onClicked(object sender, EventArgs e) {
       this.view.hide();
        ServiceExercise.instance.start = true;
    }
    #endregion
}
