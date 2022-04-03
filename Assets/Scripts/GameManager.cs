using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameManager
{    
    public static bool IsGamePaused { get; private set; } = true;

    public static void SetGamePause(bool a)
    {
        IsGamePaused = a;
    }

    public static void Init()
    {
        GameObject.Find("GOTrigger").GetComponent<GOTrigger>().GameOver += OnGameOver;
    }

    private static void OnGameOver()
    {
        SceneManager.LoadScene("Gameplay");
    }
}


