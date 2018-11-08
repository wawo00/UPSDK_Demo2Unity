using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Polymer;
using System;
using System.IO;
using UPTrace;

using System.Text.RegularExpressions;


public class TestAndroidCall : MonoBehaviour {

	private bool inited;

	private bool TEST_AD = true;

	// Use this for initialization
	void Start () {
		//onButtonClick();
		//onBtnExitAd_Click();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onBtnIntertitialClick() 
	{
		//inter_aaa
		//inter_ccc
		UPSDK.showIntersitialAd("sample_inter");
	}

	public void onBtnIntertitial_CCC_Click()
	{
		UPSDK.showIntersitialAd("rewarded_video");
	}

	public void onBtnReward_aaa_Click()
	{
		UPSDK.showRewardAd("aaa");
	}

	public void onBtnBanner_Top_Click()
	{
		//BannerAd
		#if UNITY_ANDROID && !UNITY_EDITOR
			UPSDK.showBannerAdAtTop("sample_banner");
		#else
		UPSDK.showBannerAdAtTop("sample_banner");
		#endif

		UPSDK.showBannerAdAtBottom("sample_banner");
	}

	public void onBtnBanner_Bottom_Click()
	{
		UPSDK.removeBannerAdAt ("sample_banner");
		UPSDK.removeBannerAdAt ("sample_banner");

	}

	public void onBtnBanner_Top_Del_Click()
	{
		UPSDK.removeBannerAdAt ("sample_banner");
	}

	public void onBtnBanner_Bottom_Del_Click()
	{
		UPSDK.removeBannerAdAt ("sample_banner");
	}

	public void onBtn_ClickForIntsLoadCallback() {
		UPSDK.setIntersitialLoadCallback ("sample_inter", 
			new System.Action<string, string>(actionForIntsLoadSuccess),
			new System.Action<string, string>(actionForIntsLoadFail) 
		);
	}

	public void onBtn_ClickForRewardLoadCallback() {
		UPSDK.setRewardVideoLoadCallback ( 
			new System.Action<string, string>(actionForRewardLoadSuccess),
			new System.Action<string, string>(actionForRewardLoadFail) 
		);
	}

	public void onBtnExitAd_Click()
	{

		if (TEST_AD) {
			UPSDK.onBackPressed ();
		}

	}

	public void onBtnExitApp_Click() {
		Application.Quit();
	}

	public void onBtnInitABConfig_Click()
	{
		UPSDK.initAbtConfigJson("gameAccountId", true, 1234, "324000", "gender", 33, new string[]{"This is first elements.", "Then is the second one.", "The last one."});
	}

	public void onBtnShowRewardView_Click() {
		UPSDK.showRewardDebugView();
	}

	public void onBtnShowInterstitialView_Click() {
		UPSDK.showInterstitialDebugView();
	}

	public void onBtnGetABConfig_Click()
	{
		string r = UPSDK.getAbtConfig ("hello");
		Debug.Log ("==> onBtnGetABConfig_Click:" + r);
	}
	 
	private const string PRODUCTID = "8888882";
	private const string CHANNELID = "666668";

	/// <summary>
	/// test anaylse
	/// </summary>
	public void initOuterAnalyse_Click(){
		UPTraceApi.initTraceSDK (PRODUCTID, CHANNELID, UPTraceConstant.UPTraceSDKZoneEnum.UPTraceSDKZoneDomestic);
		Debug.Log ("==> initOuterAnalyse_Click:");
	}
	int i=0;
	//test report with anaylse 
	public void reportWithAnalyse_Click(){
		Debug.Log ("==> reportWithAnalyse_Click:"+i);
		i=i+1;
		Dictionary<string, string> dic = new Dictionary<string, string> ();
		dic.Add ("appid", "com.test.demo");
		dic.Add ("name", "player-001");
		dic.Add ("level", "1");
		UPTraceApi.traceDictionary ("KEY_PLAYER_DIC", dic);
		UPTraceApi.traceString ("KEY_PLAYER_STRING", "Hello, this is a testing data.");
		UPTraceApi.traceKey ("KEY_PLAYER_ONLY");
		UPTraceApi.countKey ("KEY_PLAYER_ONLY");
	}


	public void onBtnReadAssets_Click(){
		string filePath = Application.streamingAssetsPath + "/avidly_android/js_ad_sdk_native.js";
		//		string  filePath = "jar:file://" + Application.dataPath + "!/assets/Avidly_Android/log.txt";
		string jsonString;
		Debug.Log ("==> onBtnReadAssets_Click:" + filePath);
		bool exist = File.Exists (filePath);
		Debug.Log ("==> onBtnReadAssets_Click:" + exist);
//		if (!exist) {
//			return;
//		}
		if (Application.platform == RuntimePlatform.Android)
		{
			WWW reader = new WWW(filePath);
			while (!reader.isDone) { }

			jsonString = reader.text;
		}
		else
		{
			jsonString = File.ReadAllText(filePath);
		}
		Debug.Log ("==> onBtnReadAssets_Click:" + jsonString);
	}
	 
	public void onButtonClick()
	{
		//TextEditor text = GameObject.Find ("CallText").GetComponent<TextEditor>();


		if (!inited) {
			UPSDK.UPSDKInitFinishedCallback = new System.Action<bool, string>(actionForSdkInitFinish);
			UPSDK.UPInterstitialDidClickCallback = new System.Action<string, string>(actionForInterstitialDidClick);
			UPSDK.UPInterstitialDidCloseCallback = new System.Action<string, string>(actionForInterstitialDidClose);
			UPSDK.UPInterstitialDidShowCallback = new System.Action<string, string>(actionForInterstitialDidShow);

			UPSDK.UPBannerDidShowCallback = new System.Action<string, string>(actionForSdkBannerDidShow);
			UPSDK.UPBannerDidClickCallback = new System.Action<string, string>(actionForSdkBannerDidClick);
			UPSDK.UPBannerDidRemoveCallback = new System.Action<string, string>(actionForSdkBannerRemove);

			UPSDK.UPRewardDidOpenCallback = new System.Action<string, string>(actionForSdkRewardDidOpen);
			UPSDK.UPRewardDidClickCallback = new System.Action<string, string>(actionForSdkRewardDidClick);
			UPSDK.UPRewardDidCloseCallback = new System.Action<string, string>(actionForSdkRewardDidClose);
			UPSDK.UPRewardDidGivenCallback = new System.Action<string, string>(actionForSdkRewardDidGiven);
			UPSDK.UPRewardDidAbandonCallback = new System.Action<string, string>(actionForSdkRewardDidAbandon);

			#if UNITY_ANDROID && !UNITY_EDITOR

			UPSDK.UPExitAdDidShowCallback = new System.Action<string> (actionForSdkExitAdDidShow);
			UPSDK.UPExitAdDidClickCallback = new System.Action<string> (actionForSdkExitAdDidClick);
			UPSDK.UPExitAdDidClickMoreCallback = new System.Action<string> (actionForSdkExitAdDidClickMore);
			UPSDK.UPExitAdOnExitCallback = new System.Action<string> (actionForSdkExitAdOnExit);
			UPSDK.UPExitAdOnCancelCallback = new System.Action<string> (actionForSdkExitAdOnExit);

			#endif
		}

		if (TEST_AD) {
			inited = true;


			Text text = GameObject.Find ("CallText").GetComponent<Text> ();

			//text.text = PolyADSDK.testCall ();
			PolyADSDK.setCustomerIdForAndroid("PolyADSDK");
			string tt = PolyADSDK.initPolyAdSDK (UPConstant.SDKZONE_FOREIGN);
			UPSDK.runCallbackAfterAppFocus (true);
			Debug.Log ("initPolyAdSDK ====> " + tt);
			if (tt != null) {
				text.text = tt;
			}

		}


	}

	#if UNITY_ANDROID && !UNITY_EDITOR
	private void actionForSdkExitAdDidShow(string msg)
	{
		Debug.Log ("===> actionForSdkExitAdDidShow Callback");
	}

	private void actionForSdkExitAdDidClick(string msg)
	{
		Debug.Log ("===> actionForSdkExitAdDidClick Callback");
	}

	private void actionForSdkExitAdDidClickMore(string msg)
	{
		Debug.Log ("===> actionForSdkExitAdDidClickMore Callback");
	}

	private void actionForSdkExitAdOnExit(string msg)
	{
		Debug.Log ("===> actionForSdkExitAdOnExit Callback");
	}

	private void actionForSdkExitAdDidOnCancel(string msg)
	{
		Debug.Log ("===> actionForSdkExitAdDidOnCancel Callback");
	}
	#endif

	// test for reward video callback
	private void actionForIntsLoadFail(string placeId, string msg)
	{
		Debug.Log ("===> actionForIntsLoadFail Callback at: " + placeId);
	}

	private void actionForIntsLoadSuccess(string placeId, string msg)
	{
		Debug.Log ("===> actionForIntsLoadSuccess Callback at: " + placeId);
	}

	private void actionForRewardLoadFail(string placeId, string msg)
	{
		Debug.Log ("===> actionForRewardLoadFail Callback at: " + placeId);
	}

	private void actionForRewardLoadSuccess(string placeId, string msg)
	{
		Debug.Log ("===> actionForRewardLoadSuccess Callback at: " + placeId);
	}

	private void actionForSdkRewardDidOpen(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkRewardDidOpen Callback at: " + placeId);
	}

	private void actionForSdkRewardDidClick(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkRewardDidClick Callback at: " + placeId);
	}

	private void actionForSdkRewardDidClose(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkRewardDidClose Callback at: " + placeId);
	}

	private void actionForSdkRewardDidGiven(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkRewardDidGiven Callback at: " + placeId);
	}

	private void actionForSdkRewardDidAbandon(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkRewardDidAbandon Callback at: " + placeId);
	}

	private void actionForSdkBannerRemove(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkBannerRemove Callback at: " + placeId);
	}

	private void actionForSdkBannerDidClick(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkBannerDidClick Callback at: " + placeId);
	}

	private void actionForSdkBannerDidShow(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkBannerDidShow Callback at: " + placeId);
	}

	private void actionForSdkInitFinish(bool result, string msg) {
		Debug.Log ("===> actionForSdkInitFinish Callback r: " + result + ", msg: " + msg);
	}

	private void actionForInterstitialDidShow(string placeId, string msg) {
		Debug.Log ("===> actionForInterstitialDidShow Callback at: " + placeId);
	}

	private void actionForInterstitialDidClick(string placeId, string msg) {
		Debug.Log ("===> actionForInterstitialDidClick Callback at: " + placeId);
	}

	private void actionForInterstitialDidClose(string placeId, string msg) {
		Debug.Log ("===> actionForInterstitialDidClose Callback at: " + placeId);
	}


	//获取所有路径
	public void getPathWithSetConfigurationFile(){

		XXPod.PodTool.fixPathWithSetConfigurationFile ();
	}

	 
}
