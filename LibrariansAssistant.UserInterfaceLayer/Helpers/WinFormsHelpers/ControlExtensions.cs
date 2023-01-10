namespace LibrariansAssistant.UserInterfaceLayer.Helpers.WinFormsHelpers;

/// <summary>
/// Provides methods for extending the functionality of controls.
/// </summary>
internal static class ControlExtensions
{
    /// <summary>
    /// Sorts a data source containing a list of objects.
    /// </summary>
    /// <param name="dataGridView">Instance of the DataGridView class for sorting.</param>
    /// <param name="columnIndex">Index of the column to sort by.</param>
    /// <param name="dataIsAscSortDirection">Value indicating whether ascending sorting should be applied.</param>
    /// <param name="dataPrevSortColumnIndex">Index of the previous sorted column.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    internal static void SortDataSourceObjectList(this DataGridView dataGridView,
        int columnIndex, ref bool dataIsAscSortDirection, ref int dataPrevSortColumnIndex)
    {
        if (dataGridView is null)
            throw new ArgumentNullException(nameof(dataGridView), "Data grid view must not be null.");

        if (dataGridView.DataSource is null)
            throw new ArgumentNullException(nameof(dataGridView.DataSource), "Data source must not be null.");

        List<object> data;

        try
        {
            data = (List<object>)dataGridView.DataSource;
        }
        catch (InvalidCastException)
        {
            throw new InvalidOperationException("Data source must be a list of objects.");
        }

        if (columnIndex == dataPrevSortColumnIndex)
            dataIsAscSortDirection = !dataIsAscSortDirection;

        dataGridView.DataSource =
            (dataIsAscSortDirection is true) ?
            data.OrderBy(o => o.GetType().GetProperty(dataGridView.Columns[columnIndex].Name)!.GetValue(o)).ToList() :
            data.OrderByDescending(o => o.GetType().GetProperty(dataGridView.Columns[columnIndex].Name)!.GetValue(o)).ToList();

        if (dataPrevSortColumnIndex is not -1)
        {
            string sortedCulumnHeaderText = dataGridView.Columns[dataPrevSortColumnIndex].HeaderText;

            if ((sortedCulumnHeaderText.EndsWith('\u2191') is true) || (sortedCulumnHeaderText.EndsWith('\u2193') is true))
                dataGridView.Columns[dataPrevSortColumnIndex].HeaderText =
                    sortedCulumnHeaderText.Remove(sortedCulumnHeaderText.Length - 1, 1);
        }

        if (dataIsAscSortDirection is true)
            dataGridView.Columns[columnIndex].HeaderText += '\u2191';
        else
            dataGridView.Columns[columnIndex].HeaderText += '\u2193';

        dataPrevSortColumnIndex = columnIndex;
    }
}