using System.Net;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// APIの実行結果を表します。
	/// </summary>
	/// <typeparam name="TResult"></typeparam>
	public class ApiResult<TResult>
	{
		public ApiResult(HttpStatusCode statusCode, TResult data)
		{
			StatusCode = statusCode;
			Data = data;
		}

		/// <summary>
		/// HTTPステータスコード
		/// <para>nullの場合はタイムアウトを示します。</para>
		/// </summary>
		public HttpStatusCode StatusCode { get; }
		/// <summary>
		/// API実行結果
		/// <para>実行に失敗した場合nullが代入されます。</para>
		/// </summary>
		public TResult Data { get; }
	}
}
