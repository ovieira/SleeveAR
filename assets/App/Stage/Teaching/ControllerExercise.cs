using UnityEngine;
using System.Collections;
using System;

public class ControllerExercise : Controller {

    // Use this for initialization
    protected override void Start() {
        base.Start();
        serviceExercise.onStart += this._onStart;
    }

    // Update is called once per frame
    private void Update() {

    }

    public void OnDestroy()
    {
        //StopAllCoroutines();

        serviceExercise.onStart -= this._onStart;
    }

    private void _onStart(object sender, EventArgs e) {
        ServiceExercise.instance.index = 0;

        StartCoroutine(initialPosition());
    }

    IEnumerator initialPosition() {
        while (true) {
            checkInitialPosition();
            yield return null;
        }
    }

    protected void checkInitialPosition() {
        //get arm angle
        JointsGroup jg = serviceTracking.getCurrentJointGroup();
        JointsGroup goal = serviceExercise.currentJointsGroup;

        Debug.Log(jg.angle + " : " + goal.angle);

        if (Utils.IsApproximately(jg.angle, goal.angle, 1f))
        {
            Debug.Log("TA PARECIDO");
        }
        //get single joints angle

        //if close enough to initial position, launch reachedInitialPosition Event and stop coroutine
    }
}
