using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class MeshList
	{
		[JsonPropertyName("items")]
		public object[][] Items { get; set; }
		[JsonPropertyName("dataTime")]
		public DateTime DataTime { get; set; }
		[JsonPropertyName("serialNo")]
		public string SerialNo { get; set; }

		[JsonPropertyName("result")]
		public Result Result { get; set; }
	}
}
