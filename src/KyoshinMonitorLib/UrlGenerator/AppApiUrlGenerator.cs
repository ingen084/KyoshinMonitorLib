﻿using System;

namespace KyoshinMonitorLib.UrlGenerator
{
	/// <summary>
	/// 強震モニタスマホアプリのURL生成器
	/// </summary>
	public class AppApiUrlGenerator
	{
		/// <summary>
		/// リアルタイムJsonのベースURL
		/// <para>0: 種別(jma_sなど)</para>
		/// <para>1: s=地上 b=地下</para>
		/// <para>2: yyyyMMdd</para>
		/// <para>3: yyyyMMddHHmmss</para>
		/// </summary>
		public const string RealtimeDataBase = "http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/RealTimeData/kyoshin_cnt/{0}_{1}/{2}/{3}_{0}_{1}.json";

		/// <summary>
		/// EEW関係のJsonのベースURL
		/// <para>0: データタイプ(EstShindoJsonV2など)</para>
		/// <para>1: yyyyMMdd</para>
		/// <para>2: yyyyMMddHHmmss</para>
		/// <para>3: データタイプの略称(estなど)</para>
		/// </summary>
		public const string EewJsonBase = "http://kv.kmoni.bosai.go.jp/kyoshin_monitor/static/jsondata/{0}/eew/{1}/{2}_eew_{3}.json";

		/// <summary>
		/// 観測点一覧のベースURL
		/// <para>0: baseSerialNo</para>
		/// </summary>
		public const string SiteListBase = "http://ts.qtmoni.bosai.go.jp/qt/tsapp/kyoshin_monitor/static/sip_data/site_list/eq/{0}.json";

		/// <summary>
		/// メッシュ一覧のURL
		/// </summary>
		public const string Meches = "http://kv.kmoni.bosai.go.jp/webservice/est/mesh_v2/list.json";

#pragma warning disable CS0618 // 型またはメンバーが旧型式です
		/// <summary>
		/// 与えられた値を使用してURLを生成します。
		/// </summary>
		/// <param name="urlType">生成するURLのタイプ</param>
		/// <param name="datetime">生成するURLの時間</param>
		/// <param name="realtimeShindoType">(UrlType=RealtimeDataの際に使用)取得するリアルタイム情報の種類</param>
		/// <param name="isBerehole">(UrlType=RealtimeDataの際に使用)地中の情報を取得するかどうか</param>
		/// <returns></returns>
		public static string Generate(AppApiUrlType urlType, DateTime datetime, RealtimeDataType realtimeShindoType = RealtimeDataType.Shindo, bool isBerehole = false) => urlType switch
		{
			AppApiUrlType.RealtimeData => string.Format(RealtimeDataBase, realtimeShindoType.ToUrlString(), isBerehole ? "b" : "s", datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss")),
			AppApiUrlType.EstShindoJson => string.Format(EewJsonBase, "EstShindoJsonV2", datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss"), "est"),
			AppApiUrlType.PSWaveJson => string.Format(EewJsonBase, "PSWaveJsonV2", datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss"), "psw"),
			AppApiUrlType.HypoInfoJson => string.Format(EewJsonBase, "HypoInfoJsonV2", datetime.ToString("yyyyMMdd"), datetime.ToString("yyyyMMddHHmmss"), "hypo"),
			_ => throw new ArgumentException($"URLを生成できない{nameof(AppApiUrlType)}が指定されています", nameof(urlType)),
		};
#pragma warning restore CS0618 // 型またはメンバーが旧型式です
		/// <summary>
		/// 観測点一覧のURLを生成します。
		/// </summary>
		/// <param name="baseSerialNo">シリアル番号</param>
		/// <returns></returns>
		public static string Generate(string baseSerialNo)
			=> string.Format(SiteListBase, baseSerialNo);
	}
}
