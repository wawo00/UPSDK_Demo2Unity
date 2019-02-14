//
//  ALYSDKBaseCall.h
//  AvidlyAnalysisSDK
//
//  Created by samliu on 2018/5/29.
//  Copyright © 2018年 liuguojun. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ALYSDKBaseCall : NSObject

+ (ALYSDKBaseCall*)instance;

- (void)initSDK:(NSString *)objName
        funName:(NSString *)funName
      productId:(NSString *)productid
      channelId:(NSString *)channelId
          appId:(NSString *)appId
           zone:(NSInteger)zone;

- (void)disableAccessPrivacyInformation;

- (NSString *)userIdOfAnaysis;

- (void)traceWithKey:(NSString *)key mapString:(NSString *)json;

- (void)traceWithKey:(NSString *)key valueString:(NSString *)value;

- (void)traceKey:(NSString *)key;

- (void)countKey:(NSString *)key;

- (void)logPaymentWithPlayerId:(NSString *)playerId
             receiptDataString:(NSString *)receiptDataString;

- (void)logPaymentWithPlayerId:(NSString *)playerId
             gameAccountServer:(NSString *)gameAccountServer
             receiptDataString:(NSString *)receiptDataString;

- (void)ThirdpartyLogPaymentWithPlayerId:(NSString *)playerId
                              thirdparty:(NSString *)thirdparty
                       receiptDataString:(NSString *)receiptDataString;

- (void)ThirdpartyLogPaymentWithPlayerId:(NSString *)playerId
                       gameAccountServer:(NSString *)gameAccountServer
                              thirdparty:(NSString *)thirdparty
                       receiptDataString:(NSString *)receiptDataString;

- (void)guestLoginWithGameId:(NSString *)playerId;

- (void)facebookLoginWithGameId:(NSString *)playerId
                         OpenId:(NSString *)openId
                      OpenToken:(NSString *)openToken;

- (void)twitterLoginWithPlayerId:(NSString *)playerId
                       twitterId:(int64_t)twitterId
                 twitterUserName:(NSString *)twitterUserName
                twitterAuthToken:(NSString *)twitterAuthToken;

- (void)portalLoginWithPlayerId:(NSString *)playerId
                       portalId:(NSString *)portalId;

@property (nonatomic) BOOL isSDKInited;

@end
