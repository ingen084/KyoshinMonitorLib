using Newtonsoft.Json;
using System;

namespace KyoshinMonitorLib.ApiResult.AppApi
{
	[Obsolete("このAPIは現在利用できなくなっています。")]
	public class MeshList
	{
		[JsonProperty("items")]
		public object[][] Items { get; set; }
		[JsonProperty("dataTime")]
		public DateTime DataTime { get; set; }
		[JsonProperty("serialNo")]
		public string SerialNo { get; set; }

		[JsonProperty("result")]
		public Result Result { get; set; }
	}
}
