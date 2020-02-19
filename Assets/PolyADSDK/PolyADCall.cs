
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Polymer
{
	public class PolyADCall
	{



		#if UNITY_IOS && !UNITY_EDITOR
			[DllImport("__Internal")]
			private static extern string initIosSDK(string gameName, string funName, string appKey, int zone);
			[DllImport("__Internal")]
			private static extern void showInterstitial(string cpPlaceId);
			[DllImport("__Internal")]
			private static extern void showReward(string cpCustomId);
			[DllImport("__Internal")]
			private static extern void showBannerTop(string placementid);
			[DllImport("__Internal")]
			private static extern void showIcon(double x, double y, double width, double height, double rotationAngle, string placementid);
			[DllImport("__Internal")]
			private static extern void removeIcon(string placementid);
			[DllImport("__Internal")]
			private static extern void showBannerBottom(string placementid);
			[DllImport("__Internal")]
			private static extern void removeBannerAd(string placementid);
			[DllImport("__Internal")]
			private static extern bool isInterstitialReady(string placementid);
			[DllImport("__Internal")]
			private static extern bool isRewardReady();

			[DllImport("__Internal")]
			private static extern void showRewardDebugController();  

			[DllImport("__Internal")]
			private static extern void showInterstitialDebugController(); 

			[DllImport("__Internal")]
			private static extern string getAbtConfigForIos(string placementid);

			[DllImport("__Internal")]
			private static extern void initAbtConfigJsonForIos(string gameAccountId, bool completeTask,int isPaid, string promotionChannelName, string gender, int age, string tags);	

			[DllImport("__Internal")]
			private static extern void setInterstitialLoadCallbackAt(string placementid);

			[DllImport("__Internal")]
			private static extern void setRewardloadCallback();

			[DllImport("__Internal")]
			private static extern void hideTopBanner();

			[DllImport("__Internal")]
			private static extern void hideBottomBanner();

			[DllImport("__Internal")]
			private static extern void setTopBannerPadingForIphonex(int padding);

			[DllImport("__Internal")]
			private static extern void loadAdsByManual();

			[DllImport("__Internal")]
			private static extern void updateAccessPrivacyInfoStatus(int value,int regionStatus);

			[DllImport("__Internal")]
			private static extern void requestAuthorizationWithAlert(string gameName, string funName);

			[DllImport("__Internal")]
			private static extern int getCurrentAccessPrivacyInfoStatus();

			[DllImport("__Internal")]
			private static extern void reportRDShowDid(string msg);

			[DllImport("__Internal")]
			private static extern void reportRDRewardGiven(string msg);

			[DllImport("__Internal")]
			private static extern void reportRDRewardCancel(string msg);

			[DllImport("__Internal")]
			private static extern void reportRDRewardClick(string msg);

			[DllImport("__Internal")]
			private static extern void reportRDRewardClose(string msg);

			[DllImport("__Internal")]
			private static extern void reportILShowDidCall(string placeid, string msg);

			[DllImport("__Internal")]
			private static extern void reportILClickCall(string placeid, string msg);

			[DllImport("__Internal")]
			private static extern void reportILCloseCall(string placeid, string msg);

			[DllImport("__Internal")]
			private static extern void reportInvokeMethodReceive(string msg);

			[DllImport("__Internal")]
			private static extern bool IsIosReportOnlineEnable();

            [DllImport("__Internal")]
            private static extern bool isLogOpened();

			[DllImport("__Internal")]
			private static extern void TellDoctorSomeThing(string action, string adid, string msg);

			[DllImport("__Internal")]
			private static extern void autoOneKeyInspectByIos();

			[DllImport("__Internal")]
			private static extern void adjustIDByUpIos(string msg);

			[DllImport("__Internal")]
			private static extern void appsflyerUIDByUpIos(string msg);
		
		#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.openup.sdk.unity.OpenUpPolyProxy";
			private readonly static string JavaClassStaticMethod_InitSDK = "initSDK";
			private readonly static string JavaClassStaticMethod_ShowTopBanner = "showTopBanner";
			private readonly static string JavaClassStaticMethod_ShowBottomBanner = "showBottomBanner";
			private readonly static string JavaClassStaticMethod_RemoveBanner = "removeBanner";
			private readonly static string JavaClassStaticMethod_ShowIconAd = "showIconAd";
			private readonly static string JavaClassStaticMethod_RemoveIconAd = "removeIconAd";
			private readonly static string JavaClassStaticMethod_SetManifestPackageName = "setManifestPackageName";
			private readonly static string JavaClassStaticMethod_ShowInterstitial = "showInterstitial";
			private readonly static string JavaClassStaticMethod_ShowRewardVideo = "showRewardVideo";
			private readonly static string JavaClassStaticMethod_OnBackPressed = "onBackPressed";
			private readonly static string JavaClassStaticMethod_IsInterstitialReady = "isInterstitialReady";
			private readonly static string JavaClassStaticMethod_IsRewardReady = "isRewardReady";
			private readonly static string JavaClassStaticMethod_OnApplicationFocus = "onApplicationFocus";
			private readonly static string JavaClassStaticMethod_getAbtConfig = "getAbtConfig";
			private readonly static string JavaClassStaticMethod_initAbtConfigJson = "initAbtConfigJson";
			private readonly static string JavaClassStaticMethod_ShowVideoDebugActivity = "showVideoDebugActivity";
			private readonly static string JavaClassStaticMethod_ShowInterstitialDebugActivity = "showInterstitialDebugActivity";
			private readonly static string JavaClassStaticMethod_SetInterstitialCallbackAt = "setInterstitialCallbackAt";
			private readonly static string JavaClassStaticMethod_SetRewardVideoLoadCallback = "setRewardVideoLoadCallback";
			private readonly static string JavaClassStaticMethod_HideTopBanner = "hideTopBanner";
			private readonly static string JavaClassStaticMethod_HideBottomBanner = "hideBottomBanner";
			private readonly static string JavaClassStaticMethod_PrintToken = "printToken";
			private readonly static string JavaClassStaticMethod_LoadAnroidAdsByManual = "loadAnroidAdsByManual";
			private readonly static string JavaClassStaticMethod_updateAccessPrivacyInfoStatus = "updateAccessPrivacyInfoStatus";
			private readonly static string JavaClassStaticMethod_getAccessPrivacyInfoStatus = "getAccessPrivacyInfoStatus";
			private readonly static string JavaClassStaticMethod_notifyAccessPrivacyInfoStatus = "notifyAccessPrivacyInfoStatus";
			private readonly static string JavaClassStaticMethod_CheckUserAreaRegion = "checkUserAreaRegion";
			private readonly static string JavaClassStaticMethod_SetTopBannerTopPadding = "setTopBannerTopPadding";
			private readonly static string JavaClassStaticMethod_SetCustomerId = "setCustomerId";
			
			private readonly static string JavaClassStaticMethod_ReportRDShowDid = "reportRDShowDid";
			private readonly static string JavaClassStaticMethod_ReportRDRewardGiven = "reportRDRewardGiven";
			private readonly static string JavaClassStaticMethod_ReportRDRewardCancel = "reportRDRewardCancel";
			private readonly static string JavaClassStaticMethod_ReportRDRewardClick = "reportRDRewardClick";
			private readonly static string JavaClassStaticMethod_ReportRDRewardClose = "reportRDRewardClose";

			private readonly static string JavaClassStaticMethod_ReportILShowDid = "reportILShowDid";
			private readonly static string JavaClassStaticMethod_ReportILClick = "reportILClick";
			private readonly static string JavaClassStaticMethod_ReportILClose = "reportILClose";

			private readonly static string JavaClassStaticMethod_IsReportOnlineEnable = "isReportOnlineEnable";
			private readonly static string JavaClassStaticMethod_ReportIvokePluginMethodReceive = "reportIvokePluginMethodReceive";

			private readonly static string JavaClassStaticMethod_IsLogOpened = "isLogOpened";
			private readonly static string JavaClassStaticMethod_SetIsChild = "setIsChild";
	        private readonly static string JavaClassStaticMethod_GetIsChild = "getIsChild";
			private readonly static string JavaClassStaticMethod_SetBirthday = "setBirthday";

			private readonly static string JavaClassStaticMethod_TellToDoctor = "tellToDoctor";

			private readonly static string JavaClassStaticMethod_AutoOneKeyInspect = "autoOneKeyInspect";

			private readonly static string JavaClassStaticMethod_SetAppsflyerUID = "setAppsflyerUID";

			private readonly static string JavaClassStaticMethod_SetAdjustID = "setAdjustID";

		
		#else
			// "do nothing";
		#endif

		private bool doctorWorking;

		public string getPlatformName ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return "UNITY_IOS";
			#elif UNITY_ANDROID && !UNITY_EDITOR
			return "UNITY_ANDROID";
			#else
			return "unkown";
			#endif
		}

		public void loadUpAdsByManual ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			loadAdsByManual();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
			jc.CallStatic (JavaClassStaticMethod_LoadAnroidAdsByManual);
			}
			#endif
		}

		public void setTopBannerForAndroid (int padding)
		{
			#if UNITY_IOS && !UNITY_EDITOR

			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{	
			jc.CallStatic (JavaClassStaticMethod_SetTopBannerTopPadding, padding);
			}
			#endif
		}

		public void setTopBannerForIphonex (int padding)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				setTopBannerPadingForIphonex(padding);
			#elif UNITY_ANDROID && !UNITY_EDITOR

			#endif
		}

		public void printInfo ()
		{
			
			#if UNITY_IOS && !UNITY_EDITOR

			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
			jc.CallStatic (JavaClassStaticMethod_PrintToken);
			}
			#endif
		}

		public PolyADCall ()
		{
			PolyADSDKGameObject.getInstance ().setPolyADCall (this);
			doctorWorking = false;
			#if UNITY_IOS && !UNITY_EDITOR
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> PolyADCall instanced");
				jc = new AndroidJavaClass (JavaClassName);
			}
			#endif
		}

		public void AutoOneKeyInspect () {
			

			#if UNITY_IOS && !UNITY_EDITOR
				autoOneKeyInspectByIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_AutoOneKeyInspect);
			}
			#endif
		}


		public void DoctorOnDuty () {
			doctorWorking = true;
		}

		public void DoctorOffDuty () {
			doctorWorking = false;
		}

		public bool IsDoctorWorking() {
			return doctorWorking;
		}

		public void TellToDoctor(string action, string adid, string message) {
			#if UNITY_IOS && !UNITY_EDITOR
				TellDoctorSomeThing(action, adid, message);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_TellToDoctor, action, adid, message);
			}
			#endif
		}

		public bool IsReportOnlineEnable ()
		{
			
			#if UNITY_IOS && !UNITY_EDITOR
				return IsIosReportOnlineEnable();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<bool> (JavaClassStaticMethod_IsReportOnlineEnable);
			}
			return false;
			#else
			return false;
			#endif

		}

		public void reportAdVideoRewardGiven (string msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportRDRewardGiven(msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportRDRewardGiven, msg);
			}
			#endif
		}

		public void reportAdVideoRewardCancel (string msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportRDRewardCancel(msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportRDRewardCancel, msg);
			}
			#endif
		}

		public void reportAdVideoClick (string msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportRDRewardClick(msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportRDRewardClick, msg);
			}
			#endif
		}

		public void reportAdVideoClose (string msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportRDRewardClose(msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportRDRewardClose, msg);
			}
			#endif
		}

		public void reportAdVideoShowDid (string msg)
		{
			
			#if UNITY_IOS && !UNITY_EDITOR
				reportRDShowDid(msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportRDShowDid, msg);
			}
			#endif
		}

		public void reportILDidShow (String cpPlaceId, String msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportILShowDidCall(cpPlaceId, msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportILShowDid, cpPlaceId, msg);
			}
			#endif
		}

		public void reportILClick (String cpPlaceId, String msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportILClickCall(cpPlaceId, msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportILClick, cpPlaceId, msg);
			}
			#endif
		}

		public void reportILClose (String cpPlaceId, String msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportILCloseCall(cpPlaceId, msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportILClose, cpPlaceId, msg);
			}
			#endif
		}

		public void reportPluginInvokeMethodCall (string msg)
		{
			#if UNITY_IOS && !UNITY_EDITOR
				reportInvokeMethodReceive(msg);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ReportIvokePluginMethodReceive, msg);
			}
			#endif

		}

		public void RunCallbackAfterAppFocus (bool enable)
		{
			PolyADSDKGameObject.getInstance ().RunCallbackAfterAppFocus (enable);
		}

		// Use this for initialization
		public string initSDK (string androidAppKey, string iosAppKey, int iosZone)
		{

			#if UNITY_IOS && !UNITY_EDITOR
				if (iosAppKey == null || iosAppKey.Equals("")) {
					Debug.Log ("===> failed to initSDK, iosAppKey can't be null or empty.");
					return "failed to initSDK, iosAppKey can't be null or empty.";
				}

				if (iosZone < 0 || iosZone > 2) {
					Debug.Log ("===> warning, iosZone is wrong value: " + iosZone + ", will be setted to SDKZONE_FOREIGN");
					iosZone = UPConstant.SDKZONE_FOREIGN;
				}

            	//Debug.Log ("===> init ios call:" + PolyADSDKGameObject.GameObject_Callback_Name);
            	//Debug.Log ("===> init ios fun:" + PolyADSDKGameObject.Java_Callback_Function);
            	//Debug.Log ("===> init ios iosZone:" + iosZone);
				string result = initIosSDK(PolyADSDKGameObject.GameObject_Callback_Name,
											PolyADSDKGameObject.Java_Callback_Function,
											iosAppKey,
											iosZone);

				if (OpenUpSDK.UPSDKInitFinishedCallback != null) {
					OpenUpSDK.UPSDKInitFinishedCallback (true, "UPSDK Init Ios Sdk Finish");
				}
				else if (PolyADSDK.OldSDKInitFinishedCallback != null) {
					PolyADSDK.OldSDKInitFinishedCallback (true, "UPSDK Init Ios Sdk Finish");
				}
				return result;

			#elif UNITY_ANDROID && !UNITY_EDITOR
				//Debug.Log ("===> init android call:" + PolyADSDKGameObject.GameObject_Callback_Name);
            	//Debug.Log ("===> init android fun:" + PolyADSDKGameObject.Java_Callback_Function);
            	if (androidAppKey == null || androidAppKey.Equals("")) {
					Debug.Log ("===> failed to initSDK, androidAppKey can't be null or empty.");
					return "failed to initSDK, androidAppKey can't be null or empty.";
            	}
            	//Debug.Log ("===> init android androidAppKey:" + androidAppKey);
				if (jc == null) {
					//Debug.Log (JavaClassName);
					jc = new AndroidJavaClass (JavaClassName);
				}
				string result = jc.CallStatic<string> (JavaClassStaticMethod_InitSDK, 
													PolyADSDKGameObject.GameObject_Callback_Name, 
													PolyADSDKGameObject.Java_Callback_Function,
													androidAppKey);
				if (OpenUpSDK.UPSDKInitFinishedCallback != null) {
					OpenUpSDK.UPSDKInitFinishedCallback (true, "UPSDK Init Android Sdk Finish");
				}
				else if (PolyADSDK.OldSDKInitFinishedCallback != null) {
					PolyADSDK.OldSDKInitFinishedCallback (true, "UPSDK Init Android Sdk Finish");
				}
				return result;

			#else
				// "do nothing";
				if (PolyADSDK.OldSDKInitFinishedCallback != null) {
					PolyADSDK.OldSDKInitFinishedCallback (false, "UPSDK can't Init unkown platform");
				}
				return "initSDK ()";
			#endif
		}

		private string stringAryToString (string[] tags) {
			if (tags == null || tags.Length == 0) {
				return "";
			}

			string str = "{ \"array\":[";
			int len = tags.Length;
			for (int i = 0; i < len; i++) {
				str += "\"" + tags [i];
				if (i < len - 1) {
					str += "\",";
				} else {
					str += "\"]}";
				}
			}
			//Debug.Log ("stringAryToString:" + str);
			return str;
		}

		public void initAbtConfigJson (string gameAccountId, bool completeTask,
		                              int isPaid, string promotionChannelName, string gender,
		                              int age, string[] tags) {
			 
			#if UNITY_IOS && !UNITY_EDITOR
				initAbtConfigJsonForIos(gameAccountId, completeTask, isPaid, promotionChannelName, gender, age, stringAryToString(tags));
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_initAbtConfigJson, gameAccountId, completeTask, isPaid, promotionChannelName, gender, age, stringAryToString(tags));
			}
			#endif
		}

		public string getAbtConfig (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call getAbtConfig(), the param cpPlaceId can't be null. ");
				return "";
			}
			#if UNITY_IOS && !UNITY_EDITOR
			return getAbtConfigForIos(cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				return jc.CallStatic<string> (JavaClassStaticMethod_getAbtConfig, cpPlaceId);
			}
			return "";
			#else
			return "";
			#endif
		}

		public void setManifestPackageName (string packagename) {
			#if UNITY_IOS && !UNITY_EDITOR
			 
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetManifestPackageName, packagename);
			}
			#endif
		}

		public void setCustomerId (string curstomerId) {

			if (curstomerId == null) {
				Debug.Log ("===> fail to call setCustomerId(), curstomerId can't be null.");
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> setCustomerId() is not supported by IOS." );
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetCustomerId, curstomerId);
			}
			#endif
		}

		public void removeBanner (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call removeBanner(), the param cpPlaceId can't be null. ");
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
				removeBannerAd(cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_RemoveBanner, cpPlaceId);
			}
			#endif
			 
		}

		public void setRewardVideoLoadFailCallback (Action<string, string> call) {
			PolyADSDKGameObject.getInstance ().setRewardVideoLoadFailCallback (call);
		}

		public void setRewardVideoLoadSuccessCallback (Action<string, string> call) {
			PolyADSDKGameObject.getInstance ().setRewardVideoLoadSuccessCallback (call);
		}

		public void addIntsLoadFailCallback (string cpPlaceId, Action<string, string> call) {
			PolyADSDKGameObject.getInstance ().addIntsLoadFailCallback (cpPlaceId, call);

		}

		public void addIntsLoadSuccessCallback (string cpPlaceId, Action<string, string> call) {
			PolyADSDKGameObject.getInstance ().addIntsLoadSuccessCallback (cpPlaceId, call);

		}


		public void checkUserAreaRegion (Action<UPConstant.PrivacyUserRegionStatus, string> callback) {
			PolyADSDKGameObject.getInstance ().setCheckUserAreaRegion (callback);
			#if UNITY_IOS && !UNITY_EDITOR
				checkIsEuropeanUnionUser(PolyADSDKGameObject.GameObject_Callback_Name,PolyADSDKGameObject.Java_Callback_Function);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_CheckUserAreaRegion, PolyADSDKGameObject.GameObject_Callback_Name,PolyADSDKGameObject.Java_Callback_Function);
			}
			#endif
		}

		public void notifyAccessPrivacyInfoStatus (Action<UPConstant.UPAccessPrivacyInfoStatusEnum, string> callback, int regionStatus) {
			PolyADSDKGameObject.getInstance ().setAccessPrivacyInformationCallback (callback);

			#if UNITY_IOS && !UNITY_EDITOR
				requestAuthorizationWithAlert(PolyADSDKGameObject.GameObject_Callback_Name,PolyADSDKGameObject.Java_Callback_Function);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_notifyAccessPrivacyInfoStatus, PolyADSDKGameObject.GameObject_Callback_Name, PolyADSDKGameObject.Java_Callback_Function, regionStatus);
			}
			#endif

		}

		public void setAccessPrivacyInfoStatus (UPConstant.UPAccessPrivacyInfoStatusEnum value,UPConstant.PrivacyUserRegionStatus regionStatus) {
			int result = 0;
			int status=0;
			switch (value) {
			case UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusAccepted:
				{
					result = 1;
					break;
				}
			case UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusDefined:
				{
					result = 2;
					break;
				}
			default:
				{
					result = 0;
					break;
				}
			}

			switch (regionStatus) {
			case UPConstant.PrivacyUserRegionStatus.PrivacyUserRegionStatusEU:
				{
					status = 1;
					break;
				}
			case UPConstant.PrivacyUserRegionStatus.PrivacyUserRegionStatusCA:
				{
					status = 2;
					break;
				}
			default:
				{
					status = 0;
					break;
				}
			}

			#if UNITY_IOS && !UNITY_EDITOR
				updateAccessPrivacyInfoStatus(result);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_updateAccessPrivacyInfoStatus, result,status);
			}
			#endif
		}

		public UPConstant.UPAccessPrivacyInfoStatusEnum getAccessPrivacyInfoStatus () {


			#if UNITY_IOS && !UNITY_EDITOR
				int result = getCurrentAccessPrivacyInfoStatus();
				if (result == 1) {
					return UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusAccepted;
				}
				else if (result == 2) {
					return UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusDefined;
				}
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				int result = jc.CallStatic<int> (JavaClassStaticMethod_getAccessPrivacyInfoStatus);
				if (result == 1) {
					return UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusAccepted;
				}
				else if (result == 2) {
					return UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusDefined;
				}
			}
			#endif

			return UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusUnkown;
		}



		public void callRewardVideoLoadCallback () {
			#if UNITY_IOS && !UNITY_EDITOR
				setRewardloadCallback();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetRewardVideoLoadCallback);
			}
			#endif
		}

		public void callInterstitialCallbackAt (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call setInterstitialCallbackAt(), the param cpPlaceId can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				setInterstitialLoadCallbackAt(cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetInterstitialCallbackAt, cpPlaceId);
			}
			#endif
		}

		public bool isInterstitialAdReady (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call isInterstitialAdReady(), the param cpPlaceId can't be null. ");
				return false;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				return isInterstitialReady(cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				return jc.CallStatic<bool> (JavaClassStaticMethod_IsInterstitialReady, cpPlaceId);
			}
			return false;
			#else
			return false;
			#endif

		}

		public bool isRewardAdReady () {
			#if UNITY_IOS && !UNITY_EDITOR
				return isRewardReady();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				return jc.CallStatic<bool> (JavaClassStaticMethod_IsRewardReady);
			}
			return false;
			#else
			return false;
			#endif

		}

		public void showInterstitialAd (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call isInterstitialAdReady(), the param cpPlaceId can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				showInterstitial(cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR

			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowInterstitial, cpPlaceId);
			}
			#endif
		}

		public void hideBannerAtTop () {
			#if UNITY_IOS && !UNITY_EDITOR
				hideTopBanner();
			#elif UNITY_ANDROID && !UNITY_EDITOR

			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_HideTopBanner);
			}
			#endif
		}

		public void hideBannerAtBottom ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
				hideBottomBanner();
			#elif UNITY_ANDROID && !UNITY_EDITOR

			if (jc != null) 
			{
			jc.CallStatic (JavaClassStaticMethod_HideBottomBanner);
			}
			#endif
		}

		public void showRewardAd (string cpCustomId) {
			if (cpCustomId == null) {
				Debug.Log ("===> call showRewardAd(), the param cpCustomId be null. ");
				cpCustomId = "reward_vedio";
			}
			#if UNITY_IOS && !UNITY_EDITOR
			showReward(cpCustomId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowRewardVideo, cpCustomId);
			}
			#endif
		}

		public void showBannerAdAtTop (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call showBannerAdAtTop(), the param cpPlaceId can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
			showBannerTop(cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowTopBanner, cpPlaceId);
			}
			#endif
			 
		}

		/*
		 * 根据坐标、旋转角度、广告位，展示UPSDK的Icon广告
		 * @param x: 起始位横坐标
		 * @param y: 起始位纵坐标
		 * @param width: 宽度
		 * @param height: 高度
		 * @param rotationAngle: 顺时针旋转角度
		 * @param cpPlaceId: Icon广告位标识符
		 */
		public void showIconAd (double x, double y, double width, double height, double rotationAngle, string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call showIconAd(), the param cpPlaceId can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
			showIcon(x,y,width,height,rotationAngle,cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowIconAd, x, y, width, height, rotationAngle, cpPlaceId);
			}
			#endif
		}

		/*
		 * 根据广告位，删除aUPSDK的Icon广告
		 * @param cpPlaceId: Icon广告位标识符
		 */
		public void removeIconAd (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call removeIcon(), the param cpPlaceId can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
			removeIcon(cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_RemoveIconAd, cpPlaceId);
			}
			#endif
		}

		public void showBannerAdAtBottom (string cpPlaceId) {
			if (cpPlaceId == null) {
				Debug.Log ("===> call showBannerAdAtBottom(), the param cpPlaceId can't be null. ");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
			showBannerBottom (cpPlaceId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowBottomBanner, cpPlaceId);
			}
			#endif
		}

		public void onBackPressed () {
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_OnBackPressed);
			}
			#endif
		}

		public void OnApplicationFocus (bool hasfoucus) {
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_OnApplicationFocus, hasfoucus);
			}
			#endif
		}

		public void showRewardDebugView () {
			#if UNITY_IOS && !UNITY_EDITOR
				showRewardDebugController ();
			#elif   UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_ShowVideoDebugActivity);
			}
			#endif
		}

		public void showInterstitialDebugView () {
			//
			#if UNITY_IOS && !UNITY_EDITOR
				showInterstitialDebugController();
				//Debug.Log ("===>sorry, this function is not supported. ");
			#elif  UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_ShowInterstitialDebugActivity);
			}
			#endif
		}

		public bool isSdkLogOpened ()
		{
			#if UNITY_IOS && !UNITY_EDITOR
			return isLogOpened();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				return jc.CallStatic<bool> (JavaClassStaticMethod_IsLogOpened);
			}
			return false;
			#else
			return false;
			#endif
		}

		public void setIsChild (bool isChild)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetIsChild, isChild);
			}
			#endif
		}

	    public bool getIsChild ()
		{
            #if UNITY_IOS && !UNITY_EDITOR
				return false;
            #elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				return	jc.CallStatic<bool> (JavaClassStaticMethod_GetIsChild);
			}
			return false;
            #else
            return false;
            #endif

        }

        public void setBirthday (int year, int month)
		{
			#if UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetBirthday, year, month);
			}
			#endif
		}

		public void setUpAdjustID(string ajid) {

			#if UNITY_IOS && !UNITY_EDITOR
				adjustIDByUpIos(ajid);
			#elif  UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetAdjustID, ajid);
			}
			#endif
		}

		public void setUpAppsflyerUID(string uid) {
			#if UNITY_IOS && !UNITY_EDITOR
				appsflyerUIDByUpIos(uid);
			#elif  UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) {
				jc.CallStatic (JavaClassStaticMethod_SetAppsflyerUID, uid);
			}
			#endif
		}
	}
}
