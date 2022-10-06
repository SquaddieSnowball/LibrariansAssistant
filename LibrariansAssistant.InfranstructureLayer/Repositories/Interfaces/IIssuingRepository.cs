using LibrariansAssistant.DomainModelLayer.Models.Issuing;

namespace LibrariansAssistant.InfranstructureLayer.Repositories.Interfaces;

public interface IIssuingRepository
{
    void IssuingAdd(IIssuingModel issuing);

    IEnumerable<IIssuingModel> IssuingGetAll();

    IIssuingModel? IssuingGetById(int issuingId);

    void IssuingUpdate(IIssuingModel issuing);

    void IssuingDelete(int issuingId);
}