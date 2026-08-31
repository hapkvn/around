using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    private bool isGameStarted = false;
    public static bool playAgain = false;
    [SerializeField] private GameObject CameraIn;
    [SerializeField] private GameObject ButtonStart;
    [SerializeField] private GameObject ButtonControl;
    [SerializeField] private GameObject ButtonPause;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject GameOverMenu;


    public static StartGame intance { get; private set; }

    private void Awake()
    {
        if (intance == null) { intance = this; }
        else { Destroy(gameObject); }
    }
    private void Start()
    {
        if (playAgain == true)
        {
            playAgain = false;
            StartGameButton();
        }
    }

    public void StartGameButton()
    {
        isGameStarted = true;
        CameraIn.SetActive(false);
        ButtonStart.SetActive(false);
        ButtonControl.SetActive(true);
        ButtonPause.SetActive(true);
        PauseMenu.SetActive(false);

    }
    public bool isS()
    {
        return isGameStarted;
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        ButtonControl.SetActive(false);
        ButtonPause.SetActive(false);
        PauseMenu.SetActive(true);

    }

    public void ResetGame()
    {
        Time.timeScale = 1f;
        
        SceneManager.LoadScene("Game");
    }
    public void RetartGame()
    {
        playAgain = true;
        Time.timeScale = 1f;


        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

    }

    public void ContinuesGame()
    {
        Time.timeScale = 1f;
        ButtonControl.SetActive(true);
        ButtonPause.SetActive(true);
        PauseMenu.SetActive(false);

    }

    public void GameOver()
    {        
        isGameStarted = false;
        ButtonControl.SetActive(false);
        ButtonPause.SetActive(false);
        PauseMenu.SetActive(false);
        ButtonStart.SetActive(false);
        GameOverMenu.SetActive(true);

    }
}
