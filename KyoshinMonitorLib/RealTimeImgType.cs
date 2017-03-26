namespace KyoshinMonitorLib
{
	/// <summary>
	/// リアルタイム画像の種類
	/// </summary>
	public enum RealTimeImgType
	{
		/// <summary>
		/// 震度
		/// </summary>
		Shindo,
		/// <summary>
		/// 最大加速度
		/// </summary>
		Pga,
		/// <summary>
		/// 最大速度
		/// </summary>
		Pgv,
		/// <summary>
		/// 最大変位
		/// </summary>
		Pgd,
		/// <summary>
		/// 速度応答0.125Hz
		/// </summary>
		Response_0_125Hz,
		/// <summary>
		/// 速度応答0.25Hz
		/// </summary>
		Response_0_25Hz,
		/// <summary>
		/// 速度応答0.5Hz
		/// </summary>
		Response_0_5Hz,
		/// <summary>
		/// 速度応答1Hz
		/// </summary>
		Response_1Hz,
		/// <summary>
		/// 速度応答2Hz
		/// </summary>
		Response_2Hz,
		/// <summary>
		/// 速度応答4Hz
		/// </summary>
		Response_4Hz,
	}

	/// <summary>
	/// RealTimeImgTypeの拡張メソッド
	/// </summary>
	public static class RealTimeImgTimeExtensions
	{
		/// <summary>
		/// URLに使用する文字列に変換する
		/// </summary>
		/// <param name="type">変換するRealTimeImgTypy</param>
		/// <returns>変換された文字列</returns>
		public static string ToUrlString(this RealTimeImgType type)
		{
			switch (type)
			{
				case RealTimeImgType.Shindo:
					return "jma";
				case RealTimeImgType.Pga:
					return "acmap";
				case RealTimeImgType.Pgv:
					return "vcmap";
				case RealTimeImgType.Pgd:
					return "dcmap";
				case RealTimeImgType.Response_0_125Hz:
					return "rsp0125";
				case RealTimeImgType.Response_0_25Hz:
					return "rsp0250";
				case RealTimeImgType.Response_0_5Hz:
					return "rsp0500";
				case RealTimeImgType.Response_1Hz:
					return "rsp1000";
				case RealTimeImgType.Response_2Hz:
					return "rsp2000";
				case RealTimeImgType.Response_4Hz:
					return "rsp4000";
			}
			return null;
		}
	}
}
