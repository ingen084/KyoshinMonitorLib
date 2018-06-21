using System;
using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class EstShindo
	{
		[DataMember(Name = "list")]
		public float[] List { get; set; }
		[DataMember(Name = "dataTime")]
		public DateTime DataTime { get; set; }
		[DataMember(Name = "hypoType")]
		public string HypoType { get; set; }
		[DataMember(Name = "baseData")]
		public string BaseData { get; set; }
		[DataMember(Name = "startMesh")]
		public string StartMesh { get; set; }
		[DataMember(Name = "startMeshIdx")]
		public int StartMeshIdx { get; set; }
		[DataMember(Name = "serialNo")]
		public string SerialNo { get; set; }

		[DataMember(Name = "security")]
		public Security Security { get; set; }
		[DataMember(Name = "result")]
		public Result Result { get; set; }
	}
}
