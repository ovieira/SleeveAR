using System;
using UnityEngine;
using System.Collections;

public class ServiceAudio {

    #region Correct

    public event EventHandler<EventArgs> onPlayCorrect;

    public void PlayCorrect()
    {
        Utils.LaunchEvent(this, onPlayCorrect);
    } 

    #endregion

    #region Singleton

    private static ServiceAudio _instance;


    public static ServiceAudio instance {
        get {
            if (_instance == null) {
                _instance = new ServiceAudio();
            }
            return _instance;
        }
    }

    #endregion
}
