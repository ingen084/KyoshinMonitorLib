using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	public abstract class Api : IDisposable
	{
		private HttpClient HttpClient { get; } = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

		protected async Task<ApiResult<T>> GetJsonObject<T>(string url)
		{
			try
			{
				using (var response = await HttpClient.GetAsync(url))
				{

					if (!response.IsSuccessStatusCode)
						return new ApiResult<T>(response.StatusCode, default);

					return new ApiResult<T>(response.StatusCode, JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()));
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
