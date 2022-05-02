namespace CRUD_Refactored;

using Microsoft.Extensions.Configuration;
using SimpleCRUD_WinForms;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        var cfg = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.develop.json", optional: true, reloadOnChange: true)
            .Build();

        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm(cfg));
    }
}
