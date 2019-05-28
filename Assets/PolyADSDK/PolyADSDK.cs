using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Polymer
{

	public class PolyADSDK : PolyBaseApi
	{
		
		public static Action<bool, string> OldSDKInitFinishedCallback = null;
	 
		//reward ad
		public static Action<string, string> OldRewardWillOpenCallback = null;
		public static Action<string, string> OldRewardDidOpenCallback = null;
		public static Action<string, string> OldRewardDidClickCallback = null;
		public static Action<string, string> OldRewardDidCloseCallback = null; 
		public static Action<string, string> OldRewardDidGivenCallback = null;
		public static Action<string, string> OldRewardDidAbandonCallback = null;
		//Interstitial ad
		public static Action<string, string> OldInterstitialWillShowCallback = null;
		public static Action<string, string> OldInterstitialDidShowCallback = null;
		public static Action<string, string> OldInterstitialDidCloseCallback = null;
		public static Action<string, string> OldInterstitialDidClickCallback = null;
		//banner ad
		public static Action<string, string> OldBannerDidShowCallback = null;
		public static Action<string, string> OldBannerDidClickCallback = null;
		public static Action<string, string> OldBannerDidRemoveCallback = null;

		//public static Action<OldAccessPrivacyInfoStatusEnum, string> OldAccessPrivacyInfoCallback = null;

		#if UNITY_ANDROID && !UNITY_EDITOR
		public static Action<string> OldExitAdDidShowCallback = null;
		public static Action<string> OldExitAdDidClickCallback = null;
		public static Action<string> OldExitAdDidClickMoreCallback = null;
		public static Action<string> OldExitAdOnExitCallback = null;
		public static Action<string> OldExitAdOnCancelCallback = null;

		#endif

	}

}
