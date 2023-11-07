using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Infrastructure.Repositories.InitializationStringBuilders;
using LibrariansAssistant.Infrastructure.Repositories.InitializationStringBuilders.Implementations;
using LibrariansAssistant.Infrastructure.Repositories.InitializationStringBuilders.Implementations.Entities;
using LibrariansAssistant.Infrastructure.Repositories.Interfaces;
using LibrariansAssistant.Infrastructure.RepositoryFactories;
using LibrariansAssistant.Infrastructure.RepositoryFactories.Implementations;
using LibrariansAssistant.UI.Entities;
using LibrariansAssistant.UI.Entities.WinFormsEntities;
using LibrariansAssistant.UI.Helpers.WinFormsHelpers;
using LibrariansAssistant.UI.Helpers.WinFormsHelpers.ColorTables;
using LibrariansAssistant.UI.Services.CommonServices.Interfaces;
using LibrariansAssistant.UI.Services.WinFormsServices.Implementations;

namespace LibrariansAssistant.UI.Views.WinFormsViews;

/// <summary>
/// Represents the Settings view of the application.
/// </summary>
internal sealed partial class SettingsView : Form
{
    private const int FormDefaultHeight = 195;
    private readonly IApplicationConfigurationService _applicationConfigurationService =
        DependenciesContainer.Resolve<IApplicationConfigurationService>()!;
    private readonly WinFormsMessageService _winFormsMessageService = new();
    private readonly Dictionary<SettingsGroup, Control[]> _settingsControls = new();
    private readonly Dictionary<string, object?> _modifiedSettings = new();
    private SettingsGroup _selectedSettingsGroup;

    /// <summary>
    /// Gets the selected repository type.
    /// </summary>
    internal RepositoryType SelectedRepositoryType { get; } = RepositoryType.SqlServer;

    /// <summary>
    /// Gets the infrastructure creator initialization string.
    /// </summary>
    internal string? InfrastructureCreatorInitializationString { get; private set; }

    /// <summary>
    /// Gets the repository initialization string.
    /// </summary>
    internal string? RepositoryInitializationString { get; private set; }

    /// <summary>
    /// Gets the repository instance.
    /// </summary>
    internal IRepository? Repository { get; private set; }

    /// <summary>
    /// Gets a value indicating whether to create an empty database if it does not exist.
    /// </summary>
    internal bool SettingCreateEmptyDatabase { get; private set; }

    /// <summary>
    /// Initializes a new instance of the SettingsView class.
    /// </summary>
    internal SettingsView()
    {
        InitializeComponent();
        SubsribeToControlEvents();
        InitializeView();
        UpdatePropertyValues();
    }

    private void SubsribeToControlEvents()
    {
        connectionToolStripMenuItem.Click += ConnectionToolStripMenuItemOnClick;
        additionalToolStripMenuItem.Click += AdditionalToolStripMenuItemOnClick;
        buttonApply.Click += ButtonApplyOnClick;
        FormClosing += SettingsViewOnFormClosing;
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

        switch (SelectedRepositoryType)
        {
            case RepositoryType.SqlServer:

                string serverName =
                    (_applicationConfigurationService.GetSettingValue("SqlServer_ServerName") as string)!;
                string serverInstanceName =
                    (_applicationConfigurationService.GetSettingValue("SqlServer_ServerInstanceName") as string)!;
                bool useWindowsAuthentication =
                    (bool)_applicationConfigurationService.GetSettingValue("SqlServer_UseWindowsAuthentication")!;
                string? userName =
                    _applicationConfigurationService.GetSettingValue("SqlServer_UserName") as string;
                string? password =
                    _applicationConfigurationService.GetSettingValue("SqlServer_Password") as string;
                string? databaseName =
                    _applicationConfigurationService.GetSettingValue("DatabaseName") as string;

                SqlServerIsbSettings infranstructureCreatorIsbSettings = new(serverName, serverInstanceName)
                {
                    UseWindowsAuthentication = useWindowsAuthentication,
                    UserName = userName,
                    Password = password
                };

                SqlServerIsbSettings repositoryIsbSettings = new(serverName, serverInstanceName)
                {
                    UseWindowsAuthentication = useWindowsAuthentication,
                    UserName = userName,
                    Password = password,
                    DatabaseName = databaseName
                };

                initializationStringBuilder = new SqlServerInitializationStringBuilder(infranstructureCreatorIsbSettings);
                InfrastructureCreatorInitializationString = initializationStringBuilder.Build();
                initializationStringBuilder = new SqlServerInitializationStringBuilder(repositoryIsbSettings);
                RepositoryInitializationString = initializationStringBuilder.Build();

                repositoryFactory = new SqlServerRepositoryFactory();
                Repository = repositoryFactory.GetRepository();

                break;
            default:
                break;
        }

        SettingCreateEmptyDatabase = (bool)_applicationConfigurationService.GetSettingValue("CreateEmptyDatabase")!;
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

    private Control[] GenerateConnectionSettingsControls()
    {
        TextBox textBoxSqlServerServerName = ControlCreation.SettingsCreateTextBox();
        TextBox textBoxSqlServerServerInstanceName = ControlCreation.SettingsCreateTextBox();
        CheckBox checkBoxSqlServerUseWindowsAuthentication =
            ControlCreation.SettingsCreateCheckBox("Use Windows authentication");
        TextBox textBoxSqlServerUsername = ControlCreation.SettingsCreateTextBox();
        TextBox textBoxSqlServerPassword = ControlCreation.SettingsCreateTextBox();

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

        List<Control> controls = new()
        {
            ControlCreation.SettingsCreateLabelControlTableLayoutPanel(
                ControlCreation.SettingsCreateLabel("Server name:"), textBoxSqlServerServerName),
            ControlCreation.SettingsCreateLabelControlTableLayoutPanel(
                ControlCreation.SettingsCreateLabel("Server instance name:"), textBoxSqlServerServerInstanceName),
            ControlCreation.SettingsCreateControlTableLayoutPanel(checkBoxSqlServerUseWindowsAuthentication),
            ControlCreation.SettingsCreateLabelControlTableLayoutPanel(
                ControlCreation.SettingsCreateLabel("User name:"), textBoxSqlServerUsername),
            ControlCreation.SettingsCreateLabelControlTableLayoutPanel(
                ControlCreation.SettingsCreateLabel("Password:"), textBoxSqlServerPassword)
        };

        return controls.ToArray();
    }

    private Control[] GenerateAdditionalSettingsControls()
    {
        CheckBox checkBoxCreateEmptyDatabase =
            ControlCreation.SettingsCreateCheckBox("If the database is not found, create a new empty one");
        Label labelCreateEmptyDatabaseNote =
            ControlCreation.SettingsCreateLabel("Note: \"Database name \'library\' must be available\"");

        labelCreateEmptyDatabaseNote.Margin = new Padding(52, 0, 0, 0);

        checkBoxCreateEmptyDatabase.Checked =
            (bool)_applicationConfigurationService.GetSettingValue("CreateEmptyDatabase")!;

        checkBoxCreateEmptyDatabase.CheckedChanged += (sender, e) =>
            UpdateSetting("CreateEmptyDatabase", checkBoxCreateEmptyDatabase.Checked);

        List<Control> controls = new()
        {
            ControlCreation.SettingsCreateControlTableLayoutPanel(checkBoxCreateEmptyDatabase),
            ControlCreation.SettingsCreateControlTableLayoutPanel(labelCreateEmptyDatabaseNote)
        };

        return controls.ToArray();
    }

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

            _ = tableLayoutPanelSettings.RowStyles.Add(new RowStyle(SizeType.Absolute, newControl.Height + 20));

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