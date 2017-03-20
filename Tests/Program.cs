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
			if (!File.Exists("ShindoObsPoints.pbf"))
				return;

			//観測点情報読み込み
			var points = ObservationPoint.LoadFromPbf("ShindoObsPoints.pbf");

			//時間計算(今回は適当にPC時間-5秒)
			var time = DateTime.Now.AddSeconds(-5);

			//画像を取得して結果を計算
			var result = points.CalculateIntensityFromParameterAsync(time, false).Result;

			//適当に一つ目の観測地点の震度
			Console.WriteLine("FirstInt: " + result.First().AnalysisResult);

			//現在の最大震度
			Console.WriteLine("MaxInt: " + result.Max(r => r.AnalysisResult));

			Console.ReadLine();
		}
	}
}
