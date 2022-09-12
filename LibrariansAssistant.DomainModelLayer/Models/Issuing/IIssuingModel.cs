using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Reader;

namespace LibrariansAssistant.DomainModelLayer.Models.Issuing;

public interface IIssuingModel
{
    int Id { get; set; }

    IReaderModel Reader { get; set; }

    IBookModel Book { get; set; }

    DateTime TakeDate { get; set; }

    bool Returned { get; set; }

    DateTime? ReturnDate { get; set; }

    int? ReturnState { get; set; }
}