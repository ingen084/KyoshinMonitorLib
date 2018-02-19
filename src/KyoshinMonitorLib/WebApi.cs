using KyoshinMonitorLib.ApiResult.WebApi;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Utf8Json;

namespace KyoshinMonitorLib
{
	public static class WebApi
	{
		private static readonly HttpClient HttpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

		public static async Task<Eew> GetEewInfo(DateTime time)
		{
			try
			{
				var response = await HttpClient.GetAsync(WebApiUrlGenerator.Generate(WebApiUrlType.EewJson, time));
				if (!response.IsSuccessStatusCode)
					throw new KyoshinMonitorException("GetEewInfo request not completed", response.StatusCode);

				return JsonSerializer.Deserialize<Eew>(await response.Content.ReadAsStringAsync());
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("GetEewInfo Request Timeout");
			}
		}

		public static async Task<byte[]> GetRealtimeImageData(DateTime time, RealTimeDataType dataType, bool isBehore)
		{
			try
			{
				var response = await HttpClient.GetAsync(WebApiUrlGenerator.Generate(WebApiUrlType.RealTimeImg, time, dataType, isBehore));
				if (!response.IsSuccessStatusCode)
					throw new KyoshinMonitorException("GetRealtimeImageData request not completed", response.StatusCode);

				return await response.Content.ReadAsByteArrayAsync();
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("GetRealtimeImageData Request Timeout");
			}
		}

		public static async Task<byte[]> GetEstShindoImageData(DateTime time)
		{
			try
			{
				var response = await HttpClient.GetAsync(WebApiUrlGenerator.Generate(WebApiUrlType.EstShindo, time));
				if (!response.IsSuccessStatusCode)
					throw new KyoshinMonitorException("GetEstShindoImageData request not completed", response.StatusCode);

				return await response.Content.ReadAsByteArrayAsync();
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("GetEstShindoImageData Request Timeout");
			}
		}

		public static async Task<byte[]> GetPSWaveImageData(DateTime time)
		{
			try
			{
				var response = await HttpClient.GetAsync(WebApiUrlGenerator.Generate(WebApiUrlType.PSWave, time));
				if (!response.IsSuccessStatusCode)
					throw new KyoshinMonitorException("GetPSWaveImageData request not completed", response.StatusCode);

				return await response.Content.ReadAsByteArrayAsync();
			}
			catch (TaskCanceledException)
			{
				throw new KyoshinMonitorException("GetPSWaveImageData Request Timeout");
			}
		}
	}
}
