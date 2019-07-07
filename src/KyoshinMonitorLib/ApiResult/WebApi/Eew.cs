using Newtonsoft.Json;

namespace KyoshinMonitorLib.ApiResult.WebApi
{
	public class Eew
	{
		[JsonProperty("result")]
		public Result Result { get; set; }
		[JsonProperty("report_time")]
		public string ReportTime { get; set; }
		[JsonProperty("region_code")]
		public string RegionCode { get; set; }
		[JsonProperty("request_time")]
		public string RequestTime { get; set; }
		[JsonProperty("region_name")]
		public string RegionName { get; set; }

		[JsonProperty("longitude")]
		public string LongitudeString { get; set; }

		[JsonIgnore]
		public float? Longitude
		{
			get => LongitudeString != null && float.TryParse(LongitudeString, out var lon) ? lon : null as float?;
			set => LongitudeString = value?.ToString();
		}

		[JsonProperty("is_cancel")]
		public bool? IsCancel { get; set; }
		[JsonProperty("depth")]
		public string Depth { get; set; }
		[JsonProperty("calcintensity")]
		public string Calcintensity { get; set; }
		[JsonProperty("is_final")]
		public bool? IsFinal { get; set; }
		[JsonProperty("isTraining")]
		public bool? IsTraining { get; set; }

		[JsonProperty("latitude")]
		public string LatitudeString { get; set; }
		[JsonIgnore]
		public float? Latitude
		{
			get => LatitudeString != null && float.TryParse(LatitudeString, out var lat) ? lat : null as float?;
			set => LatitudeString = value?.ToString();
		}
		[JsonIgnore]
		public Location Location
		{
			get
			{
				var lat = Latitude;
				var lng = Longitude;
				if (lat == null || lng == null)
					return null;
				return new Location(lat.Value, lng.Value);
			}
		}

		[JsonProperty("origin_time")]
		public string OriginTime { get; set; }
		[JsonProperty("security")]
		public Security Security { get; set; }
		[JsonProperty("magunitude")]
		public string Magunitude { get; set; }
		[JsonProperty("report_num")]
		public string ReportNum { get; set; }
		[JsonProperty("request_hypo_type")]
		public string RequestHypoType { get; set; }
		[JsonProperty("report_id")]
		public string ReportId { get; set; }
		[JsonProperty("alertflg")]
		public string AlertFlag { get; set; }
	}
}
