using System;
using System.Net;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 強震モニタのサーバーから画像の取得に失敗したとき
	/// </summary>
	public class KyoshinMonitorException : Exception
	{
		public KyoshinMonitorException(string message, HttpStatusCode? statusCode = null) : base(message)
		{
			StatusCode = statusCode;
		}
		public KyoshinMonitorException(string message, Exception innerException) : base(message, innerException)
		{
		}

		public HttpStatusCode? StatusCode { get; }
	}
}
