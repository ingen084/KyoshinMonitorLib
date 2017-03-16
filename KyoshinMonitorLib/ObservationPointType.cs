using ProtoBuf;

namespace KyoshinMonitorLib
{
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

	public static class ObservationPointTypeExtensions
	{
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

		public static ObservationPointType ToObservationPointType(this string str)
			=> str == "1" ? ObservationPointType.KiK_net : ObservationPointType.K_NET;
	}
}