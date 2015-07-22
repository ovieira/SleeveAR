using System;
using UnityEngine;
using System.Collections;

public class ServiceMedia {

    #region Record

    public EventHandler<EventArgs> onStartRecording, onStopRecording;

    public void Record()
    {
        Utils.LaunchEvent(this, onStartRecording);
    }

    public void Stop()
    {
        Utils.LaunchEvent(this, onStopRecording);
    }

    #endregion

    #region Play

    public event EventHandler<EventArgs> onStartPlaying;

    public void Play()
    {
        Utils.LaunchEvent(this, onStartPlaying);
    }

    #endregion

    #region Singleton

    private static ServiceMedia _instance;


    public static ServiceMedia instance {
        get {
            if (_instance == null) {
                _instance = new ServiceMedia();
            }
            return _instance;
        }
    }

    #endregion
}
