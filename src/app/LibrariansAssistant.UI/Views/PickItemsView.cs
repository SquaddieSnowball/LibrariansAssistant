using LibrariansAssistant.UI.Entities;
using LibrariansAssistant.UI.Helpers;

namespace LibrariansAssistant.UI.Views;

/// <summary>
/// Represents the "Pick Items" view of the application.
/// </summary>
internal sealed partial class PickItemsView : Form
{
    private readonly Button _buttonPlus;
    private readonly Label _labelPicked;
    private readonly bool _isMultipick;
    private Dictionary<int, string?>? _items;
    private string? _itemsTitle;
    private int _lastPickedItemId = -1;
    private bool _dataIsAscSortDirection = true;
    private int _dataPrevSortColumnIndex = -1;

    #region Properties

    /// <summary>
    /// Gets the main control of the view.
    /// </summary>
    public Control MainControl { get; }

    /// <summary>
    /// Get or sets the items of the view.
    /// </summary>
    public Dictionary<int, string?>? Items
    {
        get => _items;
        set
        {
            _items = value;

            UpdateView();
        }
    }

    /// <summary>
    /// Get or sets the title of the items.
    /// </summary>
    public string? ItemsTitle
    {
        get => _itemsTitle;
        set
        {
            _itemsTitle = value;

            UpdateView();
        }
    }

    /// <summary>
    /// Gets the IDs of the picked items.
    /// </summary>
    public IEnumerable<int> PickedItemIds { get; } = new List<int>();

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="PickItemsView"/> class.
    /// </summary>
    /// <param name="isMultipick">A value indicating whether multiple items can be selected.</param>
    public PickItemsView(bool isMultipick)
    {
        _isMultipick = isMultipick;
        MainControl = ControlCreation.PickItemsCreateTableLayoutPanel(out _buttonPlus, out _labelPicked);

        InitializeComponent();
        SubscribeToControlEvents();
        UpdateView();
    }

    /// <summary>
    /// Select or deselect all items.
    /// </summary>
    /// <param name="pick"><see langword="true"/> to select all items; otherwise, <see langword="false"/>.</param>
    public void ChangeAllItemsState(bool pick)
    {
        for (var i = 0; i < dataGridViewItems.Rows.Count; i++)
        {
            dataGridViewItems.Rows[i].Cells[0].Value = pick;

            if (pick is true)
                _lastPickedItemId = (int)dataGridViewItems.Rows[i].Cells[1].Value;
        }

        UpdatePickedItems();
    }

    /// <summary>
    /// Select or deselect items.
    /// </summary>
    /// <param name="ids">Item IDs.</param>
    /// <param name="pick"><see langword="true"/> to select items; otherwise, <see langword="false"/>.</param>
    public void ChangeItemsState(IEnumerable<int> ids, bool pick)
    {
        foreach (int id in ids)
        {
            for (var i = 0; i < dataGridViewItems.Rows.Count; i++)
            {
                if ((int)dataGridViewItems.Rows[i].Cells[1].Value == id)
                {
                    dataGridViewItems.Rows[i].Cells[0].Value = pick;

                    if (pick is true)
                        _lastPickedItemId = id;

                    break;
                }
            }
        }

        UpdatePickedItems();
    }

    private void SubscribeToControlEvents()
    {
        dataGridViewItems.ColumnAdded += DataGridViewItemsOnColumnAdded;
        dataGridViewItems.CellDoubleClick += DataGridViewItemsOnCellDoubleClick;
        dataGridViewItems.CellMouseUp += DataGridViewItemsOnCellMouseUp;
        dataGridViewItems.CellValueChanged += DataGridViewItemsOnCellValueChanged;
        dataGridViewItems.ColumnHeaderMouseClick += DataGridViewItemsOnColumnHeaderMouseClick;
        _buttonPlus.Click += ButtonPlusOnClick;
        FormClosed += PickItemsViewOnFormClosed;
    }

    private void UpdateView()
    {
        string itemsTitle = (string.IsNullOrEmpty(_itemsTitle) is true) ? "Unnamed" : _itemsTitle;

        if ((_items is not null) && (_items.Any() is true))
        {
            dataGridViewItems.DataSource = _items.Select(i => new PickItem(i.Key, i.Value)).Cast<object>().ToList();
            labelItemsName.Text = itemsTitle;
            Height = 333;
        }
        else
        {
            labelItemsName.Text = itemsTitle + ": no items to show";
            Height = 140;
        }
    }

    #region Control event handlers

    private void DataGridViewItemsOnColumnAdded(object? sender, DataGridViewColumnEventArgs e)
    {
        switch (e.Column.Index)
        {
            case 0:
                if (dataGridViewItems.Columns[0].HeaderText.StartsWith("\u2713") is false)
                    dataGridViewItems.Columns[0].HeaderText = "\u2713";

                dataGridViewItems.Columns[0].CellTemplate = new DataGridViewCheckBoxCell()
                {
                    FlatStyle = FlatStyle.Flat
                };

                dataGridViewItems.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewItems.Columns[0].Width = 40;
                break;
            case 1:
                dataGridViewItems.Columns[1].ReadOnly = true;
                dataGridViewItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dataGridViewItems.Columns[1].Width = 60;
                break;
            case 2:
                dataGridViewItems.Columns[2].ReadOnly = true;
                break;
            default:
                break;
        }
    }

    private void DataGridViewItemsOnCellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if ((e.ColumnIndex is 0) && (e.RowIndex is not -1))
            _ = dataGridViewItems.EndEdit();
    }

    private void DataGridViewItemsOnCellMouseUp(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if ((e.ColumnIndex is 0) && (e.RowIndex is not -1))
            _ = dataGridViewItems.EndEdit();
    }

    private void DataGridViewItemsOnCellValueChanged(object? sender, DataGridViewCellEventArgs e)
    {
        if ((e.ColumnIndex is 0) && (e.RowIndex is not -1))
        {
            int cellId = (int)dataGridViewItems.Rows[e.RowIndex].Cells[1].Value;

            if (cellId == _lastPickedItemId)
                return;

            if ((_isMultipick is false) && (_lastPickedItemId is not -1))
            {
                for (var i = 0; i < dataGridViewItems.Rows.Count; i++)
                {
                    if ((int)dataGridViewItems.Rows[i].Cells[1].Value == _lastPickedItemId)
                    {
                        dataGridViewItems.Rows[i].Cells[0].Value = false;

                        break;
                    }
                }
            }

            _lastPickedItemId = cellId;
        }
    }

    private void DataGridViewItemsOnColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e) =>
        dataGridViewItems.SortDataSourceObjectList(e.ColumnIndex, ref _dataIsAscSortDirection, ref _dataPrevSortColumnIndex);

    private void ButtonPlusOnClick(object? sender, EventArgs e) => _ = ShowDialog();

    private void PickItemsViewOnFormClosed(object? sender, FormClosedEventArgs e) => UpdatePickedItems();

    #endregion

    private void UpdatePickedItems()
    {
        if ((_items is not null) && (_items.Any() is true))
        {
            IEnumerable<PickItem> pickedItems = ((List<object>)dataGridViewItems.DataSource).Cast<PickItem>();
            ((List<int>)PickedItemIds).Clear();

            foreach (PickItem pickedItem in pickedItems)
            {
                if (pickedItem.IsChecked is true)
                    ((List<int>)PickedItemIds).Add(pickedItem.Id);
            }
        }

        if (PickedItemIds.Any() is true)
            _labelPicked.Text = (_isMultipick is true) ? ("Picked: " + PickedItemIds.Count()) : "\u2713";
        else
            _labelPicked.Text = "\u2717";
    }
}