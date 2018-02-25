﻿using KyoshinMonitorLib;
using KyoshinMonitorLib.ApiResult.AppApi;
using KyoshinMonitorLib.Images;
using KyoshinMonitorLib.Timers;
using KyoshinMonitorLib.UrlGenerator;
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
			using (var appApi = new AppApi(points))
			using (var webApi = new WebApi())
			{
				//タイマーのインスタンスを作成
				var timer = new SecondBasedTimer()
				{
					Offset = TimeSpan.FromSeconds(2.2),//1.1
				};

				//適当にイベント設定
				timer.Elapsed += async time =>
				{
					Console.WriteLine($"\nsys: {DateTime.Now.ToString("HH:mm:ss.fff")} ntp:{time.ToString("HH:mm:ss.fff")}");
					try
					{
						{
							//APIから結果を計算 (良い子のみんなはawaitを使おうね！)
							var result = await appApi.GetLinkedRealTimeData(time, RealTimeDataType.Shindo, false);
							//現在の最大震度
							Console.WriteLine($"*API* 最大震度: 生:{result.Max(r => r.Value)} jma:{result.Max(r => r.Value).ToJmaIntensity().ToLongString()}");
							//最大震度観測点(の1つ)
							//var maxPoint = result.OrderByDescending(r => r.Value).First();
							//Console.WriteLine($"最大観測点 {maxPoint.Point.site.Prefefecture.GetLongName()} {maxPoint.Point.point.Name} 震度:{maxPoint.Value}({maxPoint.Value.ToJmaIntensity().ToLongString()})");
						}
						{
							//APIから結果を計算 (良い子のみんなはawaitを使おうね！)
							var result = await webApi.ParseIntensityFromParameterAsync(points, time);
							//現在の最大震度
							Console.WriteLine($"*WEB* 最大震度: 生:{result.Max(r => r.AnalysisResult)} jma:{result.Max(r => r.AnalysisResult).ToJmaIntensity().ToLongString()}");
						}
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
				timer.Start(NtpAssistance.GetNetworkTimeWithNtp().Result ?? throw new Exception());

				Console.WriteLine("Enterキー入力で終了");

				//改行入力待ち
				Console.ReadLine();

				//タイマー終了
				timer.Stop();
			}
		}
	}
}