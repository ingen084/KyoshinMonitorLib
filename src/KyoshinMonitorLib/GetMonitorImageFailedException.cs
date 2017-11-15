using System;
using System.Net;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 強震モニタのサーバーから画像の取得に失敗したとき
	/// </summary>
	public class GetMonitorImageFailedException : Exception
    {
		public GetMonitorImageFailedException(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}

		public HttpStatusCode StatusCode { get; }
    }
}
