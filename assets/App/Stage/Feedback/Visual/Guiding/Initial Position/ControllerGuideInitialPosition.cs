using System;
using UnityEngine;

public class ControllerGuideInitialPosition : Controller {
    #region LifeCycle

    protected override void Start() {
        base.Start();
        ServiceGuideInitialPosition.instance.onModeChanged += _onModeChanged;
        serviceExercise.onStart += _onStart;

        if (serviceExercise.start)
        {
            updateChildren();
        }
    }

    private void _onStart(object sender, EventArgs e) {
        updateChildren();
    }

    protected override void OnDestroy() {
        base.OnDestroy();
        ServiceGuideInitialPosition.instance.onModeChanged -= _onModeChanged;
        serviceExercise.onStart -= _onStart;
    }

    #endregion

    #region Service Guide IP

    private void _onModeChanged(object sender, EventArgs e) {
        if (serviceExercise.start)
            updateChildren();
    }

    private void updateChildren() {
        switch (ServiceGuideInitialPosition.instance.selected) {
            case ServiceGuideInitialPosition.Mode.Unidirectional:
                Utils.DestroyAllChildren(transform);
                Utils.AddChildren(transform, UnidirectionalPrefab);
                break;
            case ServiceGuideInitialPosition.Mode.Bidirectional:
                Utils.DestroyAllChildren(transform);
                Utils.AddChildren(transform, BidirectionalPrefab);
                break;
            case ServiceGuideInitialPosition.Mode.ArrowsDirection:
                Utils.DestroyAllChildren(transform);
                Utils.AddChildren(transform, ArrowsPrefab);
                break;
            default:
                break;
        }
    }

    #endregion

    #region Prefabs
    [Header("Guiding Prefabs")]
    public GameObject UnidirectionalPrefab;
    public GameObject BidirectionalPrefab;
    public GameObject ArrowsPrefab;

    #endregion
}