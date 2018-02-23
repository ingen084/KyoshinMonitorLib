using System;
using System.Net;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 強震モニタのサーバーから画像の取得に失敗したとき
	/// </summary>
	public class KyoshinMonitorException : Exception
	{
		public KyoshinMonitorException(string url, string message, HttpStatusCode? statusCode = null) : base(message)
		{
			Url = url;
			StatusCode = statusCode;
		}
		public KyoshinMonitorException(string url, string message, Exception innerException) : base(message, innerException)
		{
			Url = url;
		}

		public string Url { get; }
		public HttpStatusCode? StatusCode { get; }
	}
}
