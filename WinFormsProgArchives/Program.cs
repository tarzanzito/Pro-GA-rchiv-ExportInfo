using ProgArchivesCore.Config;
using ProgArchivesCore.Statics;
using System;
using System.Windows.Forms;


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
            LoggerUtils.Start();

            ConfigurationFields configurationFields = CommonUtils.LoadConfiguration();

            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain(configurationFields));
        }
    }
}