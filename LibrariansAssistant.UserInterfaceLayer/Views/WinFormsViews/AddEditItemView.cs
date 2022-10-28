namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

internal sealed partial class AddEditItemView : Form
{
    public string FieldsTitle
    {
        get => groupBoxFields.Text;
        set => groupBoxFields.Text = value;
    }

    internal AddEditItemView()
    {
        InitializeComponent();
    }

    internal AddEditItemView AddField(string name, Control control)
    {
        var labelName = new Label()
        {
            Anchor = AnchorStyles.Left,
            Margin = new Padding(30, 0, 0, 0),
            Text = name
        };

        control.Anchor = AnchorStyles.Left;

        int maxHeight = (labelName.Height > control.Height) ? labelName.Height : control.Height;

        Height += maxHeight + 15;

        tableLayoutPanelFields.RowStyles.Insert(
            tableLayoutPanelFields.RowStyles.Count - 1,
            new RowStyle(SizeType.Absolute, maxHeight + 15));

        tableLayoutPanelFields.Controls.Add(labelName, 0, tableLayoutPanelFields.RowCount - 1);
        tableLayoutPanelFields.Controls.Add(control, 1, tableLayoutPanelFields.RowCount - 1);

        tableLayoutPanelFields.RowCount++;

        return this;
    }
}