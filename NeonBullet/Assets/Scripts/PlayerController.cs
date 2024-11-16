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

    //Fonction principale gerant la prise de degat
    public void Hit()
    {
        StartCoroutine(IsEnumeratorSwitchState());
        lives--;
        DisplayDamageUI();
        // if(lives <= 0)
        // {
        //     GameManager.Instance.GameOver();
        // }
    }

    //Declenche la prise de degat par collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvulnerable)
        {
            Hit();
        }
    }

    //Change le hud affichage des degats apres la prise d'un coup
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

    //Assure l'immortalité temporaire du joueur apres un coup
    private IEnumerator IsEnumeratorSwitchState()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(5);
        isInvulnerable = false;
    }
}
