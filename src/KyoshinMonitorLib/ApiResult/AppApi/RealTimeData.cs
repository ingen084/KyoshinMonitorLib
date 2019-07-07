using Newtonsoft.Json;
using System;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class RealTimeData
	{
		[JsonProperty("dataTime")]
		public DateTime DateTime { get; set; }
		[JsonProperty("packetType")]
		public string PacketType { get; set; }
		[JsonProperty("kyoshinType")]
		public string KyoshinType { get; set; }
		[JsonProperty("baseData")]
		public string BaseData { get; set; }
		[JsonProperty("baseSerialNo")]
		public string BaseSerialNo { get; set; }
		[JsonProperty("items")]
		public float?[] Items { get; set; }
		[JsonProperty("result")]
		public Result Result { get; set; }
		[JsonProperty("security")]
		public Security Security { get; set; }
	}
}
