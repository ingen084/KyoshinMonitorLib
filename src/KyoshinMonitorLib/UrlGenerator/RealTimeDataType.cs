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
			_ => throw new ArgumentException($"URLを生成できない{nameof(RealtimeDataType)}が指定されています", nameof(type)),
		};
	}
}