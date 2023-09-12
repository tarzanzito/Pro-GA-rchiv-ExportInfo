
using ProgArchivesCore.Config;
using System;
using System.Net;
using System.Net.Http;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace ProgArchivesCore.SiteManagers
{
    /// <summary>
    /// Generic to get all html from site
    /// </summary>
    public class SiteManager
    {
        private readonly string _proxyUserId;
        private readonly string _proxyUserPassword;
        private readonly string _proxyUserDomain;
        private readonly string _proxyAddress;
        private readonly int _proxyPort;
        private readonly bool _useProxy;
        private HttpClient _httpClient = null;

        public SiteManager(ConfigurationFields configurationFields)
        {
            _useProxy = configurationFields.HasProxy;

            if (_useProxy)
            {
                _proxyUserId = configurationFields.ProxyUserId;
                _proxyUserPassword = configurationFields.ProxyUserPassword;
                _proxyUserDomain = configurationFields.ProxyUserDomain;
                _proxyAddress = configurationFields.ProxyAddress;
                _proxyPort = configurationFields.ProxyPort;
            }
            else
            {
                _proxyUserId = "";
                _proxyUserPassword = "";
                _proxyUserDomain = "";
                _proxyAddress = "";
                _proxyPort = 0;
            }

            CreateClient();
        }

        ~SiteManager()
        {
            _httpClient.Dispose();
            _httpClient = null;
        }

        private void CreateClient()
        {
            if (_useProxy) //TODO: Confirm //not tested yet
            {
                ICredentials networkCredential = new NetworkCredential(_proxyUserId, _proxyUserPassword, _proxyUserDomain);

                string address = $"{_proxyAddress}:{_proxyPort}";

                WebProxy proxy = new WebProxy()
                {
                    Address = new Uri(address),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                    Credentials = networkCredential
                };

                // Create a client handler that uses the proxy
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy
                };

                // Disable SSL verification TODO : is for ?!?!?
                httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                //_httpClient = new HttpClient(httpClientHandler, true);  //not tested yet
                _httpClient = new HttpClient(httpClientHandler); //not tested yet
            }
            else
                _httpClient = new HttpClient();
        }

        public string GetAllHtmlData(string uri)
        {
            string allPageData = "";

            try
            {
                // string uri2 = "http://www.progarchives.com/artist.asp?id=12435";
                // string uri2 = "https://www.proggnosis.com/Artist/10";

                Task<string> task = _httpClient.GetStringAsync(uri);
                task.Wait();
                Task.WaitAll(task); //CA1843  Do not use 'WaitAll' with a single task

                allPageData = task.Result;

                //_webClient.Encoding = System.Text.Encoding.UTF8;
                //byte[] byteArray = _webClient.DownloadData(Site);
                //allPageData = System.Text.Encoding.Default.GetString(byteArray);

            }
            catch (Exception e)
            {
                //TODO send any information
            }

            return allPageData;
        }
    }
}
