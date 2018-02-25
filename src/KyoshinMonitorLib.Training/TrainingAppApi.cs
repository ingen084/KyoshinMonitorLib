using System;
using System.IO;
using System.Threading.Tasks;
using KyoshinMonitorLib.ApiResult.AppApi;
using KyoshinMonitorLib.UrlGenerator;
using Utf8Json;

namespace KyoshinMonitorLib.Training
{
	public class AppApiTraining : AppApi
	{
		public string BasePath { get; }
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="basePath">かならずディレクトリ区切り文字で終わらせること！</param>
		public AppApiTraining(string basePath, ObservationPoint[] points) : base(points)
		{
			BasePath = basePath;
		}

		public override Task<SiteList> GetSiteList(string baseSerialNo)
			=> Task.FromResult(JsonSerializer.Deserialize<SiteList>(
				File.ReadAllText(AppApiUrlGenerator.Generate(baseSerialNo).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath))));

		public override Task<RealTimeData> GetRealTimeData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
			=> Task.FromResult(JsonSerializer.Deserialize<RealTimeData>(
				File.ReadAllText(AppApiUrlGenerator.Generate(AppApiUrlType.RealTimeData, time, dataType, isBehore).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath))));

		public override Task<Hypo> GetHypoInfo(DateTime time)
			=> Task.FromResult(JsonSerializer.Deserialize<Hypo>(
				File.ReadAllText(AppApiUrlGenerator.Generate(AppApiUrlType.HypoInfoJson, time).Replace("http://kv.kmoni.bosai.go.jp/kyoshin_monitor/static/jsondata/", BasePath))));
	}
}
