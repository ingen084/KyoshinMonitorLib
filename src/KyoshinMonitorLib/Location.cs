using MessagePack;
using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 地理座標
	/// </summary>
	[MessagePackObject, DataContract]
	public class Location
	{
		/// <summary>
		/// Locationを初期化します。
		/// </summary>
		public Location()
		{
		}

		/// <summary>
		/// 初期値を指定してLocationを初期化します。
		/// </summary>
		/// <param name="latitude">緯度</param>
		/// <param name="longitude">経度</param>
		public Location(float latitude, float longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}
		/// <summary>
		/// メートル座標を指定してLocationを初期化します。
		/// </summary>
		/// <param name="x">北緯方向への距離</param>
		/// <param name="y">東経方向への距離</param>
		public static Location FromMeters(double x, double y)
		{
			x = x / 20037508.34 * 180;
			y = y / 20037508.34 * 180;
			x = 180 / Math.PI * (2 * Math.Atan(Math.Exp(x * Math.PI / 180)) - Math.PI / 2);

			return new Location((float)x, (float)y);
		}

		/// <summary>
		/// 緯度
		/// </summary>
		[Key(0), DataMember(Order = 0)]
		public float Latitude { get; set; }

		/// <summary>
		/// 経度
		/// </summary>
		[Key(1), DataMember(Order = 1)]
		public float Longitude { get; set; }

		/// <summary>
		/// 文字化
		/// </summary>
		/// <returns>文字</returns>
		public override string ToString()
			=> $"Lat:{Latitude} Lng:{Longitude}";
	}
}