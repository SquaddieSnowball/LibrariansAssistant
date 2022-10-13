namespace LibrariansAssistant.ServicesLayer.CommonServices.Interfaces;

public interface IDataAnnotationModelValidationService
{
    void Validate<TModel>(TModel model);
}