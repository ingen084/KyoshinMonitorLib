using KyoshinMonitorLib;
using KyoshinMonitorLib.ApiResult.WebApi;
using KyoshinMonitorLib.Images;
using KyoshinMonitorLib.Timers;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Tests
{
	internal class Program
	{
		public static async Task Main(string[] args)
		{
			var points = ObservationPoint.LoadFromMpk("ShindoObsPoints.mpk.lz4", true);
			using (var appApi = new AppApi(points))
			using (var webApi = new WebApi())
			{
				// タイマーのインスタンスを作成
				var timer = new SecondBasedTimer()
				{
					Offset = TimeSpan.FromSeconds(1.1),//1.1
				};
				// 適当にイベント設定
				timer.Elapsed += async time =>
				{
					Console.WriteLine($"\nsys: {DateTime.Now.ToString("HH:mm:ss.fff")} ntp:{time.ToString("HH:mm:ss.fff")}");

					try
					{
						// APIから結果を計算 (良い子のみんなはawaitを使おうね！)
						var result = await appApi.GetLinkedRealTimeData(time, RealTimeDataType.Shindo, false);
						if (result.Data != null)
						{
							var data = result.Data;
							// 現在の最大震度
							Console.WriteLine($"*API* 最大震度: 生:{data.Max(r => r.Value)} jma:{data.Max(r => r.Value).ToJmaIntensity().ToLongString()} {data.Count(r => r.ObservationPoint.Point != null)},{data.Count(r => r.ObservationPoint.Point == null)}");
							var p = data.First(r => r.ObservationPoint.Point == null);
							Console.WriteLine(p.ObservationPoint.Site.Lat + "/" + p.ObservationPoint.Site.Lng);
							// 最大震度観測点(の1つ)
							//var maxPoint = result.OrderByDescending(r => r.Value).First();
							//Console.WriteLine($"最大観測点 {maxPoint.Point.site.Prefefecture.GetLongName()} {maxPoint.Point.point.Name} 震度:{maxPoint.Value}({maxPoint.Value.ToJmaIntensity().ToLongString()})");
						}
						else
							Console.WriteLine($"*API* 取得失敗 " + result.StatusCode);
					}
					catch (KyoshinMonitorException ex)
					{
						Console.WriteLine($"API エラー発生 {ex}");
					}
					try
					{
						// WebAPIから結果を計算 (良い子のみんなはawaitを使おうね！)
						var result = await webApi.ParseIntensityFromParameterAsync(points, time);
						if (result.Data != null)
						{
							var data = result.Data;
							// 現在の最大震度
							Console.WriteLine($"*WEB* 最大震度: 生:{data.Max(r => r.AnalysisResult)} jma:{data.Max(r => r.AnalysisResult).ToJmaIntensity().ToLongString()}");
						}
						else if (result.StatusCode == HttpStatusCode.NotFound)
						{
							timer.Offset += TimeSpan.FromMilliseconds(100);
							Console.WriteLine($"404のためオフセット調整 to:{timer.Offset.TotalSeconds}s");
						}
						else
							Console.WriteLine($"*WEB* 取得失敗 " + result.StatusCode);
					}
					catch (Exception ex)
					{
						Console.WriteLine($"*WEB* 取得失敗 " + ex);
					}
				};

				var ntp = await NtpAssistance.GetNetworkTimeWithHttp();
				// タイマー開始
				timer.Start(ntp ?? throw new Exception());

				Console.WriteLine("Enterキー入力で終了");

				// 改行入力待ち
				Console.ReadLine();

				// タイマー終了
				timer.Stop();
			}
		}
	}
}
