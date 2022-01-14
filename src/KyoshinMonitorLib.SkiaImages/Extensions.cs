using KyoshinMonitorLib.UrlGenerator;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace KyoshinMonitorLib.SkiaImages
{
	/// <summary>
	/// 拡張メソッドたち
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// 与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用しスケールを取得します。
		/// <para>asyncなのはStream取得部分のみなので注意してください。</para>
		/// </summary>
		/// <param name="webApi">WebApiインスタンス</param>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="datetime">参照する日付</param>
		/// <param name="dataType">取得する情報の種類</param>
		/// <param name="isBehole">地中の情報を取得するかどうか</param>
		/// <returns>震度情報が追加された観測点情報の配列 取得に失敗した場合null</returns>
		public static async Task<ApiResult<IEnumerable<ImageAnalysisResult>?>> ParseScaleFromParameterAsync(this WebApi webApi, IEnumerable<ObservationPoint> points, DateTime datetime, RealtimeDataType dataType = RealtimeDataType.Shindo, bool isBehole = false)
			=> await webApi.ParseScaleFromParameterAsync(points.Select(p => new ImageAnalysisResult(p)).ToArray(), datetime, dataType, isBehole);

		/// <summary>
		/// 与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用しスケールを取得します。
		/// <para>asyncなのはStream取得部分のみなので注意してください。</para>
		/// </summary>
		/// <param name="webApi">WebApiインスタンス</param>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="datetime">参照する日付</param>
		/// <param name="dataType">取得する情報の種類</param>
		/// <param name="isBehole">地中の情報を取得するかどうか</param>
		/// <returns>震度情報が追加された観測点情報の配列 取得に失敗した場合null</returns>
		public static async Task<ApiResult<IEnumerable<ImageAnalysisResult>?>> ParseScaleFromParameterAsync(this WebApi webApi, IEnumerable<ImageAnalysisResult> points, DateTime datetime, RealtimeDataType dataType = RealtimeDataType.Shindo, bool isBehole = false)
		{
			var imageResult = await webApi.GetRealtimeImageData(datetime, dataType, isBehole);
			if (imageResult.Data is not byte[] data)
				return new(imageResult.StatusCode, null);

			using var bitmap = SKBitmap.Decode(data);
			return new(imageResult.StatusCode, points.ParseScaleFromImage(bitmap));
		}

		/// <summary>
		/// 与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用しスケールを取得します。
		/// <para>asyncなのはStream取得部分のみなので注意してください。</para>
		/// </summary>
		/// <param name="webApi">WebApiインスタンス</param>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="datetime">参照する日付</param>
		/// <param name="dataType">取得する情報の種類</param>
		/// <param name="isBehole">地中の情報を取得するかどうか</param>
		/// <returns>震度情報が追加された観測点情報の配列 取得に失敗した場合null</returns>
		public static async Task<ApiResult<IEnumerable<ImageAnalysisResult>?>> ParseScaleFromParameterAsync(this LpgmWebApi webApi, IEnumerable<ObservationPoint> points, DateTime datetime, RealtimeDataType dataType = RealtimeDataType.Shindo, bool isBehole = false)
			=> await webApi.ParseScaleFromParameterAsync(points.Select(p => new ImageAnalysisResult(p)).ToArray(), datetime, dataType, isBehole);

		/// <summary>
		/// 与えられた情報から強震モニタの画像を取得し、そこから観測点情報を使用しスケールを取得します。
		/// <para>asyncなのはStream取得部分のみなので注意してください。</para>
		/// </summary>
		/// <param name="webApi">WebApiインスタンス</param>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="datetime">参照する日付</param>
		/// <param name="dataType">取得する情報の種類</param>
		/// <param name="isBehole">地中の情報を取得するかどうか</param>
		/// <returns>震度情報が追加された観測点情報の配列 取得に失敗した場合null</returns>
		public static async Task<ApiResult<IEnumerable<ImageAnalysisResult>?>> ParseScaleFromParameterAsync(this LpgmWebApi webApi, IEnumerable<ImageAnalysisResult> points, DateTime datetime, RealtimeDataType dataType = RealtimeDataType.Shindo, bool isBehole = false)
		{
			var imageResult = await webApi.GetRealtimeImageData(datetime, dataType, isBehole);
			if (imageResult.Data is not byte[] data)
				return new(imageResult.StatusCode, null);

			using var bitmap = SKBitmap.Decode(data);
			return new(imageResult.StatusCode, points.ParseScaleFromImage(bitmap));
		}

		/// <summary>
		/// 与えられた画像から観測点情報を使用しスケールを取得します。
		/// </summary>
		/// <param name="points">使用する観測点情報の配列</param>
		/// <param name="bitmap">参照する画像</param>
		/// <returns>震度情報が追加された観測点情報の配列</returns>
		public static IEnumerable<ImageAnalysisResult> ParseScaleFromImage(this IEnumerable<ImageAnalysisResult> points, SKBitmap bitmap)
		{

			//var pixelData = bitmap.GetPixelSpan();
			foreach (var point in points)
			{
				if (point.ObservationPoint.Point == null || point.ObservationPoint.IsSuspended)
				{
					point.AnalysisResult = null;
					continue;
				}

				try
				{
					var color = bitmap.GetPixel(point.ObservationPoint.Point.Value.X, point.ObservationPoint.Point.Value.Y);
					// [(bitmap.Width * point.ObservationPoint.Point.Value.Y + point.ObservationPoint.Point.Value.X) * 4];
					point.Color = color;
					if (color.Alpha != 255)
					{
						point.AnalysisResult = null;
						continue;
					}

					point.AnalysisResult = ColorConverter.ConvertToScaleAtPolynomialInterpolation(color);
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
