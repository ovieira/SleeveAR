using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    #region LifeCycle

    // Use this for initialization
    protected virtual void Start() {
        serviceExercise = ServiceExercise.instance;
        serviceTeaching = ServiceTeaching.instance;
        serviceTracking = ServiceTracking.instance;
        serviceAudio = ServiceAudio.instance;
    }

    protected virtual void OnDestroy() { }

    #endregion

    #region Singletons

    protected ServiceExercise serviceExercise;
    protected ServiceTracking serviceTracking;
    protected ServiceTeaching serviceTeaching;
    protected ServiceAudio serviceAudio;

    #endregion

    
}
