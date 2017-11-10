using MessagePack;
#if !NETFX_CORE
using ProtoBuf;
#endif
using System.Drawing;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// シリアライズ+四則演算をできるようにしたPointクラス
	/// </summary>
	[MessagePackObject, DataContract
#if !NETFX_CORE
			, ProtoContract
#endif
			]
	public struct Point2
	{
		/// <summary>
		/// Point2を初期化します。
		/// </summary>
		/// <param name="x">X</param>
		/// <param name="y">Y</param>
		public Point2(int x, int y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// X座標
		/// </summary>
		[Key(0), DataMember(Order = 0)
#if !NETFX_CORE
			, ProtoMember(1)
#endif
			]
		public int X { get; set; }

		/// <summary>
		/// Y座標
		/// </summary>
		[Key(1), DataMember(Order = 1)
#if !NETFX_CORE
			, ProtoMember(2)
#endif
			]
		public int Y { get; set; }

		/// <summary>
		/// 文字にします。
		/// </summary>
		/// <returns>文字</returns>
		public override string ToString()
			=> $"X:{X} Y:{Y}";

		/// <summary>
		/// 整数とPoint2を足します。
		/// </summary>
		/// <param name="point"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static Point2 operator +(Point2 point, int num)
			=> new Point2(point.X + num, point.Y + num);

		/// <summary>
		/// Point2から整数を引きます。
		/// </summary>
		/// <param name="point"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static Point2 operator -(Point2 point, int num)
			=> new Point2(point.X - num, point.Y - num);

		/// <summary>
		/// Point2同士を足します。
		/// </summary>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <returns></returns>
		public static Point2 operator +(Point2 point1, Point2 point2)
			=> new Point2(point1.X + point2.X, point1.Y + point2.Y);

		/// <summary>
		/// Point2同士を引きます。
		/// </summary>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <returns></returns>
		public static Point2 operator -(Point2 point1, Point2 point2)
			=> new Point2(point1.X - point2.X, point1.Y - point2.Y);

		/// <summary>
		/// Point2を整数で掛けます。
		/// </summary>
		/// <param name="point"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static Point2 operator *(Point2 point, int num)
			=> new Point2(point.X * num, point.Y * num);

		/// <summary>
		/// Point2を整数で割ります。
		/// </summary>
		/// <param name="point"></param>
		/// <param name="num"></param>
		/// <returns></returns>
		public static Point2 operator /(Point2 point, int num)
			=> new Point2(point.X / num, point.Y / num);

		/// <summary>
		/// Point2同士を掛けます。
		/// </summary>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <returns></returns>
		public static Point2 operator *(Point2 point1, Point2 point2)
			=> new Point2(point1.X * point2.X, point1.Y * point2.Y);

		/// <summary>
		/// Point2同士を割ります。
		/// </summary>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <returns></returns>
		public static Point2 operator /(Point2 point1, Point2 point2)
			=> new Point2(point1.X / point2.X, point1.Y / point2.Y);

		/// <summary>
		/// 2つのPoint2が同じ値かどうかを判断します。
		/// </summary>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <returns></returns>
		public static bool operator ==(Point2 point1, Point2 point2)
			=> point1.X == point2.X && point1.Y == point2.Y;

		/// <summary>
		/// 2つのPoint2が違う値かどうかを判断します。
		/// </summary>
		/// <param name="point1"></param>
		/// <param name="point2"></param>
		/// <returns></returns>
		public static bool operator !=(Point2 point1, Point2 point2)
			=> point1.X != point2.X || point1.Y != point2.Y;

		/// <summary>
		/// Eq
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
			=> base.Equals(obj);

		/// <summary>
		/// ハッシュコードを取得します。
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode()
			=> base.GetHashCode();

		/// <summary>
		/// PointからPoint2への自動キャスト
		/// </summary>
		/// <param name="point"></param>
		public static implicit operator Point2(Point point)
			=> new Point2(point.X, point.Y);

		/// <summary>
		/// Point2からPointへの自動キャスト
		/// </summary>
		/// <param name="point"></param>
		public static implicit operator Point(Point2 point)
			=> new Point(point.X, point.Y);

		/// <summary>
		/// SizeからPoint2への自動キャスト
		/// </summary>
		/// <param name="point"></param>
		public static implicit operator Point2(Size point)
			=> new Point2(point.Width, point.Height);

		/// <summary>
		/// Point2からSizeへの自動キャスト
		/// </summary>
		/// <param name="point"></param>
		public static implicit operator Size(Point2 point)
			=> new Size(point.X, point.Y);
	}
}