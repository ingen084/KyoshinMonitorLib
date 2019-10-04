using System;
using System.Text.Json.Serialization;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	public class EstShindo
	{
		[JsonPropertyName("list")]
		public float[] List { get; set; }
		[JsonPropertyName("dataTime")]
		public DateTime DataTime { get; set; }
		[JsonPropertyName("hypoType")]
		public string HypoType { get; set; }
		[JsonPropertyName("baseData")]
		public string BaseData { get; set; }
		[JsonPropertyName("startMesh")]
		public string StartMesh { get; set; }
		[JsonPropertyName("startMeshIdx")]
		public int StartMeshIdx { get; set; }
		[JsonPropertyName("serialNo")]
		public string SerialNo { get; set; }

		[JsonPropertyName("security")]
		public Security Security { get; set; }
		[JsonPropertyName("result")]
		public Result Result { get; set; }
	}
}
