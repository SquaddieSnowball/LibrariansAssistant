using LibrariansAssistant.InfranstructureLayer.InfranstructureCreators;
using LibrariansAssistant.InfranstructureLayer.InfranstructureCreators.Implementations;
using LibrariansAssistant.PresentationLayer.Presenters.Implementations.WinFormsViewPresenters;
using LibrariansAssistant.PresentationLayer.ViewInterfaces.WinFormsViewInterfaces;
using LibrariansAssistant.UserInterfaceLayer.Entities.WinFormsEntities;
using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers;
using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers.ColorTables;
using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers.Renderers;
using LibrariansAssistant.UserInterfaceLayer.Services.WinFormsServices.Implementations;

namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

internal sealed partial class MainView : Form, IMainView
{
    private readonly Label _labelStartMessage = ControlCreation.MainCreateLabelStartMessage();
    private readonly Label _labelDataNoItems = ControlCreation.MainCreateLabelDataNoItems();
    private readonly WinFormsMessageService _winFormsMessageService = new();
    private Delegate[]? _dataUpdateEventInvokationList;
    private bool _dataIsAscSortDirection = true;
    private int _dataPrevSortColumnIndex = -1;
    private ViewType _currentViewType;
    private string _currentViewName = string.Empty;
    private Action<DataGridViewRow>? _filterMethods;
    private PickPeriodView? _pickPeriodView;

    public IEnumerable<object>? VisibleDataNormalView { get; set; }

    public IEnumerable<object?>? VisibleDataEditView { get; set; }

    public IEnumerable<KeyValuePair<int, string?>>? VisibleDataPickView { get; set; }

    public IEnumerable<KeyValuePair<int, string>>? VisibleDataPeriodView { get; set; }

    public IEnumerable<string>? VisibleDataColumnHeadersNormalView { get; set; }

    public bool IsOperationSuccessful { get; set; }

    public event EventHandler? AuthorsUpdateNormalView;

    public event EventHandler? BooksUpdateNormalView;

    public event EventHandler? IssuingsUpdateNormalView;

    public event EventHandler? ReadersUpdateNormalView;

    public event EventHandler<int>? AuthorUpdateEditView;

    public event EventHandler<int>? BookUpdateEditView;

    public event EventHandler<int>? IssuingUpdateEditView;

    public event EventHandler<int>? ReaderUpdateEditView;

    public event EventHandler? AuthorsUpdatePickView;

    public event EventHandler? BooksUpdatePickView;

    public event EventHandler? ReadersUpdatePickView;

    public event EventHandler? IssuingsUpdatePeriodView;

    public event EventHandler? ReadersUpdatePeriodView;

    public event EventHandler<IEnumerable<object?>>? IssuingOpen;

    public event EventHandler<IEnumerable<object?>>? IssuingClose;

    public event EventHandler<IEnumerable<object?>>? AuthorAdd;

    public event EventHandler<IEnumerable<object?>>? BookAdd;

    public event EventHandler<IEnumerable<object?>>? ReaderAdd;

    public event EventHandler<IEnumerable<object?>>? AuthorEdit;

    public event EventHandler<IEnumerable<object?>>? BookEdit;

    public event EventHandler<IEnumerable<object?>>? IssuingEdit;

    public event EventHandler<IEnumerable<object?>>? ReaderEdit;

    public event EventHandler<int>? AuthorRemove;

    public event EventHandler<int>? BookRemove;

    public event EventHandler<int>? IssuingRemove;

    public event EventHandler<int>? ReaderRemove;

    public event EventHandler<IEnumerable<object>>? ExportDataText;

    public event EventHandler<IEnumerable<object>>? ExportDataExcel;

    internal MainView()
    {
        InitializeComponent();
        SubscribeToControlEvents();
        InitializeView();
    }

    private void SubscribeToControlEvents()
    {
        connectToDatabaseToolStripMenuItem.Click += ConnectToDatabaseToolStripMenuItemOnClick;
        settingsToolStripMenuItem.Click += SettingsToolStripMenuItemOnClick;
        exitToolStripMenuItem.Click += ExitToolStripMenuItemOnClick;

        dataGridViewData.ColumnAdded += DataGridViewDataOnColumnAdded;
        dataGridViewData.ColumnHeaderMouseClick += DataGridViewDataOnColumnHeaderMouseClick;
        dataGridViewData.CellPainting += DataGridViewDataOnCellPainting;

        issuingsShowAllToolStripMenuItem.Click += IssuingsShowAllToolStripMenuItemOnClick;
        readersShowAllToolStripMenuItem.Click += ReadersShowAllToolStripMenuItemOnClick;
        authorsShowAllToolStripMenuItem.Click += AuthorsShowAllToolStripMenuItemOnClick;
        booksShowAllToolStripMenuItem.Click += BooksShowAllToolStripMenuItemOnClick;

        exportTextToolStripMenuItem.Click += ExportTextToolStripMenuItemOnClick;
        exportExcelToolStripMenuItem.Click += ExportExcelToolStripMenuItemOnClick;

        textBoxSearch.TextChanged += TextBoxSearchOnTextChanged;
        checkBoxApplyFilters.CheckedChanged += CheckBoxApplyFiltersOnCheckedChanged;
    }

    private void InitializeView()
    {
        menuStripMain.Renderer = new MenuStripMainRenderer(new MenuStripMainColorTable());
        Controls.Add(_labelStartMessage);
        _filterMethods = FilterSearch;

        SwitchDataView(false);
    }

    private void ConnectToDatabaseToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        SettingsView settingsView;

        try
        {
            settingsView = new SettingsView();
        }
        catch
        {
            throw;
        }

        try
        {
            if (settingsView.SettingCreateEmptyDatabase is true)
            {
                IInfranstructureCreator infranstructureCreator = settingsView.SelectedRepositoryType switch
                {
                    Entities.RepositoryType.SqlServer => new SqlServerInfranstructureCreator(),
                    _ => throw new NotImplementedException("This infranstructure creator has not yet been implemented."),
                };

                infranstructureCreator.Initialize(settingsView.InfranstructureCreatorInitializationString!);

                if (infranstructureCreator.IsInfrastructureCreated is false)
                    infranstructureCreator.Create();
            }

            _ = new MainViewPresenter(this, settingsView.Repository!,
                _winFormsMessageService, settingsView.RepositoryInitializationString!);

            SwitchDataView(true);
            ShowDefaultDataView();
        }
        catch (Exception ex)
        {
            _winFormsMessageService.ShowError(ex.Message);
        }
    }

    private void SettingsToolStripMenuItemOnClick(object? sender, EventArgs e) =>
        new SettingsView().ShowDialog();

    private void ExitToolStripMenuItemOnClick(object? sender, EventArgs e) =>
        Close();

    private void DataGridViewDataOnColumnAdded(object? sender, DataGridViewColumnEventArgs e)
    {
        if (e.Column.ValueType == typeof(bool))
            e.Column.CellTemplate = new DataGridViewCheckBoxCell() { FlatStyle = FlatStyle.Flat };
    }

    private void DataGridViewDataOnColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e) =>
        SortData(e.ColumnIndex);

    private void DataGridViewDataOnCellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        string searchText = textBoxSearch.Text.Trim();

        if ((checkBoxApplyFilters.Checked is true) &&
            (e.RowIndex > -1) &&
            (e.ColumnIndex > -1) &&
            (string.IsNullOrWhiteSpace(searchText) is false) &&
            (e.Value?.GetType() != typeof(bool)))
        {
            e.PaintBackground(e.CellBounds, true);

            var matchRectangles = new List<Rectangle>();

            string cellText = e.FormattedValue.ToString()!;
            int searchTextCellIndex = cellText.IndexOf(searchText, StringComparison.OrdinalIgnoreCase);

            while (searchTextCellIndex > -1)
            {
                var matchRectangle = new Rectangle();

                string cellIndentText = cellText[..searchTextCellIndex];
                string cellSearchText = cellText.Substring(searchTextCellIndex, searchText.Length);

                Size cellIndentTextSize =
                    TextRenderer.MeasureText(e.Graphics, cellIndentText, e.CellStyle.Font, e.CellBounds.Size);
                Size searchTextSize =
                    TextRenderer.MeasureText(e.Graphics, cellSearchText, e.CellStyle.Font, e.CellBounds.Size);

                matchRectangle.Width = searchTextSize.Width - 10;
                matchRectangle.Height = searchTextSize.Height;

                matchRectangle.X =
                    e.CellBounds.X +
                    ((cellIndentTextSize.Width is not 0) ? cellIndentTextSize.Width : 10);

                matchRectangle.Y =
                    e.CellBounds.Y +
                    (e.CellBounds.Height - matchRectangle.Height) / 2;

                matchRectangles.Add(matchRectangle);
                searchTextCellIndex = cellText.IndexOf(searchText, searchTextCellIndex + 1, StringComparison.OrdinalIgnoreCase);
            }

            using var solidBrush = new SolidBrush(Color.FromArgb(200, 50, 50));

            foreach (Rectangle matchRectangle in matchRectangles)
                e.Graphics.FillRectangle(solidBrush, matchRectangle);

            e.PaintContent(e.CellBounds);

            e.Handled = true;
        }
    }

    private void IssuingsShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = IssuingsUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Issuings, "all");
    }

    private void ReadersShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = ReadersUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Readers, "all");
    }

    private void AuthorsShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = AuthorsUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Authors, "all");
    }

    private void BooksShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = BooksUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Books, "all");
    }

    private void ExportTextToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        var saveFileDialog = new SaveFileDialog()
        {
            Filter = "Text files (*.txt)|*.txt",
            Title = "Export"
        };

        if (saveFileDialog.ShowDialog() is DialogResult.OK)
            ExportDataText?.Invoke(this, GenerateExportData(saveFileDialog.FileName));
    }

    private void ExportExcelToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        var saveFileDialog = new SaveFileDialog()
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            Title = "Export"
        };

        if (saveFileDialog.ShowDialog() is DialogResult.OK)
            ExportDataExcel?.Invoke(this, GenerateExportData(saveFileDialog.FileName));
    }

    private void TextBoxSearchOnTextChanged(object? sender, EventArgs e) =>
        UpdateFilters();

    private void CheckBoxApplyFiltersOnCheckedChanged(object? sender, EventArgs e)
    {
        if (checkBoxApplyFilters.Checked is true)
            ApplyFilters();
        else
            DisableFilters();
    }

    private void ShowDefaultDataView() =>
        IssuingsShowAllToolStripMenuItemOnClick(this, EventArgs.Empty);

    private void SwitchDataView(bool isDataVisible)
    {
        if (isDataVisible is true)
        {
            _labelStartMessage.Visible = false;

            viewToolStripMenuItem.Visible = true;
            toolsToolStripMenuItem.Visible = true;
            panelMain.Visible = true;

            connectToDatabaseToolStripMenuItem.Visible = false;
        }
        else
        {
            _labelStartMessage.Visible = true;

            viewToolStripMenuItem.Visible = false;
            toolsToolStripMenuItem.Visible = false;
            panelMain.Visible = false;

            connectToDatabaseToolStripMenuItem.Visible = true;
        }
    }

    private void SwitchDataItemsVisibility(bool isDataItemsVisible)
    {
        if (isDataItemsVisible is true)
        {
            tableLayoutPanelMain.Controls.Remove(_labelDataNoItems);
            tableLayoutPanelMain.Controls.Add(dataGridViewData, 0, 2);
        }
        else
        {
            tableLayoutPanelMain.Controls.Remove(dataGridViewData);
            tableLayoutPanelMain.Controls.Add(_labelDataNoItems, 0, 2);

            if (dataGridViewData.SelectedRows.Count > 0)
                dataGridViewData.SelectedRows[0].Selected = false;
        }
    }

    private void UpdateDataView(ViewType viewType, string viewName, bool keepSorted = false, int? selectedItemId = null)
    {
        ClearCustomActions();
        ClearCustomFilters();

        switch (viewType)
        {
            case ViewType.Issuings:
                AddIssuingActions();
                AddIssuingFilters();
                break;
            case ViewType.Readers:
                AddReaderActions();
                AddReaderFilters();
                break;
            case ViewType.Authors:
                AddAuthorActions();
                break;
            case ViewType.Books:
                AddBookActions();
                break;
            default:
                break;
        }

        labelTableName.Text = viewType.ToString();
        toolStripStatusLabelView.Text = viewType.ToString() + ": " + viewName;

        if (_dataUpdateEventInvokationList is not null)
            foreach (Delegate eventDelegate in _dataUpdateEventInvokationList)
                eventDelegate.DynamicInvoke(this, EventArgs.Empty);

        dataGridViewData.DataSource = VisibleDataNormalView?.ToList();

        if ((VisibleDataNormalView is null) || (VisibleDataNormalView.Any() is false))
            SwitchDataItemsVisibility(false);
        else
        {
            SwitchDataItemsVisibility(true);

            string[]? dataColumnHeaders = VisibleDataColumnHeadersNormalView?.ToArray();

            if (dataColumnHeaders is not null)
                for (var i = 0; (i < dataGridViewData.ColumnCount) && (i < dataColumnHeaders.Length); i++)
                    dataGridViewData.Columns[i].HeaderText = dataColumnHeaders[i];

            SetSelectedItem(selectedItemId);

            if ((keepSorted is true) && (_dataPrevSortColumnIndex is not -1))
            {
                _dataIsAscSortDirection = !_dataIsAscSortDirection;

                SortData(_dataPrevSortColumnIndex);
            }
            else
                ResetDataSorting();

            if (checkBoxApplyFilters.Checked is true)
                ApplyFilters();
        }

        _currentViewType = viewType;
        _currentViewName = viewName;
    }

    private void AddIssuingActions()
    {
        Button buttonOpen = ControlCreation.MainCreateActionButton("Open");
        Button buttonClose = ControlCreation.MainCreateActionButton("Close");
        Button buttonEdit = ControlCreation.MainCreateActionButton("Edit");
        Button buttonRemove = ControlCreation.MainCreateActionButton("Remove");

        var openView = new AddEditItemView()
        {
            FieldsTitle = "Open issuing"
        };

        var closeView = new AddEditItemView()
        {
            FieldsTitle = "Close issuing"
        };

        var editView = new AddEditItemView()
        {
            FieldsTitle = "Edit issuing"
        };

        VisibleDataPickView = default;

        ReadersUpdatePickView?.Invoke(this, EventArgs.Empty);

        var pickItemsOpenReaderView = new PickItemsView(false)
        {
            ItemsTitle = "Readers",
            Items = new Dictionary<int, string?>(VisibleDataPickView ?? Enumerable.Empty<KeyValuePair<int, string?>>())
        };

        var pickItemsEditReaderView = new PickItemsView(false)
        {
            ItemsTitle = "Readers",
            Items = new Dictionary<int, string?>(VisibleDataPickView ?? Enumerable.Empty<KeyValuePair<int, string?>>())
        };

        VisibleDataPickView = default;

        BooksUpdatePickView?.Invoke(this, EventArgs.Empty);

        var pickItemsOpenBookView = new PickItemsView(false)
        {
            ItemsTitle = "Books",
            Items = new Dictionary<int, string?>(VisibleDataPickView ?? Enumerable.Empty<KeyValuePair<int, string?>>())
        };

        var pickItemsEditBookView = new PickItemsView(false)
        {
            ItemsTitle = "Books",
            Items = new Dictionary<int, string?>(VisibleDataPickView ?? Enumerable.Empty<KeyValuePair<int, string?>>())
        };

        Control controlOpenReader = pickItemsOpenReaderView.MainControl;
        Control controlOpenBook = pickItemsOpenBookView.MainControl;

        Label labelCloseId = ControlCreation.AddEditCreateLabel(string.Empty);
        NumericUpDown numericUpDownCloseReturnState = ControlCreation.AddEditCreateNumericUpDown();

        Label labelEditId = ControlCreation.AddEditCreateLabel(string.Empty);
        Control controlEditReader = pickItemsEditReaderView.MainControl;
        Control controlEditBook = pickItemsEditBookView.MainControl;
        DateTimePicker dateTimePickerEditTakeDate = ControlCreation.AddEditCreateDateTimePicker();
        CheckBox checkBoxEditReturned = ControlCreation.AddEditCreateCheckBox(string.Empty);
        DateTimePicker dateTimePickerEditReturnDate = ControlCreation.AddEditCreateDateTimePicker();
        NumericUpDown numericUpDownEditReturnState = ControlCreation.AddEditCreateNumericUpDown();

        openView
            .AddField("Reader:", controlOpenReader)
            .AddField("Book:", controlOpenBook);

        closeView
            .AddField("Id:", labelCloseId)
            .AddField("Return state:", numericUpDownCloseReturnState);

        editView
            .AddField("Id:", labelEditId)
            .AddField("Reader:", controlEditReader)
            .AddField("Book:", controlEditBook)
            .AddField("Take date:", dateTimePickerEditTakeDate)
            .AddField("Returned:", checkBoxEditReturned)
            .AddField("Return date:", dateTimePickerEditReturnDate)
            .AddField("Return state:", numericUpDownEditReturnState);

        buttonOpen.Click += (sender, e) =>
        {
            DialogResult dialogResult;

            do
            {
                dialogResult = openView.ShowDialog();

                if (dialogResult is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        pickItemsOpenReaderView.PickedItemIds,
                        pickItemsOpenBookView.PickedItemIds
                    };

                    IssuingOpen?.Invoke(this, args);

                    if (IsOperationSuccessful is true)
                    {
                        UpdateDataView(_currentViewType, _currentViewName, true);

                        break;
                    }
                }

            } while (dialogResult is DialogResult.OK);
        };

        buttonClose.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            if (((bool)dataGridViewData.SelectedRows[0].Cells[4].Value) is true)
            {
                _winFormsMessageService.ShowMessage("This issuing is already closed.");

                return;
            }

            int? selectedItemId = GetSelectedItemId();

            labelCloseId.Text = selectedItemId.ToString()!;

            DialogResult dialogResult;

            do
            {
                dialogResult = closeView.ShowDialog();

                if (dialogResult is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        (int)selectedItemId!,
                        numericUpDownCloseReturnState.Value
                    };

                    IssuingClose?.Invoke(this, args);

                    if (IsOperationSuccessful is true)
                    {
                        UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);

                        break;
                    }
                }

            } while (dialogResult is DialogResult.OK);
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int? selectedItemId = GetSelectedItemId();

            IssuingUpdateEditView?.Invoke(this, (int)selectedItemId!);

            if (VisibleDataEditView is not null)
            {
                pickItemsEditReaderView.ChangeAllItemsState(false);
                pickItemsEditBookView.ChangeAllItemsState(false);

                var data = (VisibleDataEditView as object?[])!;

                labelEditId.Text = ((int)data[0]!).ToString();
                pickItemsEditReaderView.ChangeItemsState((data[1] as IEnumerable<int>)!, true);
                pickItemsEditBookView.ChangeItemsState((data[2] as IEnumerable<int>)!, true);
                dateTimePickerEditTakeDate.Value = (DateTime)data[3]!;
                checkBoxEditReturned.Checked = (bool)data[4]!;
                dateTimePickerEditReturnDate.Value =
                    (data[5] is not null) ? (DateTime)data[5]! : dateTimePickerEditReturnDate.MinDate;
                numericUpDownEditReturnState.Value =
                    (data[6] is not null) ? (int)data[6]! : numericUpDownEditReturnState.Minimum;

                DialogResult dialogResult;

                do
                {
                    dialogResult = editView.ShowDialog();

                    if (dialogResult is DialogResult.OK)
                    {
                        var args = new object?[]
                        {
                            (int)selectedItemId!,
                            pickItemsEditReaderView.PickedItemIds,
                            pickItemsEditBookView.PickedItemIds,
                            dateTimePickerEditTakeDate.Value,
                            checkBoxEditReturned.Checked,
                            dateTimePickerEditReturnDate.Value,
                            numericUpDownEditReturnState.Value
                        };

                        IssuingEdit?.Invoke(this, args);

                        if (IsOperationSuccessful is true)
                        {
                            UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);

                            break;
                        }
                    }

                } while (dialogResult is DialogResult.OK);
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);
        };

        buttonRemove.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            if (MessageBox.Show("Are you sure you want to remove the selected item?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) is DialogResult.Yes)
            {
                int? selectedItemId = GetSelectedItemId();

                IssuingRemove?.Invoke(this, (int)selectedItemId!);

                if (IsOperationSuccessful is true)
                    UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        AddCustomAction(buttonOpen);
        AddCustomAction(buttonClose);
        AddCustomAction(buttonEdit);
        AddCustomAction(buttonRemove);
    }

    private void AddReaderActions()
    {
        Button buttonAdd = ControlCreation.MainCreateActionButton("Add");
        Button buttonEdit = ControlCreation.MainCreateActionButton("Edit");
        Button buttonRemove = ControlCreation.MainCreateActionButton("Remove");

        var addView = new AddEditItemView()
        {
            FieldsTitle = "New reader"
        };

        var editView = new AddEditItemView()
        {
            FieldsTitle = "Edit reader"
        };

        TextBox textBoxAddFirstName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxAddLastName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxAddPatronymic = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxAddGender = ControlCreation.AddEditCreateTextBox();
        DateTimePicker dateTimePickerAddDateOfBirth = ControlCreation.AddEditCreateDateTimePicker();

        Label labelEditId = ControlCreation.AddEditCreateLabel(string.Empty);
        TextBox textBoxEditFirstName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxEditLastName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxEditPatronymic = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxEditGender = ControlCreation.AddEditCreateTextBox();
        DateTimePicker dateTimePickerEditDateOfBirth = ControlCreation.AddEditCreateDateTimePicker();

        addView
            .AddField("First name:", textBoxAddFirstName)
            .AddField("Last name:", textBoxAddLastName)
            .AddField("Patronymic:", textBoxAddPatronymic)
            .AddField("Gender:", textBoxAddGender)
            .AddField("Date of birth:", dateTimePickerAddDateOfBirth);

        editView
            .AddField("Id:", labelEditId)
            .AddField("First name:", textBoxEditFirstName)
            .AddField("Last name:", textBoxEditLastName)
            .AddField("Patronymic:", textBoxEditPatronymic)
            .AddField("Gender:", textBoxEditGender)
            .AddField("Date of birth:", dateTimePickerEditDateOfBirth);

        buttonAdd.Click += (sender, e) =>
        {
            DialogResult dialogResult;

            do
            {
                dialogResult = addView.ShowDialog();

                if (dialogResult is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        textBoxAddFirstName.Text,
                        textBoxAddLastName.Text,
                        textBoxAddPatronymic.Text,
                        textBoxAddGender.Text,
                        dateTimePickerAddDateOfBirth.Value
                    };

                    ReaderAdd?.Invoke(this, args);

                    if (IsOperationSuccessful is true)
                    {
                        UpdateDataView(_currentViewType, _currentViewName, true);

                        break;
                    }
                }

            } while (dialogResult is DialogResult.OK);
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int? selectedItemId = GetSelectedItemId();

            ReaderUpdateEditView?.Invoke(this, (int)selectedItemId!);

            if (VisibleDataEditView is not null)
            {
                var data = (VisibleDataEditView as object?[])!;

                labelEditId.Text = ((int)data[0]!).ToString();
                textBoxEditFirstName.Text = data[1] as string;
                textBoxEditLastName.Text = data[2] as string;
                textBoxEditPatronymic.Text = data[3] as string;
                textBoxEditGender.Text = data[4] as string;
                dateTimePickerEditDateOfBirth.Value = (DateTime)data[5]!;

                DialogResult dialogResult;

                do
                {
                    dialogResult = editView.ShowDialog();

                    if (dialogResult is DialogResult.OK)
                    {
                        var args = new object?[]
                        {
                            (int)selectedItemId!,
                            textBoxEditFirstName.Text,
                            textBoxEditLastName.Text,
                            textBoxEditPatronymic.Text,
                            textBoxEditGender.Text,
                            dateTimePickerEditDateOfBirth.Value
                        };

                        ReaderEdit?.Invoke(this, args);

                        if (IsOperationSuccessful is true)
                        {
                            UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);

                            break;
                        }
                    }

                } while (dialogResult is DialogResult.OK);
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);
        };

        buttonRemove.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            if (MessageBox.Show("Are you sure you want to remove the selected item?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) is DialogResult.Yes)
            {
                int? selectedItemId = GetSelectedItemId();

                ReaderRemove?.Invoke(this, (int)selectedItemId!);

                if (IsOperationSuccessful is true)
                    UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        AddCustomAction(buttonAdd);
        AddCustomAction(buttonEdit);
        AddCustomAction(buttonRemove);
    }

    private void AddAuthorActions()
    {
        Button buttonAdd = ControlCreation.MainCreateActionButton("Add");
        Button buttonEdit = ControlCreation.MainCreateActionButton("Edit");
        Button buttonRemove = ControlCreation.MainCreateActionButton("Remove");

        var addView = new AddEditItemView()
        {
            FieldsTitle = "New author"
        };

        var editView = new AddEditItemView()
        {
            FieldsTitle = "Edit author"
        };

        TextBox textBoxAddFirstName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxAddLastName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxAddPatronymic = ControlCreation.AddEditCreateTextBox();

        Label labelEditId = ControlCreation.AddEditCreateLabel(string.Empty);
        TextBox textBoxEditFirstName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxEditLastName = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxEditPatronymic = ControlCreation.AddEditCreateTextBox();

        addView
            .AddField("First name:", textBoxAddFirstName)
            .AddField("Last name:", textBoxAddLastName)
            .AddField("Patronymic:", textBoxAddPatronymic);

        editView
            .AddField("Id:", labelEditId)
            .AddField("First name:", textBoxEditFirstName)
            .AddField("Last name:", textBoxEditLastName)
            .AddField("Patronymic:", textBoxEditPatronymic);

        buttonAdd.Click += (sender, e) =>
        {
            DialogResult dialogResult;

            do
            {
                dialogResult = addView.ShowDialog();

                if (dialogResult is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        textBoxAddFirstName.Text,
                        textBoxAddLastName.Text,
                        textBoxAddPatronymic.Text
                    };

                    AuthorAdd?.Invoke(this, args);

                    if (IsOperationSuccessful is true)
                    {
                        UpdateDataView(_currentViewType, _currentViewName, true);

                        break;
                    }
                }

            } while (dialogResult is DialogResult.OK);
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int? selectedItemId = GetSelectedItemId();

            AuthorUpdateEditView?.Invoke(this, (int)selectedItemId!);

            if (VisibleDataEditView is not null)
            {
                var data = (VisibleDataEditView as object?[])!;

                labelEditId.Text = ((int)data[0]!).ToString();
                textBoxEditFirstName.Text = data[1] as string;
                textBoxEditLastName.Text = data[2] as string;
                textBoxEditPatronymic.Text = data[3] as string;

                DialogResult dialogResult;

                do
                {
                    dialogResult = editView.ShowDialog();

                    if (dialogResult is DialogResult.OK)
                    {
                        var args = new object?[]
                        {
                            (int)selectedItemId!,
                            textBoxEditFirstName.Text,
                            textBoxEditLastName.Text,
                            textBoxEditPatronymic.Text
                        };

                        AuthorEdit?.Invoke(this, args);

                        if (IsOperationSuccessful is true)
                        {
                            UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);

                            break;
                        }
                    }

                } while (dialogResult is DialogResult.OK);
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);
        };

        buttonRemove.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            if (MessageBox.Show("Are you sure you want to remove the selected item?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) is DialogResult.Yes)
            {
                int? selectedItemId = GetSelectedItemId();

                AuthorRemove?.Invoke(this, (int)selectedItemId!);

                if (IsOperationSuccessful is true)
                    UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        AddCustomAction(buttonAdd);
        AddCustomAction(buttonEdit);
        AddCustomAction(buttonRemove);
    }

    private void AddBookActions()
    {
        Button buttonAdd = ControlCreation.MainCreateActionButton("Add");
        Button buttonEdit = ControlCreation.MainCreateActionButton("Edit");
        Button buttonRemove = ControlCreation.MainCreateActionButton("Remove");

        var addView = new AddEditItemView()
        {
            FieldsTitle = "New book"
        };

        var editView = new AddEditItemView()
        {
            FieldsTitle = "Edit book"
        };

        VisibleDataPickView = default;

        AuthorsUpdatePickView?.Invoke(this, EventArgs.Empty);

        var pickItemsAddView = new PickItemsView(true)
        {
            ItemsTitle = "Authors",
            Items = new Dictionary<int, string?>(VisibleDataPickView ?? Enumerable.Empty<KeyValuePair<int, string?>>())
        };

        var pickItemsEditView = new PickItemsView(true)
        {
            ItemsTitle = "Authors",
            Items = new Dictionary<int, string?>(VisibleDataPickView ?? Enumerable.Empty<KeyValuePair<int, string?>>())
        };

        Control controlAddAuthors = pickItemsAddView.MainControl;
        TextBox textBoxAddTitle = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxAddGenre = ControlCreation.AddEditCreateTextBox();

        Label labelEditId = ControlCreation.AddEditCreateLabel(string.Empty);
        Control controlEditAuthors = pickItemsEditView.MainControl;
        TextBox textBoxEditTitle = ControlCreation.AddEditCreateTextBox();
        TextBox textBoxEditGenre = ControlCreation.AddEditCreateTextBox();

        addView
            .AddField("Authors:", controlAddAuthors)
            .AddField("Title:", textBoxAddTitle)
            .AddField("Genre:", textBoxAddGenre);

        editView
            .AddField("Id:", labelEditId)
            .AddField("Authors:", controlEditAuthors)
            .AddField("Title:", textBoxEditTitle)
            .AddField("Genre:", textBoxEditGenre);

        buttonAdd.Click += (sender, e) =>
        {
            DialogResult dialogResult;

            do
            {
                dialogResult = addView.ShowDialog();

                if (dialogResult is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        pickItemsAddView.PickedItemIds,
                        textBoxAddTitle.Text,
                        textBoxAddGenre.Text
                    };

                    BookAdd?.Invoke(this, args);

                    if (IsOperationSuccessful is true)
                    {
                        UpdateDataView(_currentViewType, _currentViewName, true);

                        break;
                    }
                }

            } while (dialogResult is DialogResult.OK);
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int? selectedItemId = GetSelectedItemId();

            BookUpdateEditView?.Invoke(this, (int)selectedItemId!);

            if (VisibleDataEditView is not null)
            {
                pickItemsEditView.ChangeAllItemsState(false);

                var data = (VisibleDataEditView as object?[])!;

                labelEditId.Text = ((int)data[0]!).ToString();
                pickItemsEditView.ChangeItemsState((data[1] as IEnumerable<int>)!, true);
                textBoxEditTitle.Text = data[2] as string;
                textBoxEditGenre.Text = data[3] as string;

                DialogResult dialogResult;

                do
                {
                    dialogResult = editView.ShowDialog();

                    if (dialogResult is DialogResult.OK)
                    {
                        var args = new object?[]
                        {
                            (int)selectedItemId!,
                            pickItemsEditView.PickedItemIds,
                            textBoxEditTitle.Text,
                            textBoxEditGenre.Text
                        };

                        BookEdit?.Invoke(this, args);

                        if (IsOperationSuccessful is true)
                        {
                            UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);

                            break;
                        }
                    }

                } while (dialogResult is DialogResult.OK);
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true, selectedItemId);
        };

        buttonRemove.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            if (MessageBox.Show("Are you sure you want to remove the selected item?", "Warning",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) is DialogResult.Yes)
            {
                int? selectedItemId = GetSelectedItemId();

                BookRemove?.Invoke(this, (int)selectedItemId!);

                if (IsOperationSuccessful is true)
                    UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        AddCustomAction(buttonAdd);
        AddCustomAction(buttonEdit);
        AddCustomAction(buttonRemove);
    }

    private void AddIssuingFilters() =>
        AddPeriodFilter(ViewType.Issuings);

    private void AddReaderFilters() =>
        AddPeriodFilter(ViewType.Readers);

    private void AddPeriodFilter(ViewType viewType)
    {
        if (_currentViewType != viewType)
        {
            switch (viewType)
            {
                case ViewType.Issuings:
                    IssuingsUpdatePeriodView?.Invoke(this, EventArgs.Empty);
                    break;
                case ViewType.Readers:
                    ReadersUpdatePeriodView?.Invoke(this, EventArgs.Empty);
                    break;
                default:
                    break;
            }

            _pickPeriodView = new PickPeriodView()
            {
                PeriodColumnIndexNames = (VisibleDataPeriodView as Dictionary<int, string>)!
            };

            _pickPeriodView.PeriodSet += (sender, e) => UpdateFilters();
        }

        AddCustomFilter(_pickPeriodView!.MainControl, FilterPickPeriod);
    }

    private void FilterSearch(DataGridViewRow dataGridViewRow)
    {
        string searchText = textBoxSearch.Text.Trim();

        if ((string.IsNullOrWhiteSpace(searchText) is false) && (dataGridViewRow.Visible is true))
        {
            for (var i = 0; i < dataGridViewRow.Cells.Count; i++)
            {
                if (dataGridViewRow.Cells[i].Value?.GetType() == typeof(bool))
                    continue;

                if ((dataGridViewRow.Cells[i].Value?.ToString()?
                    .Contains(searchText, StringComparison.OrdinalIgnoreCase) ?? false) is true)
                    return;
            }

            dataGridViewRow.Visible = false;
        }
    }

    private void FilterPickPeriod(DataGridViewRow dataGridViewRow)
    {
        if ((_pickPeriodView is not null) &&
            (_pickPeriodView.StartPeriod is not null) &&
            (_pickPeriodView.EndPeriod is not null) &&
            (_pickPeriodView.PeriodColumnIndex is not null) &&
            (dataGridViewRow.Visible is true))
        {
            var dateTime = (DateTime?)dataGridViewRow.Cells[(int)_pickPeriodView.PeriodColumnIndex].Value;

            if ((dateTime is null) || (dateTime < _pickPeriodView.StartPeriod) || (dateTime > _pickPeriodView.EndPeriod))
                dataGridViewRow.Visible = false;
        }
    }

    private void AddCustomAction(Control control)
    {
        control.Anchor = AnchorStyles.Bottom;

        tableLayoutPanelCustomActions.ColumnStyles.Insert(
            tableLayoutPanelCustomActions.ColumnStyles.Count - 1,
            new ColumnStyle(SizeType.Absolute, control.Width + 20));

        tableLayoutPanelCustomActions.Controls.Add(control, tableLayoutPanelCustomActions.ColumnCount - 1, 0);

        tableLayoutPanelCustomActions.ColumnCount++;
    }

    private void ClearCustomActions()
    {
        tableLayoutPanelCustomActions.Controls.Clear();

        while (tableLayoutPanelCustomActions.ColumnStyles.Count > 2)
            tableLayoutPanelCustomActions.ColumnStyles.RemoveAt(1);

        tableLayoutPanelCustomActions.ColumnCount = 2;
    }

    private void AddCustomFilter(Control control, Action<DataGridViewRow>? filterMethod)
    {
        control.Dock = DockStyle.Fill;

        tableLayoutPanelCustomFilters.ColumnStyles.Insert(
            tableLayoutPanelCustomFilters.ColumnStyles.Count - 1,
            new ColumnStyle(SizeType.Absolute, control.Width + 20));

        tableLayoutPanelCustomFilters.Controls.Add(control, tableLayoutPanelCustomFilters.ColumnCount - 1, 0);

        tableLayoutPanelCustomFilters.ColumnCount++;

        _filterMethods = (Action<DataGridViewRow>?)Delegate.Combine(_filterMethods, filterMethod);
    }

    private void ClearCustomFilters()
    {
        tableLayoutPanelCustomFilters.Controls.Clear();

        while (tableLayoutPanelCustomFilters.ColumnStyles.Count > 1)
            tableLayoutPanelCustomFilters.ColumnStyles.RemoveAt(0);

        tableLayoutPanelCustomFilters.ColumnCount = 1;

        _filterMethods = FilterSearch;
    }

    private void SortData(int columnIndex)
    {
        int? selectedItemId = GetSelectedItemId();

        dataGridViewData.SortDataSourceObjectList(columnIndex, ref _dataIsAscSortDirection, ref _dataPrevSortColumnIndex);

        SetSelectedItem(selectedItemId);
    }

    private void ResetDataSorting()
    {
        _dataIsAscSortDirection = true;
        _dataPrevSortColumnIndex = -1;
    }

    private void ApplyFilters()
    {
        dataGridViewData.Visible = false;

        int? selectedItemId = GetSelectedItemId();
        dataGridViewData.CurrentCell = null;

        for (var i = 0; i < dataGridViewData.RowCount; i++)
            _filterMethods?.Invoke(dataGridViewData.Rows[i]);

        SetSelectedItem(selectedItemId);

        if (dataGridViewData.SelectedRows.Count < 1)
            SwitchDataItemsVisibility(false);

        dataGridViewData.Visible = true;
    }

    private void DisableFilters()
    {
        dataGridViewData.Visible = false;

        int? selectedItemId = GetSelectedItemId();

        for (var i = 0; i < dataGridViewData.RowCount; i++)
            dataGridViewData.Rows[i].Visible = true;

        SetSelectedItem(selectedItemId);

        if (dataGridViewData.SelectedRows.Count > 0)
            SwitchDataItemsVisibility(true);

        dataGridViewData.Visible = true;
    }

    private void UpdateFilters()
    {
        if (checkBoxApplyFilters.Checked is true)
        {
            DisableFilters();
            ApplyFilters();
        }
    }

    private int? GetSelectedItemId()
    {
        if ((VisibleDataNormalView is null) ||
            (VisibleDataNormalView.Any() is false) ||
            (dataGridViewData.SelectedRows.Count < 1))
            return null;

        return (int)dataGridViewData.SelectedRows[0].Cells[0].Value;
    }

    private void SetSelectedItem(int? itemId)
    {
        if (itemId is null)
        {
            if ((VisibleDataNormalView is not null) &&
                (VisibleDataNormalView.Any() is true) &&
                (dataGridViewData.SelectedRows.Count < 1))
            {
                dataGridViewData.Rows[0].Selected = true;
                dataGridViewData.CurrentCell = dataGridViewData.Rows[0].Cells[0];
            }

            return;
        }

        for (var i = 0; i < dataGridViewData.RowCount; i++)
            if ((int)dataGridViewData.Rows[i].Cells[0].Value == itemId)
            {
                if (dataGridViewData.Rows[i].Visible is true)
                {
                    dataGridViewData.Rows[i].Selected = true;
                    dataGridViewData.CurrentCell = dataGridViewData.Rows[i].Cells[0];
                }
                else
                    for (int j = 0; j < dataGridViewData.RowCount; j++)
                        if (dataGridViewData.Rows[j].Visible is true)
                        {
                            dataGridViewData.Rows[j].Selected = true;
                            dataGridViewData.CurrentCell = dataGridViewData.Rows[j].Cells[0];

                            break;
                        }
                        else
                            dataGridViewData.Rows[j].Selected = false;

                break;
            }
    }

    private IEnumerable<object> GenerateExportData(string filePath)
    {
        int rowCount = 0;
        int columnCount = dataGridViewData.ColumnCount;

        if (tableLayoutPanelMain.Controls.Contains(dataGridViewData) is true)
            for (var i = 0; i < dataGridViewData.RowCount; i++)
                if (dataGridViewData.Rows[i].Visible is true)
                    rowCount++;

        string title = labelTableName.Text;
        var columnHeaders = new string[columnCount];
        var exportData = new string[rowCount, columnCount];

        for (var j = 0; j < columnCount; j++)
            columnHeaders[j] = dataGridViewData.Columns[j].HeaderText;

        rowCount = 0;

        if (tableLayoutPanelMain.Controls.Contains(dataGridViewData) is true)
            for (var i = 0; i < dataGridViewData.RowCount; i++)
                if (dataGridViewData.Rows[i].Visible is true)
                {
                    columnCount = 0;

                    while (columnCount < dataGridViewData.ColumnCount)
                    {
                        object cellValue = dataGridViewData.Rows[i].Cells[columnCount].Value;

                        if (cellValue is null)
                            exportData[rowCount, columnCount] = string.Empty;
                        else
                        {
                            Type cellValueType = cellValue.GetType();

                            if (cellValueType == typeof(bool))
                                exportData[rowCount, columnCount] = ((bool)cellValue is true) ? "Yes" : "No";

                            else if (cellValueType == typeof(DateTime))
                                exportData[rowCount, columnCount] = ((DateTime)cellValue).ToString("d");

                            else
                                exportData[rowCount, columnCount] = cellValue.ToString()!;
                        }

                        columnCount++;
                    }

                    rowCount++;
                }

        return new object[]
        {
            title,
            columnHeaders,
            exportData,
            filePath
        };
    }
}