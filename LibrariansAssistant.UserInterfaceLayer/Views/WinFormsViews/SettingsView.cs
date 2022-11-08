using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.InfranstructureLayer.Repositories.InitializationStringBuilders;
using LibrariansAssistant.InfranstructureLayer.Repositories.InitializationStringBuilders.Implementations;
using LibrariansAssistant.InfranstructureLayer.Repositories.InitializationStringBuilders.Implementations.Entities;
using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.InfranstructureLayer.RepositoryFactories;
using LibrariansAssistant.InfranstructureLayer.RepositoryFactories.Implementations;
using LibrariansAssistant.UserInterfaceLayer.Entities;
using LibrariansAssistant.UserInterfaceLayer.Entities.WinFormsEntities;
using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers.ColorTables;
using LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Interfaces;
using LibrariansAssistant.UserInterfaceLayer.Services.WinFormsServices.Implementations;

namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

internal sealed partial class SettingsView : Form
{
    private const int FormDefaultHeight = 195;
    private readonly IApplicationConfigurationService _applicationConfigurationService =
        DependenciesContainer.Resolve<IApplicationConfigurationService>()!;
    private readonly WinFormsMessageService _winFormsMessageService = new();
    private readonly Dictionary<SettingsGroup, Control[]> _settingsControls = new();
    private readonly Dictionary<string, object?> _modifiedSettings = new();
    private readonly RepositoryType _selectedRepositoryType = RepositoryType.SqlServer;
    private SettingsGroup _selectedSettingsGroup;

    internal IRepository? Repository { get; private set; }

    internal string? InitializationString { get; private set; }

    internal bool SettingCreateEmptyDatabase { get; private set; }

    internal SettingsView()
    {
        InitializeComponent();
        SubsribeToControlEvents();
        InitializeView();
        UpdatePropertyValues();
    }

    private void SubsribeToControlEvents()
    {
        FormClosing += SettingsViewOnFormClosing;
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

    private void UpdatePropertyValues()
    {
        IInitializationStringBuilder initializationStringBuilder;
        IRepositoryFactory repositoryFactory;

        switch (_selectedRepositoryType)
        {
            case RepositoryType.SqlServer:

                var sqlServerIsbSettings = new SqlServerIsbSettings(
                    (_applicationConfigurationService.GetSettingValue("SqlServer_ServerName") as string)!,
                    (_applicationConfigurationService.GetSettingValue("SqlServer_ServerInstanceName") as string)!)
                {
                    UseWindowsAuthentication =
                        (bool)_applicationConfigurationService.GetSettingValue("SqlServer_UseWindowsAuthentication")!,
                    UserName = _applicationConfigurationService.GetSettingValue("SqlServer_UserName") as string,
                    Password = _applicationConfigurationService.GetSettingValue("SqlServer_Password") as string
                };

                initializationStringBuilder = new SqlServerInitializationStringBuilder(sqlServerIsbSettings);
                InitializationString = initializationStringBuilder.Build();

                repositoryFactory = new SqlServerRepositoryFactory();
                Repository = repositoryFactory.GetRepository();

                break;
            default:
                break;
        }

        SettingCreateEmptyDatabase = (bool)_applicationConfigurationService.GetSettingValue("CreateEmptyDatabase")!;
    }

    private void SettingsViewOnFormClosing(object? sender, FormClosingEventArgs e)
    {
        if (_modifiedSettings.Any() is true)
        {
            DialogResult dialogResult = MessageBox.Show(
                "You have unsaved changes." + Environment.NewLine + "Do you want to save them before leaving?",
                "Warnig", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

            switch (dialogResult)
            {
                case DialogResult.Yes:
                    SaveSettings();
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }
    }

    private void ConnectionToolStripMenuItemOnClick(object? sender, EventArgs e) =>
        SwitchSettingsGroupView(SettingsGroup.Connection);

    private void AdditionalToolStripMenuItemOnClick(object? sender, EventArgs e) =>
        SwitchSettingsGroupView(SettingsGroup.Additional);

    private void ButtonApplyOnClick(object? sender, EventArgs e)
    {
        if (_modifiedSettings.Any() is false)
            return;

        SaveSettings();
    }

    private Control[] GenerateConnectionSettingsControls()
    {
        TextBox textBoxSqlServerServerName = CreateTextBox();
        TextBox textBoxSqlServerServerInstanceName = CreateTextBox();
        CheckBox checkBoxSqlServerUseWindowsAuthentication = CreateCheckBox("Use Windows authentication");
        TextBox textBoxSqlServerUsername = CreateTextBox();
        TextBox textBoxSqlServerPassword = CreateTextBox();

        textBoxSqlServerPassword.PasswordChar = '*';

        textBoxSqlServerServerName.Text = _applicationConfigurationService.GetSettingValue("SqlServer_ServerName") as string;
        textBoxSqlServerServerInstanceName.Text =
            _applicationConfigurationService.GetSettingValue("SqlServer_ServerInstanceName") as string;
        checkBoxSqlServerUseWindowsAuthentication.Checked =
            (bool)_applicationConfigurationService.GetSettingValue("SqlServer_UseWindowsAuthentication")!;
        textBoxSqlServerUsername.Text = _applicationConfigurationService.GetSettingValue("SqlServer_UserName") as string;
        textBoxSqlServerPassword.Text = _applicationConfigurationService.GetSettingValue("SqlServer_Password") as string;

        UpdateSqlServerCredentialsState(checkBoxSqlServerUseWindowsAuthentication,
            textBoxSqlServerUsername, textBoxSqlServerPassword);

        textBoxSqlServerServerName.TextChanged += (sender, e) =>
            UpdateSetting("SqlServer_ServerName", textBoxSqlServerServerName.Text);

        textBoxSqlServerServerInstanceName.TextChanged += (sender, e) =>
            UpdateSetting("SqlServer_ServerInstanceName", textBoxSqlServerServerInstanceName.Text);

        checkBoxSqlServerUseWindowsAuthentication.CheckedChanged += (sender, e) =>
        {
            UpdateSetting("SqlServer_UseWindowsAuthentication", checkBoxSqlServerUseWindowsAuthentication.Checked);
            UpdateSqlServerCredentialsState(checkBoxSqlServerUseWindowsAuthentication,
                textBoxSqlServerUsername, textBoxSqlServerPassword);
        };

        textBoxSqlServerUsername.TextChanged += (sender, e) =>
            UpdateSetting("SqlServer_UserName", textBoxSqlServerUsername.Text);

        textBoxSqlServerPassword.TextChanged += (sender, e) =>
            UpdateSetting("SqlServer_Password", textBoxSqlServerPassword.Text);

        var controls = new List<Control>()
        {
            CreateLabelControlTableLayoutPanel(CreateLabel("Server name:"), textBoxSqlServerServerName),
            CreateLabelControlTableLayoutPanel(CreateLabel("Server instance name:"), textBoxSqlServerServerInstanceName),
            CreateControlTableLayoutPanel(checkBoxSqlServerUseWindowsAuthentication),
            CreateLabelControlTableLayoutPanel(CreateLabel("User name:"), textBoxSqlServerUsername),
            CreateLabelControlTableLayoutPanel(CreateLabel("Password:"), textBoxSqlServerPassword)
        };

        return controls.ToArray();
    }

    private Control[] GenerateAdditionalSettingsControls()
    {
        CheckBox checkBoxCreateEmptyDatabase = CreateCheckBox("If the database is not found, create a new empty one");
        Label labelCreateEmptyDatabaseNote = CreateLabel("Note: \"Database name \'library\' must be available\"");

        labelCreateEmptyDatabaseNote.Margin = new Padding(52, 0, 0, 0);

        checkBoxCreateEmptyDatabase.Checked =
            (bool)_applicationConfigurationService.GetSettingValue("CreateEmptyDatabase")!;

        checkBoxCreateEmptyDatabase.CheckedChanged += (sender, e) =>
            UpdateSetting("CreateEmptyDatabase", checkBoxCreateEmptyDatabase.Checked);

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

    private static void UpdateSqlServerCredentialsState(CheckBox checkBoxUseWindowsAuthentication,
        TextBox textBoxUsername, TextBox textBoxPassword)
    {
        if (checkBoxUseWindowsAuthentication.Checked is true)
        {
            textBoxUsername.Enabled = false;
            textBoxPassword.Enabled = false;

            textBoxUsername.BackColor = Color.FromArgb(35, 35, 35);
            textBoxPassword.BackColor = Color.FromArgb(35, 35, 35);

            textBoxUsername.ForeColor = Color.FromArgb(130, 130, 130);
            textBoxPassword.ForeColor = Color.FromArgb(130, 130, 130);
        }
        else
        {
            textBoxUsername.Enabled = true;
            textBoxPassword.Enabled = true;

            textBoxUsername.BackColor = Color.FromArgb(60, 60, 60);
            textBoxPassword.BackColor = Color.FromArgb(60, 60, 60);

            textBoxUsername.ForeColor = Color.FromArgb(230, 230, 230);
            textBoxPassword.ForeColor = Color.FromArgb(230, 230, 230);
        }
    }

    private void UpdateApplyButtonState()
    {
        if (_modifiedSettings.Any() is true)
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
                break;
        }

        if (_selectedSettingsGroup is not 0)
            tableLayoutPanelSettings.Controls.CopyTo(_settingsControls[_selectedSettingsGroup], 0);

        tableLayoutPanelSettings.Controls.Clear();
        tableLayoutPanelSettings.RowStyles.Clear();

        Height = FormDefaultHeight;
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

    private void UpdateSetting(string name, object? value)
    {
        if (_modifiedSettings.ContainsKey(name) is true)
            _modifiedSettings[name] = value;
        else
            _modifiedSettings.Add(name, value);

        UpdateApplyButtonState();
    }

    private void SaveSettings()
    {
        try
        {
            _applicationConfigurationService.UpdateSettingsValues(_modifiedSettings);
            _modifiedSettings.Clear();

            UpdatePropertyValues();
            UpdateApplyButtonState();
        }
        catch (Exception ex)
        {
            _winFormsMessageService.ShowError(ex.Message);
        }
    }
}