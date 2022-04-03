using UnityEngine;

/// <summary>
/// Bound identification
/// </summary>
public enum Bound
{
    Upper,
    Lower,
    Left,
    Right
}

public class BoundsPack
{
    #region Fields

    #endregion

    #region Properties
    public float Upper { get; }
    public float Lower { get; }
    public float Left { get; }
    public float Right { get; }

    #endregion

    public BoundsPack(float upper, float lower, float left, float right)
    {
        Upper = upper;
        Lower = lower;
        Left = left;
        Right = right;
    }
}
/// <summary>
/// Class for getting access to bounds coordinates 
/// </summary>
public static class Bounds
{
    #region Fields

    private static int _currentFrame;

    #endregion

    #region Methods

    /// <summary>
    /// Get bound world coordinates
    /// </summary>
    /// <param name="selectedBound">The bound that coordinates you need to know</param>
    /// <returns></returns>
    private static float GetBound(Bound selectedBound)
    {
        if (Time.frameCount != _currentFrame) ScreenUtils.Initialize();
        _currentFrame = Time.frameCount;

        return selectedBound switch
        {
            Bound.Upper => ScreenUtils.ScreenTop - (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) * 0.1f,
            Bound.Lower => ScreenUtils.ScreenBottom + (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) * 0.45f,
            Bound.Left => ScreenUtils.ScreenLeft + (ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft) * 0.15f,
            Bound.Right => ScreenUtils.ScreenRight - (ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft) * 0.15f,
            _ => 0
        };
    }

    /// <summary>
    /// The bounds pack that contains all bounds
    /// </summary>
    /// <returns></returns>
    public static BoundsPack GetBoundsPack()
    {
        return new BoundsPack(GetBound(Bound.Upper), GetBound(Bound.Lower), GetBound(Bound.Left), GetBound(Bound.Right));
    }

    #endregion
}