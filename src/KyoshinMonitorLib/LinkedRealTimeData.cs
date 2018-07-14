using KyoshinMonitorLib.ApiResult.AppApi;

namespace KyoshinMonitorLib
{
	public class LinkedRealTimeData
	{
		public LinkedRealTimeData((Site, ObservationPoint) point, float? value)
		{
			Point = point;
			Value = value;
		}

		public (Site site, ObservationPoint point) Point { get; }
		public float? Value { get; }
		public JmaIntensity Intensity => Value.ToJmaIntensity();
	}
}
