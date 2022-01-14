using System;

namespace KyoshinMonitorLib.UrlGenerator
{
	/// <summary>
	/// 長周期地震動モニタのURL生成器
	/// </summary>
	public static class LpgmWebApiUrlGenerator
	{
		/// <summary>
		/// JsonEewのベースURL
		/// <para>0:時間</para>
		/// </summary>
		public const string JsonEewBase = "https://www.lmoni.bosai.go.jp/monitor/webservice/hypo/eew/{0}.json";

		/// <summary>
		/// PsWaveImgのベースURL
		/// <para>0:日付</para>
		/// <para>1:時間</para>
		/// </summary>
		public const string PsWaveBase = "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/PSWaveImg/eew/{0}/{1}.eew.gif";

		/// <summary>
		/// RealtimeImgのベースURL
		/// <para>0:タイプ</para>
		/// <para>1:地上(s)/地下(b)</para>
		/// <para>2:日付</para>
		/// <para>3:時間</para>
		/// </summary>
		public const string RealtimeBase = "https://smi.lmoniexp.bosai.go.jp/data/map_img/RealTimeImg/{0}_{1}/{2}/{3}.{0}_{1}.gif";

		/// <summary>
		/// RealtimeImg(長周期地震動階級)のベースURL
		/// <para>0:タイプ</para>
		/// <para>1:日付</para>
		/// <para>2:時間</para>
		/// </summary>
		public const string LpgmRealtimeBase = "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/RealTimeImg/{0}_s/{1}/{2}.{0}_s.gif";

		/// <summary>
		/// LongPeriodImg 長周期地震動の予測階級のベースURL
		/// <para>0:日付</para>
		/// <para>1:時間</para>
		/// </summary>
		public const string LongPeriodBase = "https://www.lmoni.bosai.go.jp/monitor/data/data/map_img/LongPeriodImg/eew/m2/{0}/{1}.eew_m2.abrspmx.gif";

		/// <summary>
		/// 予想震度のベースURL
		/// <para>0:日付</para>
		/// <para>1:時間</para>
		/// </summary>
		public const string EstShindoBase = "https://smi.lmoniexp.bosai.go.jp/data/map_img/EstShindoImg/eew/{0}/{1}.eew.gif";

		/// <summary>
		/// 与えられた値を使用してURLを生成します。
		/// </summary>
		/// <param name="urlType">生成するURLのタイプ</param>
		/// <param name="datetime">生成するURLの時間</param>
		/// <param name="realtimeShindoType">(UrlType=RealtimeImg/LpgmRealtimeImgの際に使用)取得するリアルタイム情報の種類</param>
		/// <param name="isBerehole">(UrlType=RealtimeImgの際に使用)地中の情報を取得するかどうか</param>
		/// <returns></returns>
		public static string Generate(LpgmWebApiUrlType urlType, DateTime datetime, RealtimeDataType realtimeShindoType = RealtimeDataType.Shindo, bool isBerehole = false) => urlType switch
		{
			LpgmWebApiUrlType.RealtimeImg => string.Format(RealtimeBase, realtimeShindoType.ToUrlString(), isBerehole ? "b" : "s", datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss")),
			LpgmWebApiUrlType.LpgmRealtimeImg => string.Format(LpgmRealtimeBase, realtimeShindoType.ToUrlString(), datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss")),
			LpgmWebApiUrlType.EstShindo => string.Format(EstShindoBase, datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss")),
			LpgmWebApiUrlType.PSWave => string.Format(PsWaveBase, datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss")),
			LpgmWebApiUrlType.EewJson => string.Format(JsonEewBase, datetime.ToString("yyyyMMddHHmmss")),
			LpgmWebApiUrlType.LongPeriodImg => string.Format(LongPeriodBase, datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss")),
			_ => throw new ArgumentException($"URLを生成できない{nameof(LpgmWebApiUrlType)}が指定されています", nameof(urlType)),
		};
	}
}