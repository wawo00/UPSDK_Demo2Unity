using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyXXJSON;
using System;
namespace Polymer {

	public class PolyADSDKGameObject : MonoBehaviour {


		private readonly static string Action_Doctor_ON_DUTY   = "auto_ad_checking_doctor_on_duty";
		private readonly static string Action_Doctor_OFF_DUTY   = "auto_ad_checking_doctor_off_duty";


		private readonly static string Action_Doctor_Ad_IL_LoadOk_Reply   = "auto_ad_il_load_ok_reply";
		private readonly static string Action_Doctor_Ad_IL_LoadFail_Reply = "auto_ad_il_load_fail_reply";
		private readonly static string Action_Doctor_Ad_IL_WillShow_Reply  = "auto_ad_il_willshow_reply";
		private readonly static string Action_Doctor_Ad_IL_DidShow_Reply  = "auto_ad_il_didshow_reply";
		private readonly static string Action_Doctor_Ad_IL_DidClick_Reply = "auto_ad_il_didclick_reply";
		private readonly static string Action_Doctor_Ad_IL_DidClose_Reply = "auto_ad_il_didclose_reply";

		private readonly static string Action_Doctor_Ad_RD_LoadOk_Reply   = "auto_ad_rd_load_ok_reply";
		private readonly static string Action_Doctor_Ad_RD_LoadFail_Reply = "auto_ad_rd_load_fail_reply";
		private readonly static string Action_Doctor_Ad_RD_WillShow_Reply = "auto_ad_rd_willshow_reply";
		private readonly static string Action_Doctor_Ad_RD_DidShow_Reply  = "auto_ad_rd_didshow_reply";
		private readonly static string Action_Doctor_Ad_RD_DidClick_Reply = "auto_ad_rd_didclick_reply";
		private readonly static string Action_Doctor_Ad_RD_DidClose_Reply = "auto_ad_rd_didclose_reply";
		private readonly static string Action_Doctor_Ad_RD_Given_Reply    = "auto_ad_rd_reward_given_reply";
		private readonly static string Action_Doctor_Ad_RD_Cancel_Reply   = "auto_ad_rd_reward_cancel_reply";

		private readonly static string Function_Doctor_IL_Show_AdId    = "auto_sample_ad_il_show_placeid";
		private readonly static string Function_Doctor_RD_Show_AdId    = "auto_sample_ad_rd_show_placeid";

		private readonly static string Function_Doctor_IL_Show_Request    = "invoke_plugin_ad_il_show_request";
		private readonly static string Function_Doctor_RD_Show_Request    = "invoke_plugin_ad_rd_show_request";

		private readonly static string Function_Doctor_IL_Load_Request    = "invoke_plugin_ad_il_load_request";
		private readonly static string Function_Doctor_RD_Load_Request    = "invoke_plugin_ad_rd_load_request";

		private readonly static string Function_Reward_WillOpen    = "reward_willopen";
		private readonly static string Function_Reward_DidOpen    = "reward_didopen";
		private readonly static string Function_Reward_DidClick   = "reward_didclick";
		private readonly static string Function_Reward_DidClose   = "reward_didclose";
		private readonly static string Function_Reward_DidGivien  = "reward_didgiven";
		private readonly static string Function_Reward_DidAbandon = "reward_didabandon";

		private readonly static string Function_Interstitial_Willshow = "interstitial_willshow";
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

		private void doctorForILLoadFail(string placeId, string msg)
		{
			Debug.Log ("===> TellToDoctor il load fail at: " + placeId);
			adCall.TellToDoctor (Action_Doctor_Ad_IL_LoadFail_Reply, placeId, msg);
		}

		private void doctorForILLoadSuccess(string placeId, string msg)
		{
			Debug.Log ("===> TellToDoctor il load ok at: " + placeId);
			adCall.TellToDoctor (Action_Doctor_Ad_IL_LoadOk_Reply, placeId, msg);
		}

		private void doctorForRDLoadFail(string placeId, string msg)
		{
			Debug.Log ("===> TellToDoctor rd load fail at: " + placeId);
			adCall.TellToDoctor (Action_Doctor_Ad_RD_LoadFail_Reply, placeId, msg);
		}

		private void replyToDoctor(string action, string adid, string msg) {
			adCall.TellToDoctor (action, adid, msg);
		}

		private void doctorForRDLoadSuccess(string placeId, string msg)
		{
			Debug.Log ("===> TellToDoctor rd load ok at: " + placeId);
			adCall.TellToDoctor (Action_Doctor_Ad_RD_LoadOk_Reply, placeId, msg);
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
					Debug.Log ("===> function:UnityPlugin AA reporting:" + isReportOnlineDebug + ",doctorWorking: " + adCall.IsDoctorWorking());
					if (isReportOnlineDebug) {
						adCall.reportPluginInvokeMethodCall ("UnityPlugin doCallback, function: " + function + ",cpadsid: " + placeId + ",doctorWorking: " + adCall.IsDoctorWorking());
					}
				}


				if (function.Equals (Action_Doctor_ON_DUTY)) {
					if (isReportOnlineDebug) {
						adCall.DoctorOnDuty ();
					}
				} else if (function.Equals (Action_Doctor_OFF_DUTY)) {
					if (isReportOnlineDebug) {
						adCall.DoctorOffDuty ();
					}
				} else if (function.Equals (Function_Doctor_IL_Show_Request)) {
					if (isReportOnlineDebug) {
						adCall.showInterstitialAd (Function_Doctor_IL_Show_AdId);
					}
				} else if (function.Equals (Function_Doctor_RD_Show_Request)) {
					if (isReportOnlineDebug) {
						adCall.showRewardAd (Function_Doctor_RD_Show_AdId);
					}
				} else if (function.Equals (Function_Doctor_IL_Load_Request)) {
					if (isReportOnlineDebug) {
						UPSDK.setIntersitialLoadCallback (Function_Doctor_IL_Show_AdId,
							new System.Action<string, string>(doctorForILLoadSuccess),
							new System.Action<string, string>(doctorForILLoadFail)
						);
					}
				} else if (function.Equals (Function_Doctor_RD_Load_Request)) {
					if (isReportOnlineDebug) {
						UPSDK.setRewardVideoLoadCallback (
							new System.Action<string, string>(doctorForRDLoadSuccess),
							new System.Action<string, string>(doctorForRDLoadFail)
						);
					}
				}

				//reward callback
				else if (function.Equals (Function_Reward_WillOpen)) {
					string fmsg = "";
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						replyToDoctor (Action_Doctor_Ad_RD_WillShow_Reply, Function_Doctor_RD_Show_AdId, "tell the willopen event to doctor.");
						return;
					}
					else if (UPSDK.UPRewardWillOpenCallback != null) {
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
					}
				} else if (function.Equals (Function_Reward_DidOpen)) {
					string fmsg = "";
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportAdVideoShowDid("unity plugin got the didopen event. ");
						replyToDoctor (Action_Doctor_Ad_RD_DidShow_Reply, Function_Doctor_RD_Show_AdId, "tell the didopen event to doctor.");
						return;
					}
					else if (UPSDK.UPRewardDidOpenCallback != null) {
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
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportAdVideoClick("unity plugin got the didclick event. ");
						replyToDoctor (Action_Doctor_Ad_RD_DidClick_Reply, Function_Doctor_RD_Show_AdId, "tell the didclick event to doctor.");
						return;
					}
					else if (UPSDK.UPRewardDidClickCallback != null) {
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
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportAdVideoClose("unity plugin got the didclose event. ");
						replyToDoctor (Action_Doctor_Ad_RD_DidClose_Reply, Function_Doctor_RD_Show_AdId, "tell the didclose event to doctor.");
						return;
					}
					else if (UPSDK.UPRewardDidCloseCallback != null) {
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
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportAdVideoRewardGiven("unity plugin got the givenreward event. ");
						replyToDoctor (Action_Doctor_Ad_RD_Given_Reply, Function_Doctor_RD_Show_AdId, "tell the givenreward event to doctor.");
						return;
					}
					else if (UPSDK.UPRewardDidGivenCallback != null) {
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
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportAdVideoRewardCancel("unity plugin got the noreward event. ");
						replyToDoctor (Action_Doctor_Ad_RD_Cancel_Reply, Function_Doctor_RD_Show_AdId, "tell the noreward event to doctor.");
						return;
					}
					else if (UPSDK.UPRewardDidAbandonCallback != null) {
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
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						replyToDoctor (Action_Doctor_Ad_IL_WillShow_Reply, Function_Doctor_IL_Show_AdId, "tell the willshow event to doctor.");
						return;
					}
					else if (UPSDK.UPInterstitialWillShowCallback != null) {
						UPSDK.UPInterstitialWillShowCallback (placeId, msg);
						fmsg = "UnityPlugin Run UPSDK.UPInterstitialWillShowCallback()";
					}
					else if (PolyADSDK.OldInterstitialWillShowCallback != null) {
						PolyADSDK.OldInterstitialWillShowCallback (placeId, msg);
						fmsg = "UnityPlugin Run PolyADSDK.OldInterstitialWillShowCallback()";
					}
					if (isReportOnlineDebug) {
						
					}
				}
				else if (function.Equals (Function_Interstitial_Didshow)) {
					string fmsg = "can't run InterstitialDidShowCallback(), no delegate object.";
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportILDidShow(placeId, "unity plugin IL got the didshow event. ");
						replyToDoctor (Action_Doctor_Ad_IL_DidShow_Reply, Function_Doctor_IL_Show_AdId, "tell the didshow event to doctor.");
						return;
					}
					else if (UPSDK.UPInterstitialDidShowCallback != null) {
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
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportILClose(placeId, "unity plugin IL got the didclose event. ");
						replyToDoctor (Action_Doctor_Ad_IL_DidClose_Reply, Function_Doctor_IL_Show_AdId, "tell the didclose event to doctor.");
						return;
					}
					else if (UPSDK.UPInterstitialDidCloseCallback != null) {
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
					if (isReportOnlineDebug && adCall.IsDoctorWorking()) {
						adCall.reportILClick(placeId, "unity plugin IL got the didclick event. ");
						replyToDoctor (Action_Doctor_Ad_IL_DidClick_Reply, Function_Doctor_IL_Show_AdId, "tell the didclick event to doctor.");
						return;
					}
					else if (UPSDK.UPInterstitialDidClickCallback != null) {
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

