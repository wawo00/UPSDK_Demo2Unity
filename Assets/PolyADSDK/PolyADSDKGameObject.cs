using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyXXJSON;
using System;
namespace Polymer {

	public class PolyADSDKGameObject : MonoBehaviour {

		private readonly static string Function_Reward_WillOpen    = "reward_willopen";
		private readonly static string Function_Reward_DidOpen    = "reward_didopen";
		private readonly static string Function_Reward_DidClick   = "reward_didclick";
		private readonly static string Function_Reward_DidClose   = "reward_didclose";
		private readonly static string Function_Reward_DidGivien  = "reward_didgiven";
		private readonly static string Function_Reward_DidAbandon = "reward_didabandon";

		private readonly static string Function_Interstitial_Willshow  = "interstitial_willshow";
		private readonly static string Function_Interstitial_Didshow  = "interstitial_didshow";
		private readonly static string Function_Interstitial_Didclose = "interstitial_didclose";
		private readonly static string Function_Interstitial_Didclick = "interstitial_didclick";

		private readonly static string Function_Banner_DidShow   = "banner_didshow";
		private readonly static string Function_Banner_DidClick  = "banner_didclick";
		private readonly static string Function_Banner_DidRemove = "banner_didremove";

		private readonly static string Function_Icon_DidLoad      = "icon_didload";
		private readonly static string Function_Icon_DidLoadFail  = "icon_didloadfail";
		private readonly static string Function_Icon_DidShow      = "icon_didshow";
		private readonly static string Function_Icon_DidClick     = "icon_didclick";

		private readonly static string Function_Reward_DidLoadFail    = "reward_didloadfail";
		private readonly static string Function_Reward_DidLoadSuccess = "reward_didloadsuccess";

		private readonly static string Function_Interstitial_DidLoadFail    = "interstitial_didloadfail";
		private readonly static string Function_Interstitial_DidLoadSuccess = "interstitial_didloadsuccess";

		private readonly static string Function_Access_Privacy_Info_Accepted = "accepte_access_privacy_information";
		private readonly static string Function_Access_Privacy_Info_Defined  = "define_access_privacy_information";
		private readonly static string Function_Access_Privacy_Info_Failed   = "fail_access_privacy_information";

		private readonly static string Function_User_Is_European_User      = "user_is_european_union";
		private readonly static string Function_User_IsNot_European_User   = "user_not_is_european_union";


		#if UNITY_ANDROID && !UNITY_EDITOR
		private readonly static string Function_ExitAd_DidShow      = "exitad_didshow";
		private readonly static string Function_ExitAd_DidClick     = "exitad_didclick";
		private readonly static string Function_ExitAd_DidClickMore = "exitad_didclickmore";
		private readonly static string Function_ExitAd_DidExit      = "exitad_onexit";
		private readonly static string Function_ExitAd_DidCancel    = "exitad_oncancel";
		#endif

		private static PolyADSDKGameObject instance = null;
		public static readonly string GameObject_Callback_Name = "PolyAdSDK_Callback_Object";
		public static readonly string Java_Callback_Function = "onJavaCallback";

		private PolyADCall adCall;
		 
		public static PolyADSDKGameObject getInstance()
		{
			if (instance == null) {
				GameObject polyCallback = new GameObject (GameObject_Callback_Name);
				polyCallback.hideFlags = HideFlags.HideAndDontSave;
				DontDestroyOnLoad (polyCallback);

				instance = polyCallback.AddComponent<PolyADSDKGameObject> ();
			}
			return instance;
		}

		//bool isPaused = false;

		Hashtable actionIntsFailMaps;
		Hashtable actionIntsSuccessMaps;
		Action<string, string> rewardVideoFailAction;
		Action<string, string> rewardVideoSuccessAction;
		Action<UPConstant.UPAccessPrivacyInfoStatusEnum, string> accessPrivacyInformationCallback;
		Action<bool, string> checkEuropeanUserCallback;

		List<string> cachedMessages = new List<string> (12);
		bool isAppFocus = false;

		bool enableCallbackAfterAppFocus = false;
		bool canObserverAppFocusCall = false;

		void OnGUI()
		{
			//Debug.Log ("===> Game onGUI Call");
		}

		void OnApplicationFocus(bool hasFocus)
		{
			Debug.Log ("===> OnApplicationFocus() hasFocus:" + hasFocus);
			isAppFocus = hasFocus;

			#if UNITY_ANDROID && !UNITY_EDITOR
			canObserverAppFocusCall = true;
			PolyADSDK.OnApplicationFocus (hasFocus);
			Debug.Log ("===> Game OnApplicationFocus Call, android hasFocus: " + hasFocus);
			#endif



		}

		void OnApplicationPause(bool pauseStatus)
		{
			Debug.Log ("===> OnApplicationPause() pauseStatus:" + pauseStatus);
			isAppFocus = !pauseStatus;
			#if UNITY_ANDROID && !UNITY_EDITOR
			canObserverAppFocusCall = true;
			Debug.Log ("===> Game OnApplicationFocus Call, android pauseStatus: " + pauseStatus);
			#endif

		}

		public void RunCallbackAfterAppFocus(bool enable) {
			this.enableCallbackAfterAppFocus = enable;
		}

		private void putLoadCallback(Hashtable map, string cpPlaceId, Action<string, string> call) {

			if (cpPlaceId == null || cpPlaceId.Length == 0) {
				return;
			}
			 
			if (map.ContainsKey (cpPlaceId)) {
				map.Remove (cpPlaceId);
			}

			if (call != null) {
				map.Add (cpPlaceId, call);
				//Debug.Log ("putLoadCallback function cpPlaceId:" + cpPlaceId);
			}
		}

		public void setPolyADCall(PolyADCall call) {
			adCall = call;
		}

		public void setAccessPrivacyInformationCallback(Action<UPConstant.UPAccessPrivacyInfoStatusEnum, string> callback) {
			accessPrivacyInformationCallback = callback;
		}

		public void setCheckEuropeanUserCallback(Action<bool, string> callback) {
			checkEuropeanUserCallback = callback;
		}

		public void setRewardVideoLoadFailCallback(Action<string, string> call) {
			this.rewardVideoFailAction = call;
		}

		public void setRewardVideoLoadSuccessCallback(Action<string, string> call) {
			this.rewardVideoSuccessAction = call;
		}

		public void addIntsLoadFailCallback(string cpPlaceId, Action<string, string> call) {
			 
			if (actionIntsFailMaps == null) {
				actionIntsFailMaps = new Hashtable ();
			}

			putLoadCallback (actionIntsFailMaps, cpPlaceId, call);
		}

		public void addIntsLoadSuccessCallback(string cpPlaceId, Action<string, string> call) {

			if (actionIntsSuccessMaps == null) {
				actionIntsSuccessMaps = new Hashtable ();
			}

			putLoadCallback (actionIntsSuccessMaps, cpPlaceId, call);
		}

		public void onJavaCallback(string message) {
			// Debug.Log ("===> onJavaCallback enableCallbackAfterAppFocus: " + enableCallbackAfterAppFocus +",canObserverAppFocusCall: " + canObserverAppFocusCall);
			if (false && enableCallbackAfterAppFocus) {
				if (canObserverAppFocusCall) {
					if (isAppFocus) {
						if (cachedMessages.Count > 0) {
							foreach (string s in cachedMessages) {
								doCallback (s);
							}
							cachedMessages.Clear();
						}
						doCallback (message);
					} else {
						cachedMessages.Add (message);
					}
				} else {
					Hashtable jsonObj = (Hashtable)PolyXXJSON.MiniJSON.jsonDecode (message);
					if (jsonObj.ContainsKey ("function")) {
						string function = (string)jsonObj ["function"];
						if (function.Equals (Function_Reward_DidOpen)
							|| function.Equals	(Function_Reward_WillOpen)
						    || function.Equals (Function_Reward_DidClick)
						    || function.Equals (Function_Reward_DidGivien)
							|| function.Equals (Function_Reward_DidAbandon)
							|| function.Equals (Function_Interstitial_Didshow)
							|| function.Equals (Function_Interstitial_Willshow)
							|| function.Equals (Function_Interstitial_Didclick)) {
							cachedMessages.Add (message);
						} else {
							if (function.Equals (Function_Reward_DidClose)
								|| function.Equals (Function_Interstitial_Didclose)) {
								if (cachedMessages.Count > 0) {
									foreach (string s in cachedMessages) {
										doCallback (s);
									}
									cachedMessages.Clear ();
								}
							}
							doCallback (message);
						}
					}

				}

			} else {
				doCallback (message);
			}

		}

		public void doCallback(string message) {
			
			Debug.Log (message);
			Hashtable jsonObj = (Hashtable)PolyXXJSON.MiniJSON.jsonDecode (message);
			if (jsonObj.ContainsKey ("function")) {
				string function = (string)jsonObj["function"];
				string msg = "";
				string placeId = "";
				if (jsonObj.ContainsKey ("function")) {
					msg = (string)jsonObj["message"];
					placeId = (string)jsonObj["cpadsid"];
				}
                
                string strFu = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                Debug.Log ("===> function: " + function +",cpadsid: " + placeId + ", time at:" + strFu);
                
				bool isReportOnlineDebug = false;
				if (adCall != null) {
					isReportOnlineDebug = adCall.IsReportOnlineEnable ();
					if (isReportOnlineDebug) {
						adCall.reportPluginInvokeMethodCall ("UnityPlugin doCallback, function: " + function + ",cpadsid: " + placeId);
					}
				}
				//reward callback
				if (function.Equals (Function_Reward_WillOpen)) {
					string fmsg = "";
					if (UPSDK.UPRewardWillOpenCallback != null) {
						Debug.Log ("===> function UPRewardWillOpenCallback(): ");
						UPSDK.UPRewardWillOpenCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.UPRewardWillOpenCallback()";
					} else if (PolyADSDK.OldRewardWillOpenCallback != null) {
						Debug.Log ("===> function OldRewardWillOpenCallback(): ");
						PolyADSDK.OldRewardWillOpenCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.OldRewardWillOpenCallback()";
					} else {
						Debug.Log ("===> function call fail, no delegate object. ");
						fmsg = "can't run RewardWillOpenCallback(), no delegate object.";
					}
					if (isReportOnlineDebug) {
						adCall.reportAdVideoShowDid(fmsg);
					}
				} else if (function.Equals (Function_Reward_DidOpen)) {
					string fmsg = "";
					if (UPSDK.UPRewardDidOpenCallback != null) {
						Debug.Log ("===> function UPRewardDidOpenCallback(): ");
						UPSDK.UPRewardDidOpenCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.RewardDidOpenCallback()";
					} else if (PolyADSDK.OldRewardDidOpenCallback != null) {
						Debug.Log ("===> function OldRewardDidOpenCallback(): ");
						PolyADSDK.OldRewardDidOpenCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.RewardDidOpenCallback()";
					} else {
						Debug.Log ("===> function call fail, no delegate object. ");
						fmsg = "can't run RewardDidOpenCallback(), no delegate object.";
					}
					if (isReportOnlineDebug) {
						adCall.reportAdVideoShowDid(fmsg);
					}
				} else if (function.Equals (Function_Reward_DidClick)) {
					string fmsg = "can't run RewardDidClickCallback(), no delegate object.";
					if (UPSDK.UPRewardDidClickCallback != null) {
						UPSDK.UPRewardDidClickCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.RewardDidClickCallback()";
					}
					else if (PolyADSDK.OldRewardDidClickCallback != null) {
						PolyADSDK.OldRewardDidClickCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.RewardDidClickCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportAdVideoClick(fmsg);
					}
				} else if (function.Equals (Function_Reward_DidClose)) {
					string fmsg = "can't run RewardDidCloseCallback(), no delegate object.";
					if (UPSDK.UPRewardDidCloseCallback != null) {
						UPSDK.UPRewardDidCloseCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.RewardDidCloseCallback()";
					}
					else if (PolyADSDK.OldRewardDidCloseCallback != null) {
						PolyADSDK.OldRewardDidCloseCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.RewardDidCloseCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportAdVideoClose(fmsg);
					}
				} else if (function.Equals (Function_Reward_DidGivien)) {
					string fmsg = "can't run RewardDidGivenCallback(), no delegate object.";
					if (UPSDK.UPRewardDidGivenCallback != null) {
						UPSDK.UPRewardDidGivenCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.RewardDidGivenCallback()";
					}
					else if (PolyADSDK.OldRewardDidGivenCallback != null) {
						PolyADSDK.OldRewardDidGivenCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.RewardDidGivenCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportAdVideoRewardGiven(fmsg);
					}
				} else if (function.Equals (Function_Reward_DidAbandon)) {
					string fmsg = "can't run RewardDidAbandonCallback(), no delegate object.";
					if (UPSDK.UPRewardDidAbandonCallback != null) {
						UPSDK.UPRewardDidAbandonCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.RewardDidAbandonCallback()";
					}
					else if (PolyADSDK.OldRewardDidAbandonCallback != null) {
						PolyADSDK.OldRewardDidAbandonCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.RewardDidAbandonCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportAdVideoRewardCancel(fmsg);
					}
				}
				//Interstitial callback
				else if (function.Equals (Function_Interstitial_Willshow)) {
					string fmsg = "can't run InterstitialWillShowCallback(), no delegate object.";
					if (UPSDK.UPInterstitialWillShowCallback != null) {
						UPSDK.UPInterstitialWillShowCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.UPInterstitialWillShowCallback()";
					}
					else if (PolyADSDK.OldInterstitialWillShowCallback != null) {
						PolyADSDK.OldInterstitialWillShowCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.OldInterstitialWillShowCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportILDidShow(placeId, fmsg);
					}
				}
				else if (function.Equals (Function_Interstitial_Didshow)) {
					string fmsg = "can't run InterstitialDidShowCallback(), no delegate object.";
					if (UPSDK.UPInterstitialDidShowCallback != null) {
						UPSDK.UPInterstitialDidShowCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.InterstitialDidShowCallback()";
					}
					else if (PolyADSDK.OldInterstitialDidShowCallback != null) {
						PolyADSDK.OldInterstitialDidShowCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.InterstitialDidShowCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportILDidShow(placeId, fmsg);
					}
				} else if (function.Equals (Function_Interstitial_Didclose)) {
					string fmsg = "can't run InterstitialDidCloseCallback(), no delegate object.";
					if (UPSDK.UPInterstitialDidCloseCallback != null) {
						UPSDK.UPInterstitialDidCloseCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.InterstitialDidCloseCallback()";
					}
					else if (PolyADSDK.OldInterstitialDidCloseCallback != null) {
						PolyADSDK.OldInterstitialDidCloseCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.InterstitialDidCloseCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportILClose(placeId, fmsg);
					}
				} else if (function.Equals (Function_Interstitial_Didclick)) {
					string fmsg = "can't run InterstitialDidClickCallback(), no delegate object.";
					if (UPSDK.UPInterstitialDidClickCallback != null) {
						UPSDK.UPInterstitialDidClickCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.InterstitialDidClickCallback()";
					}
					else if (PolyADSDK.OldInterstitialDidClickCallback != null) {
						PolyADSDK.OldInterstitialDidClickCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.InterstitialDidClickCallback()";
					}
					if (isReportOnlineDebug) {
						adCall.reportILClick(placeId, fmsg);
					}
				}
				//banner callback
				else if (function.Equals (Function_Banner_DidClick)) {
					if (UPSDK.UPBannerDidClickCallback != null) {
						UPSDK.UPBannerDidClickCallback (placeId, msg);
					}
					else if (PolyADSDK.OldBannerDidClickCallback != null) {
						PolyADSDK.OldBannerDidClickCallback (placeId, msg);
					}
				} else if (function.Equals (Function_Banner_DidShow)) {
					if (UPSDK.UPBannerDidShowCallback != null) {
						UPSDK.UPBannerDidShowCallback (placeId, msg);
					}
					else if (PolyADSDK.OldBannerDidShowCallback != null) {
						PolyADSDK.OldBannerDidShowCallback (placeId, msg);
					}
				} else if (function.Equals (Function_Banner_DidRemove)) {
					if (UPSDK.UPBannerDidRemoveCallback != null) {
						UPSDK.UPBannerDidRemoveCallback (placeId, msg);
					}
					else if (PolyADSDK.OldBannerDidRemoveCallback != null) {
						PolyADSDK.OldBannerDidRemoveCallback (placeId, msg);
					}
				}
				//icon callback
				else if (function.Equals (Function_Icon_DidLoad)) {
					if (UPSDK.UPIconDidLoadCallback != null) {
						UPSDK.UPIconDidLoadCallback (placeId, msg);
					}
				}
				else if (function.Equals (Function_Icon_DidLoadFail)) {
					if (UPSDK.UPIconDidLoadFailCallback != null) {
						UPSDK.UPIconDidLoadFailCallback (placeId, msg);
					}
				}
				else if (function.Equals (Function_Icon_DidShow)) {
					if (UPSDK.UPIconDidShowCallback != null) {
						UPSDK.UPIconDidShowCallback (placeId, msg);
					}
				}
				else if (function.Equals (Function_Icon_DidClick)) {
					if (UPSDK.UPIconDidClickCallback != null) {
						UPSDK.UPIconDidClickCallback (placeId, msg);
					}
				}
				// exitad callback 
				#if UNITY_ANDROID && !UNITY_EDITOR
				else if (function.Equals (Function_ExitAd_DidShow)) {
					if (UPSDK.UPExitAdDidShowCallback != null) {
						UPSDK.UPExitAdDidShowCallback (msg);
					}
					else if (PolyADSDK.OldExitAdDidShowCallback != null) {
						PolyADSDK.OldExitAdDidShowCallback (msg);
					}
				}
				else if (function.Equals (Function_ExitAd_DidCancel)) {
					if (UPSDK.UPExitAdOnCancelCallback != null) {
						UPSDK.UPExitAdOnCancelCallback (msg);
					}
					else if (PolyADSDK.OldExitAdOnCancelCallback!= null) {
						PolyADSDK.OldExitAdOnCancelCallback (msg);
					}
				}
				else if (function.Equals (Function_ExitAd_DidExit)) {
					if (UPSDK.UPExitAdOnExitCallback != null) {
						UPSDK.UPExitAdOnExitCallback (msg);
					}
					else if (PolyADSDK.OldExitAdOnExitCallback!= null) {
						PolyADSDK.OldExitAdOnExitCallback (msg);
					}
				}
				else if (function.Equals (Function_ExitAd_DidClick)) {
					if (UPSDK.UPExitAdDidClickCallback != null) {
						UPSDK.UPExitAdDidClickCallback (msg);
					}
					else if (PolyADSDK.OldExitAdDidClickCallback!= null) {
						PolyADSDK.OldExitAdDidClickCallback (msg);
					}
				}
				else if (function.Equals (Function_ExitAd_DidClickMore)) {
					if (UPSDK.UPExitAdDidClickMoreCallback != null) {
						UPSDK.UPExitAdDidClickMoreCallback (msg);
					}
					else if (PolyADSDK.OldExitAdDidClickMoreCallback!= null) {
						PolyADSDK.OldExitAdDidClickMoreCallback (msg);
					}
				}
				#endif
				// check European User Callback
				else if (function.Equals (Function_User_Is_European_User)) {
					if (checkEuropeanUserCallback != null) {
						checkEuropeanUserCallback (true, msg);
					}
					checkEuropeanUserCallback = null;
				}
				else if (function.Equals (Function_User_IsNot_European_User)) {
					if (checkEuropeanUserCallback != null) {
						checkEuropeanUserCallback (false, msg);
					}
					checkEuropeanUserCallback = null;
				}
				// access privacy information callback
				else if (function.Equals (Function_Access_Privacy_Info_Accepted)) {
					if (accessPrivacyInformationCallback != null) {
						accessPrivacyInformationCallback (UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusAccepted, msg);
					}
					accessPrivacyInformationCallback = null;
				}
				else if (function.Equals (Function_Access_Privacy_Info_Defined)) {
					if (accessPrivacyInformationCallback != null) {
						accessPrivacyInformationCallback (UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusDefined, msg);
					}
					accessPrivacyInformationCallback = null;
				}
				else if (function.Equals (Function_Access_Privacy_Info_Failed)) {
					if (accessPrivacyInformationCallback != null) {
						accessPrivacyInformationCallback (UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusFailed, msg);
					}
					accessPrivacyInformationCallback = null;
				}
				// load callback
				else if (function.Equals (Function_Reward_DidLoadFail)) {
					if (rewardVideoFailAction != null) {
						rewardVideoFailAction (placeId, msg);
					}
				}
				else if (function.Equals (Function_Reward_DidLoadSuccess)) {
					if (rewardVideoSuccessAction != null) {
						rewardVideoSuccessAction (placeId, msg);
					}
				}
				else if (function.Equals (Function_Interstitial_DidLoadFail)) {
					if (actionIntsFailMaps != null && placeId != null && actionIntsFailMaps.ContainsKey (placeId)) {
						Action<string, String> action = (Action<string, String>)actionIntsFailMaps [placeId];
						if (action != null) {
							action (placeId, msg);
						}
					}
				}
				else if (function.Equals (Function_Interstitial_DidLoadSuccess)) {
					if (actionIntsSuccessMaps != null && placeId != null && actionIntsSuccessMaps.ContainsKey (placeId)) {
						Action<string, String> action = (Action<string, String>)actionIntsSuccessMaps [placeId];
						if (action != null) {
							action (placeId, msg);
						}
					}
				}
				//unkown call
				else {
					Debug.Log ("unkown function:" + function);
				}
			}
		}
	}

}

