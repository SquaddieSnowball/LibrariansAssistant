using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Reader;

namespace LibrariansAssistant.DomainModelLayer.Models.Issuing;

public sealed class IssuingModel : IIssuingModel
{
    public int Id { get; set; }

    public IReaderModel Reader { get; set; }

    public IBookModel Book { get; set; }

    public DateTime TakeDate { get; set; }

    public bool Returned { get; set; }

    public DateTime? ReturnDate { get; set; }

    public int? ReturnState { get; set; }

    public IssuingModel() =>
        (Reader, Book) =
        (DependenciesContainer.Resolve<IReaderModel>()!, DependenciesContainer.Resolve<IBookModel>()!);

    public IssuingModel(IReaderModel reader, IBookModel book, DateTime takeDate,
        bool returned, DateTime? returnDate, int? returnState) =>
        (Reader, Book, TakeDate, Returned, ReturnDate, ReturnState) =
        (reader, book, takeDate, returned, returnDate, returnState);
}