using LibrariansAssistant.UserInterfaceLayer.Views.WinFormsViews;

namespace LibrariansAssistant.UserInterfaceLayer;

/// <summary>
/// The main class of the application.
/// </summary>
internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        DependenciesConfiguration.Configure();
        ApplicationConfiguration.Initialize();
        Application.Run(new MainView());
    }
}