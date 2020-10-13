
namespace ProgArchives
{
    public class SiteManager
    {
        private string _userName;
        private string _userPassword;
        private string _userDomain;
        private string _proxyAddress;
        private int _proxyPort;
        private System.Net.WebClient _webClient = null;

        public SiteManager()
        {
            _userName = "";
            _userPassword = "";
            _userDomain = "";
            _proxyAddress = "";
            _proxyPort = 0;

            CreateClient();
        }

        public SiteManager(string UserName, string UserPassword, string UserDomain, string ProxyAddress, int ProxyPort)
        {
            _userName = UserName;
            _userPassword = UserPassword;
            _userDomain = UserDomain;
            _proxyAddress = ProxyAddress;
            _proxyPort = ProxyPort;

            CreateClient();
        }

        ~SiteManager()
        {
            _webClient = null;
        }

        private void CreateClient()
        {
            System.Net.NetworkCredential networkCredential = null;
            System.Net.WebProxy proxyObj = null;

            if ((_userName != "") && (_userPassword != ""))
            {
                networkCredential = new System.Net.NetworkCredential(_userName, _userPassword, _userDomain);

                string address = _proxyAddress + ":" + _proxyPort.ToString();
                proxyObj = new System.Net.WebProxy(address);
                proxyObj.Credentials = networkCredential;
            }

            _webClient = new System.Net.WebClient();

            if (proxyObj != null)
                _webClient.Proxy = proxyObj;
                //_webClient.Proxy.Credentials = networkCredential;
                //_webClient.Credentials = networkCredential;
        }
        
        public string GetAllData(string Site)
        {
            string allData = "";

            try
            {
                _webClient.Encoding = System.Text.Encoding.UTF8;
                byte[] byteArray = _webClient.DownloadData(Site);

                allData = System.Text.Encoding.Default.GetString(byteArray);
                //System.Text.UTF8Encoding enc = new System.Text.UTF8Encoding(); 
                //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
                //allData = enc.GetString(byteArray);
                byteArray = null;
            }
            catch
            { }

            return allData;
        }
    }
}
