using LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Entities;
using OfficeOpenXml;

namespace LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Implementations;

/// <summary>
/// Represents the Excel report generator.
/// </summary>
public sealed class ExcelReportGenerator : IReportGenerator
{
    static ExcelReportGenerator() =>
       ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

    /// <summary>
    /// Generates a report and saves it to the specified file path.
    /// </summary>
    /// <param name="filePath">Path to the file.</param>
    /// <param name="reportDocument">Report document to save.</param>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public void GenerateReport(string filePath, ReportDocument reportDocument)
    {
        if (string.IsNullOrEmpty(filePath) is true)
            throw new ArgumentException("File path must not be null or empty.", nameof(filePath));

        if (reportDocument is null)
            throw new ArgumentNullException(nameof(reportDocument), "Report document must not be null.");

        try
        {
            if (File.Exists(filePath) is true)
                File.Delete(filePath);
        }
        catch
        {
            throw;
        }

        try
        {
            using ExcelPackage excelPackage = new(filePath);

            ExcelWorksheet excelWorksheet = excelPackage.Workbook.Worksheets.Add(reportDocument.Title);

            string[] columnHeaders = reportDocument.ColumnHeaders.ToArray();

            for (var j = 0; j < columnHeaders.Length; j++)
                excelWorksheet.Cells[1, j + 1].Value = columnHeaders[j];

            for (var i = 0; i < reportDocument.RowCount; i++)
                for (var j = 0; j < reportDocument.ColumnCount; j++)
                    excelWorksheet.Cells[i + 2, j + 1].Value = reportDocument[i, j];

            excelWorksheet.Cells.AutoFitColumns();
            excelWorksheet.Rows[1].Style.Font.Bold = true;

            excelPackage.Save();
        }
        catch
        {
            throw;
        }
    }
}