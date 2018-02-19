using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	public class Result
	{
		[DataMember(Name = "status")]
		public string Status { get; set; }
		[DataMember(Name = "message")]
		public string Message { get; set; }
	}
}
