using UnityEngine;
using System.Collections;
using System.Threading;

public class ControllerTutorial : Controller {

    #region LifeCycle

    protected override void Awake() {
        base.Awake();
        ServiceTutorial.instance.onCountChanged += this._onCountChanged;
    }

    protected override void Start() {
        base.Start();
        timer = 3f;
        StartCoroutine("DemoExercise");
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        ServiceTutorial.instance.onCountChanged -= this._onCountChanged;
        StopAllCoroutines();
        serviceExercise.selected = null;
    }
    #endregion

    #region Service Tutorial

    private void _onCountChanged(object sender, System.EventArgs e) {
        StopCoroutine("DemoExercise");
        if (this.count <= ServiceTutorial.instance.count) {
            Debug.Log("Tutorial Finished");
            serviceExercise.selected = null;
            serviceSection.selected = ServiceSection.Section.LEARNING;
        }
        else
        {
            timer = 3f;
            if (ServiceTutorial.instance.count <= 3)
            {
                ServiceTutorial.instance.selected = ServiceTutorial.TutorialType.FOREARM;
            }
            if (ServiceTutorial.instance.count > 3 && ServiceTutorial.instance.count <= 5)
            {
                ServiceTutorial.instance.selected = ServiceTutorial.TutorialType.UPPERARM;
            }
            if (ServiceTutorial.instance.count > 5)
            {
                ServiceTutorial.instance.selected = ServiceTutorial.TutorialType.BOTH;
            }
            StartCoroutine("DemoExercise");
        }

    }

    #endregion

    #region Demo Exercise

    IEnumerator DemoExercise() {
        ServiceTutorial.TutorialType _type = ServiceTutorial.instance.selected;

        JointsGroup currentjg, goaljg;
        Utils.DestroyAllChildren(this.exerciseContainer.transform);
        Utils.DestroyAllChildren(this.feedbackContainer.transform);
        float time = 1f;
        while (time >= 0) {
            time -= Time.deltaTime;
            yield return null;
        }
        createExercise(_type);
        while (true)
        {
           if (ServiceTutorial.instance.goalJointsGroup != null)
            {
                currentjg = serviceTracking.getCurrentJointGroup();
                checkExercise(currentjg, ServiceTutorial.instance.goalJointsGroup, _type);
            }
            
            yield return null;
        }

    }

    public GameObject exerciseContainer;
    public GameObject feedbackContainer;

    private void createExercise(ServiceTutorial.TutorialType _type) {
        
        switch (_type) {
            case ServiceTutorial.TutorialType.FOREARM:
                Utils.AddChildren(this.exerciseContainer.transform, this.forePrefab);
                Utils.AddChildren(this.feedbackContainer.transform, this.foreFeedback);
                break;
            case ServiceTutorial.TutorialType.UPPERARM:
                Utils.AddChildren(this.exerciseContainer.transform, this.upperPrefab);
                Utils.AddChildren(this.feedbackContainer.transform, this.upperFeedback);
                break;
            case ServiceTutorial.TutorialType.BOTH:
                Utils.AddChildren(this.exerciseContainer.transform, this.bothPrefab);
                Utils.AddChildren(this.feedbackContainer.transform, this.foreFeedback);
                Utils.AddChildren(this.feedbackContainer.transform, this.upperFeedback);

                break;
            default:
                break;
        }
    }

    public float timer;
    private void checkExercise(JointsGroup jg, JointsGroup goal, ServiceTutorial.TutorialType _type)
    {
        if (timer <= 0) { nextDemo();}
        switch (_type) {
            case ServiceTutorial.TutorialType.FOREARM:
                if (checkForeArmAngle(jg, goal, 3))
                {
                    timer -= Time.deltaTime;

                }
                else
                {
                    timer = 3f;
                }
                break;
            case ServiceTutorial.TutorialType.UPPERARM:
                if (CheckUpperArmDirection(jg, goal, 3)) {
                    timer -= Time.deltaTime;
                }
                else {
                    timer = 3f;
                }
                break;
            case ServiceTutorial.TutorialType.BOTH:
                if (checkForeArmAngle(jg, goal, 3) &&
                    CheckUpperArmDirection(jg, goal, 3)) {
                        timer -= Time.deltaTime;

                }
                else {
                    timer = 3f;
                }
                break;
            default:
                break;
            
        }
    }

    protected void nextDemo()
    {
        ServiceTutorial.instance.count++;
    }

    private bool checkForeArmAngle(JointsGroup a, JointsGroup b, float t) {
        return Utils.IsApproximately(a.angle, b.angle, t);
    }

    private bool CheckUpperArmDirection(JointsGroup jg, JointsGroup goal, float t) {
        return Utils.isEqualByAngle(jg.getUpperArmDirection(), goal.getUpperArmDirection(), t);
    }
    #endregion

    #region Count

    public int count;

    #endregion

    public GameObject upperPrefab;
    public GameObject forePrefab;
    public GameObject bothPrefab;

    public GameObject upperFeedback;
    public GameObject foreFeedback;
    public GameObject bothFeedback;
}
