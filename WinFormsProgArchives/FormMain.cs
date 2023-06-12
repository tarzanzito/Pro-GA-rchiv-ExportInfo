using ProgArchivesCore.Config;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.ProgArchivesSite;
using ProgArchivesCore.SiteManagers;
using ProgArchivesCore.Statics;
using System.Diagnostics;

namespace WinFormsProgArchives
{
    internal partial class FormMain : Form
    {
        private ConfigurationFields? _configurationFields = null;
        private IDataBaseManager? _dataBaseManager = null;
        private SiteManager? _siteManager = null;
        private int _lastArtistId;
        private int _lastAlbumId;
        private int _lastCountryId;

        public FormMain(ConfigurationFields configurationFields)
        {
            _configurationFields = configurationFields;

            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dataBaseManager != null)
                _dataBaseManager.Close();

            _dataBaseManager = null;
            _siteManager = null;
        }

        private void buttonProcessArtists_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUntilArtist.Text.Trim() == "")
                    throw new Exception("Invalid input value. (must be greater than zero.");

                int toArtistPage = System.Convert.ToInt32(textBoxUntilArtist.Text);

                bool processOnlyOne = checkBoxOnlyOne.Checked;

                ProcessProgArchives(ProcessAction.Artists, toArtistPage, processOnlyOne);

            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                RefreshData();
            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
        }

        private void buttonProcessAlbums_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUntilAlbum.Text.Trim() == "")
                    throw new Exception("Invalid input value. (must be greater than zero.");

                int toAlbumPage = System.Convert.ToInt32(textBoxUntilAlbum.Text);

                bool processOnlyOne = checkBoxOnlyOne.Checked;

                ProcessProgArchives(ProcessAction.Albums, toAlbumPage, processOnlyOne);

            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
        }

        private void buttonProcessCountries_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessProgArchives(ProcessAction.Countries, 0, false);

            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
        }

        private void buttonAnalyseArtists_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://www.progarchives.com/artist.asp?id=" + _lastArtistId.ToString();
                BrowserOpen(url);
            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
        }

        private void buttonAnalyseAlbums_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://www.progarchives.com/album.asp?id=" + _lastAlbumId.ToString();
                BrowserOpen(url);
            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
        }

        private void buttonAnalyseCountries_Click(object sender, EventArgs e)
        {
            try
            {
                string url = "http://www.progarchives.com/Bands-country.asp?country=" + _lastCountryId.ToString();
                BrowserOpen(url);
            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
        }

        #region Private Methods

        private void BrowserOpen(string url)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = url;
            processStartInfo.UseShellExecute = true;

            Process.Start(processStartInfo);
        }

        private void ShowError(object sender, EventArgs e, Exception ex)
        {
            MessageBox.Show(ex.Message, "App Error,", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ProcessProgArchives(ProcessAction processAction, int untilPageId, bool processOnlyOne)
        {
            RefreshData();

            if (processAction == ProcessAction.Artists)
            {
                if (untilPageId <= _lastArtistId)
                    throw new Exception("Nothing todo. ('Last Artist' is greather than 'Proc Until'");
            }

            if (processAction == ProcessAction.Albums)
            {
                if (untilPageId <= _lastAlbumId)
                    throw new Exception("Nothing todo. ('Last Album' is greather than 'Proc Until'");
            }

            //Open access database
            _dataBaseManager = CommonUtils.DatabaseLink(_configurationFields);

            //open site manager
            _siteManager = CommonUtils.SiteManagerLink(_configurationFields);


            ////ProgGnosisSiteManager progGnosisSiteManager = new ProgGnosisSiteManager(siteManager, dataBaseManager);
            ////progGnosisSiteManager.ProcessArtists(13, false);
            ////progGnosisSiteManager.ProcessAlbums(19, false);


            //Process ProgAchivesSite
            ProgAchivesSiteManager progAchivesSite = new ProgAchivesSiteManager(_siteManager, _dataBaseManager);

            //Process Artists
            if (processAction == ProcessAction.Artists)
            {
                if (untilPageId <= _lastArtistId)
                    throw new Exception("Nothing todo. (last Processed Artist is greather than Until Artist");

                progAchivesSite.ProcessArtists(untilPageId, processOnlyOne);
            }

            ////Process Albuns
            if (processAction == ProcessAction.Albums)
            {
                if (untilPageId <= _lastAlbumId)
                    throw new Exception("Nothing todo. (last Processed Album is greather than Until Album");

                progAchivesSite.ProcessAlbums(untilPageId, processOnlyOne);
            }

            //Process Countries
            if (processAction == ProcessAction.Countries)
            {
                progAchivesSite.ProcessCountries(firstDeleteAll: true);
            }
        }

        private void RefreshData()
        {
            //Open access database
            _dataBaseManager = CommonUtils.DatabaseLink(_configurationFields);

            _lastArtistId = _dataBaseManager.GetMaxArtist();
            _lastAlbumId = _dataBaseManager.GetMaxAlbum();
            _lastCountryId = _dataBaseManager.GetMaxCountry();

            textBoxLastArtist.Text = _lastArtistId.ToString();
            textBoxLastAlbum.Text = _lastAlbumId.ToString();
            textBoxLastCountry.Text = _lastCountryId.ToString();
        }

        #endregion
    }
}
