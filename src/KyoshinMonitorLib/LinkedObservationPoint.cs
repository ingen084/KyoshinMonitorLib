using KyoshinMonitorLib.ApiResult.AppApi;
using System;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 結合済みの観測点情報
	/// </summary>
	public struct LinkedObservationPoint
	{
		/// <summary>
		/// 結合済みの観測点情報の初期化
		/// </summary>
		public LinkedObservationPoint(Site site, ObservationPoint point)
		{
			Site = site;
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
		/// <summary>
		/// 自動的に精度の高い方の緯度経度座標を取得する
		/// </summary>
		public Location Location => Point?.Location ?? new Location(Site.Lat, Site.Lng);
	}
}
