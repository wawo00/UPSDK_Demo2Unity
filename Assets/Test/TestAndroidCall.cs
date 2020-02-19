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

		OpenUpSDK.showIntersitialAd("sample_inter");
	}

	public void onBtnIntertitial_CCC_Click()
	{
        OpenUpSDK.showIntersitialAd("sample_inter");
	}

	public void onBtnReward_aaa_Click()
	{
        OpenUpSDK.showRewardAd("rewarded_video");
	}

	public void onBtnBanner_Top_Click()
	{
        //sample_banner_inland is placementid		
        OpenUpSDK.showBannerAdAtTop("sample_banner");
		
	}

	public void onBtnBanner_Bottom_Click()
	{
        OpenUpSDK.showBannerAdAtBottom("sample_banner");

	}

	public void onBtnBanner_Top_Del_Click()
	{
        OpenUpSDK.removeBannerAdAt ("sample_banner");
	}

	public void onBtnBanner_Bottom_Del_Click()
	{
        OpenUpSDK.removeBannerAdAt ("sample_banner");
	}

	public void onBtn_ClickForIntsLoadCallback() {
        OpenUpSDK.setIntersitialLoadCallback ("sample_inter", 
			new System.Action<string, string>(actionForIntsLoadSuccess),
			new System.Action<string, string>(actionForIntsLoadFail) 
		);
	}

	public void onBtn_ClickForRewardLoadCallback() {
        OpenUpSDK.setRewardVideoLoadCallback ( 
			new System.Action<string, string>(actionForRewardLoadSuccess),
			new System.Action<string, string>(actionForRewardLoadFail) 
		);
	}

	public void onBtnExitAd_Click()
	{

		if (TEST_AD) {
            OpenUpSDK.onBackPressed ();
		}

	}

	public void onBtnExitApp_Click() {
		Application.Quit();
	}

	public void onBtnInitABConfig_Click()
	{
        OpenUpSDK.initAbtConfigJson("gameAccountId", true, 1234, "324000", "gender", 33, new string[]{"This is first elements.", "Then is the second one.", "The last one."});
	}

	public void onBtnShowRewardView_Click() {
        OpenUpSDK.showRewardDebugView();
	}

	public void onBtnShowInterstitialView_Click() {
        OpenUpSDK.showInterstitialDebugView();
	}

	public void onBtnGetABConfig_Click()
	{
		string r = OpenUpSDK.getAbtConfig ("freeCoins");
		Debug.Log ("==> onBtnGetABConfig_Click:" + r);
	}

	public void onBtnAutoInspect_Click()
	{
        OpenUpSDK.autoOneKeyInspect ();
	}
    public void onBtnGDPR_Click()
    {
        // 初始化代码
        UPConstant.UPAccessPrivacyInfoStatusEnum result = OpenUpSDK.getAccessPrivacyInfoStatus();
        if (result == UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusUnkown
            || result == UPConstant.UPAccessPrivacyInfoStatusEnum.UPAccessPrivacyInfoStatusFailed)
        {
            // 如未询问授权，先定位用户是否为欧盟地区
            // isEuropeanUserCallback 异步回调对象
            OpenUpSDK.checkUserAreaRegion(new Action<UPConstant.PrivacyUserRegionStatus, string>(actionUserRegionArea));

        }
        else
        {
             PolyADSDK.initUpAdSdk("873dec80afb8", "0", 0);
        }

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


            OpenUpSDK.UPSDKInitFinishedCallback = new System.Action<bool, string>(actionForSdkInitFinish);

            OpenUpSDK.UPInterstitialWillShowCallback=new System.Action<string, string>(actionForInterstitialWillShow);
            OpenUpSDK.UPInterstitialDidShowCallback = new System.Action<string, string>(actionForInterstitialDidShow);
            OpenUpSDK.UPInterstitialDidClickCallback = new System.Action<string, string>(actionForInterstitialDidClick);
            OpenUpSDK.UPInterstitialDidCloseCallback = new System.Action<string, string>(actionForInterstitialDidClose);


            OpenUpSDK.UPBannerDidShowCallback = new System.Action<string, string>(actionForSdkBannerDidShow);
            OpenUpSDK.UPBannerDidClickCallback = new System.Action<string, string>(actionForSdkBannerDidClick);
            OpenUpSDK.UPBannerDidRemoveCallback = new System.Action<string, string>(actionForSdkBannerRemove);


            OpenUpSDK.UPRewardWillOpenCallback=new  System.Action<string, string>(actionForSdkRewardWillOpen);
            OpenUpSDK.UPRewardDidOpenCallback = new System.Action<string, string>(actionForSdkRewardDidOpen);
            OpenUpSDK.UPRewardDidClickCallback = new System.Action<string, string>(actionForSdkRewardDidClick);
            OpenUpSDK.UPRewardDidCloseCallback = new System.Action<string, string>(actionForSdkRewardDidClose);
            OpenUpSDK.UPRewardDidGivenCallback = new System.Action<string, string>(actionForSdkRewardDidGiven);
            OpenUpSDK.UPRewardDidAbandonCallback = new System.Action<string, string>(actionForSdkRewardDidAbandon);
			
		}

		if (TEST_AD) {
			inited = true;


			Text text = GameObject.Find ("CallText").GetComponent<Text> ();

			//text.text = PolyADSDK.testCall ();
			
			string tt = PolyADSDK.initUpAdSdk("a399eb362d3c", "0",0);
            OpenUpSDK.runCallbackAfterAppFocus (true);
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

    private void actionUserRegionArea(UPConstant.PrivacyUserRegionStatus regionStatus,string msg)
    {
            regionStatus = UPConstant.PrivacyUserRegionStatus.PrivacyUserRegionStatusEU;
            OpenUpSDK.notifyAccessPrivacyInfoStatus(new Action<UPConstant.UPAccessPrivacyInfoStatusEnum, string>(accessPrivacyInforCallback), regionStatus);
            Debug.Log("===> actionForInterstitialDidClose Callback at: " + regionStatus);
    }

    private void accessPrivacyInforCallback(UPConstant.UPAccessPrivacyInfoStatusEnum result, string msg)
    {
        // 打印日志
        Debug.Log("===> accessPrivacyInforCallback Event result: " + result + "," + msg);
        // result 用户授权的结果
        // 不论结果如何，均需初始化 OpenUpSDK
        PolyADSDK.initUpAdSdk("a399eb362d3c", "0", 0);
    }

    public void setIsChild() {
		Debug.Log ("===> set isChild: ");
        PolyADSDK.setIsChild(true);
	}

	public void getIsChild() {
		Debug.Log ("===> getIsChild result is: " + PolyADSDK.isChild());
	}

	//获取所有路径
	public void getPathWithSetConfigurationFile(){

		XXPod.PodTool.fixPathWithSetConfigurationFile ();
	}

}
