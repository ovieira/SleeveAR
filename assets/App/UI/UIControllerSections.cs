using UnityEngine;
using System.Collections;

public class UIControllerSections : MonoBehaviour {




    #region LifeCycle
    // Use this for initialization
    void Awake() {
        ServiceSection.instance.onSectionChanged += this._onSectionChanged;

        Utils.DestroyAllChildren(this.transform);
    }

    void Start()
    {
        createSection(UILearningPrefab);
    }

    #endregion

    #region Service Section
    private void _onSectionChanged(object sender, System.EventArgs e)
    {
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

    private void createSection(GameObject prefab)
    {
        UIViewSection currentSection = this.GetComponentInChildren<UIViewSection>();

        if (currentSection == null) 
            instantiateSection(prefab);
        else
        {
            Destroy(currentSection.gameObject);
            instantiateSection(prefab);
        }
    }

    private void instantiateSection(GameObject prefab) {
        
        GameObject ob = Instantiate(prefab);
        ob.transform.SetParent(this.transform, false);
    }


    #region Prefabs

    public GameObject UILearningPrefab;
    public GameObject UITeachingPrefab;

    #endregion
}
