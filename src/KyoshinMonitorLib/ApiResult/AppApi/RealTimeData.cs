using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class RealTimeData
	{
		[JsonPropertyName("dataTime")]
		public DateTime DateTime { get; set; }
		[JsonPropertyName("packetType")]
		public string PacketType { get; set; }
		[JsonPropertyName("kyoshinType")]
		public string KyoshinType { get; set; }
		[JsonPropertyName("baseData")]
		public string BaseData { get; set; }
		[JsonPropertyName("baseSerialNo")]
		public string BaseSerialNo { get; set; }
		[JsonPropertyName("items")]
		public float?[] Items { get; set; }
		[JsonPropertyName("result")]
		public Result Result { get; set; }
		[JsonPropertyName("security")]
		public Security Security { get; set; }
	}
}
