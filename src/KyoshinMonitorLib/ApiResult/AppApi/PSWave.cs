using Newtonsoft.Json;
using System;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	[Obsolete("このAPIは現在利用できなくなっています。")]
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

	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class PSWaveItem
	{
		[JsonProperty("latitude")]
		public string LatitudeString { get; set; }
		[JsonIgnore]
		public float? Latitude
		{
			get
			{
				if (!float.TryParse(LatitudeString.Replace("E", "+").Replace("W", "-"), out var val))
					return null;
				return val;
			}
		}
		[JsonProperty("longitude")]
		public string LongitudeString { get; set; }
		[JsonIgnore]
		public float? Longitude
		{
			get
			{
				if (!float.TryParse(LongitudeString.Replace("N", "+").Replace("S", "-"), out var val))
					return null;
				return val;
			}
		}
		[JsonProperty("p_radius")]
		public string PWaveRadiusString { get; set; }
		[JsonProperty("s_radius")]
		public string SWaveRadiusString { get; set; }
	}
}
