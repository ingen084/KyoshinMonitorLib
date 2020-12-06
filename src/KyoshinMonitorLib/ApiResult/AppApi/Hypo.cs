using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	/// <summary>
	/// 複数の緊急地震速報の情報
	/// </summary>
	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class Hypo
	{
		/// <summary>
		/// 時間
		/// </summary>
		[JsonPropertyName("dataTime")]
		public DateTime DataTime { get; set; }
		/// <summary>
		/// 緊急地震速報
		/// </summary>
		[JsonPropertyName("items")]
		public EewInfo[]? Eews { get; set; }
		/// <summary>
		/// リザルト
		/// </summary>
		[JsonPropertyName("result")]
		public Result? Result { get; set; }
		/// <summary>
		/// セキュリティ情報
		/// </summary>
		[JsonPropertyName("security")]
		public Security? Security { get; set; }
	}

	/// <summary>
	/// 緊急地震速報の情報
	/// </summary>
	public class EewInfo
	{
		/// <summary>
		/// 発報時間
		/// </summary>
		[JsonPropertyName("reportTime")]
		public DateTime ReportTime { get; set; }

		/// <summary>
		/// 震源の地点コード(生の値)
		/// </summary>
		[JsonPropertyName("regionCode")]
		public string? RegionCodeString { get; set; }
		/// <summary>
		/// 震源の地点コード(変換済み)
		/// </summary>
		[JsonIgnore]
		public int? RegionCode
		{
			get
			{
				if (!int.TryParse(RegionCodeString, out var value))
					return null;
				return value;
			}
		}

		/// <summary>
		/// 震源名
		/// </summary>
		[JsonPropertyName("regionName")]
		public string? RegionName { get; set; }

		/// <summary>
		/// 経度(生の値)
		/// </summary>
		[JsonPropertyName("longitude")]
		public string? LongitudeString { get; set; }
		/// <summary>
		/// 経度(変換済)
		/// </summary>
		[JsonIgnore]
		public float? Longitude
		{
			get
			{
				if (!float.TryParse(LongitudeString?.Replace("N", "+").Replace("S", "-"), out var value))
					return null;
				return value;
			}
		}

		/// <summary>
		/// キャンセル報かどうか(生の値)
		/// </summary>
		[JsonPropertyName("isCancel")]
		public string? IsCancelString { get; set; }
		/// <summary>
		/// キャンセル報かどうか(変換済)
		/// </summary>
		[JsonIgnore]
		public bool IsCancel => IsCancelString == "true";

		/// <summary>
		/// 深さ(生の値)
		/// </summary>
		[JsonPropertyName("depth")]
		public string? DepthString { get; set; }
		/// <summary>
		/// 深さ(変換済)
		/// <para>変換できなかった場合nullが返されます。</para>
		/// </summary>
		[JsonIgnore]
		public int? Depth
		{
			get
			{
				if (!int.TryParse(DepthString?.Replace("km", ""), out var depth))
					return null;
				return depth;
			}
		}

		/// <summary>
		/// 予想最大震度(生の値)
		/// </summary>
		[JsonPropertyName("calcintensity")]
		public string? CalcintensityString { get; set; }
		/// <summary>
		/// 予想最大震度(変換済)
		/// </summary>
		[JsonIgnore]
		public JmaIntensity Calcintensity => CalcintensityString?.ToJmaIntensity() ?? JmaIntensity.Error;

		/// <summary>
		/// 最終報かどうか(生の値)
		/// </summary>
		[JsonPropertyName("isFinal")]
		public string? IsFinalString { get; set; }
		/// <summary>
		/// 最終法かどうか(変換済)
		/// </summary>
		[JsonIgnore]
		public bool IsFinal => IsFinalString == "true";

		/// <summary>
		/// 訓練報かどうか(生の値)
		/// </summary>
		[JsonPropertyName("isTraining")]
		public string? IsTrainingString { get; set; }
		/// <summary>
		/// 訓練報かどうか(変換済)
		/// </summary>
		[JsonIgnore]
		public bool IsTraining => IsTrainingString == "true";

		/// <summary>
		/// 緯度(生の値)
		/// </summary>
		[JsonPropertyName("latitude")]
		public string? LatitudeString { get; set; }
		/// <summary>
		/// 緯度(変換済み)
		/// </summary>
		[JsonIgnore]
		public float? Latitude
		{
			get
			{
				if (!float.TryParse(LatitudeString?.Replace("E", "+").Replace("W", "-"), out var value))
					return null;
				return value;
			}
		}

		/// <summary>
		/// 発生時刻
		/// </summary>
		[JsonPropertyName("originTime")]
		public DateTime OriginTime { get; set; }

		/// <summary>
		/// マグニチュード(生の値)
		/// </summary>
		[JsonPropertyName("magnitude")]
		public string? MagnitudeString { get; set; }
		/// <summary>
		/// マグニチュード(変換済み)
		/// </summary>
		[JsonIgnore]
		public float? Magnitude
		{
			get
			{
				if (!float.TryParse(MagnitudeString, out var value))
					return null;
				return value;
			}
		}

		/// <summary>
		/// 発報番号(生の値)
		/// </summary>
		[JsonPropertyName("reportNum")]
		public string? ReportNumString { get; set; }
		/// <summary>
		/// 発報番号(変換済み)
		/// </summary>
		[JsonIgnore]
		public int? ReportNum
		{
			get
			{
				if (!int.TryParse(ReportNumString, out var value))
					return null;
				return value;
			}
		}

		/// <summary>
		/// EEWID?
		/// </summary>
		[JsonPropertyName("reportId")]
		public string? ReportId { get; set; }
	}
}
