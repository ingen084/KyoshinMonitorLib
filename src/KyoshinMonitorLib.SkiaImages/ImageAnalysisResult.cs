using SkiaSharp;

namespace KyoshinMonitorLib.SkiaImages
{
	/// <summary>
	/// 画像解析結果
	/// </summary>
	public class ImageAnalysisResult
	{
		/// <summary>
		/// 観測点情報
		/// </summary>
		public ObservationPoint ObservationPoint { get; }

		/// <summary>
		/// 解析されたスケール
		/// </summary>
		public double? AnalysisResult { get; set; }

		/// <summary>
		/// 解析に使用した色
		/// </summary>
		public SKColor Color { get; set; }

		/// <summary>
		/// ObservationPointを元にImageAnalysisResultを初期化します。
		/// </summary>
		/// <param name="point">元にするObservationPoint</param>
		public ImageAnalysisResult(ObservationPoint point)
		{
			ObservationPoint = point;
		}

		/// <summary>
		/// 結果を震度として返します
		/// </summary>
		/// <returns></returns>
		public double? GetResultToIntensity()
		{
			if (AnalysisResult is not double result)
				return null;
			return ColorConverter.ConvertToIntensityFromScale(result);
		}
		/// <summary>
		/// 結果を最大加速度(PGA)として返します
		/// </summary>
		/// <returns></returns>
		public double? GetResultToPga()
		{
			if (AnalysisResult is not double result)
				return null;
			return ColorConverter.ConvertToPgaFromScale(result);
		}
		/// <summary>
		/// 結果を最大速度(PGV)として返します
		/// </summary>
		/// <returns></returns>
		public double? GetResultToPgv()
		{
			if (AnalysisResult is not double result)
				return null;
			return ColorConverter.ConvertToPgvFromScale(result);
		}
		/// <summary>
		/// 結果を最大変位(PGD)として返します
		/// </summary>
		/// <returns></returns>
		public double? GetResultToPgd()
		{
			if (AnalysisResult is not double result)
				return null;
			return ColorConverter.ConvertToPgdFromScale(result);
		}
	}
}
