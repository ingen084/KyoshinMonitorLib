namespace KyoshinMonitorLib.UrlGenerator
{
	/// <summary>
	/// 生成するURLの種類(スマホアプリApi)
	/// </summary>
	public enum UrlTypeV2
	{
		/// <summary>
		/// リアルタイム情報
		/// <para>震度、加速度など</para>
		/// </summary>
		RealTimeData,

		/// <summary>
		/// 緊急地震速報の到達予想震度
		/// </summary>
		HypoInfoJson,

		/// <summary>
		/// 緊急地震速報のP波、S波到達予想円
		/// </summary>
		PSWaveJson,

		/// <summary>
		/// 緊急地震速報の予想震度
		/// </summary>
		EstShindoJson,
	}
}
