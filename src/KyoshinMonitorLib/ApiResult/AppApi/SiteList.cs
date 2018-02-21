using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class SiteList
	{
		[DataMember(Name = "items")]
		public Site[] Sites { get; set; }
		[DataMember(Name = "security")]
		public Security Security { get; set; }
		[DataMember(Name = "dataTime")]
		public string DataTime { get; set; }
		[DataMember(Name = "result")]
		public Result Result { get; set; }
		[DataMember(Name = "serialNo")]
		public string SerialNo { get; set; }
	}

	public class Site
	{
		/// <summary>
		/// 不明(内部ID？)
		/// </summary>
		[DataMember(Name = "muni")]
		public int Muni { get; set; }
		/// <summary>
		/// RealTimeDataでのインデックスID
		/// </summary>
		[DataMember(Name = "siteidx")]
		public int Siteidx { get; set; }
		/// <summary>
		/// 都道府県ID
		/// </summary>
		[DataMember(Name = "pref")]
		public int PrefefectureId { get; set; }
		/// <summary>
		/// 都道府県
		/// </summary>
		[IgnoreDataMember]
		public Prefecture Prefefecture => (Prefecture)(Enum.ToObject(typeof(Prefecture), PrefefectureId) ?? Prefecture.Unknown);
		/// <summary>
		/// ID
		/// </summary>
		[DataMember(Name = "siteid")]
		public string SiteId { get; set; }
		/// <summary>
		/// 緯度
		/// </summary>
		[DataMember(Name = "lat")]
		public float Lat { get; set; }
		/// <summary>
		/// 経度
		/// </summary>
		[DataMember(Name = "lng")]
		public float Lng { get; set; }
	}

	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
	public class PrefectureNameAttribute : Attribute
	{
		public PrefectureNameAttribute(string shortName, string longName)
		{
			ShortName = shortName;
			LongName = longName;
		}

		public string ShortName { get; }
		public string LongName { get; }
	}

	public enum Prefecture
	{
		[PrefectureName("他", "その他")]
		Other,
		[PrefectureName("北海道", "北海道")]
		Hokkaido,
		[PrefectureName("青森", "青森県")]
		Aomori,
		[PrefectureName("岩手", "岩手県")]
		Iwate,
		[PrefectureName("宮城", "宮城県")]
		Miyagi,
		[PrefectureName("秋田", "秋田県")]
		Akita,
		[PrefectureName("山形", "山形県")]
		Yamagata,
		[PrefectureName("福島", "福島県")]
		Fukushima,
		[PrefectureName("茨城", "茨城県")]
		Ibaraki,
		[PrefectureName("栃木", "栃木県")]
		Tochigi,
		[PrefectureName("群馬", "群馬県")]
		Gunma,
		[PrefectureName("埼玉", "埼玉県")]
		Saitama,
		[PrefectureName("千葉", "千葉県")]
		Chiba,
		[PrefectureName("東京", "東京都")]
		Tokyo,
		[PrefectureName("神奈川", "神奈川県")]
		Kanagawa,
		[PrefectureName("新潟", "新潟県")]
		Niigata,
		[PrefectureName("富山", "富山県")]
		Toyama,
		[PrefectureName("石川", "石川県")]
		Ishikawa,
		[PrefectureName("福井", "福井県")]
		Fukui,
		[PrefectureName("山梨", "山梨県")]
		Yamanashi,
		[PrefectureName("長野", "長野県")]
		Nagano,
		[PrefectureName("岐阜", "岐阜県")]
		Gifu,
		[PrefectureName("静岡", "静岡県")]
		Shizuoka,
		[PrefectureName("愛知", "愛知県")]
		Aichi,
		[PrefectureName("三重", "三重県")]
		Mie,
		[PrefectureName("滋賀", "滋賀県")]
		Shiga,
		[PrefectureName("京都", "京都府")]
		Kyouto,
		[PrefectureName("大阪", "大阪府")]
		Osaka,
		[PrefectureName("兵庫", "兵庫県")]
		Hyogo,
		[PrefectureName("奈良", "奈良県")]
		Nara,
		[PrefectureName("和歌山", "和歌山県")]
		Wakayama,
		[PrefectureName("鳥取", "鳥取県")]
		Tottori,
		[PrefectureName("島根", "島根県")]
		Shimane,
		[PrefectureName("岡山", "岡山県")]
		Okayama,
		[PrefectureName("広島", "広島県")]
		Hiroshima,
		[PrefectureName("山口", "山口県")]
		Yamaguchi,
		[PrefectureName("徳島", "徳島県")]
		Tokushima,
		[PrefectureName("香川", "香川県")]
		Kagawa,
		[PrefectureName("愛媛", "愛媛県")]
		Ehime,
		[PrefectureName("高知", "高知県")]
		Kouchi,
		[PrefectureName("福岡", "福岡県")]
		Fukuoka,
		[PrefectureName("佐賀", "佐賀県")]
		Saga,
		[PrefectureName("長崎", "長崎県")]
		Nagasaki,
		[PrefectureName("熊本", "熊本県")]
		Kumamoto,
		[PrefectureName("大分", "大分県")]
		Oita,
		[PrefectureName("宮崎", "宮崎県")]
		Miyazaki,
		[PrefectureName("鹿児島", "鹿児島県")]
		Kagoshima,
		[PrefectureName("沖縄", "沖縄県")]
		Okinawa,
		[PrefectureName("不明", "不明")]
		Unknown = 99,
	}

	public static class PrefectureExtensions
	{
		public static string GetLongName(this Prefecture pref)
		{
			var attr = pref.GetType().GetTypeInfo().GetCustomAttribute<PrefectureNameAttribute>();
			if (attr == null)
				return "不明";
			return attr.LongName;
		}
		public static string GetShortName(this Prefecture pref)
		{
			var attr = pref.GetType().GetTypeInfo().GetCustomAttribute<PrefectureNameAttribute>();
			if (attr == null)
				return "不明";
			return attr.ShortName;
		}
	}
}
