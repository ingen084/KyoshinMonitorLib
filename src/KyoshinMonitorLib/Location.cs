#if !WITHOUTMPK
using MessagePack;
#endif
#if !WITHOUTPBF
using ProtoBuf;
#endif

using System.Runtime.Serialization;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// 地理座標
	/// </summary>
	[
#if !WITHOUTMPK
		MessagePackObject,
#endif
		DataContract
#if !WITHOUTPBF
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
		[
#if !WITHOUTMPK
			Key(0),
#endif
				 DataMember(Order = 0)
#if !WITHOUTPBF
			, ProtoMember(1)
#endif
			]
		public float Latitude { get; set; }

		/// <summary>
		/// 経度
		/// </summary>
		[
#if !WITHOUTMPK
			Key(1),
#endif
				 DataMember(Order = 1)
#if !WITHOUTPBF
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