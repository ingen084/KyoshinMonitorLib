using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.V2
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
		public Result Result { get; set; }
		[DataMember(Name = "security")]
		public Security Security { get; set; }
	}
}
