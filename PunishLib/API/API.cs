using Dalamud.Logging;
using ECommons.DalamudServices;
using PunishLib.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PunishLib.API
{
    internal static class API
    {
        internal static string APITestEndPoint = "https://puni.sh/api/test/auth?authKey=";

        // 共用的靜態 HttpClient + 10 秒逾時:
        //  - 共用實例:避免每次 new 造成的 socket 耗盡(TIME_WAIT 累積)。
        //  - 明確逾時:沒設 Timeout 的預設是 100 秒;網路不通時 10 秒內一定會結束,不會無限期掛著。
        private static readonly HttpClient httpClient = new()
        {
            Timeout = TimeSpan.FromSeconds(10),
        };

        public async static Task<bool> ValidateKey()
        {
            using HttpResponseMessage responseMessage = await httpClient.GetAsync(APITestEndPoint + PunishLibMain.SharedConfig.APIKey);
            Svc.Log.Debug($"{responseMessage.StatusCode} {responseMessage.ReasonPhrase}");
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
