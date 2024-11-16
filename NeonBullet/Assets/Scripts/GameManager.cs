using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    static public GameManager Instance { get { return instance; } }

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
        
    }

    void Update()
    {
        
    }

    public void LaunchGameScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 2);
    }

    public void LaunchDeadScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 3);
    }

    public void LaunchMenuScene()
    {
        SceneManager.LoadScene(sceneBuildIndex: 1);
    }
}
