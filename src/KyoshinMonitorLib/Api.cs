using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// APIのベースクラス
	/// </summary>
	public abstract class Api : IDisposable
	{
		private HttpClient HttpClient { get; } = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };
		/// <summary>
		/// APIを呼ぶのにあたってのタイムアウト時間
		/// </summary>
		public TimeSpan Timeout
		{
			get => HttpClient.Timeout;
			set => HttpClient.Timeout = value;
		}

		/// <summary>
		/// GETリクエストを送信し、Jsonをデシリアライズした結果を取得します。
		/// </summary>
		/// <typeparam name="T">デシリアライズする型</typeparam>
		/// <param name="url">使用するURL</param>
		/// <returns></returns>
		protected async Task<ApiResult<T>> GetJsonObject<T>(string url)
		{
			try
			{
				using var response = await HttpClient.GetAsync(url);
				if (!response.IsSuccessStatusCode)
					return new ApiResult<T>(response.StatusCode, default);

				return new ApiResult<T>(response.StatusCode, JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync()));
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("Request Timeout: " + url);
			}
		}
		/// <summary>
		/// GETリクエストを送信し、生のbyte配列を取得します。
		/// </summary>
		/// <param name="url">使用するURL</param>
		/// <returns></returns>
		protected async Task<ApiResult<byte[]>> GetBytes(string url)
		{
			try
			{
				using var response = await HttpClient.GetAsync(url);
				if (!response.IsSuccessStatusCode)
					return new ApiResult<byte[]>(response.StatusCode, default);

				return new ApiResult<byte[]>(response.StatusCode, await response.Content.ReadAsByteArrayAsync());
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("Request Timeout: " + url);
			}
		}

		/// <summary>
		/// オブジェクトの破棄を行います。
		/// </summary>
		public void Dispose()
		{
			HttpClient?.Dispose();
		}
	}
}
