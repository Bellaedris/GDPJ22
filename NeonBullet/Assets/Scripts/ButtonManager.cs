using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonManager : MonoBehaviour
{
    enum Type { toMenu, toGame }
    [SerializeField] private Type _type;

    private Button _btn;

    void Start()
    {
        _btn = gameObject.GetComponent<Button>();
        switch(_type)
        {
            case Type.toMenu:
                _btn.onClick.AddListener(GoToMenu);
                break;
            case Type.toGame:
                _btn.onClick.AddListener(GoToGame);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        
    }

    private void GoToMenu()
    {
        GameManager.Instance.LaunchMenuScene();
    }

    private void GoToGame()
    {
        GameManager.Instance.LaunchGameScene();
    }
}
