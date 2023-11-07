namespace LibrariansAssistant.Presentation.ViewInterfaces.WinFormsViewInterfaces;

/// <summary>
/// Provides properties and events used by the presenter to control the MainView.
/// </summary>
public interface IMainView : IView
{
    /// <summary>
    /// Gets or sets the data for Normal view.
    /// </summary>
    public IEnumerable<object>? VisibleDataNormalView { get; set; }

    /// <summary>
    /// Gets or sets the data for Edit view.
    /// </summary>
    public IEnumerable<object?>? VisibleDataEditView { get; set; }

    /// <summary>
    /// Gets or sets the data for Pick view.
    /// </summary>
    public IEnumerable<KeyValuePair<int, string?>>? VisibleDataPickView { get; set; }

    /// <summary>
    /// Gets or sets the data for Period view.
    /// </summary>
    public IEnumerable<KeyValuePair<int, string>>? VisibleDataPeriodView { get; set; }

    /// <summary>
    /// Gets or sets data for column headers in the Normal view.
    /// </summary>
    public IEnumerable<string>? VisibleDataColumnHeadersNormalView { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the requested operation was successful.
    /// </summary>
    public bool IsOperationSuccessful { get; set; }

    /// <summary>
    /// Occurs when the Normal view requests an update of the authors.
    /// </summary>
    public event EventHandler? AuthorsUpdateNormalView;

    /// <summary>
    /// Occurs when the Normal view requests an update of the books.
    /// </summary>
    public event EventHandler? BooksUpdateNormalView;

    /// <summary>
    /// Occurs when the Normal view requests an update of the issuings.
    /// </summary>
    public event EventHandler? IssuingsUpdateNormalView;

    /// <summary>
    /// Occurs when the Normal view requests an update of the readers.
    /// </summary>
    public event EventHandler? ReadersUpdateNormalView;

    /// <summary>
    /// Occurs when the Edit view requests an update of the authors.
    /// </summary>
    public event EventHandler<int>? AuthorUpdateEditView;

    /// <summary>
    /// Occurs when the Edit view requests an update of the books.
    /// </summary>
    public event EventHandler<int>? BookUpdateEditView;

    /// <summary>
    /// Occurs when the Edit view requests an update of the issuings.
    /// </summary>
    public event EventHandler<int>? IssuingUpdateEditView;

    /// <summary>
    /// Occurs when the Edit view requests an update of the readers.
    /// </summary>
    public event EventHandler<int>? ReaderUpdateEditView;

    /// <summary>
    /// Occurs when the Pick view requests an update of the authors.
    /// </summary>
    public event EventHandler? AuthorsUpdatePickView;

    /// <summary>
    /// Occurs when the Pick view requests an update of the books.
    /// </summary>
    public event EventHandler? BooksUpdatePickView;

    /// <summary>
    /// Occurs when the Pick view requests an update of the readers.
    /// </summary>
    public event EventHandler? ReadersUpdatePickView;

    /// <summary>
    /// Occurs when the Period view requests an update of the issuings.
    /// </summary>
    public event EventHandler? IssuingsUpdatePeriodView;

    /// <summary>
    /// Occurs when the Period view requests an update of the readers.
    /// </summary>
    public event EventHandler? ReadersUpdatePeriodView;

    /// <summary>
    /// Occurs when the view requests the most active reader.
    /// </summary>
    public event EventHandler? ReaderUpdateMostActiveView;

    /// <summary>
    /// Occurs when the view requests the most popular author.
    /// </summary>
    public event EventHandler? AuthorUpdateMostPopularView;

    /// <summary>
    /// Occurs when the view requests the most popular book genre.
    /// </summary>
    public event EventHandler? BookUpdateMostPopularGenreView;

    /// <summary>
    /// Occurs when the view requests the opening of a new issuing.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? IssuingOpen;

    /// <summary>
    /// Occurs when a view requests that an issuing be closed.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? IssuingClose;

    /// <summary>
    /// Occurs when the view requests the addition of a new author.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? AuthorAdd;

    /// <summary>
    /// Occurs when the view requests the addition of a new book.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? BookAdd;

    /// <summary>
    /// Occurs when the view requests the addition of a new reader.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? ReaderAdd;

    /// <summary>
    /// Occurs when the view requests editing of an existing author.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? AuthorEdit;

    /// <summary>
    /// Occurs when the view requests editing of an existing book.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? BookEdit;

    /// <summary>
    /// Occurs when the view requests editing of an existing issuing.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? IssuingEdit;

    /// <summary>
    /// Occurs when the view requests editing of an existing reader.
    /// </summary>
    public event EventHandler<IEnumerable<object?>>? ReaderEdit;

    /// <summary>
    /// Occurs when the view requests removing of an existing author.
    /// </summary>
    public event EventHandler<int>? AuthorRemove;

    /// <summary>
    /// Occurs when the view requests removing of an existing book.
    /// </summary>
    public event EventHandler<int>? BookRemove;

    /// <summary>
    /// Occurs when the view requests removing of an existing issuing.
    /// </summary>
    public event EventHandler<int>? IssuingRemove;

    /// <summary>
    /// Occurs when the view requests removing of an existing reader.
    /// </summary>
    public event EventHandler<int>? ReaderRemove;

    /// <summary>
    /// Occurs when the view requests that data be exported in text format.
    /// </summary>
    public event EventHandler<IEnumerable<object>>? ExportDataText;

    /// <summary>
    /// Occurs when the view requests that data be exported in Excel format.
    /// </summary>
    public event EventHandler<IEnumerable<object>>? ExportDataExcel;
}