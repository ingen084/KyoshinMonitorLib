using MessagePack;
using ProtoBuf;
using System.Drawing;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib
{
	[MessagePackObject, DataContract, ProtoContract]
	public struct Point2
	{
		public Point2(int x, int y)
		{
			X = x;
			Y = y;
		}

		[Key(0), DataMember(Order = 0), ProtoMember(1)]
		public int X { get; set; }

		[Key(1), DataMember(Order = 1), ProtoMember(2)]
		public int Y { get; set; }

		public override string ToString()
			=> $"X:{X} Y:{Y}";

		public static Point2 operator +(Point2 point, int num)
			=> new Point2(point.X + num, point.Y + num);

		public static Point2 operator -(Point2 point, int num)
			=> new Point2(point.X - num, point.Y - num);

		public static Point2 operator +(Point2 point1, Point2 point2)
			=> new Point2(point1.X + point2.X, point1.Y + point2.Y);

		public static Point2 operator -(Point2 point1, Point2 point2)
			=> new Point2(point1.X - point2.X, point1.Y - point2.Y);

		public static Point2 operator *(Point2 point, int num)
			=> new Point2(point.X * num, point.Y * num);

		public static Point2 operator /(Point2 point, int num)
			=> new Point2(point.X / num, point.Y / num);

		public static Point2 operator *(Point2 point1, Point2 point2)
			=> new Point2(point1.X * point2.X, point1.Y * point2.Y);

		public static Point2 operator /(Point2 point1, Point2 point2)
			=> new Point2(point1.X / point2.X, point1.Y / point2.Y);

		public static bool operator ==(Point2 point1, Point2 point2)
			=> point1.X == point2.X && point1.Y == point2.Y;

		public static bool operator !=(Point2 point1, Point2 point2)
			=> point1.X != point2.X || point1.Y != point2.Y;

		public override bool Equals(object obj)
			=> base.Equals(obj);

		public override int GetHashCode()
			=> base.GetHashCode();

		public static implicit operator Point2(Point point)
			=> new Point2(point.X, point.Y);

		public static implicit operator Point(Point2 point)
			=> new Point(point.X, point.Y);

		public static implicit operator Point2(Size point)
			=> new Point2(point.Width, point.Height);

		public static implicit operator Size(Point2 point)
			=> new Size(point.X, point.Y);
	}
}