using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.DomainModelLayer.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace LibrariansAssistant.DomainModelLayer.Models.Issuing;

public sealed class IssuingModel : IIssuingModel
{
    public int Id { get; set; }

    [NotDefaultModel(ErrorMessage = "Reader is required.")]
    public IReaderModel Reader { get; set; }

    [NotDefaultModel(ErrorMessage = "Book is required.")]
    public IBookModel Book { get; set; }

    public DateTime TakeDate { get; set; }

    public bool Returned { get; set; }

    public DateTime? ReturnDate { get; set; }

    [Range(1, 100, ErrorMessage = "Return state must be in range of 1 to 100.")]
    public int? ReturnState { get; set; }

    public IssuingModel() =>
        (Reader, Book) =
        (DependenciesContainer.Resolve<IReaderModel>()!, DependenciesContainer.Resolve<IBookModel>()!);

    public IssuingModel(IReaderModel reader, IBookModel book, DateTime takeDate,
        bool returned, DateTime? returnDate, int? returnState) =>
        (Reader, Book, TakeDate, Returned, ReturnDate, ReturnState) =
        (reader, book, takeDate, returned, returnDate, returnState);
}