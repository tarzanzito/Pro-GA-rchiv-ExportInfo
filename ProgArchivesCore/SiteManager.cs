
using System;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using Windows.Media.Protection.PlayReady;

namespace Candal.Core
{
    public class SiteManager
    {
        private string _userName;
        private string _userPassword;
        private string _userDomain;
        private string _proxyAddress;
        private int _proxyPort;
        private bool _useProxy;
        private System.Net.WebClient _webClient = null;
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
            _webClient.Dispose();
            _webClient = null;
        }


        private void CreateClient()
        {
            _httpClient = new System.Net.Http.HttpClient();

            if (_useProxy)
            {
                ICredentials networkCredential1 = new System.Net.NetworkCredential(_userName, _userPassword, _userDomain);

                string address1 = _proxyAddress + ":" + _proxyPort.ToString();

                var proxy1 = new WebProxy
                {
                    Address = new Uri(address1),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false,
                    Credentials = networkCredential1
                };

                // Create a client handler that uses the proxy
                var httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy1,
                };

                // Disable SSL verification
                httpClientHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                // Finally, create the HTTP client object
                var client = new HttpClient(handler: httpClientHandler, disposeHandler: true);

                //var result = await client.GetStringAsync("https://api.ipify.org/");
            }
        }




        public string GetAllHtmlData(string uri)
        {
 
            string allPageData = "";

            System.Threading.Tasks.Task<string> task = _httpClient.GetStringAsync(uri);
            System.Threading.Tasks.Task.WhenAll(task);
            allPageData = task.Result;


            //_webClient.Encoding = System.Text.Encoding.UTF8;
            //byte[] byteArray = _webClient.DownloadData(Site);

            //allPageData = System.Text.Encoding.Default.GetString(byteArray);
            ////System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding(); 
            ////System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
            ////allData = enc.GetString(byteArray);
            //byteArray = null;

            return allPageData;
        }

        //private void CreateWebClient()
        //{
        //    _webClient = new System.Net.WebClient();

        //    if (_useProxy)
        //    {
        //        System.Net.NetworkCredential networkCredential = null;
        //        System.Net.WebProxy proxyObj = null;

        //        networkCredential = new System.Net.NetworkCredential(_userName, _userPassword, _userDomain);

        //        string address = _proxyAddress + ":" + _proxyPort.ToString();
        //        proxyObj = new System.Net.WebProxy(address);
        //        proxyObj.Credentials = networkCredential;

        //        if (proxyObj != null)
        //            _webClient.Proxy = proxyObj;
        //        //_webClient.Proxy.Credentials = networkCredential;
        //        //_webClient.Credentials = networkCredential;
        //    }
        //}

        //public string GetAllHtmlDataWebClient(string Site)
        //{
        //    string allPageData = "";

        //    _webClient.Encoding = System.Text.Encoding.UTF8;
        //    byte[] byteArray = _webClient.DownloadData(Site);

        //    allPageData = System.Text.Encoding.Default.GetString(byteArray);
        //    //System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding(); 
        //    //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //    //allData = enc.GetString(byteArray);
        //    byteArray = null;

        //    return allPageData;
        //}

    }
}
