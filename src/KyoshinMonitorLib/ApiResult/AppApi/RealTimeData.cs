using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	/// <summary>
	/// リアルタイムデータ
	/// </summary>
	public class RealtimeData
	{
		/// <summary>
		/// 時間
		/// </summary>
		[JsonPropertyName("dataTime")]
		public DateTime DateTime { get; set; }
		/// <summary>
		/// 情報の種類?
		/// </summary>
		[JsonPropertyName("packetType")]
		public string PacketType { get; set; }
		/// <summary>
		/// 強震の種類
		/// </summary>
		[JsonPropertyName("kyoshinType")]
		public string KyoshinType { get; set; }
		/// <summary>
		/// ベースとなるデータ?
		/// </summary>
		[JsonPropertyName("baseData")]
		public string BaseData { get; set; }
		/// <summary>
		/// 観測点一覧用のID
		/// </summary>
		[JsonPropertyName("baseSerialNo")]
		public string BaseSerialNo { get; set; }
		/// <summary>
		/// 実際のデータ
		/// </summary>
		[JsonPropertyName("items")]
		public float?[] Items { get; set; }
		/// <summary>
		/// リザルト
		/// </summary>
		[JsonPropertyName("result")]
		public Result Result { get; set; }
		/// <summary>
		/// セキュリティ情報
		/// </summary>
		[JsonPropertyName("security")]
		public Security Security { get; set; }
	}
}
