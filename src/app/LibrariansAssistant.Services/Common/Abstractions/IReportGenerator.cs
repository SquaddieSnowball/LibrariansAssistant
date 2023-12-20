using LibrariansAssistant.Services.Entities.ReportGenerator;

namespace LibrariansAssistant.Services.Common.Abstractions;

/// <summary>
/// Provides methods for generating reports.
/// </summary>
public interface IReportGenerator
{
    /// <summary>
    /// Generates a report and saves it to the specified file path.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="reportDocument">Report document to save.</param>
    void GenerateReport(string filePath, ReportDocument reportDocument);
}