using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class GameManager
{    
    public static bool IsGamePaused { get; private set; } = true;
    public static bool IsGameOvered { get; private set; } = true;
    public static bool IsOnGOScreen { get; private set; } = false;

    private static Animator _menuAnimator;
    private static Text _scoreText;
    private static ScoreManager _scoreManager;
    private static GOTrigger _trigger;

    private static Animator _jpAnimator;

    public static void SetGamePause(bool a)
    {
        IsGamePaused = a;
    }

    public static void Init()
    {
        _jpAnimator = GameObject.Find("Circle").GetComponent<Animator>();
        _trigger = GameObject.Find("GOTrigger").GetComponent<GOTrigger>();
        _trigger.GameOver += OnGameOver;
        _menuAnimator = GameObject.Find("MenuCanvas").GetComponent<Animator>();
        _scoreText = GameObject.Find("gameOverPoints").GetComponent<Text>();
        _scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
    }

    public static void CloseGOMenu()
    {
        if (IsOnGOScreen)
        {
            _menuAnimator.SetTrigger("go_close");
            IsOnGOScreen = false;
        }
    }

    private static void OnGameOver()
    {
        IsGameOvered = true;
        IsOnGOScreen = true;
        _menuAnimator.SetTrigger("gp_close");

        //Make sure that Homer has done his thing)))


        _menuAnimator.SetTrigger("go_open");
        _scoreText.text = _scoreManager.Score.ToString();

        _jpAnimator.SetTrigger("hide");
    }

    public static void ResetGame() 
    {
        IsGameOvered = false;
        _trigger.ResetTrigger();
        _jpAnimator.SetTrigger("show");
    }

}


