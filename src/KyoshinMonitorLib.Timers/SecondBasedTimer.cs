using System;
using System.Threading;

namespace KyoshinMonitorLib.Timers
{
	/// <summary>
	/// 時刻補正機能付きタイマー
	/// </summary>
	public class SecondBasedTimer
	{
		private Timer _timer;
		private TimeSpan _lastTime;
		private DateTime _currentTime;

		private bool _isEventRunning;

		//最後にNTP更新された時刻
		private DateTime _lastUpdatedTime;

		private HighPerformanceStopwatch _sw;

		private TimeSpan _interval = TimeSpan.FromSeconds(1);

		private TimeSpan _offset = TimeSpan.Zero;
		/// <summary>
		/// オフセットを調整します。
		/// </summary>
		public TimeSpan Offset
		{
			get => _offset;
			set
			{
				_lastTime -= (_offset - value);
				_offset = value;
			}
		}

		/// <summary>
		/// タイマーの精度 ただし精度を保証するものではありません。
		/// <para>上限がある上、精度を高くするとその分重くなります。</para>
		/// <para>この値より短い単位でイベントが発行されることはありません。</para>
		/// <para>Intervalよりも大きな値を指定した際、誤差が蓄積されない特性が消滅することに気をつけてください。</para>
		/// </summary>
		private TimeSpan Accuracy => TimeSpan.FromMilliseconds(1);

		/// <summary>
		/// falseにするとタイマーのイベントを1度しか発行しません
		/// </summary>
		public bool AutoReset { get; set; } = true;

		/// <summary>
		/// イベント発生時にすでに他のイベントが実行中の場合新規イベントを実行させないようにするかどうか
		/// </summary>
		public bool BlockingMode { get; set; } = true;

		/// <summary>
		/// タイマーイベント
		/// </summary>
		public event Action<DateTime> Elapsed;

		/// <summary>
		/// NtpTimerを初期化します。
		/// </summary>
		public SecondBasedTimer()
		{
			_timer = new Timer(s =>
			{
				if (_sw.Elapsed - _lastTime >= _interval)
				{
					if (!BlockingMode || !_isEventRunning)
						ThreadPool.QueueUserWorkItem(s2 =>
						{
							_isEventRunning = true;
							Elapsed?.Invoke(_currentTime);
							_isEventRunning = false;
						});
					//かなり時間のズレが大きければその分修正する
					if (_sw.Elapsed.Ticks - _lastTime.Ticks >= _interval.Ticks * 2)
					{
						var skipCount = ((_sw.Elapsed.Ticks - _lastTime.Ticks) / _interval.Ticks);
						_lastTime += TimeSpan.FromTicks(_interval.Ticks * skipCount);
						_currentTime = _currentTime.AddSeconds(skipCount);
					}
					else //そうでなかったばあい普通に修正
					{
						_lastTime += _interval;
						_currentTime = _currentTime.AddSeconds(1);
					}
					if (!AutoReset)
						_timer.Change(Timeout.Infinite, Timeout.Infinite);
				}
			}, null, Timeout.Infinite, Timeout.Infinite);
			_sw = new HighPerformanceStopwatch();
		}

		/// <summary>
		/// 時間を更新します。このメソッドが呼ばれたタイミングで
		/// </summary>
		/// <param name="time">書き込む時間</param>
		public void UpdateTime(DateTime time)
		{
			_lastUpdatedTime = time;
			_currentTime = _lastUpdatedTime.AddMilliseconds(-_lastUpdatedTime.Millisecond).AddSeconds(-Math.Floor(Offset.TotalSeconds));
			_lastTime = _sw.Elapsed - TimeSpan.FromMilliseconds(_lastUpdatedTime.Millisecond);
			_lastTime += (Offset - TimeSpan.FromSeconds(Offset.Seconds));
		}

		/// <summary>
		/// タイマーを開始します。
		/// </summary>
		/// <param name="currentTime">現在時間</param>
		public void Start(DateTime currentTime)
		{
			_isEventRunning = false;
			_sw.Start();
			UpdateTime(currentTime);
			_timer.Change(Accuracy, Accuracy);
		}

		/// <summary>
		/// タイマーを停止します。
		/// </summary>
		public void Stop()
			=> _timer.Change(Timeout.Infinite, Timeout.Infinite);
	}
}
