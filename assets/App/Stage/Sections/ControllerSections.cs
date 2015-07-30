using UnityEngine;
using System.Collections;

public class ControllerSections : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Awake() {
        ServiceSection.instance.onSectionChanged += this._onSectionChanged;
    }

    void Start() {
        Utils.DestroyAllChildren(this.transform);
        this.createSection(LearningSectionPrefab);    
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
            default:
                break;
        }
    }
    #endregion

    private void createSection(GameObject prefab)
    {
        Utils.DestroyAllChildren(this.transform);
        Utils.AddChildren(this.transform, prefab);
    }

    #region Prefabs

    public GameObject LearningSectionPrefab;
    public GameObject TeachingSectionPrefab;

    #endregion
}
