namespace KyoshinMonitorLib
{
	/// <summary>
	/// 気象庁震度階級 +α
	/// </summary>
	public enum JmaIntensity
	{
		/// <summary>
		/// 震度不明
		/// </summary>
		Unknown,

		/// <summary>
		/// 震度1未満
		/// </summary>
		Int0,

		/// <summary>
		/// 震度1
		/// </summary>
		Int1,

		/// <summary>
		/// 震度2
		/// </summary>
		Int2,

		/// <summary>
		/// 震度3
		/// </summary>
		Int3,

		/// <summary>
		/// 震度4
		/// </summary>
		Int4,

		/// <summary>
		/// 震度5弱
		/// </summary>
		Int5Lower,

		/// <summary>
		/// 震度5強
		/// </summary>
		Int5Upper,

		/// <summary>
		/// 震度6弱
		/// </summary>
		Int6Lower,

		/// <summary>
		/// 震度6強
		/// </summary>
		Int6Upper,

		/// <summary>
		/// 震度7
		/// </summary>
		Int7,

		/// <summary>
		/// 震度異常
		/// </summary>
		Error,
	}

	/// <summary>
	/// Shindoの拡張メソッド
	/// </summary>
	public static class ShindoExtensions
	{
		/// <summary>
		/// 生の震度の値を気象庁震度階級に変換します。(float?版)
		/// </summary>
		/// <param name="intensity">変換対象の震度</param>
		/// <returns>変換されたShindo</returns>
		public static JmaIntensity ToJmaIntensity(this float? intensity)
		{
			if (intensity == null)
				return JmaIntensity.Unknown;
			return intensity.Value.ToJmaIntensity();
		}

		/// <summary>
		/// 生の震度の値を気象庁震度階級に変換します。
		/// </summary>
		/// <param name="intensity">変換対象の震度</param>
		/// <returns>変換されたShindo</returns>
		public static JmaIntensity ToJmaIntensity(this float intensity)
		{
			if (intensity < -3.0)
				return JmaIntensity.Error;
			if (intensity < 0.5)
				return JmaIntensity.Int0;
			if (intensity < 1.5)
				return JmaIntensity.Int1;
			if (intensity < 2.5)
				return JmaIntensity.Int2;
			if (intensity < 3.5)
				return JmaIntensity.Int3;
			if (intensity < 4.5)
				return JmaIntensity.Int4;
			if (intensity < 5.0)
				return JmaIntensity.Int5Lower;
			if (intensity < 5.5)
				return JmaIntensity.Int5Upper;
			if (intensity < 6.0)
				return JmaIntensity.Int6Lower;
			return intensity < 6.5 ? JmaIntensity.Int6Upper : JmaIntensity.Int7;
		}

		/// <summary>
		/// 気象庁震度階級を短い形式の文字列に変換します。
		/// </summary>
		/// <param name="shindo">変換するShindo</param>
		/// <returns>変換された文字列</returns>
		public static string ToShortString(this JmaIntensity shindo)
		{
			switch (shindo)
			{
				case JmaIntensity.Error:
					return "異常";

				case JmaIntensity.Int0:
					return "0";

				case JmaIntensity.Int1:
					return "1";

				case JmaIntensity.Int2:
					return "2";

				case JmaIntensity.Int3:
					return "3";

				case JmaIntensity.Int4:
					return "4";

				case JmaIntensity.Int5Lower:
					return "5-";

				case JmaIntensity.Int5Upper:
					return "5+";

				case JmaIntensity.Int6Lower:
					return "6-";

				case JmaIntensity.Int6Upper:
					return "6+";

				case JmaIntensity.Int7:
					return "7";

				default:
					return "不明";
			}
		}

		/// <summary>
		/// 気象庁震度階級を長い形式の文字列に変換します。
		/// </summary>
		/// <param name="shindo">変換するShindo</param>
		/// <returns>変換された文字列</returns>
		public static string ToLongString(this JmaIntensity shindo)
		{
			switch (shindo)
			{
				case JmaIntensity.Error:
					return "震度異常";

				case JmaIntensity.Int0:
					return "震度0";

				case JmaIntensity.Int1:
					return "震度1";

				case JmaIntensity.Int2:
					return "震度2";

				case JmaIntensity.Int3:
					return "震度3";

				case JmaIntensity.Int4:
					return "震度4";

				case JmaIntensity.Int5Lower:
					return "震度5弱";

				case JmaIntensity.Int5Upper:
					return "震度5強";

				case JmaIntensity.Int6Lower:
					return "震度6弱";

				case JmaIntensity.Int6Upper:
					return "震度6強";

				case JmaIntensity.Int7:
					return "震度7";

				default:
					return "震度不明";
			}
		}

		/// <summary>
		/// 文字から気象庁震度階級に変換します。
		/// <para>EewJsonぐらいでしか確認していません。</para>
		/// </summary>
		/// <param name="source">変換する文字列</param>
		/// <returns>変換されたShindo</returns>
		public static JmaIntensity ToJmaIntensity(this string source)
		{
			source = source.Replace("震度", "");
			switch (source)
			{
				case "0":
				case "０":
					return JmaIntensity.Int0;

				case "1":
				case "１":
					return JmaIntensity.Int1;

				case "2":
				case "２":
					return JmaIntensity.Int2;

				case "3":
				case "３":
					return JmaIntensity.Int3;

				case "4":
				case "４":
					return JmaIntensity.Int4;

				case "5-":
				case "5弱":
				case "５弱":
					return JmaIntensity.Int5Lower;

				case "5+":
				case "5強":
				case "５強":
					return JmaIntensity.Int5Upper;

				case "6-":
				case "6弱":
				case "６弱":
					return JmaIntensity.Int6Lower;

				case "6+":
				case "6強":
				case "６強":
					return JmaIntensity.Int6Upper;

				case "7":
				case "７":
					return JmaIntensity.Int7;

				case "-1":
				case "異常":
					return JmaIntensity.Error;
			}
			return JmaIntensity.Unknown;
		}
	}
}