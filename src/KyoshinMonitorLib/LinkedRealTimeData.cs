namespace KyoshinMonitorLib
{
	/// <summary>
	/// 結合済みの観測情報
	/// </summary>
	public struct LinkedRealTimeData
	{
		/// <summary>
		/// 結合済みの観測情報の初期化
		/// </summary>
		public LinkedRealTimeData(LinkedObservationPoint point, float? value)
		{
			ObservationPoint = point;
			Value = value;
		}

		/// <summary>
		/// 結合済みの観測点情報
		/// </summary>
		public LinkedObservationPoint ObservationPoint { get; }
		/// <summary>
		/// 観測値
		/// </summary>
		public float? Value { get; }
		/// <summary>
		/// 気象庁震度階級に変換した値
		/// </summary>
		public JmaIntensity Intensity => Value.ToJmaIntensity();
	}
}
