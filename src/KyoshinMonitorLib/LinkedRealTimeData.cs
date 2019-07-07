namespace KyoshinMonitorLib
{
	public struct LinkedRealTimeData
	{
		public LinkedRealTimeData(LinkedObservationPoint point, float? value)
		{
			ObservationPoint = point;
			Value = value;
		}

		public LinkedObservationPoint ObservationPoint { get; }
		public float? Value { get; }
		public JmaIntensity Intensity => Value.ToJmaIntensity();
	}
}
