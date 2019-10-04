using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	/// <summary>
	/// メッシュ情報
	/// </summary>
	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class MeshList
	{
		/// <summary>
		/// データ
		/// </summary>
		[JsonPropertyName("items")]
		public object[][] Items { get; set; }
		/// <summary>
		/// 時間
		/// </summary>
		[JsonPropertyName("dataTime")]
		public DateTime DataTime { get; set; }
		/// <summary>
		/// シリアル番号
		/// </summary>
		[JsonPropertyName("serialNo")]
		public string SerialNo { get; set; }
		/// <summary>
		/// リザルト
		/// </summary>

		[JsonPropertyName("result")]
		public Result Result { get; set; }
	}
}
