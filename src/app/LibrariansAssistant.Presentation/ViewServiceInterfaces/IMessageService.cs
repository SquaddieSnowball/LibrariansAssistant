namespace LibrariansAssistant.Presentation.ViewServiceInterfaces;

/// <summary>
/// Provides methods for displaying messages.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Displays a normal message.
    /// </summary>
    /// <param name="message">Message to display.</param>
    void ShowMessage(string message);

    /// <summary>
    /// Displays a warning message.
    /// </summary>
    /// <param name="warning">Message to display.</param>
    void ShowWarning(string warning);

    /// <summary>
    /// Displays an error message.
    /// </summary>
    /// <param name="error">Message to display.</param>
    void ShowError(string error);
}