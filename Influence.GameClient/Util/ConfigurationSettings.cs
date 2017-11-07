using System.Configuration;

namespace Influence.GameClient.Util
{
    public static class ConfigurationSettings
    {
        public static string ServerUrl
            => ConfigurationManager.AppSettings[nameof(ServerUrl)];

        public static string PlayerName
            => ConfigurationManager.AppSettings[nameof(PlayerName)];
    }
}
