using LibrariansAssistant.DependenciesLayer;
using LibrariansAssistant.DomainModelLayer.Models.Author;
using LibrariansAssistant.DomainModelLayer.Models.Book;
using LibrariansAssistant.DomainModelLayer.Models.Issuing;
using LibrariansAssistant.DomainModelLayer.Models.Reader;
using LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;
using LibrariansAssistant.ServicesLayer.ModelServices.Issuing;
using LibrariansAssistant.ServicesLayer.Tests.Mocks;

namespace LibrariansAssistant.ServicesLayer.Tests.ModelServices;

[TestClass]
public sealed class IssuingServiceTests
{
    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        DependenciesContainer.Register<IAuthorModel, AuthorModel>();
        DependenciesContainer.Register<IBookModel, BookModel>();
        DependenciesContainer.Register<IIssuingModel, IssuingModel>();
        DependenciesContainer.Register<IReaderModel, ReaderModel>();
        DependenciesContainer.Register<IDataAnnotationModelValidationService, DataAnnotationModelValidationService>();
        DependenciesContainer.Register<IIssuingService, IssuingService>();
    }

    [TestMethod]
    public void ReaderMostActiveGet_ValidData_ReturnsCorrectResult()
    {
        IIssuingService issuingService = CreateIssuingService();

        int? actual = issuingService.ReaderMostActiveGet()?.Id;

        Assert.AreEqual(1, actual);
    }

    [TestMethod]
    public void AuthorMostPopularGet_ValidData_ReturnsCorrectResult()
    {
        IIssuingService issuingService = CreateIssuingService();

        int? actual = issuingService.AuthorMostPopularGet()?.Id;

        Assert.AreEqual(1, actual);
    }

    [TestMethod]
    public void BookMostPopularGenreGet_ValidData_ReturnsCorrectResult()
    {
        IIssuingService issuingService = CreateIssuingService();

        string? actual = issuingService.BookMostPopularGenreGet();

        Assert.AreEqual("Vestibulum", actual);
    }

    private static IIssuingService CreateIssuingService()
    {
        RepositoryMock repository = new();
        repository.Initialize(string.Empty);

        IDataAnnotationModelValidationService dataAnnotationModelValidationService =
            DependenciesContainer.Resolve<IDataAnnotationModelValidationService>()!;

        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(repository, dataAnnotationModelValidationService)!;

        return issuingService;
    }
}