using System.Drawing;

namespace KyoshinMonitorLib.Images
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
		/// 解析された値
		/// </summary>
		public float? AnalysisResult { get; set; }

		/// <summary>
		/// 解析に使用した色
		/// </summary>
		public Color Color { get; set; }

		/// <summary>
		/// ObservationPointを元にImageAnalysisResultを初期化します。
		/// </summary>
		/// <param name="point">元にするObservationPoint</param>
		public ImageAnalysisResult(ObservationPoint point)
		{
			ObservationPoint = point;
		}
	}
}
