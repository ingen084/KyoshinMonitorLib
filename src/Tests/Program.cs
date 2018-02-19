using KyoshinMonitorLib;
using KyoshinMonitorLib.Images;
using KyoshinMonitorLib.Timers;
using System;
using System.Linq;
using System.Net;

namespace Tests
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			var points = ObservationPoint.LoadFromMpk("ShindoObsPoints.mpk.lz4", true);

			//誤差が蓄積しないタイマーのインスタンスを作成(デフォルトは間隔1000ms+精度1ms↓)
			var timer = new SecondBasedTimer()
			{
				Offset = TimeSpan.FromSeconds(2.2),
			};

			//適当にイベント設定
			timer.Elapsed += async time =>
			{
				Console.WriteLine($"\nsys: {DateTime.Now.ToString("HH:mm:ss.fff")} ntp:{time.ToString("HH:mm:ss.fff")}");
				try
				{
					//画像を取得して結果を計算 (良い子のみんなはawaitを使おうね！)
					var result = await points.ParseIntensityFromParameterAsync(time, false);

					//現在の最大震度
					Console.WriteLine($"最大震度: 生:{result.Max(r => r.AnalysisResult)} jma:{result.Max(r => r.AnalysisResult).ToJmaIntensity().ToLongString()}");
				}
				catch (KyoshinMonitorException ex)
				{
					Console.WriteLine($"HTTPエラー発生 {ex.StatusCode}({(int)ex.StatusCode})");
					if (ex.StatusCode != HttpStatusCode.NotFound) return;
					timer.Offset += TimeSpan.FromMilliseconds(100);
					Console.WriteLine($"404のためオフセット調整 to:{timer.Offset.TotalSeconds}s");
				}
			};

			//タイマー開始
			timer.Start(NtpAssistance.GetNetworkTimeWithNtp().Result);

			Console.WriteLine("Enterキー入力で終了");

			//改行入力待ち
			Console.ReadLine();

			//タイマー終了
			timer.Stop();
		}
	}
}
