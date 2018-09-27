using System;
using System.IO;
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

		public override Task<SiteList> GetSiteList(string baseSerialNo)
			=> Task.FromResult(JsonConvert.DeserializeObject<SiteList>(
				File.ReadAllText(AppApiUrlGenerator.Generate(baseSerialNo).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath))));

		public override Task<RealTimeData> GetRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
			=> Task.FromResult(JsonConvert.DeserializeObject<RealTimeData>(
				File.ReadAllText(AppApiUrlGenerator.Generate(AppApiUrlType.RealTimeData, time, dataType, isBehore).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath))));

		public override Task<Hypo> GetEewHypoInfo(DateTime time)
			=> Task.FromResult(JsonConvert.DeserializeObject<Hypo>(
				File.ReadAllText(AppApiUrlGenerator.Generate(AppApiUrlType.HypoInfoJson, time).Replace("http://kv.kmoni.bosai.go.jp/kyoshin_monitor/static/jsondata/", BasePath))));
	}
}
