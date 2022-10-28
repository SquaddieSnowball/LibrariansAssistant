namespace LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers.Renderers;

internal sealed class MenuStripMainRenderer : ToolStripProfessionalRenderer
{
    internal MenuStripMainRenderer() : base() { }

    internal MenuStripMainRenderer(ProfessionalColorTable professionalColorTable) : base(professionalColorTable) { }

    protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
    {
        if (e.Item as ToolStripMenuItem is not null)
            e.ArrowColor = Color.FromArgb(230, 230, 230);

        base.OnRenderArrow(e);
    }
}