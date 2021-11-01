using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour
{
    /// <summary>
    /// Awake is called before Start
    /// </summary>
    private void Awake()
    {
        // initialize screen utils
        ScreenUtils.Initialize();
        GameManager.SetGamePause(true);
    }
}
