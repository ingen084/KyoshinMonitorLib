using System;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 強震モニタの処理に失敗したとき
	/// </summary>
	public class KyoshinMonitorException : Exception
	{
		/// <summary>
		/// 例外の初期化を行います。
		/// </summary>
		public KyoshinMonitorException(string message) : base(message)
		{ }
		/// <summary>
		/// 例外の初期化を行います。
		/// </summary>
		public KyoshinMonitorException(string message, Exception innerException) : base(message, innerException)
		{ }
	}
}
