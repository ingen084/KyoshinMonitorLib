using System;
using System.Globalization;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.WebApi
{
	public class Eew
	{
		[JsonPropertyName("result")]
		public Result Result { get; set; }
		[JsonPropertyName("report_time")]
		public string ReportTimeString { get; set; }
		[JsonIgnore]
		public DateTime? ReportTime => DateTime.TryParse(ReportTimeString, out var time) ? time : null as DateTime?;
		[JsonPropertyName("region_code")]
		public string RegionCode { get; set; }
		[JsonPropertyName("request_time")]
		public string RequestTime { get; set; }
		[JsonPropertyName("region_name")]
		public string RegionName { get; set; }

		[JsonPropertyName("longitude")]
		public string LongitudeString { get; set; }

		[JsonIgnore]
		public float? Longitude => LongitudeString != null && float.TryParse(LongitudeString, out var lon) ? lon : null as float?;

		[JsonPropertyName("is_cancel")]
		public bool? IsCancel { get; set; }
		[JsonPropertyName("depth")]
		public string DepthString { get; set; }
		[JsonIgnore]
		public int? Depth => int.TryParse(DepthString.Replace("km", ""), out var depth) ? depth : null as int?;
		[JsonPropertyName("calcintensity")]
		public string CalcintensityString { get; set; }
		[JsonIgnore]
		public JmaIntensity Calcintensity => CalcintensityString?.ToJmaIntensity() ?? JmaIntensity.Unknown;

		[JsonPropertyName("is_final")]
		public bool? IsFinal { get; set; }
		[JsonPropertyName("isTraining")]
		public bool? IsTraining { get; set; }

		[JsonPropertyName("latitude")]
		public string LatitudeString { get; set; }
		[JsonIgnore]
		public float? Latitude => LatitudeString != null && float.TryParse(LatitudeString, out var lat) ? lat : null as float?;
		[JsonIgnore]
		public Location Location => (Latitude == null || Longitude == null) ? null : new Location(Latitude.Value, Longitude.Value);
		[JsonPropertyName("origin_time")]
		public string OriginTimeString { get; set; }
		[JsonIgnore]
		public DateTime? OriginTime => DateTime.TryParseExact(OriginTimeString, "yyyyMMddHHmmss", null, DateTimeStyles.None, out var time) ? time : null as DateTime?;
		[JsonPropertyName("security")]
		public Security Security { get; set; }
		[JsonPropertyName("magunitude")]
		public string MagunitudeString { get; set; }
		[JsonIgnore]
		public float? Magunitude => float.TryParse(MagunitudeString, out var val) ? val : null as float?;
		[JsonPropertyName("report_num")]
		public string ReportNumString { get; set; }
		[JsonIgnore]
		public int? ReportNum => int.TryParse(ReportNumString, out var val) ? val : null as int?;
		[JsonPropertyName("request_hypo_type")]
		public string RequestHypoType { get; set; }
		[JsonPropertyName("report_id")]
		public string ReportId { get; set; }
		[JsonPropertyName("alertflg")]
		public string AlertFlag { get; set; }
		[JsonIgnore]
		public bool IsAlert => AlertFlag == "警報";
	}
}
