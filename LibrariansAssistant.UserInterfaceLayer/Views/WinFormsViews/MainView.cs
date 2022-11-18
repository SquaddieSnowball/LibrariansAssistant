using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers.ColorTables;
using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers.Renderers;

namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

internal sealed partial class MainView : Form
{
    internal MainView()
    {
        InitializeComponent();
        SubscribeToControlEvents();
        InitializeView();
    }

    private void SubscribeToControlEvents()
    {
        exitToolStripMenuItem.Click += ExitToolStripMenuItemOnClick;
    }

    private void ExitToolStripMenuItemOnClick(object? sender, EventArgs e) =>
        Close();

    private void InitializeView()
    {
        menuStripMain.Renderer = new MenuStripMainRenderer(new MenuStripMainColorTable());
    }

    private void AddCustomFilter(Control control)
    {
        control.Anchor = AnchorStyles.Left;

        tableLayoutPanelCustomFilters.ColumnStyles.Insert(
            tableLayoutPanelCustomFilters.ColumnStyles.Count - 1,
            new ColumnStyle(SizeType.Absolute, control.Width + 20));

        tableLayoutPanelCustomFilters.Controls.Add(control, tableLayoutPanelCustomFilters.ColumnCount - 1, 0);

        tableLayoutPanelCustomFilters.ColumnCount++;
    }

    private void ClearCustomFilters()
    {
        tableLayoutPanelCustomFilters.Controls.Clear();

        while (tableLayoutPanelCustomFilters.ColumnStyles.Count > 1)
            tableLayoutPanelCustomFilters.ColumnStyles.RemoveAt(0);

        tableLayoutPanelCustomFilters.ColumnCount = 1;
    }

    private void AddCustomAction(Control control)
    {
        control.Anchor = AnchorStyles.Bottom;

        tableLayoutPanelCustomActions.ColumnStyles.Insert(
            tableLayoutPanelCustomActions.ColumnStyles.Count - 1,
            new ColumnStyle(SizeType.Absolute, control.Width + 20));

        tableLayoutPanelCustomActions.Controls.Add(control, tableLayoutPanelCustomActions.ColumnCount - 1, 0);

        tableLayoutPanelCustomActions.ColumnCount++;
    }

    private void ClearCustomActions()
    {
        tableLayoutPanelCustomActions.Controls.Clear();

        while (tableLayoutPanelCustomActions.ColumnStyles.Count > 2)
            tableLayoutPanelCustomActions.ColumnStyles.RemoveAt(1);

        tableLayoutPanelCustomActions.ColumnCount = 2;
    }
}