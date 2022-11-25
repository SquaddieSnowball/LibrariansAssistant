using LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Entities;
using System.Text;

namespace LibrariansAssistant.ServicesLayer.CommonServices.ReportGenerator.Implementations;

public sealed class TextReportGenerator : IReportGenerator
{
    public void GenerateReport(string filePath, ReportDocument reportDocument)
    {
        if (string.IsNullOrEmpty(filePath) is true)
            throw new ArgumentException("File path must not be null or empty.", nameof(filePath));

        if (reportDocument is null)
            throw new ArgumentNullException(nameof(reportDocument), "Report document must not be null.");

        var report = new StringBuilder();

        string[] columnHeaders = reportDocument.ColumnHeaders.ToArray();
        var columnMaxLength = new int[reportDocument.ColumnCount];
        int documentWidth;

        report.AppendLine(reportDocument.Title);
        report.AppendLine();

        for (var j = 0; j < reportDocument.ColumnCount; j++)
            columnMaxLength[j] = columnHeaders[j].Length;

        for (var j = 0; j < reportDocument.ColumnCount; j++)
            for (var i = 0; i < reportDocument.RowCount; i++)
                if (reportDocument[i, j].Length > columnMaxLength[j])
                    columnMaxLength[j] = reportDocument[i, j].Length;

        documentWidth = columnMaxLength.Sum() + columnMaxLength.Length * 3 + 1;

        report.AppendLine(new string('\u2015', documentWidth));

        report.Append('|');

        for (var j = 0; j < reportDocument.ColumnCount; j++)
            report.Append(" " + columnHeaders[j].PadRight(columnMaxLength[j]) + " |");

        report.AppendLine();

        report.AppendLine(new string('\u2015', documentWidth));

        for (var i = 0; i < reportDocument.RowCount; i++)
        {
            report.Append('|');

            for (var j = 0; j < reportDocument.ColumnCount; j++)
                report.Append(" " + reportDocument[i, j].PadRight(columnMaxLength[j]) + " |");

            report.AppendLine();
        }

        report.Append(new string('\u2015', documentWidth));

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