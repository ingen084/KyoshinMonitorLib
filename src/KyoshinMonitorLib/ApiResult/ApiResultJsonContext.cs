using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
    [JsonSerializable(typeof(AppApi.RealtimeData))]
    [JsonSerializable(typeof(AppApi.SiteList))]
    [JsonSerializable(typeof(WebApi.Eew))]
    internal partial class ApiResultJsonContext : JsonSerializerContext
    {
    }
}
