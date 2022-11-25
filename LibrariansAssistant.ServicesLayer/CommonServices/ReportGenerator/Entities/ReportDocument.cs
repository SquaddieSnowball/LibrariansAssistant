namespace LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Entities;

public sealed class ReportDocument
{
    private readonly string[,] _reportData;

    public string Title { get; }

    public IEnumerable<string> ColumnHeaders { get; }

    public int RowCount => _reportData.GetLength(0);

    public int ColumnCount => _reportData.GetLength(1);

    public string this[int rowIndex, int columnIndex]
    {
        get
        {
            try
            {
                return _reportData[rowIndex, columnIndex];
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(ex.ParamName, "Index was outside the bounds of the report document.");
            }
        }

        set
        {
            try
            {
                _reportData[rowIndex, columnIndex] = (value is not null) ? value : string.Empty;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(ex.ParamName, "Index was outside the bounds of the report document.");
            }
        }
    }

    public ReportDocument(string title, IEnumerable<string> columnHeaders, int rowCount, int columnCount)
    {
        if (string.IsNullOrEmpty(title) is true)
            throw new ArgumentException("Title must not be null or empty.", nameof(title));

        if (columnHeaders is null)
            throw new ArgumentNullException(nameof(columnHeaders), "Column headers must not be null.");

        if (rowCount < 1)
            throw new ArgumentException("The report document must contain at least one row.", nameof(rowCount));

        if (columnCount < 1)
            throw new ArgumentException("The report document must contain at least one column.", nameof(columnCount));

        if (columnHeaders.Count() != columnCount)
            throw new ArgumentException("The number of column headers must be equal to the number of columns.",
                nameof(columnHeaders));

        _reportData = new string[rowCount, columnCount];

        Title = title;
        ColumnHeaders = columnHeaders;
    }
}