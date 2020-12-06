using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	/// <summary>
	/// リザルト
	/// </summary>
	public class Result
	{
		/// <summary>
		/// ステータス
		/// </summary>
		[JsonPropertyName("status")]
		public string? Status { get; set; }
		/// <summary>
		/// メッセージ
		/// </summary>
		[JsonPropertyName("message")]
		public string? Message { get; set; }
	}
}
