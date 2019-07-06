using KyoshinMonitorLib.ApiResult.WebApi;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	public class WebApi : Api
	{
		public Task<ApiResult<Eew>> GetEewInfo(DateTime time)
			=> GetJsonObject<Eew>(WebApiUrlGenerator.Generate(WebApiUrlType.EewJson, time));

		public Task<ApiResult<byte[]>> GetRealtimeImageData(DateTime time, RealTimeDataType dataType, bool isBehore = false)
			=> GetBytes(WebApiUrlGenerator.Generate(WebApiUrlType.RealTimeImg, time, dataType, isBehore));

		public Task<ApiResult<byte[]>> GetEstShindoImageData(DateTime time)
			=> GetBytes(WebApiUrlGenerator.Generate(WebApiUrlType.EstShindo, time));

		public Task<ApiResult<byte[]>> GetPSWaveImageData(DateTime time)
			=> GetBytes(WebApiUrlGenerator.Generate(WebApiUrlType.PSWave, time));
	}
}
