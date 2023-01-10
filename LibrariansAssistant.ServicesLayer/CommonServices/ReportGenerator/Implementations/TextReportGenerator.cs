using LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Entities;
using System.Text;

namespace LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Implementations;

/// <summary>
/// Represents the text report generator.
/// </summary>
public sealed class TextReportGenerator : IReportGenerator
{
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

        StringBuilder report = new();

        string[] columnHeaders = reportDocument.ColumnHeaders.ToArray();
        int[] columnMaxLength = new int[reportDocument.ColumnCount];
        int documentWidth;

        _ = report.AppendLine(reportDocument.Title);
        _ = report.AppendLine();

        for (var j = 0; j < reportDocument.ColumnCount; j++)
            columnMaxLength[j] = columnHeaders[j].Length;

        for (var j = 0; j < reportDocument.ColumnCount; j++)
            for (var i = 0; i < reportDocument.RowCount; i++)
                if (reportDocument[i, j].Length > columnMaxLength[j])
                    columnMaxLength[j] = reportDocument[i, j].Length;

        documentWidth = columnMaxLength.Sum() + (columnMaxLength.Length * 3) + 1;

        _ = report.AppendLine(new string('\u2015', documentWidth));

        _ = report.Append('|');

        for (var j = 0; j < reportDocument.ColumnCount; j++)
            _ = report.Append(" " + columnHeaders[j].PadRight(columnMaxLength[j]) + " |");

        _ = report.AppendLine();

        _ = report.AppendLine(new string('\u2015', documentWidth));

        for (var i = 0; i < reportDocument.RowCount; i++)
        {
            _ = report.Append('|');

            for (var j = 0; j < reportDocument.ColumnCount; j++)
                _ = report.Append(" " + reportDocument[i, j].PadRight(columnMaxLength[j]) + " |");

            _ = report.AppendLine();
        }

        _ = report.Append(new string('\u2015', documentWidth));

        try
        {
            File.WriteAllText(filePath, report.ToString());
        }
        catch
        {
            throw;
        }
    }
}