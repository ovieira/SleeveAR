using UnityEngine;
using System.Collections;

public class ControllerSection : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
        serviceSection.onSectionChanged += this._onSectionChanged;
        Utils.DestroyAllChildren(this.transform);
    }

    protected override void Start() {
        base.Start();
        serviceSection.selected = initialSection;
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        serviceSection.onSectionChanged -= this._onSectionChanged;
    }

    #endregion

    #region Service Section
    private void _onSectionChanged(object sender, System.EventArgs e) {
        switch (ServiceSection.instance.selected) {
            case ServiceSection.Section.LEARNING:
                createSection(LearningSectionPrefab);
                break;
            case ServiceSection.Section.TEACHING:
                createSection(TeachingSectionPrefab);
                break;
                case ServiceSection.Section.TUTORIAL:
                createSection(TutorialSectionPrefab);
                break;
            default:
                break;
        }
    }
    #endregion

    private void createSection(GameObject prefab) {
        Utils.DestroyAllChildren(this.transform);
        Utils.AddChildren(this.transform, prefab);
    }

    #region Prefabs

    public GameObject LearningSectionPrefab;
    public GameObject TeachingSectionPrefab;
    public GameObject TutorialSectionPrefab;

    #endregion

    #region Initial Section

    [Header("Initial Section")] public ServiceSection.Section initialSection;

    #endregion
}
