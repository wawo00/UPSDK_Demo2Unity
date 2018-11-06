using System;

namespace UPTrace
{
	public class UPTraceConstant
	{
		public enum UPTraceSDKZoneEnum {
			UPTraceSDKZoneForeign   = 0, //海外
			UPTraceSDKZoneDomestic = 1   //中国大陆
		}

		// 插件版本号
		private readonly static string Version_Of_Ios_In_Plugin = "3008";

		private readonly static string Version_Of_Android_In_Plugin = "3018";
	}
}

