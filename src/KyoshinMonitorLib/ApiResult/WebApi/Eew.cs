using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.WebApi
{
	/// <summary>
	/// Web版APIでの緊急地震速報の情報
	/// </summary>
	public class Eew
	{
		/// <summary>
		/// リザルト
		/// </summary>
		[JsonPropertyName("result")]
		public Result? Result { get; set; }
		/// <summary>
		/// 発報時間
		/// </summary>
		[JsonPropertyName("report_time")]
		public string? ReportTimeString { get; set; }
		/// <summary>
		/// 発報時間
		/// </summary>
		[JsonIgnore]
		public DateTime? ReportTime => DateTime.TryParse(ReportTimeString, out var time) ? time : null;
		/// <summary>
		/// 地域コード
		/// </summary>
		[JsonPropertyName("region_code")]
		public string? RegionCode { get; set; }
		/// <summary>
		/// リクエスト時間
		/// </summary>
		[JsonPropertyName("request_time")]
		public string? RequestTime { get; set; }
		/// <summary>
		/// 地域名
		/// </summary>
		[JsonPropertyName("region_name")]
		public string? RegionName { get; set; }
		/// <summary>
		/// 経度(デシリアライズ用)
		/// </summary>

		[JsonPropertyName("longitude")]
		public string? LongitudeString { get; set; }
		/// <summary>
		/// 経度
		/// </summary>
		[JsonIgnore]
		public float? Longitude => LongitudeString != null && float.TryParse(LongitudeString, out var lon) ? lon : null;
		/// <summary>
		/// キャンセル報か(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("is_cancel")]
		public JsonElement IsCancelRaw { get; set; }
		/// <summary>
		/// キャンセル報か
		/// </summary>
		[JsonIgnore]
		public bool? IsCancel => IsCancelRaw.ToBoolFromStringableBool();
		/// <summary>
		/// 震源の深さ(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("depth")]
		public string? DepthString { get; set; }
		/// <summary>
		/// 震源の深さ
		/// </summary>
		[JsonIgnore]
		public int? Depth => int.TryParse(DepthString?.Replace("km", ""), out var depth) ? depth : null;
		/// <summary>
		/// 予想最大震度(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("calcintensity")]
		public string? CalcintensityString { get; set; }
		/// <summary>
		/// 予想最大震度
		/// </summary>
		[JsonIgnore]
		public JmaIntensity? Calcintensity => CalcintensityString?.ToJmaIntensity() ?? JmaIntensity.Unknown;
		/// <summary>
		/// 最終報か(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("is_final")]
		public JsonElement IsFinalRaw { get; set; }
		/// <summary>
		/// 最終報か
		/// </summary>
		[JsonIgnore]
		public bool? IsFinal => IsFinalRaw.ToBoolFromStringableBool();
		/// <summary>
		/// 訓練報か(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("isTraining")]
		public JsonElement IsTrainingRaw { get; set; }
		/// <summary>
		/// 訓練報か
		/// </summary>
		public bool? IsTraining => IsTrainingRaw.ToBoolFromStringableBool();
		/// <summary>
		/// 緯度(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("latitude")]
		public string? LatitudeString { get; set; }
		/// <summary>
		/// 緯度
		/// </summary>
		[JsonIgnore]
		public float? Latitude => LatitudeString != null && float.TryParse(LatitudeString, out var lat) ? lat : null;
		/// <summary>
		/// 震源の座標
		/// </summary>
		[JsonIgnore]
		public Location? Location => Latitude is float lat && Longitude is float lng ? new Location(lat, lng) : null;
		/// <summary>
		/// 発生時間(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("origin_time")]
		public string? OriginTimeString { get; set; }
		/// <summary>
		/// 発生時間
		/// </summary>
		[JsonIgnore]
		public DateTime? OriginTime => DateTime.TryParseExact(OriginTimeString, "yyyyMMddHHmmss", null, DateTimeStyles.None, out var time) ? time : null;
		/// <summary>
		/// セキュリティ情報
		/// </summary>
		[JsonPropertyName("security")]
		public Security? Security { get; set; }
		/// <summary>
		/// マグニチュード(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("magunitude")]
		public string? MagunitudeString { get; set; }
		/// <summary>
		/// マグニチュード
		/// </summary>
		[JsonIgnore]
		public float? Magunitude => float.TryParse(MagunitudeString, out var val) ? val : null;
		/// <summary>
		/// 発報番号(デシリアライズ用)
		/// </summary>
		[JsonPropertyName("report_num")]
		public string? ReportNumString { get; set; }
		/// <summary>
		/// 発報番号
		/// </summary>
		[JsonIgnore]
		public int? ReportNum => int.TryParse(ReportNumString, out var val) ? val : null;
		/// <summary>
		/// なにこれ?
		/// </summary>
		[JsonPropertyName("request_hypo_type")]
		public string? RequestHypoType { get; set; }
		/// <summary>
		/// 地震ID
		/// </summary>
		[JsonPropertyName("report_id")]
		public string? ReportId { get; set; }
		/// <summary>
		/// 警報 or 予報
		/// </summary>
		[JsonPropertyName("alertflg")]
		public string? AlertFlag { get; set; }
		/// <summary>
		/// 警報か
		/// </summary>
		[JsonIgnore]
		public bool IsAlert => AlertFlag == "警報";
	}
}
