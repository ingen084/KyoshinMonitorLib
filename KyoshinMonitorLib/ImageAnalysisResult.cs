namespace KyoshinMonitorLib
{
	/// <summary>
	/// 画像解析結果
	/// </summary>
	public class ImageAnalysisResult : ObservationPoint
	{
		/// <summary>
		/// 解析された値
		/// </summary>
		public float? AnalysisResult { get; set; }

		/// <summary>
		/// ObservationPointを元にImageAnalysisResultを初期化します。
		/// </summary>
		/// <param name="point">元にするObservationPoint</param>
		public ImageAnalysisResult(ObservationPoint point) : base(point)
		{
		}
	}
}
