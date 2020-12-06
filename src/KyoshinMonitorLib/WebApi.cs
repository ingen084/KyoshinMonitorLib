using KyoshinMonitorLib.ApiResult.WebApi;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// Webで使用されているAPI
	/// </summary>
	public class WebApi : Api
	{
		/// <summary>
		/// 緊急地震速報の情報を取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <returns></returns>
		public Task<ApiResult<Eew?>> GetEewInfo(DateTime time)
			=> GetJsonObject<Eew>(WebApiUrlGenerator.Generate(WebApiUrlType.EewJson, time));
		/// <summary>
		/// リアルタイム画像の生データを取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <param name="dataType">取得する画像の種類</param>
		/// <param name="isBehore">地下かどうか</param>
		/// <returns></returns>
		public Task<ApiResult<byte[]?>> GetRealtimeImageData(DateTime time, RealtimeDataType dataType, bool isBehore = false)
			=> GetBytes(WebApiUrlGenerator.Generate(WebApiUrlType.RealtimeImg, time, dataType, isBehore));
		/// <summary>
		/// 予測震度の画像を取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <returns></returns>
		public Task<ApiResult<byte[]?>> GetEstShindoImageData(DateTime time)
			=> GetBytes(WebApiUrlGenerator.Generate(WebApiUrlType.EstShindo, time));
		/// <summary>
		/// P波、S波の広がりを示す円の画像を取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <returns></returns>
		public Task<ApiResult<byte[]?>> GetPSWaveImageData(DateTime time)
			=> GetBytes(WebApiUrlGenerator.Generate(WebApiUrlType.PSWave, time));
	}
}
