namespace LibrariansAssistant.UI.Views.WinFormsViews;

/// <summary>
/// Represents the Add/Edit Item view of the application.
/// </summary>
internal sealed partial class AddEditItemView : Form
{
    /// <summary>
    /// Gets or sets the title of the fields.
    /// </summary>
    internal string FieldsTitle
    {
        get => groupBoxFields.Text;
        set => groupBoxFields.Text = value;
    }

    /// <summary>
    /// Initializes a new instance of the AddEditItemView class.
    /// </summary>
    internal AddEditItemView()
    {
        InitializeComponent();
        SubscribeToControlEvents();
    }

    /// <summary>
    /// Adds a new field to the view.
    /// </summary>
    /// <param name="name">Field name.</param>
    /// <param name="control">Field control.</param>
    /// <returns>Current instance of the AddEditItemView object.</returns>
    internal AddEditItemView AddField(string name, Control control)
    {
        Label labelName = new()
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

    private void SubscribeToControlEvents()
    {
        buttonConfirm.Click += ButtonConfirmOnClick;
    }

    private void ButtonConfirmOnClick(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.OK;

        Close();
    }
}