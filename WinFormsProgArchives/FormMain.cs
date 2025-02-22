
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Serilog;
using ProgArchivesCore.Config;
using ProgArchivesCore.DataBaseManagers;
using ProgArchivesCore.Models;
using ProgArchivesCore.ProgArchivesSite;
using ProgArchivesCore.SiteManagers;
using ProgArchivesCore.Statics;


namespace WinFormsProgArchives
{
    internal partial class FormMain : Form
    {
        private ConfigurationFields _configurationFields = null;
        private IDataBaseManager _dataBaseManager = null;
        private SiteManager _siteManager = null;
        private int _lastArtistId;
        private int _lastAlbumId;
        private int _lastCountryId;

        public FormMain(ConfigurationFields configurationFields)
        {
            _configurationFields = configurationFields;

            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ChangeVisualComponentsState(false);

            try
            {
               
                RefreshData(); //executa sql
            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }
 
            ChangeVisualComponentsState(true);
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

                //RefreshData(); //executa sql

                int toArtistPage = System.Convert.ToInt32(textBoxUntilArtist.Text);

                if (toArtistPage <= _lastArtistId)
                    throw new Exception("Nothing todo. ('Last Artist' is greather than 'Proc Until'");

                bool processOnlyOne = checkBoxOnlyOne.Checked;

                ChangeVisualComponentsState(false);

                object[] objects = new object[3];
                objects[0] = ProcessAction.Artists;
                objects[1] = toArtistPage;
                objects[2] = processOnlyOne;

                backgroundWorker1.RunWorkerAsync(objects);
                //ProcessProgArchives(ProcessAction.Artists, toArtistPage, processOnlyOne); //bloqueia a main thread
                //ChangeInputState(true);

            }
            catch (Exception ex)
            {
                ShowError(sender, e, ex);
            }

        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            buttonRefresh_Click(sender, e);
        }

        private void buttonProcessAlbums_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxUntilAlbum.Text.Trim() == "")
                    throw new Exception("Invalid input value. (must be greater than zero.");

                //RefreshData();//executa sql

                int toAlbumPage = System.Convert.ToInt32(textBoxUntilAlbum.Text);

                if (toAlbumPage <= _lastAlbumId)
                    throw new Exception("Nothing todo. ('Last Album' is greather than 'Proc Until'");

                bool processOnlyOne = checkBoxOnlyOne.Checked;

                ChangeVisualComponentsState(false);

                object[] objects = new object[3];
                objects[0] = ProcessAction.Albums;
                objects[1] = toAlbumPage;
                objects[2] = processOnlyOne;

                backgroundWorker1.RunWorkerAsync(objects);
                //ProcessProgArchives(ProcessAction.Albums, toAlbumPage, processOnlyOne); //bloqueia thread
                //ChangeInputState(true);
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
                //RefreshData();//executa sql

                ChangeVisualComponentsState(false);

                object[] objects = new object[3];
                objects[0] = ProcessAction.Countries;
                objects[1] = 0;
                objects[2] = false;

                backgroundWorker1.RunWorkerAsync(objects);
                //ProcessProgArchives(ProcessAction.Countries, 0, false); //bloqqueia a thread
                //ChangeInputState(true);
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

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (this.backgroundWorker1.IsBusy)
                this.backgroundWorker1.CancelAsync();

            Close();
        }

        #region Private Methods

        private void ChangeVisualComponentsState(bool enable)
        {
            buttonRefresh.Enabled = enable;
            buttonAnalyseArtists.Enabled = enable;
            buttonAnalyseAlbums.Enabled = enable;
            buttonAnalyseCountries.Enabled = enable;
            checkBoxOnlyOne.Enabled = enable;
            textBoxUntilArtist.Enabled = enable;
            textBoxUntilAlbum.Enabled = enable;
            buttonProcessArtists.Enabled = enable;
            buttonProcessAlbums.Enabled = enable;
            buttonProcessCountries.Enabled = enable;

            if (enable)
                this.Cursor = Cursors.Default;
            else
                this.Cursor = Cursors.WaitCursor;
        }

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
                //if (untilPageId <= _lastArtistId)
                //    throw new Exception("Nothing todo. (last Processed Artist is greather than Until Artist");

                progAchivesSite.EventArtistInfo += FireEventArtistInfo;

                progAchivesSite.ProcessArtists(untilPageId, processOnlyOne); //bloqueia a thread
            }

            //aqui de passar ao background worker
            ////Process Albuns
            if (processAction == ProcessAction.Albums)
            {
                //if (untilPageId <= _lastAlbumId)
                //    throw new Exception("Nothing todo. (last Processed Album is greather than Until Album");

                progAchivesSite.EventAlbumInfo += FireEventAlbumInfo;
                progAchivesSite.ProcessAlbums(untilPageId, processOnlyOne); //bloqueia a thread
            }

            //Process Countries
            if (processAction == ProcessAction.Countries)
            {
                progAchivesSite.EventCountryInfo += FireEventCountryInfo;
                progAchivesSite.ProcessCountries(firstDeleteAll: true); //bloqueia a thread
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

            textBoxConnectionString.Text = $"{_configurationFields?.DataBaseEngine} = {_configurationFields?.CurrentDirectory} = {_configurationFields?.DataBaseLocation}";
        }

        #endregion

        #region events

        public void FireEventCountryInfo(CountryInfo countryInfo, string uri)
        {
            this.backgroundWorker1.ReportProgress(0, uri);
            //listBox1.Items.Add(uri);

            Log.Information($"'MusicCollectionMsDos.FireEventCountryInfo' | URI={uri} | CountryInfo={countryInfo}");
        }

        public void FireEventArtistInfo(ArtistInfo artistInfo, string uri)
        {
            this.backgroundWorker1.ReportProgress(0, uri);
            //listBox1.Items.Add(uri);

            Log.Information($"'MusicCollectionMsDos.FireEventArtistInfo' | URI={uri} | ArtistInfo={artistInfo}");
        }

        public void FireEventAlbumInfo(AlbumInfo albumInfo, string uri)
        {
            this.backgroundWorker1.ReportProgress(0, uri);
            //listBox1.Items.Add(uri);

            Log.Information($"'MusicCollectionMsDos.FireEventAlbumInfo' | URI={uri} | AlbumInfo={albumInfo}");
        }

        #endregion

        #region "Background Worker"

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            object[] objects = e.Argument as object[];
            ProcessAction processAction = (ProcessAction)objects[0];
            int untilPageId = (int)objects[1];
            bool processOnlyOne = (bool)objects[2];
            ProcessProgArchives(processAction, untilPageId, processOnlyOne);

        }

        private void backgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            listBox1.Items.Add(e.UserState.ToString());
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            this.ChangeVisualComponentsState(true);
        }

        #endregion
    }
}
