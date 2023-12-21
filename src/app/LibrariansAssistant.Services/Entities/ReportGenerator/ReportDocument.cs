using LibrariansAssistant.Validation.Helpers;

namespace LibrariansAssistant.Services.Entities.ReportGenerator;

/// <summary>
/// Represents a report document.
/// </summary>
public sealed class ReportDocument
{
    private readonly string[,] _reportData;

    /// <summary>
    /// Gets the title of the document.
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// Gets the document's column headers.
    /// </summary>
    public IEnumerable<string> ColumnHeaders { get; }

    /// <summary>
    /// Gets the number of rows in the document.
    /// </summary>
    public int RowCount => _reportData.GetLength(0);

    /// <summary>
    /// Gets the number of columns in the document.
    /// </summary>
    public int ColumnCount => _reportData.GetLength(1);

    /// <summary>
    /// Gets or sets the report document element.
    /// </summary>
    /// <param name="rowIndex">Row index.</param>
    /// <param name="columnIndex">Column index.</param>
    /// <returns>Report document element.</returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                _reportData[rowIndex, columnIndex] = value is not null ? value : string.Empty;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException(ex.ParamName, "Index was outside the bounds of the report document.");
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of the ReportDocument class.
    /// </summary>
    /// <param name="title">Title of the document.</param>
    /// <param name="columnHeaders">Document's column headers.</param>
    /// <param name="rowCount">Number of rows in the document.</param>
    /// <param name="columnCount">Number of columns in the document.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public ReportDocument(string title, IEnumerable<string> columnHeaders, int rowCount, int columnCount)
    {
        Verify.NotNullOrEmpty(title);
        Verify.NotNull(columnHeaders);

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