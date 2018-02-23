using System;
using KyoshinMonitorLib.ApiResult.AppApi;

namespace KyoshinMonitorLib
{
	public class LinkedRealTimeData
	{
		public LinkedRealTimeData(Site site, ObservationPoint observationPoint, float? value)
		{
			Site = site ?? throw new ArgumentNullException(nameof(site));
			ObservationPoint = observationPoint;
			Value = value;
		}

		public Site Site { get; }
		public ObservationPoint ObservationPoint { get; }
		public float? Value { get; }
	}
}
