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
                cacheTTLSec = 3600,
                loginUrl = $"{baseUrl}/login.nhn",
                serverInfos = new Dictionary<string, string>
                {
                    { "GAME_SERVER", baseUrl }
                },
                serviceDomain = "GLOBAL",
                serviceInfo = new { },
                termsUrl = $"{baseUrl}/help/inquiry/top.nhn",
                csUrl = $"{baseUrl}/help/inquiry/top.nhn"
            };

            var json = JsonConvert.SerializeObject(response);
            ctx.Response.Headers.ContentType = "application/json";
            await ctx.Response.WriteAsync(json);
        }
