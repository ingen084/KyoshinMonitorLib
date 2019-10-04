using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	/// <summary>
	/// 予想震度
	/// </summary>
	public class EstShindo
	{
		/// <summary>
		/// 震度情報
		/// </summary>
		[JsonPropertyName("list")]
		public float[] List { get; set; }
		/// <summary>
		/// 時間
		/// </summary>
		[JsonPropertyName("dataTime")]
		public DateTime DataTime { get; set; }
		/// <summary>
		/// 地震の種類?
		/// </summary>
		[JsonPropertyName("hypoType")]
		public string HypoType { get; set; }
		/// <summary>
		/// ベースとなるメッシュのデータ
		/// </summary>
		[JsonPropertyName("baseData")]
		public string BaseData { get; set; }
		/// <summary>
		/// メッシュの開始位置?
		/// </summary>
		[JsonPropertyName("startMesh")]
		public string StartMesh { get; set; }
		/// <summary>
		/// メッシュのidxの開始位置
		/// </summary>
		[JsonPropertyName("startMeshIdx")]
		public int StartMeshIdx { get; set; }
		/// <summary>
		/// シリアルID
		/// </summary>
		[JsonPropertyName("serialNo")]
		public string SerialNo { get; set; }
		/// <summary>
		/// セキュリティ情報
		/// </summary>

		[JsonPropertyName("security")]
		public Security Security { get; set; }
		/// <summary>
		/// リザルト
		/// </summary>
		[JsonPropertyName("result")]
		public Result Result { get; set; }
	}
}
