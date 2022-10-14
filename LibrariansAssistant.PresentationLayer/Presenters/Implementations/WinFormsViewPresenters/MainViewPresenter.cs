using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;
using LibrariansAssistant.PresentationLayer.ViewInterfaces.WinFormsViewInterfaces;
using LibrariansAssistant.PresentationLayer.ViewServiceInterfaces;
using LibrariansAssistant.ServicesLayer.CommonServices.Interfaces;

namespace LibrariansAssistant.PresentationLayer.Presenters.Implementations.WinFormsViewPresenters;

public sealed class MainViewPresenter : IPresenter
{
    private readonly IMainView _mainView;
    private readonly IRepository _repository;
    private readonly IDataAnnotationModelValidationService _dataAnnotationModelValidationService;
    private readonly IMessageService _messageService;

    public MainViewPresenter(IMainView mainView, IRepository repository,
        IDataAnnotationModelValidationService dataAnnotationModelValidationService, IMessageService messageService) =>
        (_mainView, _repository, _dataAnnotationModelValidationService, _messageService) =
        (mainView, repository, dataAnnotationModelValidationService, messageService);

    public void RunView() =>
        _mainView.Show();
}