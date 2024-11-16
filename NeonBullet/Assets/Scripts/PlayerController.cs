using System;
using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int lives = 3;
    public GameObject HUD;
    public Sprite[] damagedHUD;
    private bool isInvulnerable = false;

    public void Hit()
    {
        lives--;
        // if(lives <= 0)
        // {
        //     GameManager.Instance.GameOver();
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvulnerable)
        {
            StartCoroutine(IsEnumeratorSwitchState());
            Hit();
            DisplayDamageUI();
        }
    }

    private void DisplayDamageUI()
    {
        if (lives == 2)
        {
            HUD.GetComponent<Image>().sprite = damagedHUD[0];
        }
        else if (lives == 1)
        {
            HUD.GetComponent<Image>().sprite = damagedHUD[1];
        }
        else if (lives == 0)
        {
            HUD.GetComponent<Image>().sprite = damagedHUD[2];
        }
    }

    private IEnumerator IsEnumeratorSwitchState()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(5);
        isInvulnerable = false;
    }
}
