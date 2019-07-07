using Newtonsoft.Json;
using System;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class EstShindo
	{
		[JsonProperty("list")]
		public float[] List { get; set; }
		[JsonProperty("dataTime")]
		public DateTime DataTime { get; set; }
		[JsonProperty("hypoType")]
		public string HypoType { get; set; }
		[JsonProperty("baseData")]
		public string BaseData { get; set; }
		[JsonProperty("startMesh")]
		public string StartMesh { get; set; }
		[JsonProperty("startMeshIdx")]
		public int StartMeshIdx { get; set; }
		[JsonProperty("serialNo")]
		public string SerialNo { get; set; }

		[JsonProperty("security")]
		public Security Security { get; set; }
		[JsonProperty("result")]
		public Result Result { get; set; }
	}
}
