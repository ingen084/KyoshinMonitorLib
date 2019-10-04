using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class PSWave
	{
		[JsonPropertyName("dataTime")]
		public DateTime DataTime { get; set; }
		[JsonPropertyName("hypoType")]
		public string HypoType { get; set; }
		[JsonPropertyName("items")]
		public PSWaveItem[] Items { get; set; }

		[JsonPropertyName("result")]
		public Result Result { get; set; }
		[JsonPropertyName("security")]
		public Security Security { get; set; }
	}

	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class PSWaveItem
	{
		[JsonPropertyName("latitude")]
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
		[JsonPropertyName("longitude")]
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
		[JsonPropertyName("p_radius")]
		public string PWaveRadiusString { get; set; }
		[JsonPropertyName("s_radius")]
		public string SWaveRadiusString { get; set; }
	}
}
