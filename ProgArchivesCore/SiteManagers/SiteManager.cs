
using ProgArchivesCore.Config;
using System;
using System.Net;
using System.Net.Http;
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
        private Random _random = null;

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


            //ver no fundo codigo que pode ajudar REF231

            //com esta linhas deixou de dar: "The remote server returned an error: (403) Forbidden."
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 Edg/105.0.1343.53");
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml");
            //_httpClient.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");
            _httpClient.DefaultRequestHeaders.Add("Accept-Charset", "utf-8");

            _random = new Random();
        }

        public string GetAllHtmlData(string uri)
        {
            string allPageData = "";

            try
            {
                // string uri2 = "http://www.progarchives.com/artist.asp?id=12435";
                // string uri2 = "https://www.proggnosis.com/Artist/10";

                //for to not stress the site
                int sleep = _random.Next(1, 10) * 1000;
                Task task0 = Task.Delay(sleep);
                //Task.WhenAll(task0);
                task0.Wait();

                Task<string> task = _httpClient.GetStringAsync(uri);
                ////task.Wait();
                ////Task.WaitAll(task); //CA1843  Do not use 'WaitAll' with a single task
                Task.WhenAll(task);

                allPageData = task.Result;

                /////////////////////////////////////////////////////////////////////////////////

                //See automation -> https://www.youtube.com/watch?v=fXrS73pqFho

                //OLD 
                //System.Net.WebClient _webClient = new System.Net.WebClient();
                //_webClient.Encoding = System.Text.Encoding.UTF8;
                //byte[] byteArray = _webClient.DownloadData(uri);
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


//REF231
//CookieContainer cookieContainer = new CookieContainer();
//HttpClientHandler httpClientHandler = new HttpClientHandler();
//httpClientHandler.AllowAutoRedirect = true;
//httpClientHandler.UseCookies = true;
//httpClientHandler.CookieContainer = cookieContainer;
//HttpClient httpClient = new HttpClient(httpClientHandler);

////com esta duas linhas deixou de dar: "The remote server returned an error: (403) Forbidden."
//_httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36 Edg/105.0.1343.53");
//_httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml");
////_httpClient.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");
//_httpClient.DefaultRequestHeaders.Add("Accept-Charset", "utf-8");

////até aqui deu

////ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls
////'CALL GET /api/getNonce  
//string login_url = "_host" + "/api/getNonce";
//string login_parameter = "?user_name =" + "_username" + "&password=" + "_password";

//Task<string> task1 = _httpClient.GetStringAsync(uri);
//Task<HttpResponseMessage> task1 = httpClient.GetAsync(uri); //"login_url + login_parameter"
//                                                            //HttpResponseMessage login_response = await httpClient.GetAsync("login_url + login_parameter");// as HttpResponseMessage;
//                                                            //string login_contents = await login_response.Content.ReadAsStringAsync();
//Task.WhenAll(task1);
//string htmlData = task1.Result;