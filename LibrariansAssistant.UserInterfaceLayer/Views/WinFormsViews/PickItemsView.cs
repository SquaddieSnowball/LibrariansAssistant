using LibrariansAssistant.UserInterfaceLayer.Entities.WinFormsEntities;
using LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers;

namespace LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

internal sealed partial class PickItemsView : Form
{
    private Dictionary<int, string?>? _items;
    private string? _itemsTitle;
    private bool _dataIsAscSortDirection = true;
    private int _dataPrevSortColumnIndex = -1;

    public Dictionary<int, string?>? Items
    {
        get => _items;
        set
        {
            _items = value;

            UpdateView();
        }
    }

    public string? ItemsTitle
    {
        get => _itemsTitle;
        set
        {
            _itemsTitle = value;

            UpdateView();
        }
    }

    public IEnumerable<int> PickedItemIds { get; set; } = new List<int>();

    internal PickItemsView()
    {
        InitializeComponent();
        SubscribeToControlEvents();
        UpdateView();
    }

    private void SubscribeToControlEvents()
    {
        dataGridViewItems.ColumnAdded += DataGridViewItemsOnColumnAdded;
        dataGridViewItems.ColumnHeaderMouseClick += DataGridViewItemsOnColumnHeaderMouseClick;
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

    private void DataGridViewItemsOnColumnAdded(object? sender, DataGridViewColumnEventArgs e)
    {
        switch (e.Column.Index)
        {
            case 0:
                if (dataGridViewItems.Columns[0].HeaderText.StartsWith("\u2713") is false)
                    dataGridViewItems.Columns[0].HeaderText = "\u2713";

                dataGridViewItems.Columns[0].CellTemplate = new DataGridViewCheckBoxCell() { FlatStyle = FlatStyle.Flat };
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

    private void DataGridViewItemsOnColumnHeaderMouseClick(object? sender, DataGridViewCellMouseEventArgs e) =>
        dataGridViewItems.SortDataSourceObjectList(e.ColumnIndex, ref _dataIsAscSortDirection, ref _dataPrevSortColumnIndex);

    private void PickItemsViewOnFormClosed(object? sender, FormClosedEventArgs e)
    {
        IEnumerable<PickItem> pickedItems = ((List<object>)dataGridViewItems.DataSource).Cast<PickItem>();
        ((List<int>)PickedItemIds).Clear();

        foreach (var pickedItem in pickedItems)
            if (pickedItem.IsChecked is true)
                ((List<int>)PickedItemIds).Add(pickedItem.Id);
    }
}