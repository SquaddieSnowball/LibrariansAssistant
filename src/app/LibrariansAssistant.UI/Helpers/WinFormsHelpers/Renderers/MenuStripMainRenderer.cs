namespace LibrariansAssistant.UI.Helpers.WinFormsHelpers.Renderers;

/// <summary>
/// Represents the renderer for the Main view's MenuStrip.
/// </summary>
internal sealed class MenuStripMainRenderer : ToolStripProfessionalRenderer
{
    /// <summary>
    /// Initializes a new instance of the MenuStripMainRenderer class.
    /// </summary>
    internal MenuStripMainRenderer() : base() { }

    /// <summary>
    /// Initializes a new instance of the MenuStripMainRenderer class with the specified color table.
    /// </summary>
    /// <param name="professionalColorTable">Color table.</param>
    internal MenuStripMainRenderer(ProfessionalColorTable professionalColorTable) : base(professionalColorTable) { }

    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        if (e.Item as ToolStripMenuItem is not null)
            e.ArrowColor = Color.FromArgb(230, 230, 230);

        base.OnRenderArrow(e);
    }
}