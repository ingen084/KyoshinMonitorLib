using MessagePack;

#if !NETFX_CORE
using ProtoBuf;
#endif

using System.Runtime.Serialization;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 地理座標
	/// </summary>
	[MessagePackObject, DataContract
#if !NETFX_CORE
		, ProtoContract
#endif
		]
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
		/// 緯度
		/// </summary>
		[Key(0), DataMember(Order = 0)
#if !NETFX_CORE
			, ProtoMember(1)
#endif
			]
		public float Latitude { get; set; }

		/// <summary>
		/// 経度
		/// </summary>
		[Key(1), DataMember(Order = 1)
#if !NETFX_CORE
			, ProtoMember(2)
#endif
			]
		public float Longitude { get; set; }

		/// <summary>
		/// 文字化
		/// </summary>
		/// <returns>文字</returns>
		public override string ToString()
			=> $"Lat:{Latitude} Lng:{Longitude}";
	}
}