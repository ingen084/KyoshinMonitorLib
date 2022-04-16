using KyoshinMonitorLib.ApiResult.AppApi;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// スマホアプリAPI
	/// </summary>
	public class AppApi : Api
	{
		/// <summary>
		/// 観測点情報のキャッシュ
		/// <para>BaseSerialNoと観測点情報･idxに適合した情報のマッピング</para>
		/// </summary>
		private static IDictionary<string, LinkedObservationPoint[]> SiteListCache { get; set; } = new ConcurrentDictionary<string, LinkedObservationPoint[]>();

		private ObservationPoint[]? ObservationPoints { get; }
		/// <summary>
		/// スマホアプリAPIを初期化します。
		/// </summary>
		/// <param name="observationPoints">結合時に使用する観測点情報</param>
		public AppApi(ObservationPoint[]? observationPoints = null)
		{
			ObservationPoints = observationPoints;
		}

		/// <summary>
		/// 観測点一覧を取得します。
		/// </summary>
		public virtual Task<ApiResult<SiteList?>> GetSiteList(string baseSerialNo)
			=> GetJsonObject(AppApiUrlGenerator.Generate(baseSerialNo), ApiResult.ApiResultJsonContext.Default.SiteList);

		/// <summary>
		/// リアルタイムなデータを取得します。
		/// </summary>
		public virtual Task<ApiResult<RealtimeData?>> GetRealtimeData(DateTime time, RealtimeDataType dataType, bool isBehore = false)
			=> GetJsonObject(AppApiUrlGenerator.Generate(AppApiUrlType.RealtimeData, time, dataType, isBehore), ApiResult.ApiResultJsonContext.Default.RealtimeData);

		/// <summary>
		/// 観測点情報と結合済みのリアルタイムなデータを取得します。
		/// </summary>
		public async Task<ApiResult<LinkedRealtimeData[]?>> GetLinkedRealtimeData(DateTime time, RealtimeDataType dataType, bool isBehore = false)
		{
			var dataResult = await GetRealtimeData(time, dataType, isBehore);
			if (dataResult.Data is not RealtimeData data ||
				data.BaseSerialNo is null ||
				data.Items is null)
				return new(dataResult.StatusCode, null);


			var pair = await GetOrLinkObservationPoint(data.BaseSerialNo);
			var result = new List<LinkedRealtimeData>();
			for (var i = 0; i < data.Items.Length; i++)
			{
				if (pair.Length <= i)
					throw new KyoshinMonitorException("リアルタイムデータの結合に失敗しました。 SiteListの観測点が少なすぎます。");
				result.Add(new(pair[i], data.Items[i]));
			}
			return new(dataResult.StatusCode, result.ToArray());
		}
		/// <summary>
		/// 観測点情報を結合もしくはキャッシュから取得します。
		/// </summary>
		/// <param name="serialNo">観測点一覧の番号</param>
		/// <returns>結合された観測点情報 親となるリストが存在しない場合null</returns>
		public async Task<LinkedObservationPoint[]> GetOrLinkObservationPoint(string serialNo)
		{
			if (SiteListCache.TryGetValue(serialNo, out var pair))
				return pair;

			var siteListResult = await GetSiteList(serialNo);
			if (siteListResult.Data is null
			 || siteListResult.Data.Sites is null)
				throw new KyoshinMonitorException("SiteListの取得に失敗しました。");

			var pairList = new List<LinkedObservationPoint>();

			for (var i = 0; i < siteListResult.Data.Sites.Length; i++)
			{
				var site = siteListResult.Data.Sites[i];
				if (i < site.Siteidx)
				{
					pairList.Add(new(site, null));
					continue;
				}
				if (i != site.Siteidx)
					throw new KyoshinMonitorException("リアルタイムデータの結合に失敗しました。 idxが一致しません。");

				ObservationPoint? point = null;
				if (ObservationPoints != null)
					for (int j = 0; j < ObservationPoints.Length; j++)
					{
						var p = ObservationPoints[j];
						if (/*p.IsSuspended || */p.Location == null) continue;
						if (CheckNearLocation(p.Location, site))
						{
							point = p;
							break;
						}
						if (p.OldLocation == null) continue;
						if (CheckNearLocation(p.OldLocation, site))
						{
							point = p;
							break;
						}
					}

				pairList.Add(new(site, point));
			}
			pair = pairList.ToArray();
			SiteListCache.Add(serialNo, pair);
			return pair;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool CheckNearLocation(Location l, Site s)
			=> s.Lat is float lat && s.Lng is float lng &&
				Math.Abs(Math.Floor(l.Latitude * 1000) / 1000 - lat) <= 0.01 && Math.Abs(Math.Floor(l.Longitude * 1000) / 1000 - lng) <= 0.01;
	}
}
