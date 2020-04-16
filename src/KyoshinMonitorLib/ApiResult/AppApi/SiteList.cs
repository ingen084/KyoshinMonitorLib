using System;
using System.Text.Json.Serialization;
using System.Reflection;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	/// <summary>
	/// 観測点一覧
	/// </summary>
	public class SiteList
	{
		/// <summary>
		/// 観測点一覧
		/// </summary>
		[JsonPropertyName("items")]
		public Site[] Sites { get; set; }
		/// <summary>
		/// セキュリティ情報
		/// </summary>
		[JsonPropertyName("security")]
		public Security Security { get; set; }
		/// <summary>
		/// 時間
		/// </summary>
		[JsonPropertyName("dataTime")]
		public string DataTime { get; set; }
		/// <summary>
		/// リザルト
		/// </summary>
		[JsonPropertyName("result")]
		public Result Result { get; set; }
		/// <summary>
		/// シリアル番号
		/// </summary>
		[JsonPropertyName("serialNo")]
		public string SerialNo { get; set; }
	}

	/// <summary>
	/// APIの観測点
	/// </summary>
	public class Site
	{
		/// <summary>
		/// 不明(内部ID？)
		/// </summary>
		[JsonPropertyName("muni")]
		public int Muni { get; set; }
		/// <summary>
		/// RealtimeDataでのインデックス
		/// </summary>
		[JsonPropertyName("siteidx")]
		public int Siteidx { get; set; }
		/// <summary>
		/// 都道府県ID
		/// </summary>
		[JsonPropertyName("pref")]
		public int PrefefectureId { get; set; }
		/// <summary>
		/// 都道府県
		/// </summary>
		[JsonIgnore]
		public Prefecture Prefefecture => (Prefecture)(Enum.ToObject(typeof(Prefecture), PrefefectureId) ?? Prefecture.Unknown);
		/// <summary>
		/// ID
		/// </summary>
		[JsonPropertyName("siteid")]
		public string SiteId { get; set; }
		/// <summary>
		/// 緯度
		/// </summary>
		[JsonPropertyName("lat")]
		public float Lat { get; set; }
		/// <summary>
		/// 経度
		/// </summary>
		[JsonPropertyName("lng")]
		public float Lng { get; set; }
	}

	/// <summary>
	/// 都道府県の名前
	/// </summary>
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public sealed class PrefectureNameAttribute : Attribute
	{
		/// <summary>
		/// 初期化
		/// </summary>
		/// <param name="shortName"></param>
		/// <param name="longName"></param>
		public PrefectureNameAttribute(string shortName, string longName)
		{
			ShortName = shortName;
			LongName = longName;
		}

		/// <summary>
		/// 短縮名
		/// </summary>
		public string ShortName { get; }
		/// <summary>
		/// 正式名称
		/// </summary>
		public string LongName { get; }
	}

	/// <summary>
	/// 都道府県
	/// </summary>
	public enum Prefecture
	{
		/// <summary>
		/// その他
		/// </summary>
		[PrefectureName("他", "その他")]
		Other,
		/// <summary>
		/// 北海道
		/// </summary>
		[PrefectureName("北海道", "北海道")]
		Hokkaido,
		/// <summary>
		/// 青森県
		/// </summary>
		[PrefectureName("青森", "青森県")]
		Aomori,
		/// <summary>
		/// 岩手県
		/// </summary>
		[PrefectureName("岩手", "岩手県")]
		Iwate,
		/// <summary>
		/// 宮城県
		/// </summary>
		[PrefectureName("宮城", "宮城県")]
		Miyagi,
		/// <summary>
		/// 秋田県
		/// </summary>
		[PrefectureName("秋田", "秋田県")]
		Akita,
		/// <summary>
		/// 山形県
		/// </summary>
		[PrefectureName("山形", "山形県")]
		Yamagata,
		/// <summary>
		/// 福島県
		/// </summary>
		[PrefectureName("福島", "福島県")]
		Fukushima,
		/// <summary>
		/// 茨城県
		/// </summary>
		[PrefectureName("茨城", "茨城県")]
		Ibaraki,
		/// <summary>
		/// 栃木県
		/// </summary>
		[PrefectureName("栃木", "栃木県")]
		Tochigi,
		/// <summary>
		/// 群馬県
		/// </summary>
		[PrefectureName("群馬", "群馬県")]
		Gunma,
		/// <summary>
		/// 埼玉県
		/// </summary>
		[PrefectureName("埼玉", "埼玉県")]
		Saitama,
		/// <summary>
		/// 千葉県
		/// </summary>
		[PrefectureName("千葉", "千葉県")]
		Chiba,
		/// <summary>
		/// 東京都
		/// </summary>
		[PrefectureName("東京", "東京都")]
		Tokyo,
		/// <summary>
		/// 神奈川県
		/// </summary>
		[PrefectureName("神奈川", "神奈川県")]
		Kanagawa,
		/// <summary>
		/// 新潟県
		/// </summary>
		[PrefectureName("新潟", "新潟県")]
		Niigata,
		/// <summary>
		/// 富山県
		/// </summary>
		[PrefectureName("富山", "富山県")]
		Toyama,
		/// <summary>
		/// 石川県
		/// </summary>
		[PrefectureName("石川", "石川県")]
		Ishikawa,
		/// <summary>
		/// 福井県
		/// </summary>
		[PrefectureName("福井", "福井県")]
		Fukui,
		/// <summary>
		/// 山梨県
		/// </summary>
		[PrefectureName("山梨", "山梨県")]
		Yamanashi,
		/// <summary>
		/// 長野県
		/// </summary>
		[PrefectureName("長野", "長野県")]
		Nagano,
		/// <summary>
		/// 岐阜県
		/// </summary>
		[PrefectureName("岐阜", "岐阜県")]
		Gifu,
		/// <summary>
		/// 静岡県
		/// </summary>
		[PrefectureName("静岡", "静岡県")]
		Shizuoka,
		/// <summary>
		/// 愛知県
		/// </summary>
		[PrefectureName("愛知", "愛知県")]
		Aichi,
		/// <summary>
		/// 三重県
		/// </summary>
		[PrefectureName("三重", "三重県")]
		Mie,
		/// <summary>
		/// 滋賀県
		/// </summary>
		[PrefectureName("滋賀", "滋賀県")]
		Shiga,
		/// <summary>
		/// 京都府
		/// </summary>
		[PrefectureName("京都", "京都府")]
		Kyouto,
		/// <summary>
		/// 大阪府
		/// </summary>
		[PrefectureName("大阪", "大阪府")]
		Osaka,
		/// <summary>
		/// 兵庫県
		/// </summary>
		[PrefectureName("兵庫", "兵庫県")]
		Hyogo,
		/// <summary>
		/// 奈良県
		/// </summary>
		[PrefectureName("奈良", "奈良県")]
		Nara,
		/// <summary>
		/// 和歌山県
		/// </summary>
		[PrefectureName("和歌山", "和歌山県")]
		Wakayama,
		/// <summary>
		/// 鳥取県
		/// </summary>
		[PrefectureName("鳥取", "鳥取県")]
		Tottori,
		/// <summary>
		/// 島根県
		/// </summary>
		[PrefectureName("島根", "島根県")]
		Shimane,
		/// <summary>
		/// 岡山県
		/// </summary>
		[PrefectureName("岡山", "岡山県")]
		Okayama,
		/// <summary>
		/// 広島県
		/// </summary>
		[PrefectureName("広島", "広島県")]
		Hiroshima,
		/// <summary>
		/// 山口県
		/// </summary>
		[PrefectureName("山口", "山口県")]
		Yamaguchi,
		/// <summary>
		/// 徳島県
		/// </summary>
		[PrefectureName("徳島", "徳島県")]
		Tokushima,
		/// <summary>
		/// 香川県
		/// </summary>
		[PrefectureName("香川", "香川県")]
		Kagawa,
		/// <summary>
		/// 愛媛県
		/// </summary>
		[PrefectureName("愛媛", "愛媛県")]
		Ehime,
		/// <summary>
		/// 高知県
		/// </summary>
		[PrefectureName("高知", "高知県")]
		Kouchi,
		/// <summary>
		/// 福岡県
		/// </summary>
		[PrefectureName("福岡", "福岡県")]
		Fukuoka,
		/// <summary>
		/// 佐賀県
		/// </summary>
		[PrefectureName("佐賀", "佐賀県")]
		Saga,
		/// <summary>
		/// 長崎県
		/// </summary>
		[PrefectureName("長崎", "長崎県")]
		Nagasaki,
		/// <summary>
		/// 熊本県
		/// </summary>
		[PrefectureName("熊本", "熊本県")]
		Kumamoto,
		/// <summary>
		/// 大分県
		/// </summary>
		[PrefectureName("大分", "大分県")]
		Oita,
		/// <summary>
		/// 宮崎県
		/// </summary>
		[PrefectureName("宮崎", "宮崎県")]
		Miyazaki,
		/// <summary>
		/// 鹿児島県
		/// </summary>
		[PrefectureName("鹿児島", "鹿児島県")]
		Kagoshima,
		/// <summary>
		/// 沖縄県
		/// </summary>
		[PrefectureName("沖縄", "沖縄県")]
		Okinawa,
		/// <summary>
		/// 不明
		/// </summary>
		[PrefectureName("不明", "不明")]
		Unknown = 99,
	}
	/// <summary>
	/// 都道府県のenumに対する拡張
	/// </summary>
	public static class PrefectureExtensions
	{
		/// <summary>
		/// 正式名称を取得する
		/// </summary>
		public static string GetLongName(this Prefecture pref)
		{
			var attr = pref.GetType().GetField(pref.ToString()).GetCustomAttribute<PrefectureNameAttribute>();
			if (attr == null)
				return "不明";
			return attr.LongName;
		}
		/// <summary>
		/// 短縮名を取得する
		/// </summary>
		public static string GetShortName(this Prefecture pref)
		{
			var attr = pref.GetType().GetField(pref.ToString()).GetCustomAttribute<PrefectureNameAttribute>();
			if (attr == null)
				return "不明";
			return attr.ShortName;
		}
	}
}
