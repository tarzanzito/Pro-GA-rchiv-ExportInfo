using ProgArchivesCore.Config;
using ProgArchivesCore.Statics;

namespace WinFormsProgArchives
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ConfigurationFields configurationFields = CommonUtils.LoadConfiguration();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain(configurationFields));
        }
    }
}