namespace KyoshinMonitorLib.UrlGenerator
{
	/// <summary>
	/// 生成するURLの種類(WebApi)
	/// </summary>
	public enum WebApiUrlType
	{
		/// <summary>
		/// リアルタイム情報
		/// <para>震度、加速度など</para>
		/// </summary>
		RealTimeImg = 0,

		/// <summary>
		/// 到達予想震度
		/// </summary>
		EstShindo,

		/// <summary>
		/// P波、S波到達予想円
		/// </summary>
		PSWave,

		/// <summary>
		/// 緊急地震速報のJson
		/// </summary>
		EewJson,
	}
}