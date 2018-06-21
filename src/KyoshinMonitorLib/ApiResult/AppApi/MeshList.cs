using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class MeshList
	{
		[DataMember(Name = "items")]
		public object[][] Items { get; set; }
		[DataMember(Name = "dataTime")]
		public DateTime DataTime { get; set; }
		[DataMember(Name = "serialNo")]
		public string SerialNo { get; set; }

		[DataMember(Name = "result")]
		public Result Result { get; set; }
	}
}
