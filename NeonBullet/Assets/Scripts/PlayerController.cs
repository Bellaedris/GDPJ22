using System;
using System.Collections;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private AudioManager _audioManager;

    public int lives = 3;
    public GameObject HUD;
    public Sprite[] damagedHUD;
    private bool isInvulnerable = false;

    //Fonction principale gerant la prise de degat
    public void Hit(int degats)
    {
        StartCoroutine(IsEnumeratorSwitchState());
        lives -= degats;
        DisplayDamageUI();
    }

    public void win()
    {
        GameManager.Instance.Win();
    }

    //Declenche la prise de degat par collision
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvulnerable)
        {
            Hit(1);
        }
    }

    //Change le hud affichage des degats apres la prise d'un coup
    private void DisplayDamageUI()
    {
        _audioManager.PlayBlessure();
        HUD.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
        if (lives == 2)
        {
            _audioManager.PlayHeartBeatHurt1();
            HUD.GetComponent<Image>().sprite = damagedHUD[0];
        }
        else if (lives == 1)
        {
            _audioManager.PlayHeartBeatHurt2();
            HUD.GetComponent<Image>().sprite = damagedHUD[1];
        }
        else if (lives <= 0)
        {
            _audioManager.PlayDeath();
            HUD.GetComponent<Image>().sprite = damagedHUD[2];
            GameManager.Instance.Lose();
        }
    }

    //Assure l'immortalit  temporaire du joueur apres un coup
    private IEnumerator IsEnumeratorSwitchState()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(5);
        isInvulnerable = false;
    }
}
