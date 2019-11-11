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

		UPSDK.showIntersitialAd("sample_inter");
	}

	public void onBtnIntertitial_CCC_Click()
	{
		UPSDK.showIntersitialAd("sample_inter");
	}

	public void onBtnReward_aaa_Click()
	{
		UPSDK.showRewardAd("rewarded_video");
	}

	public void onBtnBanner_Top_Click()
	{
         //sample_banner_inland is placementid		
		UPSDK.showBannerAdAtTop("sample_banner");
		
	}

	public void onBtnBanner_Bottom_Click()
	{
    	UPSDK.showBannerAdAtBottom("sample_banner");

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
		string r = UPSDK.getAbtConfig ("freeCoins");
		Debug.Log ("==> onBtnGetABConfig_Click:" + r);
	}

	public void onBtnAutoInspect_Click()
	{
		UPSDK.autoOneKeyInspect ();
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

			UPSDK.UPInterstitialWillShowCallback=new System.Action<string, string>(actionForInterstitialWillShow);
			UPSDK.UPInterstitialDidShowCallback = new System.Action<string, string>(actionForInterstitialDidShow);
			UPSDK.UPInterstitialDidClickCallback = new System.Action<string, string>(actionForInterstitialDidClick);
			UPSDK.UPInterstitialDidCloseCallback = new System.Action<string, string>(actionForInterstitialDidClose);
			

			UPSDK.UPBannerDidShowCallback = new System.Action<string, string>(actionForSdkBannerDidShow);
			UPSDK.UPBannerDidClickCallback = new System.Action<string, string>(actionForSdkBannerDidClick);
			UPSDK.UPBannerDidRemoveCallback = new System.Action<string, string>(actionForSdkBannerRemove);


            UPSDK.UPRewardWillOpenCallback=new  System.Action<string, string>(actionForSdkRewardWillOpen);
			UPSDK.UPRewardDidOpenCallback = new System.Action<string, string>(actionForSdkRewardDidOpen);
			UPSDK.UPRewardDidClickCallback = new System.Action<string, string>(actionForSdkRewardDidClick);
			UPSDK.UPRewardDidCloseCallback = new System.Action<string, string>(actionForSdkRewardDidClose);
			UPSDK.UPRewardDidGivenCallback = new System.Action<string, string>(actionForSdkRewardDidGiven);
			UPSDK.UPRewardDidAbandonCallback = new System.Action<string, string>(actionForSdkRewardDidAbandon);
			
		}

		if (TEST_AD) {
			inited = true;


			Text text = GameObject.Find ("CallText").GetComponent<Text> ();

			//text.text = PolyADSDK.testCall ();
			
			string tt = PolyADSDK.initPolyAdSDK (UPConstant.SDKZONE_FOREIGN);
			UPSDK.runCallbackAfterAppFocus (true);
			Debug.Log ("initPolyAdSDK ====> " + tt);
			if (tt != null) {
				text.text = tt;
			}

		}


	}

	private void actionForSdkInitFinish(bool result, string msg) {
		Debug.Log ("===> actionForSdkInitFinish Callback r: " + result + ", msg: " + msg);
	}

  

	// test for reward video callback
	
	private void actionForRewardLoadFail(string placeId, string msg)
	{
		Debug.Log ("===> actionForRewardLoadFail Callback at: " + placeId);
	}

	private void actionForRewardLoadSuccess(string placeId, string msg)
	{
		Debug.Log ("===> actionForRewardLoadSuccess Callback at: " + placeId);
	}


	private void actionForSdkRewardWillOpen(string placeId, string msg)
	{
		Debug.Log ("===> actionForSdkRewardWillOpen Callback at: " + placeId);
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
   // Callback for banner 
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

   //  Callback for Interstitial
    private void actionForIntsLoadFail(string placeId, string msg)
	{
		Debug.Log ("===> actionForIntsLoadFail Callback at: " + placeId);
	}

	private void actionForIntsLoadSuccess(string placeId, string msg)
	{
		Debug.Log ("===> actionForIntsLoadSuccess Callback at: " + placeId);
	}

    private void actionForInterstitialWillShow(string placeId, string msg) {
		Debug.Log ("===> actionForInterstitialWillShow Callback at: " + placeId);
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
