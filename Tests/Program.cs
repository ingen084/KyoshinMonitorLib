using KyoshinMonitorLib;
using System;
using System.Linq;
using System.Net.Http;

namespace Tests
{
	internal class Program
	{
		public static void Main(string[] args)
		{

			var points = ObservationPoint.LoadFromMpk("ShindoObsPoints.mpk.lz4", true);

			//誤差が蓄積しないタイマーのインスタンスを作成(デフォルトは間隔1000ms+精度1ms↓)
			var timer = new NtpTimer()
			{
				Offset = TimeSpan.FromSeconds(1.1),
			};

			//適当にイベント設定
			timer.Elapsed += time =>
			{
				Console.WriteLine($"\nsys: {DateTime.Now.ToString("HH:mm:ss.fff")} ntp:{time.ToString("HH:mm:ss.fff")}");
				try
				{
					//画像を取得して結果を計算 (良い子のみんなはawaitを使おうね！)
					var result = points.ParseIntensityFromParameterAsync(time, false).Result;

					//適当に一つ目の観測地点の震度
					Console.WriteLine($"FirstInt: raw:{result.First().AnalysisResult} jma:{result.First().AnalysisResult.ToJmaIntensity().ToLongString()}");

					//現在の最大震度
					Console.WriteLine($"MaxInt: raw:{result.Max(r => r.AnalysisResult)} jma:{result.Max(r => r.AnalysisResult).ToJmaIntensity().ToLongString()}");
				}
				catch(AggregateException ex)
				{
					var ex2 = ex.InnerException;
					if (!ex2.Message.Contains("404")) return;
					timer.Offset += TimeSpan.FromMilliseconds(100);
					Console.WriteLine($"404のためオフセット調整 to:{timer.Offset.TotalSeconds}s");
				}
			};

			//タイマー開始
			timer.Start().Wait();

			Console.WriteLine("Enterキー入力で終了");

			//改行入力待ち
			Console.ReadLine();

			//タイマー終了
			timer.Stop();
		}
	}
}