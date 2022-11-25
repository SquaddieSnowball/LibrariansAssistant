using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;
using LibrariansAssistant.ServicesLayer.ModelServices.Author;
using LibrariansAssistant.ServicesLayer.ModelServices.Book;
using LibrariansAssistant.ServicesLayer.ModelServices.Issuing;
using LibrariansAssistant.ServicesLayer.ModelServices.Reader;
using LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Implementations;
using LibrariansAssistant.UserInterfaceLayer.Services.CommonServices.Interfaces;

namespace LibrariansAssistant.UserInterfaceLayer;

internal static class DependenciesConfiguration
{
    internal static void Configure()
    {
        DependenciesContainer.Register<IAuthorModel, AuthorModel>();
        DependenciesContainer.Register<IBookModel, BookModel>();
        DependenciesContainer.Register<IIssuingModel, IssuingModel>();
        DependenciesContainer.Register<IReaderModel, ReaderModel>();
        DependenciesContainer.Register<IAuthorService, AuthorService>();
        DependenciesContainer.Register<IBookService, BookService>();
        DependenciesContainer.Register<IIssuingService, IssuingService>();
        DependenciesContainer.Register<IReaderService, ReaderService>();
        DependenciesContainer.Register<IDataAnnotationModelValidationService, DataAnnotationModelValidationService>();
        DependenciesContainer.Register<IApplicationConfigurationService, ApplicationConfigurationService>();
    }
}