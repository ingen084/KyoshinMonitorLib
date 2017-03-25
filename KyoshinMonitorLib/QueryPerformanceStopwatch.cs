using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// Win32のQueryPerformanceCounterのラッパクラス
	/// </summary>
	public class QueryPerformanceStopwatch
	{
		[DllImport("kernel32.dll")]
		static extern bool QueryPerformanceCounter(ref long lpPerformanceCount);
		[DllImport("kernel32.dll")]
		static extern bool QueryPerformanceFrequency(ref long lpFrequency);

		private long startCounter;

		/// <summary>
		/// ストップウォッチの値を初期化し、計測を開始します。
		/// </summary>
		/// <returns>PCが高精度タイマーに対応しているかどうか</returns>
		public bool Start()
			=> QueryPerformanceCounter(ref startCounter);

		/// <summary>
		/// 経過時間 Startする前に呼ぶと大変な時間が帰ってきます
		/// </summary>
		public TimeSpan Elapsed
		{
			get
			{
				long stopCounter = 0;
				QueryPerformanceCounter(ref stopCounter);
				long frequency = 0;
				QueryPerformanceFrequency(ref frequency);
				return TimeSpan.FromMilliseconds((stopCounter - startCounter) * 1000.0 / frequency);
			}
		}
	}
}
