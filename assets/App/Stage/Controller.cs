using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

    #region LifeCycle

    protected virtual void Awake() {
        serviceExercise = ServiceExercise.instance;
        serviceTeaching = ServiceTeaching.instance;
        serviceTracking = ServiceTracking.instance;
        serviceAudio = ServiceAudio.instance;
        serviceSection = ServiceSection.instance;
        serviceDifficulty = ServiceDifficulty.instance;
    }

    // Use this for initialization
    protected virtual void Start() {

    }

    protected virtual void OnDestroy() { }

    #endregion

    #region Singletons

    protected ServiceExercise serviceExercise;
    protected ServiceTracking serviceTracking;
    protected ServiceTeaching serviceTeaching;
    protected ServiceAudio serviceAudio;
    protected ServiceSection serviceSection;
    protected ServiceDifficulty serviceDifficulty;

    #endregion


}
