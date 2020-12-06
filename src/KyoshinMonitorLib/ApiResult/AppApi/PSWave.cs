using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	/// <summary>
	/// 緊急地震速報の揺れの広がり
	/// </summary>
	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class PSWave
	{
		/// <summary>
		/// 時間
		/// </summary>
		[JsonPropertyName("dataTime")]
		public DateTime? DataTime { get; set; }
		/// <summary>
		/// 地震の種類?
		/// </summary>
		[JsonPropertyName("hypoType")]
		public string? HypoType { get; set; }
		/// <summary>
		/// 揺れの広がり
		/// </summary>
		[JsonPropertyName("items")]
		public PSWaveItem[]? Items { get; set; }
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
	/// 揺れの広がり
	/// </summary>
	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class PSWaveItem
	{
		/// <summary>
		/// 緯度(string)
		/// </summary>
		[JsonPropertyName("latitude")]
		public string? LatitudeString { get; set; }
		/// <summary>
		/// 緯度
		/// </summary>
		[JsonIgnore]
		public float? Latitude
		{
			get
			{
				if (!float.TryParse(LatitudeString?.Replace("E", "+").Replace("W", "-"), out var val))
					return null;
				return val;
			}
		}
		/// <summary>
		/// 経度(string)
		/// </summary>
		[JsonPropertyName("longitude")]
		public string? LongitudeString { get; set; }
		/// <summary>
		/// 経度
		/// </summary>
		[JsonIgnore]
		public float? Longitude
		{
			get
			{
				if (!float.TryParse(LongitudeString?.Replace("N", "+").Replace("S", "-"), out var val))
					return null;
				return val;
			}
		}
		/// <summary>
		/// P波の範囲
		/// </summary>
		[JsonPropertyName("p_radius")]
		public string? PWaveRadiusString { get; set; }
		/// <summary>
		/// S波の範囲
		/// </summary>
		[JsonPropertyName("s_radius")]
		public string? SWaveRadiusString { get; set; }
	}
}
