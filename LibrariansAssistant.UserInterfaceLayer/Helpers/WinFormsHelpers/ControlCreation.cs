namespace LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers;

/// <summary>
/// Provides methods for creating controls.
/// </summary>
internal static class ControlCreation
{
    /// <summary>
    /// Creates a start message Label for the Main view.
    /// </summary>
    /// <returns>New instance of the Label class.</returns>
    internal static Label MainCreateLabelStartMessage() =>
        new()
        {
            AutoSize = false,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 14),
            ForeColor = Color.FromArgb(230, 230, 230),
            Text = "Set up the settings and go to \"Application \u2192 Connect to database\" to display data",
            TextAlign = ContentAlignment.MiddleCenter
        };

    /// <summary>
    /// Creates an items absence Label for the Main view.
    /// </summary>
    /// <returns>New instance of the Label class.</returns>
    internal static Label MainCreateLabelDataNoItems() =>
        new()
        {
            AutoSize = false,
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 12),
            ForeColor = Color.FromArgb(230, 230, 230),
            Text = "No items to show",
            TextAlign = ContentAlignment.MiddleCenter
        };

    /// <summary>
    /// Creates an action Button for the Main view.
    /// </summary>
    /// <param name="text">Button text.</param>
    /// <returns>New instance of the Button class.</returns>
    internal static Button MainCreateActionButton(string text) =>
        new()
        {
            AutoSize = true,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(10, 0, 10, 20),
            Text = text
        };

    /// <summary>
    /// Creates a Label for the Add/Edit Item view.
    /// </summary>
    /// <param name="text">Label text.</param>
    /// <returns>New instance of the Label class.</returns>
    internal static Label AddEditCreateLabel(string text) =>
        new()
        {
            Text = text
        };

    /// <summary>
    /// Creates a TextBox for the Add/Edit Item view.
    /// </summary>
    /// <returns>New instance of the TextBox class.</returns>
    internal static TextBox AddEditCreateTextBox() =>
        new()
        {
            BackColor = Color.FromArgb(60, 60, 60),
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.FromArgb(230, 230, 230),
            Width = 130
        };

    /// <summary>
    /// Creates a CheckBox for the Add/Edit Item view.
    /// </summary>
    /// <param name="text">CheckBox text.</param>
    /// <returns>New instance of the CheckBox class.</returns>
    internal static CheckBox AddEditCreateCheckBox(string text) =>
        new()
        {
            Text = text
        };

    /// <summary>
    /// Creates a NumericUpDown for the Add/Edit Item view.
    /// </summary>
    /// <returns>New instance of the NumericUpDown class.</returns>
    internal static NumericUpDown AddEditCreateNumericUpDown() =>
        new()
        {
            BackColor = Color.FromArgb(60, 60, 60),
            ForeColor = Color.FromArgb(230, 230, 230),
            Minimum = 1,
            Maximum = 100,
            Value = 100,
            Width = 50
        };

    /// <summary>
    /// Creates a DateTimePicker for the Add/Edit Item view.
    /// </summary>
    /// <returns>New instance of the DateTimePicker class.</returns>
    internal static DateTimePicker AddEditCreateDateTimePicker() =>
        new()
        {
            Format = DateTimePickerFormat.Short,
            MinDate = new DateTime(1900, 1, 1),
            MaxDate = DateTime.Now,
            Width = 130
        };

    /// <summary>
    /// Creates a main control for the Pick Items view.
    /// </summary>
    /// <param name="buttonPlus">Instance of the Button class that opens the view.</param>
    /// <param name="labelPicked">Instance of the Label class that shows the number of items picked.</param>
    /// <returns>New instance of the TableLayoutPanel class.</returns>
    internal static TableLayoutPanel PickItemsCreateTableLayoutPanel(out Button buttonPlus, out Label labelPicked)
    {
        buttonPlus = new Button()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = false,
            FlatStyle = FlatStyle.Flat,
            Height = 30,
            Margin = new Padding(0),
            Text = "+",
            Width = 30
        };

        labelPicked = new Label()
        {
            Anchor = AnchorStyles.Left,
            Margin = new Padding(10, 0, 0, 0),
            Text = "\u2717"
        };

        TableLayoutPanel tableLayoutPanel = new()
        {
            Height = buttonPlus.Height
        };

        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        _ = tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(buttonPlus, 0, 0);
        tableLayoutPanel.Controls.Add(labelPicked, 1, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    /// <summary>
    /// Creates a main control for the Pick Period view.
    /// </summary>
    /// <param name="labelPickedPeriod">Instance of the Label class that shows the picked period.</param>
    /// <param name="buttonPick">Instance of the Button class that opens the view.</param>
    /// <returns>New instance of the TableLayoutPanel class.</returns>
    internal static TableLayoutPanel PickPeriodCreateTableLayoutPanel(out Label labelPickedPeriod, out Button buttonPick)
    {
        Label labelPeriod = new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10),
            Text = "Period:"
        };

        labelPickedPeriod = new Label()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(0, 0, 0, 10),
            Text = "none"
        };

        buttonPick = new Button()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = false,
            FlatStyle = FlatStyle.Flat,
            Height = 30,
            Margin = new Padding(10, 0, 0, 10),
            Text = "\uFE19",
            Width = 30
        };

        TableLayoutPanel tableLayoutPanel = new()
        {
            Width = 270
        };

        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        _ = tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(labelPeriod, 0, 0);
        tableLayoutPanel.Controls.Add(labelPickedPeriod, 1, 0);
        tableLayoutPanel.Controls.Add(buttonPick, 2, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    /// <summary>
    /// Creates a TableLayoutPanel for the Settings view that contains the control.
    /// </summary>
    /// <param name="control">Control to place inside the TableLayoutPanel.</param>
    /// <returns>New instance of the TableLayoutPanel class.</returns>
    internal static TableLayoutPanel SettingsCreateControlTableLayoutPanel(Control control)
    {
        TableLayoutPanel tableLayoutPanel = new()
        {
            Height = control.Height
        };

        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        _ = tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(control, 0, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    /// <summary>
    /// Creates a TableLayoutPanel for the Settings view that contains the control and label.
    /// </summary>
    /// <param name="label">Label to place inside the TableLayoutPanel.</param>
    /// <param name="control">Control to place inside the TableLayoutPanel.</param>
    /// <returns>New instance of the TableLayoutPanel class.</returns>
    internal static TableLayoutPanel SettingsCreateLabelControlTableLayoutPanel(Label label, Control control)
    {
        TableLayoutPanel tableLayoutPanel = new()
        {
            Height = (label.Height > control.Height) ? label.Height : control.Height
        };

        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        _ = tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        _ = tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(label, 0, 0);
        tableLayoutPanel.Controls.Add(control, 1, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    /// <summary>
    /// Creates a Label for the Settings view.
    /// </summary>
    /// <param name="text">Label text.</param>
    /// <returns>New instance of the Label class.</returns>
    internal static Label SettingsCreateLabel(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(30, 0, 0, 0),
            Text = text
        };

    /// <summary>
    /// Creates a TextBox for the Settings view.
    /// </summary>
    /// <returns>New instance of the TextBox class.</returns>
    internal static TextBox SettingsCreateTextBox() =>
        new()
        {
            Anchor = AnchorStyles.Left,
            BackColor = Color.FromArgb(60, 60, 60),
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.FromArgb(230, 230, 230),
            Margin = new Padding(0),
            Width = 180
        };

    /// <summary>
    /// Creates a CheckBox for the Settings view.
    /// </summary>
    /// <param name="text">CheckBox text.</param>
    /// <returns>New instance of the CheckBox class.</returns>
    internal static CheckBox SettingsCreateCheckBox(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(33, 0, 0, 0),
            Text = text
        };
}