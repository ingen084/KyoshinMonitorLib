using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using KyoshinMonitorLib.ApiResult.AppApi;
using KyoshinMonitorLib.UrlGenerator;

namespace KyoshinMonitorLib.Training
{
	/// <summary>
	/// トレーニング用API
	/// </summary>
	public class TrainingAppApi : AppApi
	{
		/// <summary>
		/// ベースディレクトリ
		/// </summary>
		public string BasePath { get; }
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="basePath">かならずディレクトリ区切り文字で終わらせること！</param>
		/// <param name="points">観測点一覧</param>
		public TrainingAppApi(string basePath, ObservationPoint[] points) : base(points)
		{
			BasePath = basePath;
		}

		/// <summary>
		/// 観測点一覧
		/// </summary>
		public override async Task<ApiResult<SiteList?>> GetSiteList(string baseSerialNo)
		{
			var path = AppApiUrlGenerator.Generate(baseSerialNo).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath);
			if (!File.Exists(path))
				return new(HttpStatusCode.NotFound, null);
			return new(HttpStatusCode.OK, JsonSerializer.Deserialize<SiteList>(await File.ReadAllTextAsync(path)));
		}

		/// <summary>
		/// 観測データ
		/// </summary>
		public override async Task<ApiResult<RealtimeData?>> GetRealtimeData(DateTime time, RealtimeDataType dataType, bool isBehore = false)
		{ 
			var path = AppApiUrlGenerator.Generate(AppApiUrlType.RealtimeData, time, dataType, isBehore).Replace("http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/", BasePath);
			if (!File.Exists(path))
				return new(HttpStatusCode.NotFound, null);
			return new(HttpStatusCode.OK, JsonSerializer.Deserialize<RealtimeData>(await File.ReadAllTextAsync(path)));
		}
	}
}
