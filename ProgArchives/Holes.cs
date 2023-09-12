
using System.IO;
using System;
using ProgArchivesCore.SiteManagers;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.ProgArchivesSite;
using ProgArchivesCore.Config;
using ProgArchivesCore.Statics;
using WinFormsProgArchives;

namespace Candal
{
    internal static class Holes
    {
        public static int Main1(string[] args)
        {
            LoggerUtils.Start(); 

            ConfigurationFields configurationFields = null;
            //FillDetectedHoles()

            //before in  ACCESS DB i run the function  "VerificaBuracos" and save de debug in file at "\Resources\Holes.txt"

            try
            {
                string currentDirectory = Environment.CurrentDirectory;
#if DEBUG
                currentDirectory = Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
#endif
                string fullNameFile = System.IO.Path.Join(currentDirectory, @"\Resources\Holes.txt");

                StreamReader file = new StreamReader(fullNameFile);

                IDataBaseManager dataBaseManager = CommonUtils.DatabaseLink(configurationFields);
                SiteManager siteManager = CommonUtils.SiteManagerLink(configurationFields);
                ProgAchivesSiteManager progAchivesSite = new ProgAchivesSiteManager(siteManager, dataBaseManager);

                int counterLines = 0;
                int counterPages = 0;
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    counterLines++;

                    if (line.Trim() == "")
                        continue;

                    int pageIni = GetPageFromLine(line);
                    int quantity = GetQuantityFromLine(line);

                    Console.WriteLine(line);
                    int page;
                    for (int i = 0; i < quantity; i++)
                    {
                        page = pageIni + i;
                        counterPages++;
                        try
                        {
                            //progAchivesSite.ProcessArtists(page, true);
                            progAchivesSite.ProcessAlbums(page, true);
                        }
                        catch ( Exception ex)
                        {
                            System.Console.WriteLine("ERROR:");
                            System.Console.WriteLine(ex.Message);
                            System.Console.WriteLine(ex.Source);
                            System.Console.WriteLine(ex.StackTrace);
                        }
                    }
                }

                file.Close();
                dataBaseManager.Close();
                dataBaseManager = null;
                siteManager = null;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("");
                System.Console.WriteLine("ERROR:");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine(ex.Source);
                System.Console.WriteLine(ex.StackTrace);
            }

            return 0;
        }

        private static int GetPageFromLine(string line)
        {
            int pos1 = line.IndexOf(":");
            int pos2 = line.IndexOf("-");
            string pageS = line.Substring(pos1 + 1, pos2 - pos1 - 1).Trim();

            return System.Convert.ToInt32(pageS);
        }

        private static int GetQuantityFromLine(string line)
        {
            int pos = line.LastIndexOf(":");
            string qtS = line.Substring(pos + 1, line.Length - pos - 1).Trim();

            return System.Convert.ToInt32(qtS);
        }
    }
}
