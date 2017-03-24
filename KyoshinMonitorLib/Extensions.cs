using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	public static class Extensions
	{
		/// <summary>
		/// 与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用し震度を取得します。
		/// <para>asyncなのはStream取得部分のみなので注意してください。</para>
		/// </summary>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="datetime">参照する日付</param>
		/// <param name="isBehole">地中の情報を取得するかどうか</param>
		/// <returns>震度情報が追加された観測点情報の配列</returns>
		public static async Task<ImageAnalysisResult[]> ParseIntensityFromParameterAsync(this IEnumerable<ObservationPoint> points, DateTime datetime, bool isBehole = false)
		{
			using (var client = new HttpClient())
			using (var bitmap = new Bitmap(await client.GetStreamAsync(UrlGenerator.Generate(UrlType.RealTimeImg, datetime, RealTimeImgType.Shindo, isBehole))))
				return points.ParseIntensityFromImage(bitmap);
		}

		/// <summary>
		/// 与えられた画像から観測点情報を使用し震度を取得します。
		/// </summary>
		/// <param name="obsPoints">使用する観測点情報の配列</param>
		/// <param name="bitmap">参照する画像</param>
		/// <returns>震度情報が追加された観測点情報の配列</returns>
		public static ImageAnalysisResult[] ParseIntensityFromImage(this IEnumerable<ObservationPoint> obsPoints, Bitmap bitmap)
		{
			var points = obsPoints.Select(p => new ImageAnalysisResult(p)).ToArray();
			foreach (var point in points)
			{
				if (point.Point == null || point.IsSuspended)
					continue;

				try
				{
					var color = bitmap.GetPixel(point.Point.Value.X, point.Point.Value.Y);
					point.AnalysisResult = ColorToIntensityConverter.Convert(color);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("parseEx: " + ex.ToString());
				}
			}
			return points;
		}

		/// <summary>
		/// 観測点情報をcsvに保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="points">書き込む観測点情報の配列</param>
		/// <param name="path">書き込むpbfファイルのパス</param>
		public static void SaveToCsv(this IEnumerable<ObservationPoint> points, string path)
			=> ObservationPoint.SaveToCsv(path, points);
		/// <summary>
		/// 観測点情報をpbfに保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="points">書き込む観測点情報の配列</param>
		/// <param name="path">書き込むcsvファイルのパス</param>
		public static void SaveToPbf(this IEnumerable<ObservationPoint> points, string path)
			=> ObservationPoint.SaveToPbf(path, points);

		/// <summary>
		/// 観測点情報をmpk形式で保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むmpkファイルのパス</param>
		/// <param name="points">書き込む観測点情報の配列</param>
		/// <param name="isUsingLz4">lz4で圧縮させるかどうか(させる場合は拡張子を.mpk.lz4にすることをおすすめします)</param>
		public static void SaveToMpk(this IEnumerable<ObservationPoint> points, string path, bool isUsingLz4 = false)
			=> ObservationPoint.SaveToMpk(path, points, isUsingLz4);

		/// <summary>
		/// 観測点情報をJson形式で保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むJsonファイルのパス</param>
		/// <param name="points">書き込む観測点情報の配列</param>
		public static void SaveToJson(this IEnumerable<ObservationPoint> points, string path)
			=> ObservationPoint.SaveToJson(path, points);
	}
}
