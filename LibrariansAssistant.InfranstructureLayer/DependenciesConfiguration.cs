using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.InfranstructureLayer.ObjectRelationalMappers;

namespace LibrariansAssistant.InfranstructureLayer;

internal static class DependenciesConfiguration
{
    internal static void ConfigureOrmDependencies(IObjectRelationalMapper objectRelationalMapper)
    {
        objectRelationalMapper.RegisterDependency(typeof(IAuthorModel), typeof(AuthorModel));
        objectRelationalMapper.RegisterDependency(typeof(IBookModel), typeof(BookModel));
        objectRelationalMapper.RegisterDependency(typeof(IIssuingModel), typeof(IssuingModel));
        objectRelationalMapper.RegisterDependency(typeof(IReaderModel), typeof(ReaderModel));
    }
}