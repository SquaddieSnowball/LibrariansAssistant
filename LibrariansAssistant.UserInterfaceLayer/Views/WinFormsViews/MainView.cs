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

    public IEnumerable<object>? VisibleDataNormalView { get; set; }

    public IEnumerable<object?>? VisibleDataEditView { get; set; }

    public IEnumerable<KeyValuePair<int, string?>>? VisibleDataPickView { get; set; }

    public IEnumerable<string>? VisibleDataColumnHeadersNormalView { get; set; }

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

        issuingsShowAllToolStripMenuItem.Click += IssuingsShowAllToolStripMenuItemOnClick;
        readersShowAllToolStripMenuItem.Click += ReadersShowAllToolStripMenuItemOnClick;
        authorsShowAllToolStripMenuItem.Click += AuthorsShowAllToolStripMenuItemOnClick;
        booksShowAllToolStripMenuItem.Click += BooksShowAllToolStripMenuItemOnClick;
    }

    private void InitializeView()
    {
        menuStripMain.Renderer = new MenuStripMainRenderer(new MenuStripMainColorTable());
        Controls.Add(_labelStartMessage);

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
            _ = new MainViewPresenter(this, settingsView.Repository!,
                _winFormsMessageService, settingsView.InitializationString!);

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
        dataGridViewData.SortDataSourceObjectList(e.ColumnIndex, ref _dataIsAscSortDirection, ref _dataPrevSortColumnIndex);

    private void IssuingsShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = IssuingsUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Issuings, "all", false);
    }

    private void ReadersShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = ReadersUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Readers, "all", false);
    }

    private void AuthorsShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = AuthorsUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Authors, "all", false);
    }

    private void BooksShowAllToolStripMenuItemOnClick(object? sender, EventArgs e)
    {
        _dataUpdateEventInvokationList = BooksUpdateNormalView?.GetInvocationList();

        UpdateDataView(ViewType.Books, "all", false);
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

    private void UpdateDataView(ViewType viewType, string viewName, bool keepSorted)
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
                AddAuthorFilters();
                break;
            case ViewType.Books:
                AddBookActions();
                AddBookFilters();
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
        {
            tableLayoutPanelMain.Controls.Remove(dataGridViewData);
            tableLayoutPanelMain.Controls.Add(_labelDataNoItems, 0, 2);
        }
        else
        {
            tableLayoutPanelMain.Controls.Remove(_labelDataNoItems);
            tableLayoutPanelMain.Controls.Add(dataGridViewData, 0, 2);

            string[]? dataColumnHeaders = VisibleDataColumnHeadersNormalView?.ToArray();

            if (dataColumnHeaders is not null)
                for (var i = 0; (i < dataGridViewData.ColumnCount) && (i < dataColumnHeaders.Length); i++)
                    dataGridViewData.Columns[i].HeaderText = dataColumnHeaders[i];
        }

        if ((keepSorted is true) && (_dataPrevSortColumnIndex is not -1))
        {
            _dataIsAscSortDirection = !_dataIsAscSortDirection;

            dataGridViewData.SortDataSourceObjectList(_dataPrevSortColumnIndex,
                ref _dataIsAscSortDirection, ref _dataPrevSortColumnIndex);
        }
        else
            ResetSorting();

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
            if (openView.ShowDialog() is DialogResult.OK)
            {
                var args = new object?[]
                {
                    pickItemsOpenReaderView.PickedItemIds,
                    pickItemsOpenBookView.PickedItemIds
                };

                IssuingOpen?.Invoke(this, args);

                UpdateDataView(_currentViewType, _currentViewName, true);
            }
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

            labelCloseId.Text = dataGridViewData.SelectedRows[0].Cells[0].Value.ToString()!;

            if (closeView.ShowDialog() is DialogResult.OK)
            {
                var args = new object?[]
                {
                        int.Parse(labelCloseId.Text),
                        numericUpDownCloseReturnState.Value
                };

                IssuingClose?.Invoke(this, args);

                UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

            IssuingUpdateEditView?.Invoke(this, id);

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

                if (editView.ShowDialog() is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        int.Parse(labelEditId.Text),
                        pickItemsEditReaderView.PickedItemIds,
                        pickItemsEditBookView.PickedItemIds,
                        dateTimePickerEditTakeDate.Value,
                        checkBoxEditReturned.Checked,
                        dateTimePickerEditReturnDate.Value,
                        numericUpDownEditReturnState.Value
                    };

                    IssuingEdit?.Invoke(this, args);

                    UpdateDataView(_currentViewType, _currentViewName, true);
                }
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true);
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
                int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

                IssuingRemove?.Invoke(this, id);

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
            if (addView.ShowDialog() is DialogResult.OK)
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

                UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

            ReaderUpdateEditView?.Invoke(this, id);

            if (VisibleDataEditView is not null)
            {
                var data = (VisibleDataEditView as object?[])!;

                labelEditId.Text = ((int)data[0]!).ToString();
                textBoxEditFirstName.Text = data[1] as string;
                textBoxEditLastName.Text = data[2] as string;
                textBoxEditPatronymic.Text = data[3] as string;
                textBoxEditGender.Text = data[4] as string;
                dateTimePickerEditDateOfBirth.Value = (DateTime)data[5]!;

                if (editView.ShowDialog() is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        int.Parse(labelEditId.Text),
                        textBoxEditFirstName.Text,
                        textBoxEditLastName.Text,
                        textBoxEditPatronymic.Text,
                        textBoxEditGender.Text,
                        dateTimePickerEditDateOfBirth.Value
                    };

                    ReaderEdit?.Invoke(this, args);

                    UpdateDataView(_currentViewType, _currentViewName, true);
                }
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true);
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
                int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

                ReaderRemove?.Invoke(this, id);

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
            if (addView.ShowDialog() is DialogResult.OK)
            {
                var args = new object?[]
                {
                    textBoxAddFirstName.Text,
                    textBoxAddLastName.Text,
                    textBoxAddPatronymic.Text
                };

                AuthorAdd?.Invoke(this, args);

                UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

            AuthorUpdateEditView?.Invoke(this, id);

            if (VisibleDataEditView is not null)
            {
                var data = (VisibleDataEditView as object?[])!;

                labelEditId.Text = ((int)data[0]!).ToString();
                textBoxEditFirstName.Text = data[1] as string;
                textBoxEditLastName.Text = data[2] as string;
                textBoxEditPatronymic.Text = data[3] as string;

                if (editView.ShowDialog() is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        int.Parse(labelEditId.Text),
                        textBoxEditFirstName.Text,
                        textBoxEditLastName.Text,
                        textBoxEditPatronymic.Text
                    };

                    AuthorEdit?.Invoke(this, args);

                    UpdateDataView(_currentViewType, _currentViewName, true);
                }
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true);
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
                int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

                AuthorRemove?.Invoke(this, id);

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
            if (addView.ShowDialog() is DialogResult.OK)
            {
                var args = new object?[]
                {
                    pickItemsAddView.PickedItemIds,
                    textBoxAddTitle.Text,
                    textBoxAddGenre.Text
                };

                BookAdd?.Invoke(this, args);

                UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        buttonEdit.Click += (sender, e) =>
        {
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                _winFormsMessageService.ShowMessage("No items selected.");

                return;
            }

            VisibleDataEditView = default;

            int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

            BookUpdateEditView?.Invoke(this, id);

            if (VisibleDataEditView is not null)
            {
                pickItemsEditView.ChangeAllItemsState(false);

                var data = (VisibleDataEditView as object?[])!;

                labelEditId.Text = ((int)data[0]!).ToString();
                pickItemsEditView.ChangeItemsState((data[1] as IEnumerable<int>)!, true);
                textBoxEditTitle.Text = data[2] as string;
                textBoxEditGenre.Text = data[3] as string;

                if (editView.ShowDialog() is DialogResult.OK)
                {
                    var args = new object?[]
                    {
                        int.Parse(labelEditId.Text),
                        pickItemsEditView.PickedItemIds,
                        textBoxEditTitle.Text,
                        textBoxEditGenre.Text
                    };

                    BookEdit?.Invoke(this, args);

                    UpdateDataView(_currentViewType, _currentViewName, true);
                }
            }
            else
                UpdateDataView(_currentViewType, _currentViewName, true);
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
                int id = (int)dataGridViewData.SelectedRows[0].Cells[0].Value;

                BookRemove?.Invoke(this, id);

                UpdateDataView(_currentViewType, _currentViewName, true);
            }
        };

        AddCustomAction(buttonAdd);
        AddCustomAction(buttonEdit);
        AddCustomAction(buttonRemove);
    }

    private void AddIssuingFilters()
    {
        //throw new NotImplementedException();
    }

    private void AddReaderFilters()
    {
        //throw new NotImplementedException();
    }

    private void AddAuthorFilters()
    {
        //throw new NotImplementedException();
    }

    private void AddBookFilters()
    {
        //throw new NotImplementedException();
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

    private void AddCustomFilter(Control control)
    {
        control.Anchor = AnchorStyles.Left;

        tableLayoutPanelCustomFilters.ColumnStyles.Insert(
            tableLayoutPanelCustomFilters.ColumnStyles.Count - 1,
            new ColumnStyle(SizeType.Absolute, control.Width + 20));

        tableLayoutPanelCustomFilters.Controls.Add(control, tableLayoutPanelCustomFilters.ColumnCount - 1, 0);

        tableLayoutPanelCustomFilters.ColumnCount++;
    }

    private void ClearCustomFilters()
    {
        tableLayoutPanelCustomFilters.Controls.Clear();

        while (tableLayoutPanelCustomFilters.ColumnStyles.Count > 1)
            tableLayoutPanelCustomFilters.ColumnStyles.RemoveAt(0);

        tableLayoutPanelCustomFilters.ColumnCount = 1;
    }

    private void ResetSorting()
    {
        _dataIsAscSortDirection = true;
        _dataPrevSortColumnIndex = -1;
    }
}