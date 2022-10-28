using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.UserInterfaceLayer.Entities.WinFormsEntities;
using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers.ColorTables;
using LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Interfaces;
using LibrariansAssistant.UserInterfaceLayer.Services.WinFormsServices.Implementations;

namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

internal sealed partial class SettingsView : Form
{
    private readonly IApplicationConfigurationService _applicationConfigurationService =
        DependenciesContainer.Resolve<IApplicationConfigurationService>()!;
    private readonly WinFormsDefaultMessageService _winFormsDefaultMessageService = new();
    private readonly Dictionary<SettingsGroup, Control[]> _settingsControls = new();
    private readonly Dictionary<string, string> _changedSettings = new();
    private readonly int _formDefaultHeight = 195;
    private SettingsGroup _selectedSettingsGroup;

    internal SettingsView()
    {
        InitializeComponent();
        SubsribeToControlEvents();
        InitializeView();
    }

    private void SubsribeToControlEvents()
    {
        connectionToolStripMenuItem.Click += ConnectionToolStripMenuItemOnClick;
        additionalToolStripMenuItem.Click += AdditionalToolStripMenuItemOnClick;
        buttonApply.Click += ButtonApplyOnClick;
    }

    private void InitializeView()
    {
        menuStripSettings.Renderer = new ToolStripProfessionalRenderer(new MenuStripSettingsColorTable());

        _settingsControls.Add(SettingsGroup.Connection, GenerateConnectionSettingsControls());
        _settingsControls.Add(SettingsGroup.Additional, GenerateAdditionalSettingsControls());

        SwitchSettingsGroupView(SettingsGroup.Connection);
    }

    private void ConnectionToolStripMenuItemOnClick(object? sender, EventArgs e) =>
        SwitchSettingsGroupView(SettingsGroup.Connection);

    private void AdditionalToolStripMenuItemOnClick(object? sender, EventArgs e) =>
        SwitchSettingsGroupView(SettingsGroup.Additional);

    private void ButtonApplyOnClick(object? sender, EventArgs e)
    {
        try
        {
            _applicationConfigurationService.UpdateAppSettings(_changedSettings);

            _changedSettings.Clear();

            UpdateApplyButtonState();
        }
        catch (Exception ex)
        {
            _winFormsDefaultMessageService.ShowError(ex.Message);
        }
    }

    private void UpdateSetting(string name, string value)
    {
        if (_changedSettings.ContainsKey(name) is true)
            _changedSettings[name] = value;
        else
            _changedSettings.Add(name, value);

        UpdateApplyButtonState();
    }

    private void UpdateApplyButtonState()
    {
        if (_changedSettings.Any() is true)
            buttonApply.ForeColor = Color.FromArgb(230, 230, 230);
        else
            buttonApply.ForeColor = Color.FromArgb(130, 130, 130);
    }

    private void SwitchSettingsGroupView(SettingsGroup newSettingsGroup)
    {
        if (newSettingsGroup == _selectedSettingsGroup)
            return;

        foreach (object? toolStripMenuItem in menuStripSettings.Items)
            ((ToolStripMenuItem)toolStripMenuItem).BackColor = Color.FromArgb(30, 30, 30);

        switch (newSettingsGroup)
        {
            case SettingsGroup.Connection:
                connectionToolStripMenuItem.BackColor = Color.FromArgb(70, 70, 70);
                break;
            case SettingsGroup.Additional:
                additionalToolStripMenuItem.BackColor = Color.FromArgb(70, 70, 70);
                break;
            default:
                throw new NotImplementedException("This setting group view is not yet implemented.");
        }

        if (_selectedSettingsGroup is not 0)
            tableLayoutPanelSettings.Controls.CopyTo(_settingsControls[_selectedSettingsGroup], 0);

        tableLayoutPanelSettings.Controls.Clear();
        tableLayoutPanelSettings.RowStyles.Clear();

        Height = _formDefaultHeight;
        groupBoxSettings.Text = newSettingsGroup.ToString() + " settings";

        foreach (Control newControl in _settingsControls[newSettingsGroup])
        {
            Height += newControl.Height + 20;
            newControl.Dock = DockStyle.Top;

            tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, newControl.Height + 20));
            tableLayoutPanelSettings.Controls.Add(newControl);

            tableLayoutPanelSettings.RowCount++;
        }

        _selectedSettingsGroup = newSettingsGroup;
    }

    private Control[] GenerateConnectionSettingsControls()
    {
        TextBox textBoxServerName = CreateTextBox();
        TextBox textBoxServerInstanceName = CreateTextBox();
        CheckBox checkBoxUseWindowsAuthentication = CreateCheckBox("Use Windows authentication");
        TextBox textBoxUsername = CreateTextBox();
        TextBox textBoxPassword = CreateTextBox();

        textBoxUsername.Enabled = false;
        textBoxUsername.BackColor = Color.FromArgb(35, 35, 35);
        textBoxUsername.ForeColor = Color.FromArgb(130, 130, 130);
        textBoxPassword.Enabled = false;
        textBoxPassword.BackColor = Color.FromArgb(35, 35, 35);
        textBoxPassword.ForeColor = Color.FromArgb(130, 130, 130);
        textBoxPassword.PasswordChar = '*';

        textBoxServerName.TextChanged += (sender, e) =>
            UpdateSetting("Server name", textBoxServerName.Text);

        textBoxServerInstanceName.TextChanged += (sender, e) =>
            UpdateSetting("Server instance name", textBoxServerInstanceName.Text);

        checkBoxUseWindowsAuthentication.CheckedChanged += (sender, e) =>
        {
            UpdateSetting("Use Windows authentication", checkBoxUseWindowsAuthentication.Checked.ToString());

            if (checkBoxUseWindowsAuthentication.Checked is true)
            {
                textBoxUsername.Enabled = true;
                textBoxPassword.Enabled = true;

                textBoxUsername.BackColor = Color.FromArgb(60, 60, 60);
                textBoxPassword.BackColor = Color.FromArgb(60, 60, 60);

                textBoxUsername.ForeColor = Color.FromArgb(230, 230, 230);
                textBoxPassword.ForeColor = Color.FromArgb(230, 230, 230);
            }
            else
            {
                textBoxUsername.Enabled = false;
                textBoxPassword.Enabled = false;

                textBoxUsername.BackColor = Color.FromArgb(35, 35, 35);
                textBoxPassword.BackColor = Color.FromArgb(35, 35, 35);

                textBoxUsername.ForeColor = Color.FromArgb(130, 130, 130);
                textBoxPassword.ForeColor = Color.FromArgb(130, 130, 130);
            }
        };

        textBoxUsername.TextChanged += (sender, e) =>
            UpdateSetting("User name", textBoxUsername.Text);

        textBoxPassword.TextChanged += (sender, e) =>
            UpdateSetting("Password", textBoxPassword.Text);

        var controls = new List<Control>()
        {
            CreateLabelControlTableLayoutPanel(CreateLabel("Server name:"), textBoxServerName),
            CreateLabelControlTableLayoutPanel(CreateLabel("Server instance name:"), textBoxServerInstanceName),
            CreateControlTableLayoutPanel(checkBoxUseWindowsAuthentication),
            CreateLabelControlTableLayoutPanel(CreateLabel("User name:"), textBoxUsername),
            CreateLabelControlTableLayoutPanel(CreateLabel("Password:"), textBoxPassword)
        };

        return controls.ToArray();
    }

    private Control[] GenerateAdditionalSettingsControls()
    {
        CheckBox checkBoxCreateEmptyDatabase = CreateCheckBox("If the database is not found, create a new empty one");
        Label labelCreateEmptyDatabaseNote = CreateLabel("Note: \"Database name \'library\' must be available\"");

        labelCreateEmptyDatabaseNote.Margin = new Padding(52, 0, 0, 0);

        checkBoxCreateEmptyDatabase.CheckedChanged += (sender, e) =>
            UpdateSetting("Create empty database", checkBoxCreateEmptyDatabase.Checked.ToString());

        var controls = new List<Control>()
        {
            CreateControlTableLayoutPanel(checkBoxCreateEmptyDatabase),
            CreateControlTableLayoutPanel(labelCreateEmptyDatabaseNote)
        };

        return controls.ToArray();
    }

    private static TableLayoutPanel CreateControlTableLayoutPanel(Control control)
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

    private static TableLayoutPanel CreateLabelControlTableLayoutPanel(Label label, Control control)
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

    private static Label CreateLabel(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(30, 0, 0, 0),
            Text = text
        };

    private static TextBox CreateTextBox() =>
        new()
        {
            Anchor = AnchorStyles.Left,
            BackColor = Color.FromArgb(60, 60, 60),
            BorderStyle = BorderStyle.FixedSingle,
            ForeColor = Color.FromArgb(230, 230, 230),
            Margin = new Padding(0),
            Width = 180
        };

    private static CheckBox CreateCheckBox(string text) =>
        new()
        {
            Anchor = AnchorStyles.Left,
            AutoSize = true,
            Margin = new Padding(33, 0, 0, 0),
            Text = text
        };
}