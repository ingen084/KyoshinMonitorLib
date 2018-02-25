using MessagePack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Utf8Json;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// NIEDの観測点情報
	/// </summary>
	[MessagePackObject, DataContract]
	public class ObservationPoint : IComparable
	{
		/// <summary>
		/// 観測点情報をmpkから読み込みます。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">読み込むmpkファイルのパス</param>
		/// <param name="useLz4">lz4で圧縮させるかどうか(させる場合は拡張子を.mpk.lz4にすることをおすすめします)</param>
		/// <returns>読み込まれた観測点情報</returns>
		public static ObservationPoint[] LoadFromMpk(string path, bool useLz4 = false)
		{
			using (var stream = new FileStream(path, FileMode.Open))
				return useLz4
					? LZ4MessagePackSerializer.Deserialize<ObservationPoint[]>(stream)
					: MessagePackSerializer.Deserialize<ObservationPoint[]>(stream);
		}

		/// <summary>
		/// 観測点情報をmpk形式で保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むmpkファイルのパス</param>
		/// <param name="points">書き込む観測点情報の配列</param>
		/// <param name="useLz4">lz4で圧縮させるかどうか(させる場合は拡張子を.mpk.lz4にすることをおすすめします)</param>
		public static void SaveToMpk(string path, IEnumerable<ObservationPoint> points, bool useLz4 = false)
		{
			using (var stream = new FileStream(path, FileMode.Create))
				if (useLz4)
					LZ4MessagePackSerializer.Serialize(stream, points.ToArray());
				else
					MessagePackSerializer.Serialize(stream, points.ToArray());
		}

		/// <summary>
		/// 観測点情報をJsonから読み込みます。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">読み込むJsonファイルのパス</param>
		/// <returns>読み込まれた観測点情報</returns>
		public static ObservationPoint[] LoadFromJson(string path)
		{
			using (var stream = new FileStream(path, FileMode.Open))
				return JsonSerializer.Deserialize<ObservationPoint[]>(stream);
		}

		/// <summary>
		/// 観測点情報をJson形式で保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むJsonファイルのパス</param>
		/// <param name="points">書き込む観測点情報の配列</param>
		public static void SaveToJson(string path, IEnumerable<ObservationPoint> points)
		{
			using (var stream = new FileStream(path, FileMode.Create))
				JsonSerializer.Serialize(stream, points.ToArray());
		}

		/// <summary>
		/// 観測点情報をcsvから読み込みます。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">読み込むcsvファイルのパス</param>
		/// <param name="encoding">読み込むファイル文字コード 何も指定していない場合はUTF8が使用されます。</param>
		/// <returns>list:読み込まれた観測点情報 success:読み込みに成功した項目のカウント error:読み込みに失敗した項目のカウント</returns>
		public static (ObservationPoint[] points, uint success, uint error) LoadFromCsv(string path, Encoding encoding = null)
		{
			var addedCount = 0u;
			var errorCount = 0u;

			var points = new List<ObservationPoint>();

			using (var stream = File.OpenRead(path))
			using (var reader = new StreamReader(stream))
				while (reader.Peek() >= 0)
					try
					{
						var strings = reader.ReadLine().Split(',');

						var point = new ObservationPoint()
						{
							Type = (ObservationPointType)int.Parse(strings[0]),
							Code = strings[1],
							IsSuspended = bool.Parse(strings[2]),
							Name = strings[3],
							Region = strings[4],
							Location = new Location(float.Parse(strings[5]), float.Parse(strings[6])),
							Point = null
						};
						if (!string.IsNullOrWhiteSpace(strings[7]) && !string.IsNullOrWhiteSpace(strings[8]))
							point.Point = new Point2(int.Parse(strings[7]), int.Parse(strings[8]));
						if (strings.Length >= 11)
						{
							point.ClassificationId = int.Parse(strings[9]);
							point.PrefectureClassificationId = int.Parse(strings[10]);
						}
						if (strings.Length >= 13)
							point.OldLocation = new Location(float.Parse(strings[11]), float.Parse(strings[12]));

						points.Add(point);
						addedCount++;
					}
					catch
					{
						errorCount++;
					}

			return (points.ToArray(), addedCount, errorCount);
		}

		/// <summary>
		/// 観測点情報をcsvに保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むcsvファイルのパス</param>
		/// <param name="points">書き込む観測点情報の配列</param>
		public static void SaveToCsv(string path, IEnumerable<ObservationPoint> points)
		{
			using (var stream = File.OpenWrite(path))
			using (var writer = new StreamWriter(stream))
				foreach (var point in points)
					writer.WriteLine($"{(int)point.Type},{point.Code},{point.IsSuspended},{point.Name},{point.Region},{point.Location.Latitude},{point.Location.Longitude},{point.Point?.X.ToString() ?? ""},{point.Point?.Y.ToString() ?? ""},{point.ClassificationId?.ToString() ?? ""},{point.PrefectureClassificationId?.ToString() ?? ""},{point.OldLocation.Latitude},{point.OldLocation.Longitude}");
		}

		/// <summary>
		/// ObservationPointを初期化します。
		/// </summary>
		public ObservationPoint()
		{
		}

		/// <summary>
		/// 観測地点のネットワークの種類
		/// </summary>
		[Key(0), DataMember(Order = 0)]
		public ObservationPointType Type { get; set; }

		/// <summary>
		/// 観測点コード
		/// </summary>
		[Key(1), DataMember(Order = 1)]
		public string Code { get; set; }

		/// <summary>
		/// 観測点名
		/// </summary>
		[Key(2), DataMember(Order = 2)]
		public string Name { get; set; }

		/// <summary>
		/// 観測点広域名
		/// </summary>
		[Key(3), DataMember(Order = 3)]
		public string Region { get; set; }

		/// <summary>
		/// 観測地点が休止状態(無効)かどうか
		/// </summary>
		[Key(4), DataMember(Order = 4)]
		public bool IsSuspended { get; set; }

		/// <summary>
		/// 地理座標
		/// </summary>
		[Key(5), DataMember(Order = 5)]
		public Location Location { get; set; }

		/// <summary>
		/// 地理座標(日本座標系)
		/// </summary>
		[Key(9), DataMember(Order = 9)]
		public Location OldLocation { get; set; }

		/// <summary>
		/// 強震モニタ画像上での座標
		/// </summary>
		[Key(6), DataMember(Order = 6)]
		public Point2? Point { get; set; }

		/// <summary>
		/// 緊急地震速報や震度速報で用いる区域のID(EqWatchインポート用)
		/// </summary>
		[Key(7), DataMember(Order = 7)]
		public int? ClassificationId { get; set; }

		/// <summary>
		/// 緊急地震速報で用いる府県予報区のID(EqWatchインポート用)
		/// </summary>
		[Key(8), DataMember(Order = 8)]
		public int? PrefectureClassificationId { get; set; }

		/// <summary>
		/// ObservationPoint同士を比較します。
		/// </summary>
		/// <param name="obj">比較対象のObservationPoint</param>
		/// <returns></returns>
		public int CompareTo(object obj)
		{
			if (!(obj is ObservationPoint ins))
				throw new ArgumentException("比較対象はObservationPointにキャストできる型でなければなりません。");
			return Code.CompareTo(ins.Code);
		}
	}
}