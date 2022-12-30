using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers;
using LibrariansAssistant.UserInterfaceLayer.Services.WinFormsServices.Implementations;

namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

internal sealed partial class PickPeriodView : Form
{
    private readonly WinFormsMessageService _winFormsMessageService = new();
    private readonly Label _labelPickedPeriod;
    private readonly Button _buttonPick;
    private Dictionary<int, string> _periodColumnIndexNames = new();

    internal Control MainControl { get; }

    internal DateTime? StartPeriod { get; private set; }

    internal DateTime? EndPeriod { get; private set; }

    internal int? PeriodColumnIndex { get; private set; }

    internal Dictionary<int, string> PeriodColumnIndexNames
    {
        get => _periodColumnIndexNames;
        set
        {
            _periodColumnIndexNames = value;

            if ((_periodColumnIndexNames is not null) && (_periodColumnIndexNames.Any() is true))
            {
                foreach (KeyValuePair<int, string> periodColumnIndexName in _periodColumnIndexNames)
                    comboBoxColumn.Items.Add(periodColumnIndexName.Value);

                comboBoxColumn.SelectedIndex = 0;
            }
        }
    }

    internal event EventHandler? PeriodSet;

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
        ShowDialog();

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