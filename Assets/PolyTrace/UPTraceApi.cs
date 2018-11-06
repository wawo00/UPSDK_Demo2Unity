using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace UPTrace
{

	public class UPTraceApi
	{

		// 是否已经初始化
		private static bool isInited; 
		private static UPTraceCall polyCall = null;


		private static void instanceOfCall() {
			if (polyCall == null) {
				polyCall = new UPTraceCall (); 
			}
		}

		/**
		 * 初始化统计包(Android与Ios均支持)
		 * @param productId 产品ID，必须正确指定且不能为空
		 * @param channelId 产品推广渠道 ，必须正确指定且不能为空
		 * @param zone 枚举类型，分中国大陆以及海外两个区域
		 */
		public static void initTraceSDK(string productId, string channelId, UPTraceConstant.UPTraceSDKZoneEnum zone) {
			if (isInited) {
				return;
			}

			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.initTtraceSDK (productId, channelId, zone);
			isInited = true;
		}

		/**
		 * 判断统计SDK是否被初始化(Android与Ios均支持)
		 */
		public static bool isTraceSDKInited() {
			return isInited;
		}

		/**
		 * 无参数事件打点方法(Android与Ios均支持)
		 * @param key, 打点事件id
		 */
		public static void traceKey(string key) {
			if (isInited) {
				polyCall.logKey (key);
			} else {
				Debug.Log ("Fail to call traceKey(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 单参数事件打点方法(Android与Ios均支持)
		 * @param key, 打点事件id
		 * @param value, string类型参数内容
		 */
		public static void traceString(string key, string value) {
			if (isInited) {
				polyCall.logString (key, value);
			} else {
				Debug.Log ("Fail to call traceString(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 多参数事件打点方法(Android与Ios均支持)
		 * @param key, 打点事件id
		 * @param dic, string类型的打点参数以Dictionary类型存储
		 */
		public static void traceDictionary(string key, Dictionary<string, string> dic) {
			if (isInited) {
				polyCall.logDictionary (key, dic);
			} else {
				Debug.Log ("Fail to call traceDictionary(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 计数事件，用于只用记录事件的次数的场景(Android与Ios均支持)
		 * @param key, 打点事件的id
		 */
		public static void countKey(string key) {
			if (isInited) {
				polyCall.countKey (key);
			} else {
				Debug.Log ("Fail to call countKey(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 支付上报，仅用于IOS平台，非Appstore的第三方支付数据上报
		 * @param playerId, 打点事件的id
		 */
		public static void traceThirdpartyPaymentForIos(string playerId, string thirdparty, string receiptDataString) {
			if (isInited) {
				#if UNITY_IOS && !UNITY_EDITOR
					polyCall.logPayment (playerId, thirdparty, receiptDataString);
				#endif
			} else {
				Debug.Log ("Fail to call traceThirdpartyPaymentForIos(), please initialize the analysis SDK first!!!");
			}
		}
		 
		/**
		 * 支付上报，仅用于IOS平台，非Appstore的第三方支付数据上报
		 * @param playerId, 打点事件的id
		 */
		public static void traceThirdpartyPaymentWithServerForIos(string playerId, string gameAccountServer, string thirdparty, string receiptDataString) {
			if (isInited) {
				#if UNITY_IOS && !UNITY_EDITOR
					polyCall.logPaymentWithServer (playerId, gameAccountServer, thirdparty, receiptDataString);
				#endif
			} else {
				Debug.Log ("Fail to call traceThirdpartyPaymentWithServerForIos(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 支付上报，仅用于IOS平台，方法内部做了平台检查，IOS会自动忽略
		 * @param playerId, 游戏用户ID
		 * @param receiptDataString IAP收据
		 * @param isbase64 receiptDataString 是否已经base64编码处理，如果没有方法内部完成base64处理
		 */
		public static void tracePaymentWithPlayerIdForIos(string playerId, string receiptDataString, bool isbase64) {
			if (isInited) {
				#if UNITY_IOS && !UNITY_EDITOR
				polyCall.logPaymentPlayIdForIap (playerId, receiptDataString, isbase64);
				#endif
			} else {
				Debug.Log ("Fail to call tracePaymentWithPlayerIdForIos(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 支付上报，仅用于IOS平台，方法内部做了平台检查，IOS会自动忽略
		 * @param playerId, 游戏用户ID
		 * @param gameAccountServer, 游戏区/服ID
		 * @param receiptDataString IAP收据
		 * @param isbase64 receiptDataString 是否已经base64编码处理，如果没有方法内部完成base64处理
		 */
		public static void tracePaymentWithServerForIos(string playerId, string gameAccountServer, string receiptDataString, bool isbase64) {
			if (isInited) {
				#if UNITY_IOS && !UNITY_EDITOR
				polyCall.logPaymentServerForIap (playerId, gameAccountServer, receiptDataString, isbase64);
				#endif
			} else {
				Debug.Log ("Fail to call tracePaymentWithServerForIos(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 支付上报，仅用于Android平台，方法内部做了平台检查，IOS会自动忽略
		 * @param playerId, 游戏用户ID，请传入CP方自己的player ID（请确认同登录上报的playerId保持一致）
		 * @param iabPurchaseOriginalJson
		 * @param iabPurchaseSignature
		 * 详细请参考统计包Android说明文档的HolaPay.logPayment()方法介绍
		 * https://coding.net/u/holaverse/p/HolaAnalysisSDK/git/blob/master/android/doc/integration.md?public=true
		 */
		public static void tracePaymentForAndroid(string gameAccountId, string iabPurchaseOriginalJson, string iabPurchaseSignature) {
			if (isInited) {
				#if UNITY_ANDROID && !UNITY_EDITOR
					polyCall.logPayment (gameAccountId, iabPurchaseOriginalJson, iabPurchaseSignature);
				#endif
			} else {
				Debug.Log ("Fail to call tracePaymentForAndroid(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * 支付上报，可以区分游戏的服务器分区，仅用于Android平台，方法内部做了平台检查，IOS会自动忽略
		 * 详细请参考统计包Android说明文档的HolaPay.logPaymentWithServer()方法介绍
		 * https://coding.net/u/holaverse/p/HolaAnalysisSDK/git/blob/master/android/doc/integration.md?public=true
		 */
		public static void tracePaymentWithServerForAndroid(string gameAccountId, string gameAccountServer, string iabPurchaseOriginalJson,
			string iabPurchaseSignature) {
			if (isInited) {
				#if UNITY_ANDROID && !UNITY_EDITOR
					polyCall.logPaymentWithServer (gameAccountId, gameAccountServer, iabPurchaseOriginalJson, iabPurchaseSignature);
				#endif
			} else {
				Debug.Log ("Fail to call tracePaymentWithServerForAndroid(), please initialize the analysis SDK first!!!");
			}
		}

		/**
		 * Twitter登录日志上报(Android与Ios均支持)
		 * 详细请参考统计包说明文档的HolaPay.twitterLogin()方法介绍
		 * https://coding.net/u/holaverse/p/HolaAnalysisSDK/git/blob/master/android/doc/integration.md?public=true
		 */
		public static void twitterLogin(string playerId, string twitterId, string twitterUserName, string twitterAuthToken) {
			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.twitterLogin (playerId, twitterId, twitterUserName, twitterAuthToken);
		}

		/**
		 * 平台账号登录日志上报(Android与Ios均支持)
		 * 详细请参考统计包说明文档的HolaPay.portalLogin()方法介绍
		 * https://coding.net/u/holaverse/p/HolaAnalysisSDK/git/blob/master/android/doc/integration.md?public=true
		 */
		public static void portalLogin(string playerId, string portalId) {
			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.portalLogin (playerId, portalId);
		}
		
		/**
		 * Facebook登录日志上报(Android与Ios均支持)
		 * 详细请参考统计包说明文档的HolaPay.facebookLogin()方法介绍
		 * https://coding.net/u/holaverse/p/HolaAnalysisSDK/git/blob/master/android/doc/integration.md?public=true
		 */
		public static void facebookLogin(string playerId, string openId, string openToken) {
			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.facebookLogin (playerId, openId, openToken);
		}
			
		/**
		 * 游客登录日志上报(Android与Ios均支持)
		 * 详细请参考统计包说明文档的HolaPay.guestLogin()方法介绍
		 * https://coding.net/u/holaverse/p/HolaAnalysisSDK/git/blob/master/android/doc/integration.md?public=true
		 */
		public static void guestLogin(string playerId) {
			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.guestLogin (playerId);
		}

		/**
		 * 获取统计包的UserId(Android与Ios均支持)
		 * 如果返回值为空，建议延迟多次获取
		 */
		public static string getUserId() {
			if (isInited) {
				return polyCall.getUserId ();
			} else {
				Debug.Log ("Fail to call getUserId(), please initialize the analysis SDK first!!!");
				return "";
			}
		}

		/**
		 * 获取统计包的OpenId(仅Android支持)
		 * 可在初始化SDK之前调用
		 */
		public static string getOpenIdForAndroid() {
			if (null == polyCall) {
				instanceOfCall ();
			}
			return polyCall.getOpenId ();
		}

		/**
		 * 获取向统计包的传递的CustomerId(仅Android支持)
		 * 可在初始化SDK之前调用
		 */
		public static string getCustomerIdForAndroid() {
			if (null == polyCall) {
				instanceOfCall ();
			}
			return polyCall.getCustomerId ();
		}

		/**
		 * 向统计包的传递CustomerId(仅Android支持)
		 * 请在初始化SDK之前调用
		 * 对于非GP的包，对于非欧盟用户可以传androidid，欧盟用户传统计包的openid
		 */
		public static void setCustomerIdForAndroid(string curstomerId) {
			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.setCustomerId (curstomerId);
		}

		/**
		 * 欧盟用户且明确拒绝GDPR授权申请后调用此方法(Android与Ios均支持)
		 * 可在初始化SDK之前调用
		 */
		public static void disableAccessPrivacyInformation() {
			if (null == polyCall) {
				instanceOfCall ();
			}
			polyCall.disableAccessPrivacyInformation ();
		}
	}

}
