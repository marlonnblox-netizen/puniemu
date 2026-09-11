using Newtonsoft.Json;

namespace Puniemu.Src.Server.GameServer.Requests.LaunchingInfos.Logic
{
    public class LaunchingInfosHandler
    {
        public static async Task HandleAsync(HttpContext ctx)
        {
            var scheme = ctx.Request.Scheme;
            var host = ctx.Request.Host.Value;
            var baseUrl = $"{scheme}://{host}";

            var response = new
            {
                state = "00",
                stateMessage = "",
                loginable = "Y",
                playable = "Y",
                currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                heartBeatInterval = 60,
                cacheTTLSec = 3600,
                brandType = "TOASTGAME",
                serviceDomain = "GLOBAL",

                serverInfos = new Dictionary<string, string>
                {
                    { "LOGIN", baseUrl },
                    { "GAMESVR", baseUrl },
                    { "CS", baseUrl },
                    { "NOTICE", baseUrl },
                    { "IMAGE", baseUrl },
                    { "IMAGE_RESIZE", baseUrl },
                    { "MFS", baseUrl },
                    { "LCS", baseUrl },
                    { "NOMAD", baseUrl },
                    { "PHOTO", baseUrl },
                    { "SILOS", baseUrl },
                    { "GAME_PLUS", baseUrl },
                    { "GAME_INDICATOR_BIP", baseUrl },
                    { "RTA_BIP", baseUrl },
                    { "STABILITY_BIP", baseUrl },
                    { "GAME_SERVER", baseUrl }
                },

                loginUrl = new
                {
                    hangameLoginUrl_JP = baseUrl,
                    hangamejp = baseUrl,
                    hangameOAuthUrl = $"{baseUrl}/authorize.nhn",
                    hangameEmailOAuthUrl = $"{baseUrl}/egm/login.nhn"
                },

                userAuthentication = new
                {
                    checkAuthUrl = $"{baseUrl}/hsp/checkAuth.nhn",
                    reAuthUrl = $"{baseUrl}/hsp/reAuth.nhn"
                },

                idpInfo = new
                {
                    hangame = new { selected = "Y", loginable = "Y", consumerKey = "DUMMY_KEY", consumerSecret = "DUMMY_SECRET", redirectionUrl = baseUrl },
                    hangamejp = new { selected = "Y", loginable = "Y", consumerKey = "DUMMY_KEY", consumerSecret = "DUMMY_SECRET", redirectionUrl = baseUrl },
                    google = new { selected = "N", loginable = "N", consumerKey = "", consumerSecret = "", redirectionUrl = "" },
                    facebook = new { selected = "N", loginable = "N", consumerKey = "", consumerSecret = "", redirectionUrl = "" },
                    gree = new { selected = "N", loginable = "N", consumerKey = "", consumerSecret = "", redirectionUrl = "" }
                },

                lncNotices = new object[] { },

                serviceInfo = new { maxRankingSize = 100 },

                clientAttributes = new
                {
                    ENFORCED_IDP_LOGIN = "N",
                    HOLD_IDP_LOGIN = "N"
                },

                payment = new { selected = "N" },

                logncrash = new
                {
                    appKey = "DUMMY_APPKEY",
                    logLevel = "ERROR",
                    logtype = "NELO2"
                },

                stabilityStatus = "Y",

                toastCloudAppInfo = new
                {
                    Push = new { appKey = "DUMMY_PUSH_APPKEY" }
                },

                indicatorLogServer = baseUrl,

                loginUrlSimple = $"{baseUrl}/login.nhn",
                termsUrl = $"{baseUrl}/help/inquiry/top.nhn",
                csUrl = $"{baseUrl}/help/inquiry/top.nhn",
                personalInfoCollectionUrl = $"{baseUrl}/help/inquiry/top.nhn",
                punishReasonUrl = $"{baseUrl}/help/inquiry/top.nhn",

                LGCConfigKey = "",
                LGCGameUrl = "",
                LGCGcmProjectNum = ""
            };

            var json = JsonConvert.SerializeObject(response);
            ctx.Response.Headers.ContentType = "application/json";
            await ctx.Response.WriteAsync(json);
        }
    }
}
