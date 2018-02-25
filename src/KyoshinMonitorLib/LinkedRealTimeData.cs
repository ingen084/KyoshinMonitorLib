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

		public (Site, ObservationPoint) Point { get; }
		public float? Value { get; }
	}
}
