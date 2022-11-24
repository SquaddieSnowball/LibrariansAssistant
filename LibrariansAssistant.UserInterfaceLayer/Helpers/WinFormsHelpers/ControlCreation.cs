namespace LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers;

internal static class ControlCreation
{
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

    internal static Button MainCreateActionButton(string text) =>
        new()
        {
            AutoSize = true,
            FlatStyle = FlatStyle.Flat,
            Margin = new Padding(10, 0, 10, 20),
            Text = text
        };

    internal static Label AddEditCreateLabel(string text) =>
        new()
        {
            Text = text
        };

    internal static TextBox AddEditCreateTextBox() =>
        new()
        {
            BackColor = Color.FromArgb(60, 60, 60),
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.FromArgb(230, 230, 230),
            Width = 130
        };

    internal static CheckBox AddEditCreateCheckBox(string text) =>
        new()
        {
            Text = text
        };

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

    internal static DateTimePicker AddEditCreateDateTimePicker() =>
        new()
        {
            Format = DateTimePickerFormat.Short,
            MinDate = new DateTime(1900, 1, 1),
            MaxDate = DateTime.Now,
            Width = 130
        };

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

        var tableLayoutPanel = new TableLayoutPanel()
        {
            Height = buttonPlus.Height
        };

        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(buttonPlus, 0, 0);
        tableLayoutPanel.Controls.Add(labelPicked, 1, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    internal static TableLayoutPanel PickPeriodCreateTableLayoutPanel(out Label labelPickedPeriod, out Button buttonPick)
    {
        var labelPeriod = new Label()
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

        var tableLayoutPanel = new TableLayoutPanel()
        {
            Width = 270
        };

        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(labelPeriod, 0, 0);
        tableLayoutPanel.Controls.Add(labelPickedPeriod, 1, 0);
        tableLayoutPanel.Controls.Add(buttonPick, 2, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    internal static TableLayoutPanel SettingsCreateControlTableLayoutPanel(Control control)
    {
        var tableLayoutPanel = new TableLayoutPanel()
        {
            Height = control.Height
        };

        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
        tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(control, 0, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    internal static TableLayoutPanel SettingsCreateLabelControlTableLayoutPanel(Label label, Control control)
    {
        var tableLayoutPanel = new TableLayoutPanel()
        {
            Height = (label.Height > control.Height) ? label.Height : control.Height
        };

        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
        tableLayoutPanel.RowStyles.Add(new RowStyle());

        tableLayoutPanel.Controls.Add(label, 0, 0);
        tableLayoutPanel.Controls.Add(control, 1, 0);

        tableLayoutPanel.RowCount++;

        return tableLayoutPanel;
    }

    internal static Label SettingsCreateLabel(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(30, 0, 0, 0),
            Text = text
        };

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

    internal static CheckBox SettingsCreateCheckBox(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(33, 0, 0, 0),
            Text = text
        };
}