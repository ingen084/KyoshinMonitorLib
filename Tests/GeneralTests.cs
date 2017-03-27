using KyoshinMonitorLib;
using System;
using Xunit;

namespace Tests
{
	public static class GeneralTests
	{
		[Fact(DisplayName = "UrlGenerator Test")]
		public static void Urlgenerator()
		{
			Assert.Equal(UrlGenerator.Generate(UrlType.EewJson, DateTime.Parse("2017/03/19 22:13:47")), "http://www.kmoni.bosai.go.jp/new/webservice/hypo/eew/20170319221347.json");
		}
	}
}