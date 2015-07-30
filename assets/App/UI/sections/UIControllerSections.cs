using UnityEngine;
using System.Collections;

public class UIControllerSections : Controller {

    #region LifeCycle
    // Use this for initialization
    protected override void Awake() {
        base.Awake();
        serviceSection.onSectionChanged += this._onSectionChanged;
        Utils.DestroyAllChildren(this.transform);
    }

    protected override void Start() {
        base.Start();
        this.createSection(UILearningPrefab);
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
                createSection(UILearningPrefab);
                break;
            case ServiceSection.Section.TEACHING:
                createSection(UITeachingPrefab);
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

    public GameObject UILearningPrefab;
    public GameObject UITeachingPrefab;

    #endregion
}
