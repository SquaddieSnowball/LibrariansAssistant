namespace LibrariansAssistant.UI.Helpers;

/// <summary>
/// Provides methods for creating controls.
/// </summary>
internal static class ControlCreation
{
    #region "Main" view controls

    /// <summary>
    /// Creates a start message <see cref="Label"/> for the "Main" view.
    /// </summary>
    /// <returns>New instance of the <see cref="Label"/> class.</returns>
    public static Label MainCreateLabelStartMessage() =>
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
    /// Creates an items absence <see cref="Label"/> for the "Main" view.
    /// </summary>
    /// <returns>New instance of the <see cref="Label"/> class.</returns>
    public static Label MainCreateLabelDataNoItems() =>
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
    /// Creates an action <see cref="Button"/> for the "Main" view.
    /// </summary>
    /// <param name="text">Button text.</param>
    /// <returns>New instance of the <see cref="Button"/> class.</returns>
    public static Button MainCreateActionButton(string text) =>
        new()
        {
            AutoSize = true,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(10, 0, 10, 20),
            Text = text
        };

    #endregion

    #region "Add/Edit item" view controls

    /// <summary>
    /// Creates a <see cref="Label"/> for the "Add/Edit item" view.
    /// </summary>
    /// <param name="text">Label text.</param>
    /// <returns>New instance of the <see cref="Label"/> class.</returns>
    public static Label AddEditCreateLabel(string text) =>
        new()
        {
            Text = text
        };

    /// <summary>
    /// Creates a <see cref="TextBox"/> for the "Add/Edit item" view.
    /// </summary>
    /// <returns>New instance of the <see cref="TextBox"/> class.</returns>
    public static TextBox AddEditCreateTextBox() =>
        new()
        {
            BackColor = Color.FromArgb(60, 60, 60),
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.FromArgb(230, 230, 230),
            Width = 130
        };

    /// <summary>
    /// Creates a <see cref="CheckBox"/> for the "Add/Edit item" view.
    /// </summary>
    /// <param name="text">CheckBox text.</param>
    /// <returns>New instance of the <see cref="CheckBox"/> class.</returns>
    public static CheckBox AddEditCreateCheckBox(string text) =>
        new()
        {
            Text = text
        };

    /// <summary>
    /// Creates a <see cref="NumericUpDown"/> for the "Add/Edit item" view.
    /// </summary>
    /// <returns>New instance of the <see cref="NumericUpDown"/> class.</returns>
    public static NumericUpDown AddEditCreateNumericUpDown() =>
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
    /// Creates a <see cref="DateTimePicker"/> for the "Add/Edit item" view.
    /// </summary>
    /// <returns>New instance of the <see cref="DateTimePicker"/> class.</returns>
    public static DateTimePicker AddEditCreateDateTimePicker() =>
        new()
        {
            Format = DateTimePickerFormat.Short,
            MinDate = new DateTime(1900, 1, 1),
            MaxDate = DateTime.Now,
            Width = 130
        };

    #endregion

    #region "Pick items" view controls

    /// <summary>
    /// Creates a main control for the "Pick items" view.
    /// </summary>
    /// <param name="buttonPlus">Instance of the <see cref="Button"/> class that opens the view.</param>
    /// <param name="labelPicked">Instance of the <see cref="Label"/> class that shows the number of items picked.</param>
    /// <returns>New instance of the <see cref="TableLayoutPanel"/> class.</returns>
    public static TableLayoutPanel PickItemsCreateTableLayoutPanel(out Button buttonPlus, out Label labelPicked)
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

    #endregion

    #region "Pick period" view controls

    /// <summary>
    /// Creates a main control for the "Pick period" view.
    /// </summary>
    /// <param name="labelPickedPeriod">Instance of the <see cref="Label"/> class that shows the picked period.</param>
    /// <param name="buttonPick">Instance of the <see cref="Button"/> class that opens the view.</param>
    /// <returns>New instance of the <see cref="TableLayoutPanel"/> class.</returns>
    public static TableLayoutPanel PickPeriodCreateTableLayoutPanel(out Label labelPickedPeriod, out Button buttonPick)
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

    #endregion

    #region "Settings" view controls

    /// <summary>
    /// Creates a <see cref="TableLayoutPanel"/> for the "Settings" view that contains the control.
    /// </summary>
    /// <param name="control"><see cref="Control"/> to place inside the <see cref="TableLayoutPanel"/>.</param>
    /// <returns>New instance of the <see cref="TableLayoutPanel"/> class.</returns>
    public static TableLayoutPanel SettingsCreateControlTableLayoutPanel(Control control)
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
    /// Creates a <see cref="TableLayoutPanel"/> for the "Settings" view that contains the control and label.
    /// </summary>
    /// <param name="label"><see cref="Label"/> to place inside the <see cref="TableLayoutPanel"/>.</param>
    /// <param name="control"><see cref="Control"/> to place inside the <see cref="TableLayoutPanel"/>.</param>
    /// <returns>New instance of the <see cref="TableLayoutPanel"/> class.</returns>
    public static TableLayoutPanel SettingsCreateLabelControlTableLayoutPanel(Label label, Control control)
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
    /// Creates a <see cref="Label"/> for the "Settings" view.
    /// </summary>
    /// <param name="text">Label text.</param>
    /// <returns>New instance of the <see cref="Label"/> class.</returns>
    public static Label SettingsCreateLabel(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(30, 0, 0, 0),
            Text = text
        };

    /// <summary>
    /// Creates a <see cref="TextBox"/> for the "Settings" view.
    /// </summary>
    /// <returns>New instance of the <see cref="TextBox"/> class.</returns>
    public static TextBox SettingsCreateTextBox() =>
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
    /// Creates a <see cref="CheckBox"/> for the "Settings" view.
    /// </summary>
    /// <param name="text">CheckBox text.</param>
    /// <returns>New instance of the <see cref="CheckBox"/> class.</returns>
    public static CheckBox SettingsCreateCheckBox(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(33, 0, 0, 0),
            Text = text
        };

    #endregion
}