using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    public AudioClip _death;
    public AudioClip _fakeShot;
    public AudioClip _gameOver;
    [SerializeField] private AudioSource _heartBeatHurt1;
    [SerializeField] private AudioSource _heartBeatHurt2;
    public AudioClip _lanceGrenade;
    public AudioClip _touched;
    public AudioClip _victory;
    public AudioClip _blessure;
    public AudioClip _recharge;

    void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeath()
    {
        _audioSource.PlayOneShot(_death);
    }

    public void PlayFakeShot()
    {
        _audioSource.PlayOneShot(_fakeShot);
    }

    public void PlayGameOver()
    {
        _heartBeatHurt1.Stop();
        _heartBeatHurt2.Stop();
        _audioSource.PlayOneShot(_gameOver);
    }

    public void PlayHeartBeatHurt1()
    {
        _heartBeatHurt2.Stop();
        _heartBeatHurt1.Play();
    }

    public void PlayHeartBeatHurt2()
    {
        _heartBeatHurt1.Stop();
        _heartBeatHurt2.Play();
    }

    public void PlayLanceGrenade()
    {
        _audioSource.PlayOneShot(_lanceGrenade);
    }

    public void PlayTouched()
    {
        _audioSource.PlayOneShot(_touched);
    }

    public void PlayVictory()
    {
        _audioSource.PlayOneShot(_victory);
    }

    public void PlayBlessure()
    {
        _audioSource.PlayOneShot(_blessure);
    }

    public void PlayRecharge()
    {
        _audioSource.PlayOneShot(_recharge);
    }
}
