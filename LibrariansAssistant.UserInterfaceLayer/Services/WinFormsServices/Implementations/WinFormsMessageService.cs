using LibrariansAssistant.PresentationLayer.ViewServiceInterfaces;

namespace LibrariansAssistant.UserInterfaceLayer.Services.WinFormsServices.Implementations;

/// <summary>
/// Represents the WinForms message service.
/// </summary>
internal class WinFormsMessageService : IMessageService
{
    /// <summary>
    /// Displays a normal message.
    /// </summary>
    /// <param name="message">Message to display.</param>
    public void ShowMessage(string message) =>
        _ = MessageBox.Show(message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

    /// <summary>
    /// Displays a warning message.
    /// </summary>
    /// <param name="warning">Message to display.</param>
    public void ShowWarning(string warning) =>
        _ = MessageBox.Show(warning, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    /// <summary>
    /// Displays an error message.
    /// </summary>
    /// <param name="error">Message to display.</param>
    public void ShowError(string error) =>
        _ = MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
}