namespace KyoshinMonitorLib.UrlGenerator
{
	/// <summary>
	/// 生成するURLの種類(WebApi)
	/// </summary>
	public enum LpgmWebApiUrlType
	{
		/// <summary>
		/// リアルタイム情報
		/// <para>震度、加速度など</para>
		/// </summary>
		RealtimeImg = 0,

		/// <summary>
		/// リアルタイム情報(長周期地震動)
		/// <para>長周期地震動階級、階級データ(周期n秒台)</para>
		/// </summary>
		LpgmRealtimeImg,

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

		/// <summary>
		/// 長周期地震動の予測階級
		/// </summary>
		LongPeriodImg,
	}
}