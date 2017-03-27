using System;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 新強震モニタのURL生成器
	/// </summary>
	public static class UrlGenerator
	{
		/// <summary>
		/// JsonEewのベースURL
		/// <para>0:時間</para>
		/// </summary>
		public static string JsonEewBase = "http://www.kmoni.bosai.go.jp/new/webservice/hypo/eew/{0}.json";

		/// <summary>
		/// PsWaveImgのベースURL
		/// <para>0:日付</para>
		/// <para>1:時間</para>
		/// </summary>
		public static string PsWaveBase = "http://www.kmoni.bosai.go.jp/new/data/map_img/PSWaveImg/eew/{0}/{1}.eew.gif";

		/// <summary>
		/// RealTimeImgのベースURL
		/// <para>0:タイプ</para>
		/// <para>1:地上(s)/地下(b)</para>
		/// <para>2:日付</para>
		/// <para>3:時間</para>
		/// </summary>
		public static string RealTimeBase = "http://www.kmoni.bosai.go.jp/new/data/map_img/RealTimeImg/{0}_{1}/{2}/{3}.{0}_{1}.gif";

		/// <summary>
		/// 予想震度のベースURL
		/// <para>0:日付</para>
		/// <para>1:時間</para>
		/// </summary>
		public static string EstShindoBase = "http://www.kmoni.bosai.go.jp/new/data/map_img/EstShindoImg/eew/{0}/{1}.eew.gif";

		/// <summary>
		/// 与えられた値を使用してURLを生成します。
		/// </summary>
		/// <param name="urlType">生成するURLのタイプ</param>
		/// <param name="datetime">生成するURLの時間</param>
		/// <param name="realTimeShindoType">(UrlType=RealTimeImgの際に使用)取得するリアルタイム情報の種類</param>
		/// <param name="isBerehole">(UrlType=RealTimeImgの際に使用)地中の情報を取得するかどうか</param>
		/// <returns></returns>
		public static string Generate(UrlType urlType, DateTime datetime,
			RealTimeImgType realTimeShindoType = RealTimeImgType.Shindo, bool isBerehole = false)
		{
			switch (urlType)
			{
				case UrlType.RealTimeImg:
					return string.Format(RealTimeBase, realTimeShindoType.ToUrlString(), isBerehole ? "b" : "s", datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss"));

				case UrlType.RestShindo:
					return string.Format(EstShindoBase, datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss"));

				case UrlType.PSWave:
					return string.Format(PsWaveBase, datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss"));

				case UrlType.EewJson:
					return string.Format(JsonEewBase, datetime.ToString("yyyyMMddHHmmss"));
			}
			return null;
		}
	}
}