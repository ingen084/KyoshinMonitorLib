using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	public class Security
	{
		[JsonPropertyName("realm")]
		public string Realm { get; set; }
		[JsonPropertyName("hash")]
		public string Hash { get; set; }
	}
}
