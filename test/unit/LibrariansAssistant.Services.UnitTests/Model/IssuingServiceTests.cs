﻿using LibrariansAssistant.Dependencies;
using LibrariansAssistant.Domain.Models.Abstractions;
using LibrariansAssistant.Domain.Models.Implementations;
using LibrariansAssistant.Infrastructure.Repositories.Abstractions;
using LibrariansAssistant.Services.Common.Abstractions;
using LibrariansAssistant.Services.Common.Implementations;
using LibrariansAssistant.Services.Model.Abstractions;
using LibrariansAssistant.Services.Model.Implementations;
using LibrariansAssistant.Services.UnitTests.Mocks;

namespace LibrariansAssistant.Services.UnitTests.Model;

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
        DependenciesContainer.Register<IDataAnnotationsModelValidationService, DataAnnotationsModelValidationService>();
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
        IRepository repository = new MockRepository();
        repository.Initialize(string.Empty);

        IDataAnnotationsModelValidationService dataAnnotationModelValidationService =
            DependenciesContainer.Resolve<IDataAnnotationsModelValidationService>()!;
        IIssuingService issuingService =
            DependenciesContainer.Resolve<IIssuingService>(repository, dataAnnotationModelValidationService)!;

        return issuingService;
    }
}