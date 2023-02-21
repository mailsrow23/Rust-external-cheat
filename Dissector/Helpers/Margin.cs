/// <summary>
/// Represents the margins of a rectangular area.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct MARGIN
{
    /// <summary>
    /// The width of the left margin.
    /// </summary>
    public int cxLeftWidth;

    /// <summary>
    /// The width of the right margin.
    /// </summary>
    public int cxRightWidth;

    /// <summary>
    /// The height of the top margin.
    /// </summary>
    public int cyTopHeight;

    /// <summary>
    /// The height of the bottom margin.
    /// </summary>
    public int cyBottomHeight;
}
