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

    #region CountDown

    public event EventHandler<EventArgs> onPlayCountDown;

    public void PlayCountDown() {
        Utils.LaunchEvent(this, onPlayCountDown);
    }

    #endregion

    #region Stop

    public event EventHandler<EventArgs> onStopAudio;

    public void StopAudio() {
        Utils.LaunchEvent(this, onStopAudio);
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
