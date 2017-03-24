using MessagePack;
using ProtoBuf;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib
{
	[MessagePackObject, DataContract, ProtoContract]
	public class Location
	{
		public Location()
		{
		}

		public Location(float latitude, float longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		[Key(0), DataMember(Order = 0), ProtoMember(1)]
		public float Latitude { get; set; }

		[Key(1), DataMember(Order = 1), ProtoMember(2)]
		public float Longitude { get; set; }

		public override string ToString()
			=> $"Lat:{Latitude} Lng:{Longitude}";
	}
}