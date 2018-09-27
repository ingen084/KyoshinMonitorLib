using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class Hypo
	{
		[JsonProperty("dataTime")]
		public DateTime DataTime { get; set; }
		[JsonProperty("items")]
		public EewInfo[] Eews { get; set; }
		[JsonProperty("result")]
		public Result Result { get; set; }
		[JsonProperty("security")]
		public Security Security { get; set; }
	}

	public class EewInfo
	{
		/// <summary>
		/// 発報時間
		/// </summary>
		[JsonProperty("reportTime")]
		public DateTime ReportTime { get; set; }

		/// <summary>
		/// 震源の地点コード(生の値)
		/// </summary>
		[JsonProperty("regionCode")]
		public string RegionCodeString { get; set; }
		/// <summary>
		/// 震源の地点コード(変換済み)
		/// </summary>
		[JsonIgnore]
		public int RegionCode => int.Parse(RegionCodeString);

		/// <summary>
		/// 震源名
		/// </summary>
		[JsonProperty("regionName")]
		public string RegionName { get; set; }

		/// <summary>
		/// 経度(生の値)
		/// </summary>
		[JsonProperty("longitude")]
		public string LongitudeString { get; set; }
		/// <summary>
		/// 経度(変換済)
		/// </summary>
		[JsonIgnore]
		public float Longitude => float.Parse(LongitudeString.Replace("N", "+").Replace("S", "-"));

		/// <summary>
		/// キャンセル報かどうか(生の値)
		/// </summary>
		[JsonProperty("isCancel")]
		public string IsCancelString { get; set; }
		/// <summary>
		/// キャンセル報かどうか(変換済)
		/// </summary>
		[JsonIgnore]
		public bool IsCancel => IsCancelString == "true";

		/// <summary>
		/// 深さ(生の値)
		/// </summary>
		[JsonProperty("depth")]
		public string DepthString { get; set; }
		/// <summary>
		/// 深さ(変換済)
		/// <para>変換できなかった場合nullが返されます。</para>
		/// </summary>
		[JsonIgnore]
		public int? Depth
		{
			get
			{
				if (!int.TryParse(DepthString.Replace("km", ""), out var depth))
					return null;
				return depth;
			}
		}

		/// <summary>
		/// 予想最大震度(生の値)
		/// </summary>
		[JsonProperty("calcintensity")]
		public string CalcintensityString { get; set; }
		/// <summary>
		/// 予想最大震度(変換済)
		/// </summary>
		[JsonIgnore]
		public JmaIntensity Calcintensity => CalcintensityString.ToJmaIntensity();

		/// <summary>
		/// 最終報かどうか(生の値)
		/// </summary>
		[JsonProperty("isFinal")]
		public string IsFinalString { get; set; }
		/// <summary>
		/// 最終法かどうか(変換済)
		/// </summary>
		[JsonIgnore]
		public bool IsFinal => IsFinalString == "true";

		/// <summary>
		/// 訓練報かどうか(生の値)
		/// </summary>
		[JsonProperty("isTraining")]
		public string IsTrainingString { get; set; }
		/// <summary>
		/// 訓練報かどうか(変換済)
		/// </summary>
		[JsonIgnore]
		public bool IsTraining => IsTrainingString == "true";

		/// <summary>
		/// 緯度(生の値)
		/// </summary>
		[JsonProperty("latitude")]
		public string LatitudeString { get; set; }
		/// <summary>
		/// 緯度(変換済み)
		/// </summary>
		[JsonIgnore]
		public float Latitude => float.Parse(LatitudeString.Replace("E", "+").Replace("W", "-"));

		/// <summary>
		/// 発生時刻
		/// </summary>
		[JsonProperty("originTime")]
		public DateTime OriginTime { get; set; }

		/// <summary>
		/// マグニチュード(生の値)
		/// </summary>
		[JsonProperty("magnitude")]
		public string MagnitudeString { get; set; }
		/// <summary>
		/// マグニチュード(変換済み)
		/// </summary>
		[JsonIgnore]
		public float Magnitude => float.Parse(MagnitudeString);

		/// <summary>
		/// 発報番号(生の値)
		/// </summary>
		[JsonProperty("reportNum")]
		public string ReportNumString { get; set; }
		/// <summary>
		/// 発報番号(変換済み)
		/// </summary>
		[JsonIgnore]
		public int ReportNum => int.Parse(ReportNumString);

		/// <summary>
		/// EEWID?
		/// </summary>
		[JsonProperty("reportId")]
		public string ReportId { get; set; }
	}
}
