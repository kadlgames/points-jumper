using UnityEngine;

/// <summary>
/// Provides screen utilities
/// </summary>
public static class ScreenUtils
{
    #region Fields

    // cached for efficient boundary checking

    #endregion

    /// <summary>
    /// Gets the left edge of the screen in world coordinates
    /// </summary>
    /// <value>left edge of the screen</value>
    public static float ScreenLeft { get; private set; }

    /// <summary>
    /// Gets the right edge of the screen in world coordinates
    /// </summary>
    /// <value>right edge of the screen</value>
    public static float ScreenRight { get; private set; }

    /// <summary>
    /// Gets the top edge of the screen in world coordinates
    /// </summary>
    /// <value>top edge of the screen</value>
    public static float ScreenTop { get; private set; }

    /// <summary>
    /// Gets the bottom edge of the screen in world coordinates
    /// </summary>
    /// <value>bottom edge of the screen</value>
    public static float ScreenBottom { get; private set; }
    

    #region Methods

    /// <summary>
    /// Initializes the screen utilities
    /// </summary>
    public static void Initialize()
    {
        // save screen edges in world coordinates
        if (Camera.main is null) return;
        var screenZ = -Camera.main.transform.position.z;
        var lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
        var upperRightCornerScreen = new Vector3(
            Screen.width, Screen.height, screenZ);
        var lowerLeftCornerWorld =
            Camera.main.ScreenToWorldPoint(lowerLeftCornerScreen);
        var upperRightCornerWorld =
            Camera.main.ScreenToWorldPoint(upperRightCornerScreen);
        ScreenLeft = lowerLeftCornerWorld.x;
        ScreenRight = upperRightCornerWorld.x;
        ScreenTop = upperRightCornerWorld.y;
        ScreenBottom = lowerLeftCornerWorld.y;
    }

    #endregion
}
