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
		private static IDictionary<string, LinkedObservationPoint[]> SiteListCache { get; set; } = new ConcurrentDictionary<string, LinkedObservationPoint[]>();

		private ObservationPoint[] ObservationPoints { get; }
		public AppApi(ObservationPoint[] observationPoints = null)
		{
			ObservationPoints = observationPoints;
		}

		/// <summary>
		/// 観測点一覧を取得します。
		/// </summary>
		public virtual Task<ApiResult<SiteList>> GetSiteList(string baseSerialNo)
			=> GetJsonObject<SiteList>(AppApiUrlGenerator.Generate(baseSerialNo));

		/// <summary>
		/// リアルタイムなデータを取得します。
		/// </summary>
		public virtual Task<ApiResult<RealTimeData>> GetRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
			=> GetJsonObject<RealTimeData>(AppApiUrlGenerator.Generate(AppApiUrlType.RealTimeData, time, dataType, isBehore));

		/// <summary>
		/// 観測点情報と結合済みのリアルタイムなデータを取得します
		/// </summary>
		public async Task<ApiResult<LinkedRealTimeData[]>> GetLinkedRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
		{
			var dataResult = await GetRealTimeData(time, dataType, isBehore);
			if (dataResult.Data == null)
				return new ApiResult<LinkedRealTimeData[]>(dataResult.StatusCode, null);
			var data = dataResult.Data;

			var pair = await GetOrLinkObservationPoint(data.BaseSerialNo);
			var result = new List<LinkedRealTimeData>();
			for (var i = 0; i < data.Items.Length; i++)
			{
				if (pair.Length <= i)
					throw new KyoshinMonitorException("リアルタイムデータの結合に失敗しました。 SiteListの観測点が少なすぎます。");
				result.Add(new LinkedRealTimeData(pair[i], data.Items[i]));
			}
			return new ApiResult<LinkedRealTimeData[]>(dataResult.StatusCode, result.ToArray());
		}
		public async Task<LinkedObservationPoint[]> GetOrLinkObservationPoint(string serialNo)
		{
			if (SiteListCache.TryGetValue(serialNo, out var pair))
				return pair;

			var pairList = new List<LinkedObservationPoint>();
			var siteListResult = await GetSiteList(serialNo);
			if (siteListResult.Data == null)
				throw new KyoshinMonitorException("SiteListの取得に失敗しました。");
			var siteList = siteListResult.Data;
			var count = 0;
			foreach (var site in siteList.Sites/*.OrderBy(s => s.Siteidx)*/)
			{
				if (count < site.Siteidx)
				{
					pairList.Add(new LinkedObservationPoint(site, null));
					count++;
					continue;
				}
				if (count != site.Siteidx)
					throw new KyoshinMonitorException("リアルタイムデータの結合に失敗しました。 idxが一致しません。");
				count++;

				//世界座標系で検索してだめだったら日本座標系で検索
				var point = ObservationPoints.Where(p => !p.IsSuspended && p.Location != null).FirstOrDefault(p => Math.Abs(Math.Floor(p.Location.Latitude * 100) / 100 - site.Lat) <= 0.01 && Math.Abs(Math.Floor(p.Location.Longitude * 100) / 100 - site.Lng) <= 0.01);
				if (point == null)
					point = ObservationPoints.Where(p => !p.IsSuspended && p.OldLocation != null).FirstOrDefault(p => Math.Abs(Math.Floor(p.OldLocation.Latitude * 100) / 100 - site.Lat) <= 0.01 && Math.Abs(Math.Floor(p.OldLocation.Longitude * 100) / 100 - site.Lng) <= 0.01);

				pairList.Add(new LinkedObservationPoint(site, point));
			}
			pair = pairList.ToArray();
			SiteListCache.Add(serialNo, pair);
			return pair;
		}

		/// <summary>
		/// 緊急地震速報の情報を取得します。
		/// </summary>
		public virtual Task<ApiResult<Hypo>> GetEewHypoInfo(DateTime time)
			=> GetJsonObject<Hypo>(AppApiUrlGenerator.Generate(AppApiUrlType.HypoInfoJson, time));

		/// <summary>
		/// 緊急地震速報から算出された揺れの広がりを取得します。
		/// </summary>
		public virtual Task<ApiResult<PSWave>> GetPSWave(DateTime time)
			=> GetJsonObject<PSWave>(AppApiUrlGenerator.Generate(AppApiUrlType.PSWaveJson, time));

		/// <summary>
		/// 緊急地震速報から算出された予想震度のメッシュ情報を取得します。
		/// </summary>
		public virtual Task<ApiResult<EstShindo>> GetEstShindo(DateTime time)
			=> GetJsonObject<EstShindo>(AppApiUrlGenerator.Generate(AppApiUrlType.EstShindoJson, time));

		/// <summary>
		/// メッシュ一覧を取得します。
		/// 非常に時間がかかるため、キャッシュしておくことを推奨します。
		/// </summary>
		public virtual async Task<ApiResult<Mesh[]>> GetMeshes()
		{
			var mechesResult = await GetJsonObject<MeshList>(AppApiUrlGenerator.Meches);
			if (mechesResult.Data == null)
				return new ApiResult<Mesh[]>(mechesResult.StatusCode, null);
			var meches = mechesResult.Data;
			var result = new List<Mesh>();

			await Task.Run(() =>
			{
				var length = meches.Items.GetLength(0);
				for (var i = 0; i < length; i++)
					result.Add(new Mesh(meches.Items[i][0] as string, (double)meches.Items[i][1], (double)meches.Items[i][2]));
			});

			return new ApiResult<Mesh[]>(mechesResult.StatusCode, result.ToArray());
		}
	}
}
