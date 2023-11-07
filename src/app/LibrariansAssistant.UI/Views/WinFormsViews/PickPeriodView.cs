using LibrariansAssistant.UI.Helpers.WinFormsHelpers;
using LibrariansAssistant.UI.Services.WinFormsServices.Implementations;

namespace LibrariansAssistant.UI.Views.WinFormsViews;

/// <summary>
/// Represents the Pick Period view of the application.
/// </summary>
internal sealed partial class PickPeriodView : Form
{
    private readonly WinFormsMessageService _winFormsMessageService = new();
    private readonly Label _labelPickedPeriod;
    private readonly Button _buttonPick;
    private Dictionary<int, string> _periodColumnIndexNames = new();

    /// <summary>
    /// Gets the main control of the view.
    /// </summary>
    internal Control MainControl { get; }

    /// <summary>
    /// Get the start period.
    /// </summary>
    internal DateTime? StartPeriod { get; private set; }

    /// <summary>
    /// Get the end period.
    /// </summary>
    internal DateTime? EndPeriod { get; private set; }

    /// <summary>
    /// Gets the index of the column to which to apply the period.
    /// </summary>
    internal int? PeriodColumnIndex { get; private set; }

    /// <summary>
    /// Gets or sets the names of the columns to which the period can be applied.
    /// </summary>
    internal Dictionary<int, string> PeriodColumnIndexNames
    {
        get => _periodColumnIndexNames;
        set
        {
            _periodColumnIndexNames = value;

            if ((_periodColumnIndexNames is not null) && (_periodColumnIndexNames.Any() is true))
            {
                foreach (KeyValuePair<int, string> periodColumnIndexName in _periodColumnIndexNames)
                    _ = comboBoxColumn.Items.Add(periodColumnIndexName.Value);

                comboBoxColumn.SelectedIndex = 0;
            }
        }
    }

    /// <summary>
    /// Occurs when the period is set.
    /// </summary>
    internal event EventHandler? PeriodSet;

    /// <summary>
    /// Initializes a new instance of the PickPeriodView class.
    /// </summary>
    internal PickPeriodView()
    {
        MainControl = ControlCreation.PickPeriodCreateTableLayoutPanel(out _labelPickedPeriod, out _buttonPick);

        InitializeComponent();
        SubscribeToControlEvents();
        InitializeView();
    }

    private void SubscribeToControlEvents()
    {
        _buttonPick.Click += ButtonPickOnClick;
        buttonConfirm.Click += ButtonConfirmOnClick;
        buttonReset.Click += ButtonResetOnClick;
    }

    private void InitializeView() =>
        ResetPeriod();

    private void ButtonPickOnClick(object? sender, EventArgs e) =>
        _ = ShowDialog();

    private void ButtonConfirmOnClick(object? sender, EventArgs e)
    {
        if (dateTimePickerStartPeriod.Value > dateTimePickerEndPeriod.Value)
        {
            _winFormsMessageService.ShowWarning("The end period value must be greater than or equal to the start period value.");

            return;
        }

        int periodColumnIndex = PeriodColumnIndexNames
            .FirstOrDefault(indName => indName.Value.Equals(comboBoxColumn.SelectedItem as string, StringComparison.Ordinal))
            .Key;

        StartPeriod = dateTimePickerStartPeriod.Value;
        EndPeriod = dateTimePickerEndPeriod.Value;
        PeriodColumnIndex = (periodColumnIndex is not 0) ? periodColumnIndex : default;

        labelStatus.Text = "Enabled";
        _labelPickedPeriod.Text = $"{dateTimePickerStartPeriod.Value:d} - {dateTimePickerEndPeriod.Value:d}";

        PeriodSet?.Invoke(sender, EventArgs.Empty);

        Close();
    }

    private void ButtonResetOnClick(object? sender, EventArgs e)
    {
        ResetPeriod();

        PeriodSet?.Invoke(sender, EventArgs.Empty);

        Close();
    }

    private void ResetPeriod()
    {
        StartPeriod = default;
        EndPeriod = default;
        PeriodColumnIndex = default;

        labelStatus.Text = "Disabled";
        _labelPickedPeriod.Text = "none";

        DateTime currentDate = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        dateTimePickerStartPeriod.MaxDate = currentDate;
        dateTimePickerEndPeriod.MaxDate = currentDate;

        dateTimePickerStartPeriod.Value = new DateTime(currentDate.Year, currentDate.Month, 1);
        dateTimePickerEndPeriod.Value = currentDate;
    }
}