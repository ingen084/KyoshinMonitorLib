using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KyoshinMonitorLib.Images
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
		public static async Task<IEnumerable<ImageAnalysisResult>> ParseIntensityFromParameterAsync(this IEnumerable<ObservationPoint> points, DateTime datetime, bool isBehole = false)
			=> await points.Select(p => new ImageAnalysisResult(p)).ToArray().ParseIntensityFromParameterAsync(datetime, isBehole);

		/// <summary>
		/// 与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用し震度を取得します。
		/// <para>asyncなのはStream取得部分のみなので注意してください。</para>
		/// </summary>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="datetime">参照する日付</param>
		/// <param name="isBehole">地中の情報を取得するかどうか</param>
		/// <returns>震度情報が追加された観測点情報の配列</returns>
		public static async Task<IEnumerable<ImageAnalysisResult>> ParseIntensityFromParameterAsync(this IEnumerable<ImageAnalysisResult> points, DateTime datetime, bool isBehole = false)
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(UrlGeneratorV1.Generate(UrlType.RealTimeImg, datetime, RealTimeImgType.Shindo, isBehole));
				if (!response.IsSuccessStatusCode)
					throw new GetMonitorImageFailedException(response.StatusCode);
				using (var bitmap = new Bitmap(await response.Content.ReadAsStreamAsync()))
					return points.ParseIntensityFromImage(bitmap);
			}
		}

		/// <summary>
		/// 与えられた画像から観測点情報を使用し震度を取得します。
		/// </summary>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="bitmap">参照する画像</param>
		/// <returns>震度情報が追加された観測点情報の配列</returns>
		public static IEnumerable<ImageAnalysisResult> ParseIntensityFromImage(this IEnumerable<ImageAnalysisResult> points, Bitmap bitmap)
		{
			foreach (var point in points)
			{
				if (point.Point == null || point.IsSuspended)
				{
					point.AnalysisResult = null;
					continue;
				}

				try
				{
					var color = bitmap.GetPixel(point.Point.Value.X, point.Point.Value.Y);
					point.Color = color;
					point.AnalysisResult = ColorToIntensityConverter.Convert(color);
				}
				catch (Exception ex)
				{
					point.AnalysisResult = null;
					Debug.WriteLine("parseEx: " + ex.ToString());
				}
			}
			return points;
		}
	}
}
