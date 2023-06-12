
namespace ProgArchivesCore.Config
{
    public class ConfigurationFields
    {
        public string ProxyNeeded { get; private set; }
        public bool HasProxy { get; private set; }
        public string ProxyUserId { get; private set; }
        public string ProxyUserPassword { get; private set; }
        public string ProxyUserDomain { get; private set; }
        public string ProxyAddress { get; private set; }
        public int ProxyPort { get; private set; }
        public string DataBaseEngine { get; private set; }
        public string DataBaseLocation { get; private set; }
        public string CurrentDirectory { get; private set; }

        public ConfigurationFields(string proxyNeeded, string proxyAddress, int proxyPort, string proxyUserDomain,
            string proxyUserId, string proxyUserPassword, string dataBaseEngine, string dataBaseLocation, string currentDir)
        {
            ProxyNeeded = proxyNeeded;
            HasProxy = System.Convert.ToBoolean(ProxyNeeded);

            if (HasProxy)
            {
                ProxyUserId = proxyUserId;
                ProxyUserPassword = proxyUserPassword;
                ProxyUserDomain = proxyUserDomain;
                ProxyAddress = proxyAddress;

                ProxyPort = proxyPort;
            }

            DataBaseEngine = dataBaseEngine;
            DataBaseLocation = dataBaseLocation;

            CurrentDirectory = currentDir;
        }
    }
}
