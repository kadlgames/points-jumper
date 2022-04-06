using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameManager
{    
    public static bool IsGamePaused { get; private set; } = true;
    public static bool IsGameOvered { get; private set; } = false;

    private static Animator _menuAnimator;
    private static Text _scoreText;
    private static ScoreManager _scoreManager;

    public static void SetGamePause(bool a)
    {
        IsGamePaused = a;
    }

    public static void Init()
    {
        GameObject.Find("GOTrigger").GetComponent<GOTrigger>().GameOver += OnGameOver;
        _menuAnimator = GameObject.Find("MenuCanvas").GetComponent<Animator>();
        _scoreText = GameObject.Find("gameOverPoints").GetComponent<Text>();
        _scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    public static void CloseGOMenu()
    {
        IsGameOvered = false;
        _menuAnimator.SetTrigger("go_close");
    }

    private static void OnGameOver()
    {
        IsGameOvered = true;
        _menuAnimator.SetTrigger("gp_close");
        _menuAnimator.SetTrigger("go_open");
        _scoreText.text = _scoreManager.Score.ToString();
    }

}


