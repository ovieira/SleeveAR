using UnityEngine;
using System.Collections;

public class ServiceMovementGuidance {

    #region Singleton

    private static ServiceMovementGuidance _instance;

    public static ServiceMovementGuidance instance {
        get {
            if (_instance == null) {
                _instance = new ServiceMovementGuidance();
            }
            return _instance;
        }
    }

    #endregion
}
