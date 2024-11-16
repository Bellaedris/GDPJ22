using System;
using UnityEditor.Build.Content;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives = 3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hit()
    {
        lives--;
        // if(lives <= 0)
        // {
        //     GameManager.Instance.GameOver();
        // }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.CompareTag("Enemy"))
            Hit();
    }
}
