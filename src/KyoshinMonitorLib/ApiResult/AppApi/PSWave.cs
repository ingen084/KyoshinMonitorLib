using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class PSWave
	{
		[JsonProperty("dataTime")]
		public DateTime DataTime { get; set; }
		[JsonProperty("hypoType")]
		public string HypoType { get; set; }
		[JsonProperty("items")]
		public PSWaveItem[] Items { get; set; }

		[JsonProperty("result")]
		public Result Result { get; set; }
		[JsonProperty("security")]
		public Security Security { get; set; }
	}

	public class PSWaveItem
	{
		[JsonProperty("latitude")]
		public string Latitude { get; set; }
		[JsonProperty("longitude")]
		public string Longitude { get; set; }
		[JsonProperty("p_radius")]
		public string PWaveRadius { get; set; }
		[JsonProperty("s_radius")]
		public string SWaveRadius { get; set; }
	}
}
