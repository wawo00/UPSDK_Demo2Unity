//
//  AvidlyAdSDKUnityProxy.h
//  AvidlyAdsSDK
//
//  Created by samliu on 2017/5/18.
//  Copyright © 2017年 liuguojun. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface AvidlyAdSDKUnityProxy : NSObject

extern "C" const char* initIosSDKByZone(const char* objName, const char* funName, const int zone);

extern "C" const char* initIosSDK(const char* objName, const char* funName);

extern "C" bool isInterstitialReady(const char* placeid);
extern "C" bool isRewardReady();
extern "C" void showInterstitial(const char* placementid);

extern "C" void showReward(const char* cpCustomId);

extern "C" void showBannerTop(const char *placementid);

extern "C" void showBannerBottom(const char *placementid);

extern "C" void showIcon(const double x, const double y, const double width, const double height, const double rotationAngle, const char *placementid);

extern "C" void removeIcon(const char *placementid);

extern "C" void hideTopBanner();

extern "C" void hideBottomBanner();

extern "C" void removeBannerAd(const char *placementid);

extern "C" void initAbtConfigJsonForIos(const char* gameAccountId, bool completeTask,
                                        int isPaid, const char* promotionChannelName,  const char* gender,
                                        int age, const char* tags);

extern "C" const char* getAbtConfigForIos(const char* placementId);

extern "C" void showRewardDebugController();

extern "C" void showInterstitialDebugController();
extern "C" void setInterstitialLoadCallbackAt(const char* placementid);

extern "C" void setRewardloadCallback();

extern "C" void loadAdsByManual();

extern "C" void setTopBannerPadingForIphonex(int padding);

extern "C" void updateAccessPrivacyInfoStatus(int value);

extern "C" void requestAuthorizationWithAlert(const char* objName, const char* funName);

extern "C" int getCurrentAccessPrivacyInfoStatus();

extern "C" void checkIsEuropeanUnionUser(const char* objName, const char* funName);

extern "C" void reportRDShowDid(const char* msg);

extern "C" void reportRDRewardGiven(const char* msg);

extern "C" void reportRDRewardCancel(const char* msg);

extern "C" void reportRDRewardClick(const char* msg);

extern "C" void reportRDRewardClose(const char* msg);

extern "C" void reportILShowDidCall(const char* cpPlaceId, const char* msg);

extern "C" void reportILClickCall(const char* cpPlaceId, const char* msg);

extern "C" void reportILCloseCall(const char* cpPlaceId, const char* msg);

extern "C" void reportInvokeMethodReceive(const char* msg);

extern "C" bool IsIosReportOnlineEnable();

@end
