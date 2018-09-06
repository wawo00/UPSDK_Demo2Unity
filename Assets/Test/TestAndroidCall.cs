using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Polymer;
using System;
using System.IO;

using System.Text.RegularExpressions;


public class TestAndroidCall : MonoBehaviour {

	private bool inited;
	private bool isInitSDK;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void onBtnIntertitialClick() 
	{
		//inter_aaa
		//inter_ccc
		Polymer.PolyADSDK.showIntersitialAd("inter_aaa");
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnIntertitial_CCC_Click()
	{
		Polymer.PolyADSDK.showIntersitialAd("inter_ccc");
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnReward_aaa_Click()
	{
		Polymer.PolyADSDK.showRewardAd("aaa");
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnBanner_Top_Click()
	{
		Polymer.PolyADSDK.showBannerAdAtTop("banner_aaa");
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnBanner_Bottom_Click()
	{
		Polymer.PolyADSDK.showBannerAdAtBottom("banner_bbb");
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnBanner_Top_Del_Click()
	{
		Polymer.PolyADSDK.removeBannerAdAt ("banner_aaa");
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnBanner_Bottom_Del_Click()
	{
		Polymer.PolyADSDK.removeBannerAdAt ("banner_bbb");
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtn_ClickForIntsLoadCallback() {
		if(!isInitSDK){
			showTextMessage("");
		}
		Polymer.PolyADSDK.setIntersitialLoadCallback ("inter_aaa", 
			new System.Action<string, string>(actionForIntsLoadSuccess),
			new System.Action<string, string>(actionForIntsLoadFail) 
		);
	}

	public void onBtn_ClickForRewardLoadCallback() {
		if(!isInitSDK){
			showTextMessage("");
		}
		Polymer.PolyADSDK.setRewardVideoLoadCallback ( 
			new System.Action<string, string>(actionForRewardLoadSuccess),
			new System.Action<string, string>(actionForRewardLoadFail) 
		);
	}

	public void onBtnExitAd_Click()
	{
		if(!isInitSDK){
			showTextMessage("");
		}
		Polymer.PolyADSDK.onBackPressed ();
	}

	public void onBtnExitApp_Click() {
		//Application.Quit();
		Polymer.PolyADSDK.setCustomerIdForAndroid("666");
	}

	public void onBtnInitABConfig_Click()
	{
		Polymer.PolyADSDK.initAbtConfigJson("gameAccountId", true, 1234, "324000", "gender", 33, new string[]{"This is first elements.", "Then is the second one.", "The last one."});
		showTextMessage("初始化ABTest数据，等待几秒，可以通过获取ABTest功能查看数据 ！");
	}

	public void onBtnShowRewardView_Click() {
		Polymer.PolyADSDK.showRewardDebugView();
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnShowInterstitialView_Click() {
		Polymer.PolyADSDK.showInterstitialDebugView();
		if(!isInitSDK){
			showTextMessage("");
		}
	}

	public void onBtnGetABConfig_Click()
	{
		string r = Polymer.PolyADSDK.getAbtConfig ("addStep");
		Debug.Log ("==> onBtnGetABConfig_Click:" + r);
		showTextMessage("获取ABTest数据==>" + r);
//		PolyADSDK.loadAvidlyAdsByManual();
//		Debug.Log ("==> loadAvidlyAdsByManual:");
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
			Polymer.PolyADSDK.AvidlySDKInitFinishedCallback = new System.Action<bool, string>(actionForSdkInitFinish);
			Polymer.PolyADSDK.AvidlyInterstitialDidClickCallback = new System.Action<string, string>(actionForInterstitialDidClick);
			Polymer.PolyADSDK.AvidlyInterstitialDidCloseCallback = new System.Action<string, string>(actionForInterstitialDidClose);
			Polymer.PolyADSDK.AvidlyInterstitialDidShowCallback = new System.Action<string, string>(actionForInterstitialDidShow);

			Polymer.PolyADSDK.AvidlyBannerDidShowCallback = new System.Action<string, string>(actionForSdkBannerDidShow);
			Polymer.PolyADSDK.AvidlyBannerDidClickCallback = new System.Action<string, string>(actionForSdkBannerDidClick);
			Polymer.PolyADSDK.AvidlyBannerDidRemoveCallback = new System.Action<string, string>(actionForSdkBannerRemove);

			Polymer.PolyADSDK.AvidlyRewardDidOpenCallback = new System.Action<string, string>(actionForSdkRewardDidOpen);
			Polymer.PolyADSDK.AvidlyRewardDidClickCallback = new System.Action<string, string>(actionForSdkRewardDidClick);
			Polymer.PolyADSDK.AvidlyRewardDidCloseCallback = new System.Action<string, string>(actionForSdkRewardDidClose);
			Polymer.PolyADSDK.AvidlyRewardDidGivenCallback = new System.Action<string, string>(actionForSdkRewardDidGiven);
			Polymer.PolyADSDK.AvidlyRewardDidAbandonCallback = new System.Action<string, string>(actionForSdkRewardDidAbandon);

			#if UNITY_ANDROID && !UNITY_EDITOR

			Polymer.PolyADSDK.AvidlyExitAdDidShowCallback = new System.Action<string> (actionForSdkExitAdDidShow);
			Polymer.PolyADSDK.AvidlyExitAdDidClickCallback = new System.Action<string> (actionForSdkExitAdDidClick);
			Polymer.PolyADSDK.AvidlyExitAdDidClickMoreCallback = new System.Action<string> (actionForSdkExitAdDidClickMore);
			Polymer.PolyADSDK.AvidlyExitAdOnExitCallback = new System.Action<string> (actionForSdkExitAdOnExit);
			Polymer.PolyADSDK.AvidlyExitAdOnCancelCallback = new System.Action<string> (actionForSdkExitAdOnExit);

			#endif
		}
		inited = true;
		isInitSDK = true;
		//init方法之前使用
		PolyADSDK.setCustomerIdForAndroid (SystemInfo.deviceUniqueIdentifier);
		PolyADSDK.initPolyAdSDK (UPConstant.SDKZONE_AUTO);
		Debug.Log ("---android id--"+SystemInfo.deviceUniqueIdentifier);
	

		// Text text = GameObject.Find ("CallText").GetComponent<Text> ();

		//text.text = PolyADSDK.testCall ();

		// string tt = PolyADSDK.initPolyAdSDK (PolyADSDK.SDKZONE_FOREIGN);
		//Debug.Log ("initPolyAdSDK ====> " + tt);
		// if (tt != null) {
		// 	text.text = tt;
		// }
	}
	private void showTextMessage(string msg){
		Text textMsg = GameObject.Find ("ABText").GetComponent<Text> ();
		if(isInitSDK){
			textMsg.text = msg;
		}else{
			textMsg.text = "请先初始化SDK！！";
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
		showTextMessage("插屏失败回调===>" + placeId + "《==msg==>" + msg);
	}

	private void actionForIntsLoadSuccess(string placeId, string msg)
	{
		Debug.Log ("===> actionForIntsLoadSuccess Callback at: " + placeId);
		showTextMessage("插屏成功回调===>" + placeId + "《==msg==>"  + msg);
	}

	private void actionForRewardLoadFail(string placeId, string msg)
	{
		Debug.Log ("===> actionForRewardLoadFail Callback at: " + placeId);
		showTextMessage("激励视频失败回调===>" + placeId + msg);
	}

	private void actionForRewardLoadSuccess(string placeId, string msg)
	{
		Debug.Log ("===> actionForRewardLoadSuccess Callback at: " + placeId);
		showTextMessage("激励视频成功回调===>" + placeId + msg);
	}

	private void actionForSdkRewardDidOpen(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkRewardDidOpen Callback at: " + placeId);
		showTextMessage("展示激励视频===>" + placeId);
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
		showTextMessage("展示Banner===>" + placeId);
	}

	private void actionForSdkInitFinish(bool result, string msg) {
		Debug.Log ("===> actionForSdkInitFinish Callback r: " + result + ", msg: " + msg);
		showTextMessage("初始化回调===>" + result + ", msg: " + msg);
	}

	private void actionForInterstitialDidShow(string placeId, string msg) {
		Debug.Log ("===> actionForInterstitialDidShow Callback at: " + placeId);
		showTextMessage("展示插屏===>" + placeId);
	}

	private void actionForInterstitialDidClick(string placeId, string msg) {
		Debug.Log ("===> actionForInterstitialDidClick Callback at: " + placeId);
	}

	private void actionForInterstitialDidClose(string placeId, string msg) {
		Debug.Log ("===> actionForInterstitialDidClose Callback at: " + placeId);
	}

	 
}
