using System;
using System.Net;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 強震モニタの処理に失敗したとき
	/// </summary>
	public class KyoshinMonitorException : Exception
	{
		public KyoshinMonitorException(string message) : base(message)
		{ }
		public KyoshinMonitorException(string message, Exception innerException) : base(message, innerException)
		{ }
	}
}
