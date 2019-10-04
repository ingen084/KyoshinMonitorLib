using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
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

		protected async Task<ApiResult<T>> GetJsonObject<T>(string url)
		{
			try
			{
				using (var response = await HttpClient.GetAsync(url))
				{

					if (!response.IsSuccessStatusCode)
						return new ApiResult<T>(response.StatusCode, default);

					return new ApiResult<T>(response.StatusCode, JsonSerializer.Deserialize<T>(await response.Content.ReadAsStringAsync()));
				}
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("Request Timeout: " + url);
			}
		}
		protected async Task<ApiResult<byte[]>> GetBytes(string url)
		{
			try
			{
				using (var response = await HttpClient.GetAsync(url))
				{
					if (!response.IsSuccessStatusCode)
						return new ApiResult<byte[]>(response.StatusCode, default);

					return new ApiResult<byte[]>(response.StatusCode, await response.Content.ReadAsByteArrayAsync());
				}
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("Request Timeout: " + url);
			}
		}

		public void Dispose()
		{
			HttpClient?.Dispose();
		}
	}
}
