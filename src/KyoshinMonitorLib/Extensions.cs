using System.Collections.Generic;
using System.Text.Json;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 拡張メソッドたち
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// 観測点情報をcsvに保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="points">書き込む観測点情報の配列</param>
		/// <param name="path">書き込むpbfファイルのパス</param>
		public static void SaveToCsv(this IEnumerable<ObservationPoint> points, string path)
			=> ObservationPoint.SaveToCsv(path, points);

		/// <summary>
		/// 観測点情報をmpk形式で保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むmpkファイルのパス</param>
		/// <param name="points">書き込む観測点情報の配列</param>
		/// <param name="usingLz4">lz4で圧縮させるかどうか(させる場合は拡張子を.mpk.lz4にすることをおすすめします)</param>
		public static void SaveToMpk(this IEnumerable<ObservationPoint> points, string path, bool usingLz4 = false)
			=> ObservationPoint.SaveToMpk(path, points, usingLz4);

		/// <summary>
		/// 観測点情報をJson形式で保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むJsonファイルのパス</param>
		/// <param name="points">書き込む観測点情報の配列</param>
		public static void SaveToJson(this IEnumerable<ObservationPoint> points, string path)
			=> ObservationPoint.SaveToJson(path, points);

		/// <summary>
		/// nullの際何故か空白の文字列になってしまうJson要素の解析
		/// </summary>
		public static bool? ToBoolFromStringableBool(this JsonElement element)
		{
			switch (element.ValueKind)
			{
				case JsonValueKind.True:
					return true;
				case JsonValueKind.False:
					return false;
				default:
					return null;
			}
		}
	}
}