using ProtoBuf;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 観測点のタイプ
	/// </summary>
	[ProtoContract]
	public enum ObservationPointType
	{
		/// <summary>
		/// 不明(なるべく使用しないように)
		/// </summary>
		Unknown,

		/// <summary>
		/// KiK-net
		/// </summary>
		KiK_net,

		/// <summary>
		/// K-NET
		/// </summary>
		K_NET,
	}

	/// <summary>
	/// ObservationPointTypeの拡張メソッドたち
	/// </summary>
	public static class ObservationPointTypeExtensions
	{
		/// <summary>
		/// 人が読みやすい文字に変換します。
		/// </summary>
		/// <param name="type">変換させるObservationPointType</param>
		/// <returns>変換された文字列</returns>
		public static string ToNaturalString(this ObservationPointType type)
		{
			switch (type)
			{
				case ObservationPointType.Unknown:
					return "不明";

				case ObservationPointType.KiK_net:
					return "KiK-net";

				case ObservationPointType.K_NET:
					return "K-NET";
			}
			return "エラー";
		}

		/// <summary>
		/// EqWatchの観測点の種類からObservationPointTypeに変換します。
		/// </summary>
		/// <param name="str">変換元</param>
		/// <returns>変換後</returns>
		public static ObservationPointType ToObservationPointType(this string str)
			=> str == "1" ? ObservationPointType.KiK_net : ObservationPointType.K_NET;
	}
}