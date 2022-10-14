namespace LibrariansAssistant.UserInterfaceLayer;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        DependenciesConfigurator.Configure();

        ApplicationConfiguration.Initialize();
        //Application.Run(new MainView());
    }
}