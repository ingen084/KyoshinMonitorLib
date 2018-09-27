using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	public class Result
	{
		[JsonProperty("status")]
		public string Status { get; set; }
		[JsonProperty("message")]
		public string Message { get; set; }
	}
}
