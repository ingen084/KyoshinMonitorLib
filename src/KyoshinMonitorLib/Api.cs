using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// APIのベースクラス
	/// </summary>
	public abstract class Api : IDisposable
	{
		private HttpClient HttpClient { get; } = new() { Timeout = TimeSpan.FromSeconds(10) };
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
		/// <param name="jsonTypeInfo">使用するTypeInfo</param>
		/// <returns></returns>
		protected async Task<ApiResult<T?>> GetJsonObject<T>(string url, JsonTypeInfo<T> jsonTypeInfo)
		{
			try
			{
				using var response = await HttpClient.GetAsync(url);
				if (!response.IsSuccessStatusCode)
					return new(response.StatusCode, default);

				return new(response.StatusCode, JsonSerializer.Deserialize(await response.Content.ReadAsStringAsync(), jsonTypeInfo));
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
		protected async Task<ApiResult<byte[]?>> GetBytes(string url)
		{
			try
			{
				using var response = await HttpClient.GetAsync(url);
				if (!response.IsSuccessStatusCode)
					return new(response.StatusCode, default);

				return new(response.StatusCode, await response.Content.ReadAsByteArrayAsync());
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
			GC.SuppressFinalize(this);
		}
	}
}
