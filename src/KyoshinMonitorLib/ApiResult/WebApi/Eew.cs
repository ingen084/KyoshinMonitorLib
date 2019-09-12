using Newtonsoft.Json;
using System;
using System.Globalization;

namespace KyoshinMonitorLib.ApiResult.WebApi
{
	public class Eew
	{
		[JsonProperty("result")]
		public Result Result { get; set; }
		[JsonProperty("report_time")]
		public string ReportTimeString { get; set; }
		[JsonIgnore]
		public DateTime? ReportTime => DateTime.TryParse(ReportTimeString, out var time) ? time : null as DateTime?;
		[JsonProperty("region_code")]
		public string RegionCode { get; set; }
		[JsonProperty("request_time")]
		public string RequestTime { get; set; }
		[JsonProperty("region_name")]
		public string RegionName { get; set; }

		[JsonProperty("longitude")]
		public string LongitudeString { get; set; }

		[JsonIgnore]
		public float? Longitude => LongitudeString != null && float.TryParse(LongitudeString, out var lon) ? lon : null as float?;

		[JsonProperty("is_cancel")]
		public bool? IsCancel { get; set; }
		[JsonProperty("depth")]
		public string DepthString { get; set; }
		[JsonIgnore]
		public int? Depth => int.TryParse(DepthString.Replace("km", ""), out var depth) ? depth : null as int?;
		[JsonProperty("calcintensity")]
		public string CalcintensityString { get; set; }
		[JsonIgnore]
		public JmaIntensity Calcintensity => CalcintensityString?.ToJmaIntensity() ?? JmaIntensity.Unknown;

		[JsonProperty("is_final")]
		public bool? IsFinal { get; set; }
		[JsonProperty("isTraining")]
		public bool? IsTraining { get; set; }

		[JsonProperty("latitude")]
		public string LatitudeString { get; set; }
		[JsonIgnore]
		public float? Latitude => LatitudeString != null && float.TryParse(LatitudeString, out var lat) ? lat : null as float?;
		[JsonIgnore]
		public Location Location => (Latitude == null || Longitude == null) ? null : new Location(Latitude.Value, Longitude.Value);
		[JsonProperty("origin_time")]
		public string OriginTimeString { get; set; }
		[JsonIgnore]
		public DateTime? OriginTime => DateTime.TryParseExact(OriginTimeString, "yyyyMMddHHmmss", null, DateTimeStyles.None, out var time) ? time : null as DateTime?;
		[JsonProperty("security")]
		public Security Security { get; set; }
		[JsonProperty("magunitude")]
		public string MagunitudeString { get; set; }
		[JsonIgnore]
		public float? Magunitude => float.TryParse(MagunitudeString, out var val) ? val : null as float?;
		[JsonProperty("report_num")]
		public string ReportNumString { get; set; }
		[JsonIgnore]
		public int? ReportNum => int.TryParse(ReportNumString, out var val) ? val : null as int?;
		[JsonProperty("request_hypo_type")]
		public string RequestHypoType { get; set; }
		[JsonProperty("report_id")]
		public string ReportId { get; set; }
		[JsonProperty("alertflg")]
		public string AlertFlag { get; set; }
		[JsonIgnore]
		public bool IsAlert => AlertFlag == "警報";
	}
}
