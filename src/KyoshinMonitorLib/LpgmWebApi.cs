using KyoshinMonitorLib.ApiResult.WebApi;
using KyoshinMonitorLib.UrlGenerator;
using System;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 長周期地震動モニタで使用されているAPI
	/// </summary>
	public class LpgmWebApi : Api
	{
		/// <summary>
		/// 緊急地震速報の情報を取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <returns></returns>
		public Task<ApiResult<Eew?>> GetEewInfo(DateTime time)
			=> GetJsonObject<Eew>(LpgmWebApiUrlGenerator.Generate(LpgmWebApiUrlType.EewJson, time));

		/// <summary>
		/// リアルタイム画像の生データを取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <param name="dataType">取得する画像の種類</param>
		/// <param name="isBehore">地下かどうか</param>
		/// <returns></returns>
		public Task<ApiResult<byte[]?>> GetRealtimeImageData(DateTime time, RealtimeDataType dataType, bool isBehore = false)
			=> GetBytes(LpgmWebApiUrlGenerator.Generate(LpgmWebApiUrlType.RealtimeImg, time, dataType, isBehore));

		/// <summary>
		/// 長周期地震動関連のリアルタイム画像の生データを取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <param name="dataType">取得する画像の種類</param>
		/// <returns></returns>
		public Task<ApiResult<byte[]?>> GetLpgmRealtimeImageData(DateTime time, RealtimeDataType dataType)
			=> GetBytes(LpgmWebApiUrlGenerator.Generate(LpgmWebApiUrlType.LpgmRealtimeImg, time, dataType));

		/// <summary>
		/// 予測震度の画像を取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <returns></returns>
		public Task<ApiResult<byte[]?>> GetEstShindoImageData(DateTime time)
			=> GetBytes(LpgmWebApiUrlGenerator.Generate(LpgmWebApiUrlType.EstShindo, time));

		/// <summary>
		/// P波、S波の広がりを示す円の画像を取得します。
		/// </summary>
		/// <param name="time">取得する時間</param>
		/// <returns></returns>
		public Task<ApiResult<byte[]?>> GetPSWaveImageData(DateTime time)
			=> GetBytes(LpgmWebApiUrlGenerator.Generate(LpgmWebApiUrlType.PSWave, time));
	}
}
