using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class Hypo
	{
		[DataMember(Name = "dataTime")]
		public DateTime DataTime { get; set; }
		[DataMember(Name = "items")]
		public EewInfo[] Eews { get; set; }
		[DataMember(Name = "result")]
		public Result Result { get; set; }
		[DataMember(Name = "security")]
		public Security Security { get; set; }
	}

	public class EewInfo
	{
		/// <summary>
		/// 発報時間
		/// </summary>
		[DataMember(Name = "reportTime")]
		public DateTime ReportTime { get; set; }

		/// <summary>
		/// 震源の地点コード(生の値)
		/// </summary>
		[DataMember(Name = "regionCode")]
		public string RegionCodeString { get; set; }
		/// <summary>
		/// 震源の地点コード(変換済み)
		/// </summary>
		[IgnoreDataMember]
		public int RegionCode => int.Parse(RegionCodeString);

		/// <summary>
		/// 震源名
		/// </summary>
		[DataMember(Name = "regionName")]
		public string RegionName { get; set; }

		/// <summary>
		/// 経度(生の値)
		/// </summary>
		[DataMember(Name = "longitude")]
		public string LongitudeString { get; set; }
		/// <summary>
		/// 経度(変換済)
		/// </summary>
		[IgnoreDataMember]
		public float Longitude => float.Parse(LongitudeString.Replace("N", "+").Replace("S", "-"));

		/// <summary>
		/// キャンセル報かどうか(生の値)
		/// </summary>
		[DataMember(Name = "isCancel")]
		public string IsCancelString { get; set; }
		/// <summary>
		/// キャンセル報かどうか(変換済)
		/// </summary>
		[IgnoreDataMember]
		public bool IsCancel => IsCancelString == "true";

		/// <summary>
		/// 深さ(生の値)
		/// </summary>
		[DataMember(Name = "depth")]
		public string DepthString { get; set; }
		/// <summary>
		/// 深さ(変換済)
		/// <para>変換できなかった場合nullが返されます。</para>
		/// </summary>
		[IgnoreDataMember]
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
		[DataMember(Name = "calcintensity")]
		public string CalcintensityString { get; set; }
		/// <summary>
		/// 予想最大震度(変換済)
		/// </summary>
		[IgnoreDataMember]
		public JmaIntensity Calcintensity => CalcintensityString.ToJmaIntensity();

		/// <summary>
		/// 最終報かどうか(生の値)
		/// </summary>
		[DataMember(Name = "isFinal")]
		public string IsFinalString { get; set; }
		/// <summary>
		/// 最終法かどうか(変換済)
		/// </summary>
		[IgnoreDataMember]
		public bool IsFinal => IsFinalString == "true";

		/// <summary>
		/// 訓練報かどうか(生の値)
		/// </summary>
		[DataMember(Name = "isTraining")]
		public string IsTrainingString { get; set; }
		/// <summary>
		/// 訓練報かどうか(変換済)
		/// </summary>
		[IgnoreDataMember]
		public bool IsTraining => IsTrainingString == "true";

		/// <summary>
		/// 緯度(生の値)
		/// </summary>
		[DataMember(Name = "latitude")]
		public string LatitudeString { get; set; }
		/// <summary>
		/// 緯度(変換済み)
		/// </summary>
		[IgnoreDataMember]
		public float Latitude => float.Parse(LatitudeString.Replace("E", "+").Replace("W", "-"));

		/// <summary>
		/// 発生時刻
		/// </summary>
		[DataMember(Name = "originTime")]
		public DateTime OriginTime { get; set; }

		/// <summary>
		/// マグニチュード(生の値)
		/// </summary>
		[DataMember(Name = "magnitude")]
		public string MagnitudeString { get; set; }
		/// <summary>
		/// マグニチュード(変換済み)
		/// </summary>
		[IgnoreDataMember]
		public float Magnitude => float.Parse(MagnitudeString);

		/// <summary>
		/// 発報番号(生の値)
		/// </summary>
		[DataMember(Name = "reportNum")]
		public string ReportNumString { get; set; }
		/// <summary>
		/// 発報番号(変換済み)
		/// </summary>
		[IgnoreDataMember]
		public int ReportNum => int.Parse(ReportNumString);

		/// <summary>
		/// EEWID?
		/// </summary>
		[DataMember(Name = "reportId")]
		public string ReportId { get; set; }
	}
}
