using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] GameObject _gameOver;
    [SerializeField] GameObject _victory;
    [SerializeField] GameObject _playerACacher;

    private GameManager _gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

            if (_gameManager.isVictory())
            {
                _victory.SetActive(true);
                _gameOver.SetActive(false);
                _playerACacher.SetActive(false);
            }
            else
            {
                _victory.SetActive(false);
                _gameOver.SetActive(true);
                _playerACacher.SetActive(true);
            }
    }
}
