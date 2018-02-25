using KyoshinMonitorLib.ApiResult.AppApi;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Collections.Concurrent;
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
		private static IDictionary<string, (Site, ObservationPoint)[]> SiteListCache { get; set; } = new ConcurrentDictionary<string, (Site, ObservationPoint)[]>();

		private ObservationPoint[] ObservationPoints { get; }
		public AppApi(ObservationPoint[] observationPoints = null)
		{
			ObservationPoints = observationPoints;
		}

		/// <summary>
		/// 観測点一覧を取得します。
		/// </summary>
		public virtual Task<SiteList> GetSiteList(string baseSerialNo)
			=> GetJsonObject<SiteList>(AppApiUrlGenerator.Generate(baseSerialNo));

		/// <summary>
		/// リアルタイムなデータを取得します。
		/// </summary>
		public virtual Task<RealTimeData> GetRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
			=> GetJsonObject<RealTimeData>(AppApiUrlGenerator.Generate(AppApiUrlType.RealTimeData, time, dataType, isBehore));

		/// <summary>
		/// 観測点情報と結合済みのリアルタイムなデータを取得します
		/// </summary>
		public async Task<LinkedRealTimeData[]> GetLinkedRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
		{
			var data = await GetRealTimeData(time, dataType, isBehore);

			//存在しない場合作成
			if (!SiteListCache.TryGetValue(data.BaseSerialNo, out var pair))
			{
				var pairList = new List<(Site, ObservationPoint)>();
				var siteList = await GetSiteList(data.BaseSerialNo);
				var count = siteList.Sites.Min(s => s.Siteidx);
				foreach (var site in siteList.Sites.OrderBy(s => s.Siteidx))
				{
					if (count != site.Siteidx)
						throw new KyoshinMonitorException(null, "リアルタイムデータの結合に失敗しました。 APIから送られてくる値が不正です。");
					count++;

					//世界座標系で検索してだめだったら日本座標系で検索
					var point = ObservationPoints.Where(p => !p.IsSuspended).FirstOrDefault(p => Math.Abs(p.Location.Latitude - site.Lat) < 0.001 && Math.Abs(p.Location.Longitude - site.Lng) < 0.001);
					if (point == null)
						point = ObservationPoints.Where(p => !p.IsSuspended).FirstOrDefault(p => Math.Abs(p.OldLocation.Latitude - site.Lat) < 0.001 && Math.Abs(p.OldLocation.Longitude - site.Lng) < 0.001);

					pairList.Add((site, point));
				}
				pair = pairList.ToArray();
				SiteListCache.Add(data.BaseSerialNo, pair);
			}

			var result = new List<LinkedRealTimeData>();
			for (var i = 0; i < data.Items.Length; i++)
			{
				if (pair.Length <= i)
					throw new KyoshinMonitorException(null, "リアルタイムデータの結合に失敗しました。 SiteListの観測点が少なすぎます。");
				result.Add(new LinkedRealTimeData(pair[i], data.Items[i]));
			}
			return result.ToArray();
		}

		/// <summary>
		/// 緊急地震速報の情報を取得します。
		/// </summary>
		public virtual Task<Hypo> GetHypoInfo(DateTime time)
			=> GetJsonObject<Hypo>(AppApiUrlGenerator.Generate(AppApiUrlType.HypoInfoJson, time));
	}
}
