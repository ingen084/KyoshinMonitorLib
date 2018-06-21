using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class PSWave
	{
		[DataMember(Name = "dataTime")]
		public DateTime DataTime { get; set; }
		[DataMember(Name = "hypoType")]
		public string HypoType { get; set; }
		[DataMember(Name = "items")]
		public PSWaveItem[] Items { get; set; }

		[DataMember(Name = "result")]
		public Result Result { get; set; }
		[DataMember(Name = "security")]
		public Security Security { get; set; }
	}

	public class PSWaveItem
	{
		[DataMember(Name = "latitude")]
		public string Latitude { get; set; }
		[DataMember(Name = "longitude")]
		public string Longitude { get; set; }
		[DataMember(Name = "p_radius")]
		public string PWaveRadius { get; set; }
		[DataMember(Name = "s_radius")]
		public string SWaveRadius { get; set; }
	}
}
