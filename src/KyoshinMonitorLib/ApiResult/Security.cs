using System.Runtime.Serialization;

namespace KyoshinMonitorLib.ApiResult
{
	public class Security
	{
		[DataMember(Name = "realm")]
		public string Realm { get; set; }
		[DataMember(Name = "hash")]
		public string Hash { get; set; }
	}
}
