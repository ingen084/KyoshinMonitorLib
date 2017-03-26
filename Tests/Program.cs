using KyoshinMonitorLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	class Program
	{
		public static void Main(string[] args)
		{
			//ファイルがなかったら帰る
			//if (!File.Exists("ShindoObsPoints.pbf"))
			// 	return;

			//観測点情報読み込み
			//var points = ObservationPoint.LoadFromPbf("ShindoObsPoints.pbf");
			var points = ObservationPoint.LoadFromMpk("ShindoObsPoints.mpk.lz4", true);

			//誤差が蓄積しないタイマーのインスタンスを作成(デフォルトは間隔1000ms+精度1ms↓)
			var timer = new FixedTimer();

			//適当にイベント設定
			timer.Elapsed += () =>
			{
				//時間計算(今回は適当にPC時間-5秒)
				var time = DateTime.Now.AddSeconds(-5);

				Console.WriteLine($"\n**{time.ToString("HH:mm:ss.fff")}");

				//画像を取得して結果を計算 (良い子のみんなはawaitを使おうね！)
				var result = points.ParseIntensityFromParameterAsync(time, false).Result;

				//適当に一つ目の観測地点の震度
				Console.WriteLine("FirstInt: " + result.First().AnalysisResult);

				//現在の最大震度
				Console.WriteLine("MaxInt: " + result.Max(r => r.AnalysisResult));
			};

			//タイマー開始
			timer.Start();

			Console.WriteLine("Enterキー入力で終了します。");

			//改行入力待ち
			Console.ReadLine();

			//タイマー終了
			timer.Stop();
		}
	}
}
