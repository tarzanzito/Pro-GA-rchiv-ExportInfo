using System;
using System.IO;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.ProgArchivesSite;
using ProgArchivesCore.SiteManagers;
using ProgArchivesCore.Config;
using ProgArchivesCore.Statics;
using WinFormsProgArchives;

namespace Candal
{
    internal class InactiveAlbuns
    {
        public static int Main1(string[] args)
        {
            LoggerUtils.Start();

            //before run this : ACCESS DB i run the function  query "SELECT ID FROM ALBUMS WHERE ISACTIVE = FALSE"
            //and save de result in file ebug in file at "\Resources\inactives_albums.txt.txt"

            //This function confirm on site if album is invalid or not. if not make an update on table "Albums"

            try
            {
                ConfigurationFields configurationFields = CommonUtils.LoadConfiguration();

                string fullNameFile = System.IO.Path.Join(configurationFields.CurrentDirectory, @"\Resources\inactives_albums.txt");
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

                    int page = GetPageFromLine(line);

                    Console.WriteLine(line);
                    counterPages++;
                    try
                    {

                        //progAchivesSite.ProcessArtists(page, true);
                        progAchivesSite.ProcessAlbums(page, true);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine("ERROR:");
                        System.Console.WriteLine(ex.Message);
                        System.Console.WriteLine(ex.Source);
                        System.Console.WriteLine(ex.StackTrace);
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
            return System.Convert.ToInt32(line.Trim());
        }
    }
}
