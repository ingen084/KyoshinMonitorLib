using Newtonsoft.Json;

namespace KyoshinMonitorLib.ApiResult
{
	public class Security
	{
		[JsonProperty("realm")]
		public string Realm { get; set; }
		[JsonProperty("hash")]
		public string Hash { get; set; }
	}
}
