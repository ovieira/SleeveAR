using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    #region LifeCycle

    // Use this for initialization
    protected virtual void Start() {
        serviceExercise = ServiceExercise.instance;
        serviceTeaching = ServiceTeaching.instance;
        serviceTracking = ServiceTracking.instance;
    }

    #endregion

    #region Singletons

    protected ServiceExercise serviceExercise;
    protected ServiceTracking serviceTracking;
    protected ServiceTeaching serviceTeaching;

    #endregion
}
