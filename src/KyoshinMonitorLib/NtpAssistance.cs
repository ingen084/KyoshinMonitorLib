using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KyoshinMonitorLib
{
	/// <summary>
	/// Ntpによる時刻取得を補助するクラス
	/// </summary>
	public class NtpAssistance
	{
		static Regex TimeRegex = new Regex("[^0-9]*(\\d+\\.\\d+)+.*", RegexOptions.Compiled);

		/// <summary>
		/// Http通信を使用してネットワーク上から時刻を取得します。
		/// </summary>
		/// <param name="url">要求するURL POSIX Timeが生で返されるURLである必要があります。</param>
		/// <param name="timeout">タイムアウト時間(ミリ秒)</param>
		/// <returns>取得された時刻 取得に失敗した場合はnullが返されます。</returns>
		public static async Task<DateTime?> GetNetworkTimeWithHttp(string url = "https://ntp-a1.nict.go.jp/cgi-bin/jst", double timeout = 1000)
		{
			try
			{
				using (var client = new HttpClient() { Timeout = TimeSpan.FromMilliseconds(timeout) })
				{
					var match = TimeRegex.Match(await client.GetStringAsync(url));
					return (new DateTime(1970, 1, 1, 9, 0, 0)).AddSeconds(double.Parse(match.Groups[1].Value));
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine("GetNetworkTimeWhithHttpAsync Error: " + ex);
				return null;
			}
		}

		/// <summary>
		/// Ntp通信を使用してネットワーク上から時刻を取得します。
		/// </summary>
		/// <param name="hostName">ホスト名</param>
		/// <param name="port">ポート番号 通常はデフォルトのままで構いません。</param>
		/// <param name="timeout">タイムアウト時間(ミリ秒)</param>
		/// <returns>取得された時刻 取得に失敗した場合はnullが返されます。</returns>
		public static async Task<DateTime?> GetNetworkTimeWithNtp(string hostName = "ntp.nict.jp", ushort port = 123, int timeout = 100)
		{
			// RFC 2030に準拠しています。
			var ntpData = new byte[48];

			//特に使用しません
			ntpData[0] = 0b00_100_011;//うるう秒指定子 = 0 (警告なし), バージョン = 4 (SNTP), Mode = 3 (クライアント)

			DateTime sendedTime, recivedTime;
			sendedTime = recivedTime = DateTime.Now;

			try
			{
				var addresses = (await Dns.GetHostEntryAsync(hostName)).AddressList;
				var ipEndPoint = new IPEndPoint(addresses[0], port);
				using (var socket = new Socket(ipEndPoint.AddressFamily, SocketType.Dgram, ProtocolType.Udp))
				{
					socket.Connect(ipEndPoint);

					//Stops code hang if NTP is blocked
					socket.ReceiveTimeout = timeout;

					socket.Send(ntpData);
					sendedTime = DateTime.Now;

					socket.Receive(ntpData);
					recivedTime = DateTime.Now;
				}
			}
			catch (SocketException)
			{
				return null;
			}

			//受信時刻=32 送信時刻=40
			var serverReceivedTime = ToTime(ntpData, 32);
			var serverSendedTime = ToTime(ntpData, 40);

			var delta = TimeSpan.FromTicks(((recivedTime - sendedTime) - (serverReceivedTime - serverSendedTime)).Ticks / 2);
			Debug.WriteLine("delta:" + delta);
			return sendedTime - delta;
		}

		private static DateTime ToTime(byte[] bytes, ushort offset)
		{
			ulong intPart = SwapEndianness(BitConverter.ToUInt32(bytes, offset));
			ulong fractPart = SwapEndianness(BitConverter.ToUInt32(bytes, offset + 4));

			var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);

			//時間生成
			return (new DateTime(1900, 1, 1, 9, 0, 0)).AddMilliseconds((long)milliseconds);
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