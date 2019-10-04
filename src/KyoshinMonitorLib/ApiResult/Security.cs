using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	/// <summary>
	/// セキュリティ情報
	/// </summary>
	public class Security
	{
		/// <summary>
		/// realm
		/// </summary>
		[JsonPropertyName("realm")]
		public string Realm { get; set; }
		/// <summary>
		/// ハッシュ
		/// </summary>
		[JsonPropertyName("hash")]
		public string Hash { get; set; }
	}
}
