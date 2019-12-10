﻿using System;

namespace Polymer
{
	public class UPSDK : PolyBaseApi
	{
		
		public static Action<bool, string> UPSDKInitFinishedCallback = null;

		//reward ad
		public static Action<string, string> UPRewardWillOpenCallback = null;
		public static Action<string, string> UPRewardDidOpenCallback = null;
		public static Action<string, string> UPRewardDidClickCallback = null;
		public static Action<string, string> UPRewardDidCloseCallback = null; 
		public static Action<string, string> UPRewardDidGivenCallback = null;
		public static Action<string, string> UPRewardDidAbandonCallback = null;
		//Interstitial ad
		public static Action<string, string> UPInterstitialWillShowCallback = null;
		public static Action<string, string> UPInterstitialDidShowCallback = null;
		public static Action<string, string> UPInterstitialDidCloseCallback = null;
		public static Action<string, string> UPInterstitialDidClickCallback = null;
		//banner ad
		public static Action<string, string> UPBannerDidShowCallback = null;
		public static Action<string, string> UPBannerDidClickCallback = null;
		public static Action<string, string> UPBannerDidRemoveCallback = null;
		//icon ad
		public static Action<string, string> UPIconDidLoadCallback = null;
		public static Action<string, string> UPIconDidLoadFailCallback = null;
		public static Action<string, string> UPIconDidShowCallback = null;
		public static Action<string, string> UPIconDidClickCallback = null;
	}
}

