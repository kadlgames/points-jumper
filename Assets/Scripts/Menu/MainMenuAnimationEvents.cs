using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimationEvents : MonoBehaviour
{
    /// <summary>
    /// Дает доступ ивенту из анимации меню
    /// </summary>
    /// <param name="value"></param>
    public void ChangeGamePauseState()
    {
        GameManager.SetGamePause(GameManager.IsGamePaused != true);
    }
}
