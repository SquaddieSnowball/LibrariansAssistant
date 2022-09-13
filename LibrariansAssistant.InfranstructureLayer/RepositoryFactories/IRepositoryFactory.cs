using LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

namespace LibrariansAssistant.InfranstructureLayer.RepositoryFactories;

public interface IRepositoryFactory
{
    IRepository GetRepository();
}