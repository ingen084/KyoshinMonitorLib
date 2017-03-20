using ProtoBuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KyoshinMonitorLib
{
	[ProtoContract]
	public class ObservationPoint : IComparable
	{
		/// <summary>
		/// 観測点情報をpbfから読み込みます。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">読み込むファイルのパス</param>
		/// <returns>読み込まれた観測点情報</returns>
		public static ObservationPoint[] LoadFromPbf(string path)
		{
			using (var stream = new FileStream(path, FileMode.Open))
				return Serializer.Deserialize<ObservationPoint[]>(stream);
		}

		/// <summary>
		/// 観測点情報をcsvから読み込みます。ファイルが存在しないなどの場合は例外がスローされます。
		/// </summary>
		/// <param name="path">読み込むファイルのパス</param>
		/// <param name="encoding">読み込むファイル文字コード 何も指定していない場合はUTF8が使用されます。</param>
		/// <returns>list:読み込まれた観測点情報 success:読み込みに成功した項目のカウント error:読み込みに失敗した項目のカウント</returns>
		public static (ObservationPoint[] points, uint success, uint error) LoadFromCsv(string path, Encoding encoding = null)
		{
			var addedCount = 0u;
			var errorCount = 0u;

			var points = new List<ObservationPoint>();

			using (var reader = new StreamReader(path))
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
						points.Add(point);
						addedCount++;
					}
					catch
					{
						errorCount++;
					}

			return (points.ToArray(), addedCount, errorCount);
		}

		public ObservationPoint()
		{
		}

		public ObservationPoint(ObservationPoint point) :
			this(point.Type, point.Code, point.IsSuspended, point.Name, point.Region, point.Location, point.Point, point.ClassificationId, point.PrefectureClassificationId)
		{
		}

		public ObservationPoint(ObservationPointType type, string code, bool isSuspended, string name, string region, Location location, Point2? point = null, int? classId = null, int? prefClassId = null)
		{
			Type = type;
			Code = code;
			IsSuspended = isSuspended;
			Name = name;
			Region = region;
			Location = location;
			Point = point;
			ClassificationId = classId;
			PrefectureClassificationId = prefClassId;
		}

		/// <summary>
		/// 観測地点のネットワークの種類
		/// </summary>
		[ProtoMember(1)]
		public ObservationPointType Type { get; set; }

		/// <summary>
		/// 観測点コード
		/// </summary>
		[ProtoMember(2)]
		public string Code { get; set; }

		/// <summary>
		/// 観測地点が休止状態(無効)かどうか
		/// </summary>
		[ProtoMember(3)]
		public bool IsSuspended { get; set; }

		/// <summary>
		/// 観測点名
		/// </summary>
		[ProtoMember(4)]
		public string Name { get; set; }

		/// <summary>
		/// 観測点広域名
		/// </summary>
		[ProtoMember(5)]
		public string Region { get; set; }

		/// <summary>
		/// 地理座標
		/// </summary>
		[ProtoMember(6)]
		public Location Location { get; set; }

		/// <summary>
		/// 強震モニタ画像上での座標
		/// </summary>
		[ProtoMember(7)]
		public Point2? Point { get; set; }

		/// <summary>
		/// 緊急地震速報や震度速報で用いる区域のID(EqWatchインポート用)
		/// </summary>
		[ProtoMember(8, IsRequired = false)]
		public int? ClassificationId { get; set; }

		/// <summary>
		/// 緊急地震速報で用いる府県予報区のID(EqWatchインポート用)
		/// </summary>
		[ProtoMember(9, IsRequired = false)]
		public int? PrefectureClassificationId { get; set; }
		public int CompareTo(object obj)
		{
			if (!(obj is ObservationPoint ins))
				throw new ArgumentException("比較対象はObservationPointでなければなりません。");
			return Code.CompareTo(ins.Code);
		}
	}
}