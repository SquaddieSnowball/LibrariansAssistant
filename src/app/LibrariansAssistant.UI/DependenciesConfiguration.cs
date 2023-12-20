using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Domain.Models.Implementations;
using LibrariansAssistant.Services.Common.Abstractions;
using LibrariansAssistant.Services.Common.Implementations;
using LibrariansAssistant.Services.Model.Abstractions;
using LibrariansAssistant.Services.Model.Implementations;
using LibrariansAssistant.UI.Services.Abstractions;
using LibrariansAssistant.UI.Services.Implementations;

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