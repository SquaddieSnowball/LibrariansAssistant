namespace LibrariansAssistant.InfranstructureLayer.InfranstructureCreators;

public interface IInfranstructureCreator
{
    void Initialize(string initializationString);

    bool Create();
}