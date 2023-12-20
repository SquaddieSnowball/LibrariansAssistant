namespace LibrariansAssistant.UI.Entities.ColorTables;

/// <summary>
/// Represents the color table for the Settings view's MenuStrip.
/// </summary>
internal sealed class MenuStripSettingsColorTable : ProfessionalColorTable
{
    /// <summary>
    /// Get the border color to use with a ToolStripMenuItem.
    /// </summary>
    public override Color MenuItemBorder => Color.FromArgb(70, 70, 70);

    /// <summary>
    /// Gets the starting color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientBegin => Color.FromArgb(70, 70, 70);

    /// <summary>
    /// Gets the end color of the gradient used when the ToolStripMenuItem is selected.
    /// </summary>
    public override Color MenuItemSelectedGradientEnd => Color.FromArgb(70, 70, 70);
}