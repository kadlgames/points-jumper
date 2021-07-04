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
    float upper;
    float lower;
    float left;
    float right;
    #endregion

    #region Properties
    public float Upper { get { return upper; }}
    public float Lower { get { return lower; }}
    public float Left { get { return left; }}
    public float Right { get { return right; }}
    #endregion

    public BoundsPack(float upper, float lower, float left, float right)
    {
        this.upper = upper;
        this.lower = lower;
        this.left = left;
        this.right = right;
    }
}
/// <summary>
/// Class for getting access to bounds coordinates 
/// </summary>
public static class Bounds
{
    #region Fields
    
    static int currentFrame = 0;

    #endregion

    #region Methods

    /// <summary>
    /// Get bound world coordinates
    /// </summary>
    /// <param name="selectedBound">The bound that coordinates you need to know</param>
    /// <returns></returns>
    public static float GetBound(Bound selectedBound)
    {
        if (Time.frameCount != currentFrame) ScreenUtils.Initialize();
        currentFrame = Time.frameCount;

        switch (selectedBound)
        {
            case Bound.Upper:
                return ScreenUtils.ScreenTop - (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) * 0.1f;
            case Bound.Lower:
                return ScreenUtils.ScreenBottom + (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) * 0.45f;
            case Bound.Left:
                return ScreenUtils.ScreenLeft + (ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft) * 0.15f;
            case Bound.Right:
                return ScreenUtils.ScreenRight - (ScreenUtils.ScreenRight - ScreenUtils.ScreenLeft) * 0.15f;
            default:
                return 0;
        }

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