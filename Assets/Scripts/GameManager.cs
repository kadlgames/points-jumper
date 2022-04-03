public static class GameManager
{

    #region Properties

    public static bool IsGamePaused { get; private set; } = true;

    #endregion

    #region Methods

    public static void SetGamePause(bool a)
    {
        IsGamePaused = a;
    }

    #endregion

}


