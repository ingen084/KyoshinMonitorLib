using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	public class Result
	{
		[JsonPropertyName("status")]
		public string Status { get; set; }
		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
