namespace LibrariansAssistant.ServicesLayer.CommonServices.DataAnnotationModelValidationService;

public interface IDataAnnotationModelValidationService
{
    void Validate<TModel>(TModel model);
}