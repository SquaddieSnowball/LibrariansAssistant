namespace LibrariansAssistant.PresentationLayer.ViewInterfaces.WinFormsViewInterfaces;

public interface IMainView : IView
{
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
}