using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public GameManager Instance { get { return instance; } }

    public AudioManager _audioManager;
    private AudioSource _audioSource;

    private bool _switchToGame = false;
    private bool _switchToMenu = false;
    private bool _switchToDead = false;
    private bool _canSwitchScene = false;

    private bool _isVictory = false;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
            Destroy(this);
    }

    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.Play();
    }

    void Update()
    {
        if (_switchToGame && _canSwitchScene)
            SceneManager.LoadScene(sceneBuildIndex: 1);
        else if (_switchToDead && _canSwitchScene)
            SceneManager.LoadScene(sceneBuildIndex: 2);
        else if (_switchToMenu && _canSwitchScene)
            SceneManager.LoadScene(sceneBuildIndex: 0);

        if (_switchToDead && SceneManager.GetActiveScene().buildIndex == 2)
            _switchToDead = false;
        else if (_switchToGame && SceneManager.GetActiveScene().buildIndex == 1)
            _switchToGame = false;
        else if (_switchToMenu && SceneManager.GetActiveScene().buildIndex == 0)
            _switchToMenu = false;
    }

    public void LaunchGameScene()
    {
        _audioSource.Play();
        _switchToGame = true;
        _audioManager.GetComponent<AudioSource>().Stop();
        StartCoroutine(WaitToSwitch());
    }

    public void LaunchDeadScene()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _switchToDead = true;
        StartCoroutine(WaitToSwitch());
    }

    public void LaunchMenuScene()
    {
        _audioSource.Play();
        _switchToMenu = true;
        _audioManager.GetComponent<AudioSource>().Stop();
        StartCoroutine(WaitToSwitch());
    }

    public void Win()
    {
        _isVictory = true;
        LaunchDeadScene();
        _audioSource.Pause();
        _audioManager.PlayVictory();
    }

    public void Lose()
    {
        _isVictory = false;
        LaunchDeadScene();
        _audioSource.Pause();
        _audioManager.PlayGameOver();
    }

    private IEnumerator WaitToSwitch()
    {
        _canSwitchScene = false;
        yield return new WaitForSeconds(0.3f);
        _canSwitchScene = true;
    }

    public bool isVictory()
    {
        return _isVictory;
    }
}
