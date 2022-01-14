using System;

namespace KyoshinMonitorLib.UrlGenerator
{
	/// <summary>
	/// リアルタイム画像の種類
	/// </summary>
	public enum RealtimeDataType
	{
		/// <summary>
		/// 震度
		/// </summary>
		Shindo,

		/// <summary>
		/// 最大加速度
		/// </summary>
		Pga,

		/// <summary>
		/// 最大速度
		/// </summary>
		Pgv,

		/// <summary>
		/// 最大変位
		/// </summary>
		Pgd,

		/// <summary>
		/// 速度応答0.125Hz
		/// </summary>
		Response_0_125Hz,

		/// <summary>
		/// 速度応答0.25Hz
		/// </summary>
		Response_0_25Hz,

		/// <summary>
		/// 速度応答0.5Hz
		/// </summary>
		Response_0_5Hz,

		/// <summary>
		/// 速度応答1Hz
		/// </summary>
		Response_1Hz,

		/// <summary>
		/// 速度応答2Hz
		/// </summary>
		Response_2Hz,

		/// <summary>
		/// 速度応答4Hz
		/// </summary>
		Response_4Hz,

		/// <summary>
		/// 長周期地震動階級
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrspmx,

		/// <summary>
		/// 階級データ(周期1秒台)
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrsp_1s,

		/// <summary>
		/// 階級データ(周期2秒台)
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrsp_2s,

		/// <summary>
		/// 階級データ(周期3秒台)
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrsp_3s,

		/// <summary>
		/// 階級データ(周期4秒台)
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrsp_4s,

		/// <summary>
		/// 階級データ(周期5秒台)
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrsp_5s,

		/// <summary>
		/// 階級データ(周期6秒台)
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrsp_6s,

		/// <summary>
		/// 階級データ(周期7秒台)
		/// Lpgm系列でのみ利用可
		/// </summary>
		Abrsp_7s,
	}

	/// <summary>
	/// RealtimeImgTypeの拡張メソッド
	/// </summary>
	public static class RealtimeDataExtensions
	{
		/// <summary>
		/// URLに使用する文字列に変換する
		/// </summary>
		/// <param name="type">変換するRealtimeImgTypy</param>
		/// <returns>変換された文字列</returns>
		public static string ToUrlString(this RealtimeDataType type) => type switch
		{
			RealtimeDataType.Shindo => "jma",
			RealtimeDataType.Pga => "acmap",
			RealtimeDataType.Pgv => "vcmap",
			RealtimeDataType.Pgd => "dcmap",
			RealtimeDataType.Response_0_125Hz => "rsp0125",
			RealtimeDataType.Response_0_25Hz => "rsp0250",
			RealtimeDataType.Response_0_5Hz => "rsp0500",
			RealtimeDataType.Response_1Hz => "rsp1000",
			RealtimeDataType.Response_2Hz => "rsp2000",
			RealtimeDataType.Response_4Hz => "rsp4000",
			RealtimeDataType.Abrspmx => "abrspmx",
			RealtimeDataType.Abrsp_1s => "abrsp1s",
			RealtimeDataType.Abrsp_2s => "abrsp2s",
			RealtimeDataType.Abrsp_3s => "abrsp3s",
			RealtimeDataType.Abrsp_4s => "abrsp4s",
			RealtimeDataType.Abrsp_5s => "abrsp5s",
			RealtimeDataType.Abrsp_6s => "abrsp6s",
			RealtimeDataType.Abrsp_7s => "abrsp7s",
			_ => throw new ArgumentException($"URLを生成できない{nameof(RealtimeDataType)}が指定されています", nameof(type)),
		};
	}
}