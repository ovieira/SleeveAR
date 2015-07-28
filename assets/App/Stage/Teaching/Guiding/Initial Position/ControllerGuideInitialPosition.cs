using UnityEngine;
using System.Collections;

public class ControllerGuideInitialPosition : Controller {

    
    
    #region LifeCycle
    protected override void Start() {
        base.Start();
        ServiceGuideInitialPosition.instance.onModeChanged += this._onModeChanged;
        serviceExercise.onStart += this._onStart;
    }

    private void _onStart(object sender, System.EventArgs e) {
        updateChildren();
    }

    

    protected override void OnDestroy() {
        base.OnDestroy();
        ServiceGuideInitialPosition.instance.onModeChanged -= this._onModeChanged;

    } 
    #endregion

    #region Service Guide IP

    private void _onModeChanged(object sender, System.EventArgs e)
    {
        updateChildren();
    }

    private void updateChildren()
    {
        switch (ServiceGuideInitialPosition.instance.selected)
        {
            case ServiceGuideInitialPosition.Mode.Unidirectional:
                Utils.DestroyAllChildren(this.transform);
                Utils.AddChildren(this.transform, UnidirectionalPrefab);
                break;
            case ServiceGuideInitialPosition.Mode.Bidirectional:
                Utils.DestroyAllChildren(this.transform);
                Utils.AddChildren(this.transform, BidirectionalPrefab);
                break;
            default:
                break;
        }
    }

    #endregion


    #region Prefabs

    public GameObject UnidirectionalPrefab;
    public GameObject BidirectionalPrefab;

    #endregion
}
