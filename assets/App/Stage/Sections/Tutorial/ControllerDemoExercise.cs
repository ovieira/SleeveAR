using System;
using UnityEngine;
using System.Collections;

public class ControllerDemoExercise : Controller {

	#region LifeCycle
	void Start ()
	{

	    int random = UnityEngine.Random.Range(0, exercises.Length);

	    ExerciseModel em = ServiceFileManager.instance.LoadExerciseModel(exercises[random]);

	    ServiceTutorial.instance.goalJointsGroup = em.exerciseModel[0];

	    serviceExercise.selected = em;
	}
	
	void Update () {
	
	}
	#endregion

    #region MyRegion

    public String[] exercises;

    #endregion
}
