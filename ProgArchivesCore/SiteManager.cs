
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Candal.Core
{
    /// <summary>
    /// Generic to get all html from site
    /// </summary>
    public class SiteManager
    {
        private string _userName;
        private string _userPassword;
        private string _userDomain;
        private string _proxyAddress;
        private int _proxyPort;
        private bool _useProxy;
        private System.Net.Http.HttpClient _httpClient = null;

        public SiteManager()
        {
            _userName = "";
            _userPassword = "";
            _userDomain = "";
            _proxyAddress = "";
            _proxyPort = 0;
            _useProxy = false;

            CreateClient();
        }

        public SiteManager(string UserName, string UserPassword, string UserDomain, string ProxyAddress, int ProxyPort)
        {
            _userName = UserName;
            _userPassword = UserPassword;
            _userDomain = UserDomain;
            _proxyAddress = ProxyAddress;
            _proxyPort = ProxyPort;
            _useProxy = true;

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
                ICredentials networkCredential = new System.Net.NetworkCredential(_userName, _userPassword, _userDomain);

                string address = $"{_proxyAddress}:{_proxyPort.ToString()}";

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

            Task<string> task = _httpClient.GetStringAsync(uri);
            Task.WhenAll(task);
            allPageData = task.Result;


            //_webClient.Encoding = System.Text.Encoding.UTF8;
            //byte[] byteArray = _webClient.DownloadData(Site);
            //allPageData = System.Text.Encoding.Default.GetString(byteArray);


            return allPageData;
        }
    }
}
