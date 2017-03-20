namespace KyoshinMonitorLib
{
	public class ImageAnalysisResult : ObservationPoint
	{
		/// <summary>
		/// 解析された値
		/// </summary>
		public float? AnalysisResult { get; set; }

		public ImageAnalysisResult(ObservationPoint point) : base(point)
		{
		}
	}
}
