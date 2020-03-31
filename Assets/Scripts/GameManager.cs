using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject pausePanel;
    public Button pauseResumeButton;
    public Button pauseRestartButton;
    public Button pauseMenuButton;

    public GameObject finalPanel;
    public Text finalText;
    public Button finalRestartButton;
    public Button finalMenuButton;

    public GameObject transition;

    [HideInInspector] public bool isPaused;
    [HideInInspector] public bool isEnded;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

        pauseResumeButton.onClick.AddListener(PauseGame);
        pauseRestartButton.onClick.AddListener(Restart);
        pauseMenuButton.onClick.AddListener(BackToMenu);

        finalRestartButton.onClick.AddListener(Restart);
        finalMenuButton.onClick.AddListener(BackToMenu);

        pausePanel.SetActive(false);
        finalPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isEnded)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            pausePanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.SetActive(true);
        }
    }
    void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void EndGame(bool isWin)
    {
        finalPanel.SetActive(true);
        if (isWin)
        {
            finalText.text = "Win";
        }
        else
        {
            finalText.text = "Lose";
        }
        transition.SetActive(false);
        isEnded = true;
        Time.timeScale = 0;
    }

}
