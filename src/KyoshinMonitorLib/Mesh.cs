using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MessagePack;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 5kmメッシュ情報
	/// </summary>
	public class Mesh
	{
		/// <summary>
		/// 初期化
		/// </summary>
		public Mesh()
		{
		}
		/// <summary>
		/// 初期化
		/// </summary>
		public Mesh(string code, Location location1, Location location2)
		{
			Code = code ?? throw new ArgumentNullException(nameof(code));
			LocationLeftTop = location1 ?? throw new ArgumentNullException(nameof(location1));
			LocationRightBottom = location2 ?? throw new ArgumentNullException(nameof(location2));
		}
		/// <summary>
		/// 初期化
		/// </summary>
		public Mesh(string code, double x, double y)
		{
			Code = code ?? throw new ArgumentNullException(nameof(code));

			LocationLeftTop = Location.FromMeters(x, y);
			LocationRightBottom = Location.FromMeters(x - 5334, y + 5334); //5kmメッシュ ちょっと大きめに設定する
		}

		/// <summary>
		/// メッシュ情報をmpkから読み込みます。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">読み込むmpkファイルのパス</param>
		/// <param name="useLz4">lz4で圧縮させるかどうか(させる場合は拡張子を.mpk.lz4にすることをおすすめします)</param>
		/// <returns>読み込まれたメッシュ情報</returns>
		public static ObservationPoint[] LoadFromMpk(string path, bool useLz4 = false)
		{
			using var stream = new FileStream(path, FileMode.Open);
			return useLz4
				? LZ4MessagePackSerializer.Deserialize<ObservationPoint[]>(stream)
				: MessagePackSerializer.Deserialize<ObservationPoint[]>(stream);
		}
		/// <summary>
		/// メッシュ情報をmpk形式で保存します。失敗した場合は例外がスローされます。
		/// </summary>
		/// <param name="path">書き込むmpkファイルのパス</param>
		/// <param name="points">書き込むメッシュ情報の配列</param>
		/// <param name="useLz4">lz4で圧縮させるかどうか(させる場合は拡張子を.mpk.lz4にすることをおすすめします)</param>
		public static void SaveToMpk(string path, IEnumerable<Mesh> points, bool useLz4 = false)
		{
			using var stream = new FileStream(path, FileMode.Create);
			if (useLz4)
				LZ4MessagePackSerializer.Serialize(stream, points.ToArray());
			else
				MessagePackSerializer.Serialize(stream, points.ToArray());
		}


		/// <summary>
		/// 地点コード
		/// </summary>
		[Key(0)]
		public string Code { get; set; }
		/// <summary>
		/// 座標(左上)
		/// </summary>
		[Key(1)]
		public Location LocationLeftTop { get; set; }
		/// <summary>
		/// 座標(右下)
		/// </summary>
		[Key(2)]
		public Location LocationRightBottom { get; set; }
	}
}
