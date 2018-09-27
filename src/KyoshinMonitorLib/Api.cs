using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	public abstract class Api : IDisposable
	{
		private HttpClient HttpClient { get; } = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

		protected async Task<T> GetJsonObject<T>(string url)
		{
			try
			{
				using (var response = await HttpClient.GetAsync(url))
				{

					if (!response.IsSuccessStatusCode)
						throw new KyoshinMonitorException(url, "Request Not completed", response.StatusCode);

					return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
				}
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException(url, "Request Timeout");
			}
		}
		protected async Task<byte[]> GetBytes(string url)
		{
			try
			{
				using (var response = await HttpClient.GetAsync(url))
				{
					if (!response.IsSuccessStatusCode)
						throw new KyoshinMonitorException(url, "Request Not completed", response.StatusCode);

					return await response.Content.ReadAsByteArrayAsync();
				}
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException(url, "Request Timeout");
			}
		}

		public void Dispose()
		{
			HttpClient?.Dispose();
		}
	}
}
