using UnityEngine;
using System.Collections;

public class ControllerSections : MonoBehaviour {

    #region LifeCycle
    // Use this for initialization
    void Awake() {
        ServiceSection.instance.onSectionChanged += this._onSectionChanged;
    }

    void Start() {
        currentSection = this.GetComponentInChildren<ViewSection>();

        createSection(LearningSectionPrefab);
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

    private void createSection(GameObject prefab) {

        if (currentSection == null)
            instantiateSection(prefab);
        else {
            Destroy(currentSection.gameObject);
            instantiateSection(prefab);
        }
    }

    private void instantiateSection(GameObject prefab) {

        GameObject ob = Instantiate(prefab);
        currentSection = ob.GetComponent<ViewSection>();
        //ob.transform.SetParent(this.transform);
        ob.transform.parent = this.transform;
    }


    #region Prefabs

    public GameObject LearningSectionPrefab;
    public GameObject TeachingSectionPrefab;
    private ViewSection currentSection;

    #endregion
}
