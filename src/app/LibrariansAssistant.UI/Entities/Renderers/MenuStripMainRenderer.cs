namespace LibrariansAssistant.UI.Entities.Renderers;

/// <summary>
/// Represents the renderer for the "Main" view's <see cref="MenuStrip"/>.
/// </summary>
internal sealed class MenuStripMainRenderer : ToolStripProfessionalRenderer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MenuStripMainRenderer"/> class.
    /// </summary>
    public MenuStripMainRenderer() : base() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuStripMainRenderer"/> class with the specified color table.
    /// </summary>
    /// <param name="professionalColorTable">Color table.</param>
    public MenuStripMainRenderer(ProfessionalColorTable professionalColorTable) : base(professionalColorTable) { }

    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        if (e.Item as ToolStripMenuItem is not null)
            e.ArrowColor = Color.FromArgb(230, 230, 230);

        base.OnRenderArrow(e);
    }
}