using KyoshinMonitorLib;
using KyoshinMonitorLib.SkiaImages;
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
		public static async Task Main()
		{
			var points = ObservationPoint.LoadFromMpk("ShindoObsPoints.mpk.lz4", true);
			using var webApi = new LpgmWebApi();
			// タイマーのインスタンスを作成
			var timer = new SecondBasedTimer()
			{
				Offset = TimeSpan.FromSeconds(1.1),//1.1
			};
			// 適当にイベント設定
			timer.Elapsed += async time =>
			{
				Console.WriteLine($"\nsys: {DateTime.Now:HH:mm:ss.fff} ntp:{time:HH:mm:ss.fff}");

				try
				{
					// WebAPIから結果を計算 (良い子のみんなはawaitを使おうね！)
					var result = await webApi.ParseScaleFromParameterAsync(points, time).ConfigureAwait(false);
					if (result.Data != null)
					{
						var data = result.Data.ToArray();
						// 現在の最大震度
						Console.WriteLine($"*WEB* 最大震度: 生:{data.Max(r => r.GetResultToIntensity()):0.0} jma:{data.Max(r => r.GetResultToIntensity()).ToJmaIntensity().ToLongString()} 数:{data.Count(d => d.AnalysisResult != null)}");
					}
					else if (result.StatusCode == HttpStatusCode.NotFound)
					{
						// timer.Offset += TimeSpan.FromMilliseconds(100);
						// Console.WriteLine($"404のためオフセット調整 to:{timer.Offset.TotalSeconds}s");
						Console.WriteLine($"404");
					}
					else
						Console.WriteLine($"*WEB* 取得失敗 " + result.StatusCode);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"*WEB* 取得失敗 " + ex);
				}
				try
				{
					// WebAPIから結果を計算 (良い子のみんなはawaitを使おうね！)
					var result = await webApi.ParseScaleFromParameterAsync(points, time, RealtimeDataType.Pga).ConfigureAwait(false);
					if (result.Data != null)
					{
						var data = result.Data.ToArray();
						// 現在の最大震度
						Console.WriteLine($"*WEB* 最大PGA: 生:{data.Max(r => r.GetResultToPga()):0.0} 数:{data.Count(d => d.AnalysisResult != null)}");
					}
					else if (result.StatusCode == HttpStatusCode.NotFound)
					{
						// timer.Offset += TimeSpan.FromMilliseconds(100);
						// Console.WriteLine($"404のためオフセット調整 to:{timer.Offset.TotalSeconds}s");
						Console.WriteLine($"404");
					}
					else
						Console.WriteLine($"*WEB* 取得失敗 " + result.StatusCode);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"*WEB* 取得失敗 " + ex);
				}
			};

			var ntp = await NtpAssistance.GetNetworkTimeWithNtp().ConfigureAwait(false);
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
