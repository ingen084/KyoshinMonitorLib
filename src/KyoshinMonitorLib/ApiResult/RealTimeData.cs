using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	public class RealTimeData
	{
		[DataMember(Name = "dataTime")]
		public DateTime DateTime { get; set; }
		[DataMember(Name = "packetType")]
		public string PacketType { get; set; }
		[DataMember(Name = "kyoshinType")]
		public string KyoshinType { get; set; }
		[DataMember(Name = "baseData")]
		public string BaseData { get; set; }
		[DataMember(Name = "baseSerialNo")]
		public string BaseSerialNo { get; set; }
		[DataMember(Name = "items")]
		public float?[] Items { get; set; }
		[DataMember(Name = "result")]
		public RealTimeDataResult Result { get; set; }
		[DataMember(Name = "security")]
		public Security Security { get; set; }
	}

	public class RealTimeDataResult
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }
		[DataMember(Name = "message")]
		public string Message { get; set; }
	}

	public class Security
	{
		[DataMember(Name = "realm")]
		public string Realm { get; set; }
		[DataMember(Name = "hash")]
		public string Hash { get; set; }
	}

}
