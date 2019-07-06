using System;
using System.Threading;
using System.Threading.Tasks;

namespace KyoshinMonitorLib.Timers
{
	/// <summary>
	/// 時刻補正機能付きタイマー
	/// </summary>
	public class SecondBasedTimer
	{
		private readonly TimeSpan Interval = TimeSpan.FromSeconds(1);
		private Timer InnerTimer { get; }
		private TimeSpan LastTime { get; set; }
		private HighPerformanceStopwatch StopWatch { get; }

		/// <summary>
		/// 現在のタイマー内の時間
		/// </summary>
		public DateTime CurrentTime { get; private set; }
		/// <summary>
		/// イベントタスクが実行中かどうか
		/// </summary>
		public bool IsEventRunning { get; private set; }
		/// <summary>
		/// 最後に内部時刻が更新された時刻
		/// </summary>
		public DateTime LastUpdatedTime { get; private set; }

		private TimeSpan _offset = TimeSpan.Zero;
		/// <summary>
		/// オフセットを調整します。
		/// </summary>
		public TimeSpan Offset
		{
			get => _offset;
			set
			{
				LastTime -= _offset - value;
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
		public event Func<DateTime, Task> Elapsed;

		/// <summary>
		/// NtpTimerを初期化します。
		/// </summary>
		public SecondBasedTimer()
		{
			InnerTimer = new Timer(s =>
			{
				if (StopWatch.Elapsed - LastTime >= Interval)
				{
					if (!BlockingMode || !IsEventRunning)
						ThreadPool.QueueUserWorkItem(s2 =>
						{
							IsEventRunning = true;
							Elapsed?.Invoke(CurrentTime)?.Wait();
							IsEventRunning = false;
						});
					//かなり時間のズレが大きければその分修正する
					if (StopWatch.Elapsed.Ticks - LastTime.Ticks >= Interval.Ticks * 2)
					{
						var skipCount = (StopWatch.Elapsed.Ticks - LastTime.Ticks) / Interval.Ticks;
						LastTime += TimeSpan.FromTicks(Interval.Ticks * skipCount);
						CurrentTime = CurrentTime.AddSeconds(skipCount);
					}
					else //そうでなかったばあい普通に修正
					{
						LastTime += Interval;
						CurrentTime = CurrentTime.AddSeconds(1);
					}
					if (!AutoReset)
						InnerTimer.Change(Timeout.Infinite, Timeout.Infinite);
				}
			}, null, Timeout.Infinite, Timeout.Infinite);
			StopWatch = new HighPerformanceStopwatch();
		}

		/// <summary>
		/// 時間を更新します。
		/// </summary>
		/// <param name="time">書き込む時間</param>
		public void UpdateTime(DateTime time)
		{
			LastUpdatedTime = time;
			CurrentTime = LastUpdatedTime.AddMilliseconds(-LastUpdatedTime.Millisecond).AddSeconds(-Math.Floor(Offset.TotalSeconds));
			LastTime = StopWatch.Elapsed - TimeSpan.FromMilliseconds(LastUpdatedTime.Millisecond);
			LastTime += Offset - TimeSpan.FromSeconds(Offset.Seconds);
		}

		/// <summary>
		/// タイマーを開始します。
		/// </summary>
		/// <param name="currentTime">現在時間</param>
		public void Start(DateTime currentTime)
		{
			IsEventRunning = false;
			StopWatch.Start();
			UpdateTime(currentTime);
			InnerTimer.Change(Accuracy, Accuracy);
		}

		/// <summary>
		/// タイマーを停止します。
		/// </summary>
		public void Stop()
			=> InnerTimer.Change(Timeout.Infinite, Timeout.Infinite);
	}
}
