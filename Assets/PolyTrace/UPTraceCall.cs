
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UPTrace
{
	public class UPTraceCall {



		#if UNITY_IOS && !UNITY_EDITOR

			[DllImport("__Internal")]
			private static extern void initSDKForIos(string gameName, string funName, string productid, string channelid, int zone);


			[DllImport("__Internal")]
			private static extern void disableAccessPrivacyInformationForIos();

			[DllImport("__Internal")]
			private static extern string getUserIdForIos();

			[DllImport("__Internal")]
			private static extern void logMapForIos(string key, string json);

			[DllImport("__Internal")]
			private static extern void logStringForIos(string key, string value);

			[DllImport("__Internal")]
			private static extern void logKeyForIos(string key);

			[DllImport("__Internal")]
			private static extern void countKeyForIos(string key);

			[DllImport("__Internal")]
			private static extern void logPaymentServerIap(string playerId, string gameAccountServer, string receiptDataString, bool isbase64);

			[DllImport("__Internal")]
			private static extern void logPaymentPlayerIap(string playerId, string receiptDataString, bool isbase64);


			[DllImport("__Internal")]
			private static extern void thirdpartyLogPaymentWithPlayerId(string playerId, string thirdparty, string receiptDataString);  

			[DllImport("__Internal")]
			private static extern void thirdpartyLogPaymentWithServerPlayerId(string playerId, string gameAccountServer, string thirdparty, string receiptDataString); 

			[DllImport("__Internal")]
			private static extern void guestLoginWithGameId(string playerId);

			[DllImport("__Internal")]
			private static extern void facebookLoginWithGameId(string playerId, string openId, string openToken);	

			[DllImport("__Internal")]
			private static extern void twitterLoginWithPlayerId(string playerId, string twitterId, string twitterUserName, string twitterAuthToken);

			[DllImport("__Internal")]
			private static extern void portalLoginWithPlayerId(string playerId, string portalId);

			

		#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.hola.unity.UPTraceProxy";
			private readonly static string JavaClassStaticMethod_InitTrace = "initTrace";
			private readonly static string JavaClassStaticMethod_DisableAccessPrivacyInformation = "disableAccessPrivacyInformation";
			private readonly static string JavaClassStaticMethod_GetCustomerId = "getCustomerId";
			private readonly static string JavaClassStaticMethod_SetCustomerId = "setCustomerId";
			private readonly static string JavaClassStaticMethod_GetOpenId = "getOpenId";
			private readonly static string JavaClassStaticMethod_GetUserId = "getUserId";

			private readonly static string JavaClassStaticMethod_LogMap = "logMap";
			private readonly static string JavaClassStaticMethod_LogString = "logString";
			private readonly static string JavaClassStaticMethod_LogKey = "logKey";
			private readonly static string JavaClassStaticMethod_Count = "countKey";

			private readonly static string JavaClassStaticMethod_LogPaymentWithServer = "logPaymentWithServer";
			private readonly static string JavaClassStaticMethod_LogPayment = "logPayment";

			private readonly static string JavaClassStaticMethod_GuestLogin = "guestLogin";
			private readonly static string JavaClassStaticMethod_FacebookLogin = "facebookLogin";
			private readonly static string JavaClassStaticMethod_TwitterLogin = "twitterLogin";
			private readonly static string JavaClassStaticMethod_PortalLogin = "portalLogin";



		#else
		// "do nothing";
		#endif



		public UPTraceCall() {
			UPTraceObject.getInstance ();
			#if UNITY_IOS && !UNITY_EDITOR
				Debug.Log ("===> UPTraceCall instanced.");
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> UPTraceCall instanced.");
				jc = new AndroidJavaClass (JavaClassName);
			}
			#endif
		}

		public void logPaymentPlayIdForIap(string playerId, string receiptDataString, bool isbase64) {
			if (null == playerId || playerId.Length == 0) {
				Debug.Log ("===> UPTraceCall.logPaymentPlayIdForIap(), error: the value of parameter playerId is null or empty.");
				return;
			}

			if (null == receiptDataString || receiptDataString.Length == 0) {
				Debug.Log ("===> UPTraceCall.logPaymentPlayIdForIap(), error: the value of parameter receiptDataString is null or empty.");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				logPaymentPlayerIap (playerId, receiptDataString, isbase64);
			#endif
		}

		public void logPaymentServerForIap(string playerId, string gameAccountServer, string receiptDataString, bool isbase64) {
			if (null == playerId || playerId.Length == 0) {
				Debug.Log ("===> UPTraceCall.logPaymentServerForIap(), error: the value of parameter playerId is null or empty.");
				return;
			}

			if (null == gameAccountServer || gameAccountServer.Length == 0) {
				Debug.Log ("===> UPTraceCall.logPaymentServerForIap(), error: the value of parameter gameAccountServer is null or empty.");
				return;
			}

			if (null == receiptDataString || receiptDataString.Length == 0) {
				Debug.Log ("===> UPTraceCall.logPaymentServerForIap(), error: the value of parameter receiptDataString is null or empty.");
				return;
			}
			#if UNITY_IOS && !UNITY_EDITOR
				logPaymentServerIap (playerId, gameAccountServer, receiptDataString, isbase64);
			#endif
		}
	
		public void initTtraceSDK (string productId, string channelId, UPTraceConstant.UPTraceSDKZoneEnum zone) {

			if (null == productId || productId.Length == 0) {
				Debug.Log ("===> UPTraceCall.initTtraceSDK(), error: the value of parameter productId is null or empty.");
				return;
			}

			if (null == channelId || channelId.Length == 0) {
				Debug.Log ("===> UPTraceCall.initTtraceSDK(), error: the value of parameter channelId is null or empty.");
				return;
			}

			int intzone = 0;
			if (zone == UPTraceConstant.UPTraceSDKZoneEnum.UPTraceSDKZoneDomestic) {
				intzone = 1;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			initSDKForIos(UPTraceObject.GameObject_Callback_Name, 
				UPTraceObject.Java_Callback_Function,
				productId,
				channelId,
				intzone);
		
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				//Debug.Log (JavaClassName);
				jc = new AndroidJavaClass (JavaClassName);
			}
			jc.CallStatic (JavaClassStaticMethod_InitTrace, 
				UPTraceObject.GameObject_Callback_Name, 
				UPTraceObject.Java_Callback_Function,
				productId,
				channelId,
				intzone);
			#endif
		}

		public void logDictionary(string key, Dictionary<string, string> dic) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}

			string value = dicationaryToString (dic);
			if (value == null) {
				Debug.Log ("can't trace a null dictionary." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			logMapForIos(key, value);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogMap, key, value);
			}
			#endif
		}

		public void logString(string key, string value) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}
				
			if (value == null) {
				Debug.Log ("can't trace a null value." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			logStringForIos(key, value);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogString, key, value);
			}
			#endif
		}

		public void logKey(string key) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}
				

			#if UNITY_IOS && !UNITY_EDITOR
			logKeyForIos(key);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogKey, key);
			}
			#endif
		}

		public void countKey(string key) {

			if (key == null) {
				return;
			}

			if (key.Length > 128) {
				Debug.Log ("the key's length must be lower than 128." );
				return;
			}


			#if UNITY_IOS && !UNITY_EDITOR
			countKeyForIos(key);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_Count, key);
			}
			#endif
		}



		public void disableAccessPrivacyInformation() {
			#if UNITY_IOS && !UNITY_EDITOR
			disableAccessPrivacyInformationForIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_DisableAccessPrivacyInformation);
			}
			#endif
		}

		public void setCustomerId(string curstomerId) {

			if (curstomerId == null) {
				Debug.Log ("===> fail to call setCustomerId(), curstomerId can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> setCustomerId() is not supported by IOS." );
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
			jc.CallStatic (JavaClassStaticMethod_SetCustomerId, curstomerId);
			}
			#endif
		}

		public string getCustomerId() {

			#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> getCustomerId() is not supported by IOS." );
			return "";
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_GetCustomerId);
			}
			return "";
			#else
			return "";
			#endif
		}

		public string getOpenId() {

			#if UNITY_IOS && !UNITY_EDITOR
			Debug.Log ("===> getOpenId() is not supported by IOS." );
			return "";
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_GetOpenId);
			}
			return "";
			#else
			return "";
			#endif
		}


		public string getUserId() {

			#if UNITY_IOS && !UNITY_EDITOR
			return getUserIdForIos();
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				return jc.CallStatic<string> (JavaClassStaticMethod_GetUserId);
			}
			return "";
			#else
			return "";
			#endif
		}

		public void logPaymentWithServer(string gameAccountId, string gameAccountServer, string iabPurchaseOriginalJson,
			string iabPurchaseSignature) {

			if (gameAccountId == null) {
				Debug.Log ("===> fail to call logPaymentWithServer(), gameAccountId can't be null." );
				return;
			}

			if (gameAccountServer == null) {
				Debug.Log ("===> fail to call logPaymentWithServer(), gameAccountServer can't be null." );
				return;
			}

			if (iabPurchaseOriginalJson == null) {
				Debug.Log ("===> fail to call logPaymentWithServer(), iabPurchaseOriginalJson can't be null." );
				return;
			}

			if (iabPurchaseSignature == null) {
				Debug.Log ("===> fail to call logPaymentWithServer(), iabPurchaseSignature can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			thirdpartyLogPaymentWithServerPlayerId(gameAccountId, gameAccountServer, iabPurchaseOriginalJson, iabPurchaseSignature);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogPaymentWithServer, gameAccountId, gameAccountServer, iabPurchaseOriginalJson, iabPurchaseSignature);
			}
			#endif
		}

		public void logPayment(string gameAccountId, string iabPurchaseOriginalJson, string iabPurchaseSignature) {

			if (gameAccountId == null) {
				Debug.Log ("===> fail to call logPayment(), gameAccountId can't be null." );
				return;
			}

			if (iabPurchaseOriginalJson == null) {
				Debug.Log ("===> fail to call logPayment(), iabPurchaseOriginalJson can't be null." );
				return;
			}

			if (iabPurchaseSignature == null) {
				Debug.Log ("===> fail to call logPayment(), iabPurchaseSignature can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			thirdpartyLogPaymentWithPlayerId(gameAccountId, iabPurchaseOriginalJson, iabPurchaseSignature);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_LogPayment, gameAccountId, iabPurchaseOriginalJson, iabPurchaseSignature);
			}
			#endif
		}
			
		public void guestLogin(string playerId) {

			if (playerId == null) {
				Debug.Log ("===> fail to call guestLogin(), gameAccountId can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			guestLoginWithGameId(playerId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
			jc.CallStatic (JavaClassStaticMethod_GuestLogin, playerId);
			}
			#endif
		}
			
		public void facebookLogin(string playerId, string openId, string openToken) {

			if (playerId == null) {
				Debug.Log ("===> fail to call facebookLogin(), gameAccountId can't be null." );
				return;
			}

			if (openId == null) {
				Debug.Log ("===> fail to call facebookLogin(), openId can't be null." );
				return;
			}

			if (openToken == null) {
				Debug.Log ("===> fail to call facebookLogin(), openToken can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			facebookLoginWithGameId(playerId, openId, openToken);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_FacebookLogin, playerId, openId, openToken);
			}
			#endif
		}

		public void portalLogin(string playerId, string portalId) {

			if (playerId == null) {
				Debug.Log ("===> fail to call portalLogin(), gameAccountId can't be null." );
				return;
			}

			if (portalId == null) {
				Debug.Log ("===> fail to call portalLogin(), portalId can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			portalLoginWithPlayerId(playerId, portalId);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_PortalLogin, playerId, portalId);
			}
			#endif
		}

		public void twitterLogin(string playerId, string twitterId, string twitterUserName, string twitterAuthToken) {

			if (playerId == null) {
				Debug.Log ("===> fail to call twitterLogin(), gameAccountId can't be null." );
				return;
			}

			if (twitterId == null) {
				Debug.Log ("===> fail to call twitterLogin(), twitterId can't be null." );
				return;
			}

			if (twitterUserName == null) {
				Debug.Log ("===> fail to call twitterLogin(), twitterUserName can't be null." );
				return;
			}

			if (twitterAuthToken == null) {
				Debug.Log ("===> fail to call twitterLogin(), twitterAuthToken can't be null." );
				return;
			}

			#if UNITY_IOS && !UNITY_EDITOR
			twitterLoginWithPlayerId(playerId, twitterId, twitterUserName, twitterAuthToken);
			#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc != null) 
			{
				jc.CallStatic (JavaClassStaticMethod_TwitterLogin, playerId, twitterId, twitterUserName, twitterAuthToken);
			}
			#endif
		}

		private string dicationaryToString(Dictionary<string, string> dic) {
			if (dic == null || dic.Count == 0) {
				return null;
			}

			string str = "{ \"array\":[";
			int len = dic.Count;
			int i = 0;
			foreach (KeyValuePair<string, string> kvp in dic) {
				str += "{\"k\":\"" + kvp.Key + "\",";
				str += "\"v\":\"" + kvp.Value + "\"}";
				if (i < len - 1) {
					str += ",";
				} else {
					str += "]}";
				}
				i++;
			}

			//Debug.Log ("dicationaryToString:" + str);
			return str;
		}

	}
}
