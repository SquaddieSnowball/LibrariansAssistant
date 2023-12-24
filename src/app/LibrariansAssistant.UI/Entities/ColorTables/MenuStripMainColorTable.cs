namespace LibrariansAssistant.UI.Entities.ColorTables;

/// <summary>
/// Represents the color table for the "Main" view's <see cref="MenuStrip"/>.
/// </summary>
internal sealed class MenuStripMainColorTable : ProfessionalColorTable
{
    /// <summary>
    /// Gets the solid background color of the <see cref="ToolStripDropDown"/>.
    /// </summary>
    public override Color ToolStripDropDownBackground => Color.FromArgb(50, 50, 50);

    /// <summary>
    /// Get the border color to use with a <see cref="ToolStripMenuItem"/>.
    /// </summary>
    public override Color MenuItemBorder => Color.FromArgb(150, 150, 150);

    /// <summary>
    /// Gets the starting color of the gradient used in the image margin of a <see cref="ToolStripDropDownMenu"/>.
    /// </summary>
    public override Color ImageMarginGradientBegin => Color.FromArgb(50, 50, 50);

    /// <summary>
    /// Gets the end color of the gradient used in the image margin of a <see cref="ToolStripDropDownMenu"/>.
    /// </summary>
    public override Color ImageMarginGradientEnd => Color.FromArgb(50, 50, 50);

    /// <summary>
    /// Gets the starting color of the gradient used when the <see cref="ToolStripMenuItem"/> is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientBegin => Color.FromArgb(70, 70, 70);

    /// <summary>
    /// Gets the end color of the gradient used when the <see cref="ToolStripMenuItem"/> is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientEnd => Color.FromArgb(70, 70, 70);

    /// <summary>
    /// Gets the starting color of the gradient used when a top-level <see cref="ToolStripMenuItem"/> is pressed.
    /// </summary>
    public override Color MenuItemPressedGradientBegin => Color.FromArgb(50, 50, 50);

    /// <summary>
    /// Gets the end color of the gradient used when a top-level <see cref="ToolStripMenuItem"/> is pressed.
    /// </summary>
    public override Color MenuItemPressedGradientEnd => Color.FromArgb(50, 50, 50);
}