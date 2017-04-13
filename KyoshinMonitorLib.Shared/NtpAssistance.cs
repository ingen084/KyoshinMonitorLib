using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// Ntpによる時刻取得を補助するクラス
	/// </summary>
	public class NtpAssistance
	{
		/// <summary>
		/// Http通信を使用してネットワーク上から時刻を取得します。
		/// </summary>
		/// <param name="url">要求するURL NTPの時刻が生で返されるURLである必要があります。</param>
		/// <param name="timeout">タイムアウト時間(ミリ秒)</param>
		/// <returns>取得された時刻 取得に失敗した場合はDateTime.MinValueが返されます。</returns>
		public static async Task<DateTime> GetNetworkTimeWhithHttpAsync(string url = "http://ntp-a1.nict.go.jp/cgi-bin/ntp", double timeout = 100)
		{
			try
			{
				using (var client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(timeout) })
					return (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds(double.Parse(await client.GetStringAsync(url))).ToLocalTime();
			}
			catch
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>
		/// Ntp通信を使用してネットワーク上から時刻を取得します。
		/// </summary>
		/// <param name="hostName">ホスト名</param>
		/// <param name="port">ポート番号 通常はデフォルトのままで構いません。</param>
		/// <param name="timeout">タイムアウト時間(ミリ秒)</param>
		/// <returns>取得された時刻 取得に失敗した場合はDateTime.MinValueが返されます。</returns>
		public static async Task<DateTime> GetNetworkTimeWithNtp(string hostName = "ntp.nict.jp", ushort port = 123, int timeout = 100)
		{
			// RFC 2030に準拠しています。
			var ntpData = new byte[48];

			//特に使用しません
			ntpData[0] = 0b00_100_011;//うるう秒指定子 = 0 (警告なし), バージョン = 4 (SNTP), Mode = 3 (クライアント)

			await Task.Run(async () =>
			{
				try
				{
					var addresses = (await Dns.GetHostEntryAsync(hostName)).AddressList;
					var ipEndPoint = new IPEndPoint(addresses[0], port);
					using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
					{
						socket.Connect(ipEndPoint);

						//Stops code hang if NTP is blocked
						socket.ReceiveTimeout = timeout;

						socket.Send(ntpData);
						socket.Receive(ntpData);
					}
				}
				catch (SocketException)
				{
					ntpData = null;
				}
			});
			if (ntpData == null) return DateTime.MinValue;

			//Transmit Timestamp(受信タイムスタンプ)までのオフセット
			const int replyTimeOffset = 40;

			ulong intPart = BitConverter.ToUInt32(ntpData, replyTimeOffset);
			ulong fractPart = BitConverter.ToUInt32(ntpData, replyTimeOffset + 4);

			intPart = SwapEndianness(intPart);
			fractPart = SwapEndianness(fractPart);

			var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

			//時間生成
			return (new DateTime(1900, 1, 1, 0, 0, 0, DateTimeKind.Utc)).AddMilliseconds((long)milliseconds).ToLocalTime();
		}

		//ビット列を逆にする stackoverflow.com/a/3294698/162671
		internal static uint SwapEndianness(ulong x)
		{
			return (uint)(((x & 0x000000ff) << 24) +
						   ((x & 0x0000ff00) << 8) +
						   ((x & 0x00ff0000) >> 8) +
						   ((x & 0xff000000) >> 24));
		}
	}
}