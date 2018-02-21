using KyoshinMonitorLib.ApiResult.AppApi;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	public class AppApi : Api
	{
		private static Dictionary<string, SiteList> SiteListCache { get; set; } = new Dictionary<string, SiteList>();

		private ObservationPoint[] ObservationPoints { get; }
		public AppApi(ObservationPoint[] observationPoints = null)
		{
			ObservationPoints = observationPoints;
		}

		public Task<SiteList> GetSiteList(string baseSerialNo)
			=> GetJsonObject<SiteList>(AppApiUrlGenerator.Generate(baseSerialNo));

		public Task<RealTimeData> GetRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore)
			=> GetJsonObject<RealTimeData>(AppApiUrlGenerator.Generate(AppApiUrlType.RealTimeData, time, dataType, isBehore));

	}
}
