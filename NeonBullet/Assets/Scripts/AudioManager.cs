using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _death;
    [SerializeField] private AudioSource _fakeShot;
    [SerializeField] private AudioSource _gameOver;
    [SerializeField] private AudioSource _heartBeatHurt1;
    [SerializeField] private AudioSource _heartBeatHurt2;
    [SerializeField] private AudioSource _lanceGrenade;
    [SerializeField] private AudioSource _touched;
    [SerializeField] private AudioSource _victory;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDeath()
    {
        _death.Play();
    }

    public void PlayFakeShot()
    {
        _fakeShot.Play();
    }

    public void PlayGameOver()
    {
        _gameOver.Play();
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
        _lanceGrenade.Play();
    }

    public void PlayTouched()
    {
        _touched.Play();
    }

    public void PlayVictory()
    {
        _victory.Play();
    }
}
