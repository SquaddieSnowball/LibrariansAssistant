namespace LibrariansAssistant.InfranstructureLayer.InfranstructureCreators;

public interface IInfranstructureCreator
{
    public bool? IsInfrastructureCreated { get; }

    void Initialize(string initializationString);

    void Create();
}