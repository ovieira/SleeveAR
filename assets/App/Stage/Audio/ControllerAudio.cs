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
        serviceAudio.onPlayCountDown += this._onPlayCountDown;
        serviceAudio.onStopAudio += this._onStopAudio;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();

    }

    #endregion

    #region Service Audio

    protected void _onPlayCorrect(object sender, EventArgs e)
    {
        this.audioSource.volume = 1;

        this.audioSource.PlayOneShot(CorrectAudioClip);
    }

    protected void _onPlayCountDown(object sender, EventArgs e) {
        this.audioSource.volume = 1;

        this.audioSource.PlayOneShot(CountDownAudioClip);
    }

    protected void _onStopAudio(object sender, EventArgs e)
    {
        this.audioSource.Stop();
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
