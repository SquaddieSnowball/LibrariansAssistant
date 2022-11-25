using LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Entities;

namespace LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator;

public interface IReportGenerator
{
    void GenerateReport(string filePath, ReportDocument reportDocument);
}