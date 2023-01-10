namespace LibrariansAssistant.PresentationLayer.Presenters;

/// <summary>
/// Provides methods that are used by presenters.
/// </summary>
public interface IPresenter
{
    /// <summary>
    /// Runs the view controlled by the current presenter.
    /// </summary>
    void RunView();
}