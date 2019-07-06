using KyoshinMonitorLib.ApiResult.AppApi;
using System;

namespace KyoshinMonitorLib
{
	public struct LinkedObservationPoint
	{
		public LinkedObservationPoint(Site site, ObservationPoint point)
		{
			Site = site ?? throw new ArgumentNullException(nameof(site));
			Point = point;
		}

		/// <summary>
		/// 強震モニタAPI上の観測地点情報
		/// </summary>
		public Site Site { get; }
		/// <summary>
		/// 設定ファイルから読み込んだ観測地点情報
		/// </summary>
		public ObservationPoint Point { get; }
		public Location Location => Point?.Location ?? new Location(Site.Lat, Site.Lng);
	}
}
