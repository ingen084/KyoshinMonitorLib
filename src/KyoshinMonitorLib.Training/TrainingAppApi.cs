using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using KyoshinMonitorLib.ApiResult.AppApi;
using KyoshinMonitorLib.UrlGenerator;
using Newtonsoft.Json;

namespace KyoshinMonitorLib.Training
{
	public class TrainingAppApi : AppApi
	{
		public string BasePath { get; }
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="basePath">かならずディレクトリ区切り文字で終わらせること！</param>
		public TrainingAppApi(string basePath, ObservationPoint[] points) : base(points)
		{
			BasePath = basePath;
		}

		public override Task<ApiResult<SiteList>> GetSiteList(string baseSerialNo)
		{
			var path = AppApiUrlGenerator.Generate(baseSerialNo).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath);
			if (!File.Exists(path))
				return Task.FromResult(new ApiResult<SiteList>(HttpStatusCode.NotFound, null));
			return Task.FromResult(new ApiResult<SiteList>(HttpStatusCode.OK, JsonConvert.DeserializeObject<SiteList>(File.ReadAllText(path))));
		}

		public override Task<ApiResult<RealTimeData>> GetRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
		{ 
			var path = AppApiUrlGenerator.Generate(AppApiUrlType.RealTimeData, time, dataType, isBehore).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath);
			if (!File.Exists(path))
				return Task.FromResult(new ApiResult<RealTimeData>(HttpStatusCode.NotFound, null));
			return Task.FromResult(new ApiResult<RealTimeData>(HttpStatusCode.OK, JsonConvert.DeserializeObject<RealTimeData>(File.ReadAllText(path))));
		}

		public override Task<ApiResult<Hypo>> GetEewHypoInfo(DateTime time)
		{
			var path = AppApiUrlGenerator.Generate(AppApiUrlType.HypoInfoJson, time).Replace("http://kv.kmoni.bosai.go.jp/kyoshin_monitor/static/jsondata/", BasePath);
			if (!File.Exists(path))
				return Task.FromResult(new ApiResult<Hypo>(HttpStatusCode.NotFound, null));
			return Task.FromResult(new ApiResult<Hypo>(HttpStatusCode.OK, JsonConvert.DeserializeObject<Hypo>(File.ReadAllText(path))));
		}
	}
}
