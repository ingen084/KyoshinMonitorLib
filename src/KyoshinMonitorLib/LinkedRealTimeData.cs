namespace KyoshinMonitorLib
{
	/// <summary>
	/// 結合済みの観測情報
	/// </summary>
	public struct LinkedRealtimeData
	{
		/// <summary>
		/// 結合済みの観測情報の初期化
		/// </summary>
		public LinkedRealtimeData(LinkedObservationPoint point, float? value)
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
	}
}
