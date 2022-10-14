using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.DomainModelLayer.Models.Reader;

namespace LibrariansAssistant.UserInterfaceLayer;

internal static class DependenciesConfigurator
{
    internal static void Configure()
    {
        DependenciesContainer.Register<IAuthorModel, AuthorModel>();
        DependenciesContainer.Register<IBookModel, BookModel>();
        DependenciesContainer.Register<IIssuingModel, IssuingModel>();
        DependenciesContainer.Register<IReaderModel, ReaderModel>();
    }
}