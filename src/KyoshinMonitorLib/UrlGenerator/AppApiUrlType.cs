using System;

namespace KyoshinMonitorLib.UrlGenerator
{
	/// <summary>
	/// 生成するURLの種類(スマホアプリApi)
	/// </summary>
	public enum AppApiUrlType
	{
		/// <summary>
		/// リアルタイム情報
		/// <para>震度、加速度など</para>
		/// </summary>
		RealTimeData,

		[Obsolete("このAPIは現在利用できなくなっています。")]
		/// <summary>
		/// 緊急地震速報の到達予想震度
		/// </summary>
		HypoInfoJson,

		[Obsolete("このAPIは現在利用できなくなっています。")]
		/// <summary>
		/// 緊急地震速報のP波、S波到達予想円
		/// </summary>
		PSWaveJson,

		[Obsolete("このAPIは現在利用できなくなっています。")]
		/// <summary>
		/// 緊急地震速報の予想震度
		/// </summary>
		EstShindoJson,
	}
}
