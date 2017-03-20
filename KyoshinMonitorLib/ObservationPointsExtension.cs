using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	public static class ObservationPointsExtension
	{
		/// <summary>
		/// 与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用し震度を解析します。
		/// <para>asyncなのはStream取得部分のみなので注意してください。</para>
		/// </summary>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="datetime">参照する日付</param>
		/// <param name="isBehole">地中の情報を取得するかどうか</param>
		/// <returns>震度情報が追加された観測点情報の配列</returns>
		public static async Task<ImageAnalysisResult[]> CalculateIntensityFromParameterAsync(this IEnumerable<ObservationPoint> points, DateTime datetime, bool isBehole = false)
		{
			using (var client = new HttpClient())
			using (var bitmap = new Bitmap(await client.GetStreamAsync(UrlGenerator.Generate(UrlType.RealTimeImg, datetime, RealTimeImgType.Shindo, isBehole))))
				return points.CalculateIntensityFromImage(bitmap);
		}

		/// <summary>
		/// 与えられた画像から観測点情報を使用し震度を解析します。
		/// </summary>
		/// <param name="obsPoints">使用する観測点情報の配列</param>
		/// <param name="bitmap">参照する画像</param>
		/// <returns>震度情報が追加された観測点情報の配列</returns>
		public static ImageAnalysisResult[] CalculateIntensityFromImage(this IEnumerable<ObservationPoint> obsPoints, Bitmap bitmap)
		{
			var points = obsPoints.Select(p => new ImageAnalysisResult(p)).ToArray();
			foreach (var point in points)
			{
				if (point.Point == null || point.IsSuspended)
					continue;

				try
				{
					var color = bitmap.GetPixel(point.Point.Value.X,point.Point.Value.Y);
					point.AnalysisResult = ColorToIntensityConverter.Convert(color);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("parseEx: " + ex.ToString());
				}
			}
			return points;
		}
	}
}
