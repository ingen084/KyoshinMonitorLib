using ProtoBuf;

namespace KyoshinMonitorLib
{
	[ProtoContract]
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

		[ProtoMember(1)]
		public float Latitude { get; set; }

		[ProtoMember(2)]
		public float Longitude { get; set; }

		public override string ToString()
			=> $"Lat:{Latitude} Lng:{Longitude}";
	}
}