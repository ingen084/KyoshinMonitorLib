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
		/// <summary>
		/// 観測点情報のキャッシュ
		/// <para>BaseSerialNoと観測点情報･idxに適合した情報のマッピング</para>
		/// </summary>
		private static Dictionary<string, (Site, ObservationPoint)[]> SiteListCache { get; set; } = new Dictionary<string, (Site, ObservationPoint)[]>();

		private ObservationPoint[] ObservationPoints { get; }
		public AppApi(ObservationPoint[] observationPoints = null)
		{
			ObservationPoints = observationPoints;
		}

		/// <summary>
		/// 観測点一覧を取得します。
		/// </summary>
		public Task<SiteList> GetSiteList(string baseSerialNo)
			=> GetJsonObject<SiteList>(AppApiUrlGenerator.Generate(baseSerialNo));

		/// <summary>
		/// リアルタイムなデータを取得します。
		/// </summary>
		public Task<RealTimeData> GetRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
			=> GetJsonObject<RealTimeData>(AppApiUrlGenerator.Generate(AppApiUrlType.RealTimeData, time, dataType, isBehore));

		/// <summary>
		/// 観測点情報と結合済みのリアルタイムなデータを取得します
		/// </summary>
		//public async Task<LinkedRealTimeData[]> GetLinkedRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
		//{
		//	var data = await GetRealTimeData(time, dataType, isBehore);

		//	//存在しない場合作成
		//	if (!SiteListCache.TryGetValue(data.BaseSerialNo, out var pair))
		//	{
		//		var siteList = await GetSiteList(data.BaseSerialNo);

		//	}
		//}
	}
}
