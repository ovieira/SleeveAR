using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class ControllerAudio : Controller {

    #region LifeCycle

    protected override void Start()
    {
        base.Start();
        serviceAudio.onPlayCorrect += this._onPlayCorrect;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();

    }

    #endregion

    #region Service Audio

    protected void _onPlayCorrect(object sender, EventArgs e)
    {
        this.audioSource.PlayOneShot(CorrectAudioClip);
    }

    #endregion


    #region Audio References
    public AudioClip CorrectAudioClip;
    public AudioClip WrongAudioClip;
    public AudioClip CountDownAudioClip;
    #endregion

    #region AudioSource

    public AudioSource audioSource
    {
        get { return this.GetComponent<AudioSource>(); }
    }

    #endregion
}
