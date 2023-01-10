namespace LibrariansAssistant.PresentationLayer.ViewInterfaces;

/// <summary>
/// Provides methods used by views.
/// </summary>
public interface IView
{
    /// <summary>
    /// Shows the current view.
    /// </summary>
    void Show();

    /// <summary>
    /// Closes the current view.
    /// </summary>
    void Close();
}