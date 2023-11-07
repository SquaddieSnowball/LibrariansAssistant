using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Domain.Models.Author;
using LibrariansAssistant.Domain.Models.Book;
using LibrariansAssistant.Domain.Models.Issuing;
using LibrariansAssistant.Domain.Models.Reader;
using LibrariansAssistant.Services.CommonServices.DataAnnotationModelValidationService;
using LibrariansAssistant.Services.ModelServices.Author;
using LibrariansAssistant.Services.ModelServices.Book;
using LibrariansAssistant.Services.ModelServices.Issuing;
using LibrariansAssistant.Services.ModelServices.Reader;
using LibrariansAssistant.UI.Services.CommonServices.Implementations;
using LibrariansAssistant.UI.Services.CommonServices.Interfaces;

namespace LibrariansAssistant.UI;

/// <summary>
/// Represents the dependencies configurator.
/// </summary>
internal static class DependenciesConfiguration
{
    /// <summary>
    /// Configures project dependencies.
    /// </summary>
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