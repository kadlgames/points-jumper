using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    #region  Fields
    
    static bool isGamePaused = true;

    #endregion

    #region Properties

    public static bool IsGamePaused
    {
        get { return isGamePaused; }
    }

    #endregion

    #region Methods

    public static void SetGamePause(bool a)
    {
        isGamePaused = a;
    }

    #endregion

}


