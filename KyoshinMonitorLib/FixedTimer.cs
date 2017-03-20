using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 誤差を蓄積させないようにするタイマー
	/// </summary>
	public class FixedTimer
	{
		private Timer _timer;
		private DateTime _lastTime;

		public TimeSpan _interval = TimeSpan.FromMilliseconds(1000);
		/// <summary>
		/// タイマーの間隔
		/// </summary>
		public TimeSpan Interval
		{
			get => _interval;
			set
			{
				if (_accuracy.Ticks <= 0)
					throw new ArgumentOutOfRangeException("Intervalには1Tick以上の時間を指定してください。");
				_interval = value;
			}
		}

		private TimeSpan _accuracy = TimeSpan.FromMilliseconds(1);
		/// <summary>
		/// タイマーの精度 ただし精度を保証するものではありません。
		/// <para>上限がある上、精度を高くするとその分重くなります。</para>
		/// <para>この値より短い単位でイベントが発行されることはありません。</para>
		/// <para>Intervalよりも大きな値を指定した際、誤差が蓄積されない特性が消滅することに気をつけてください。</para>
		/// </summary>
		public TimeSpan Accuracy
		{
			get => _accuracy;
			set
			{
				if (_accuracy.Ticks <= 0)
					throw new ArgumentOutOfRangeException("Accuracyには1Tick以上の時間を指定してください。");
				_accuracy = value;
			}
		}

		/// <summary>
		/// タイマーが1度実行された後
		/// </summary>
		public bool AutoReset { get; set; } = true;

		public event Action Elapsed;


		public FixedTimer(TimeSpan interval) : this()
		{
			Interval = interval;
		}
		public FixedTimer()
		{
			_timer = new Timer(s =>
			{
				if (DateTime.Now - _lastTime >= Interval)
				{
					ThreadPool.QueueUserWorkItem(s2 => Elapsed?.Invoke());
					_lastTime += Interval;
					if (!AutoReset)
						_timer.Change(Timeout.Infinite, Timeout.Infinite);
				}
			}, null, Timeout.Infinite, Timeout.Infinite);
		}

		public void Start()
		{
			_lastTime = DateTime.Now;
			_timer.Change(Accuracy, Accuracy);
		}

		public void Stop()
			=> _timer.Change(Timeout.Infinite, Timeout.Infinite);
	}
}
