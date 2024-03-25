#import "HeaderFileTest.h"

void UnityPause(int pause);
void UnitySetAudioSessionActive(bool active);
UIViewController *UnityGetGLViewController();

@interface HeaderFileTest()

@property(nonatomic, retain)MPMediaPickerController *myPicker;
@property(nonatomic,retain)AVAudioPlayer *myPlayer;
@property(nonatomic,retain)NSMutableArray *myPlaylist;
@property(nonatomic)NSUInteger currentIndex;
@property(nonatomic)bool isAppending;
@property(nonatomic, retain)NSMutableDictionary *allItemsDictionary;

@end

@implementation HeaderFileTest

BOOL isPad() {
#ifdef UI_USER_INTERFACE_IDIOM
    return (UI_USER_INTERFACE_IDIOM() == UIUserInterfaceIdiomPad);
#else
    return NO;
#endif
}

+(HeaderFileTest*)TestMusicManager
{
	static HeaderFileTest *sharedSingleton;
	if(!sharedSingleton)
		sharedSingleton = [[HeaderFileTest alloc] init];
	return sharedSingleton;
}

-(id)init
{
    if((self = [super init]))
    {
        
    }
    
    return self;
}

 - (BOOL)prefersStatusBarHidden {

   return YES;
}

- (UIViewController *)childViewControllerForStatusBarHidden {
    return self.myPicker;
}

-(void)setupiOSMusic {
    [MPMediaLibrary requestAuthorization:^(MPMediaLibraryAuthorizationStatus status){
        switch (status) {
            case MPMediaLibraryAuthorizationStatusNotDetermined: {
                NSLog(@"Authorization status not determined");
                break;
            }
            case MPMediaLibraryAuthorizationStatusRestricted: {
                NSLog(@"Authorization status restricted");
                break;
            }
            case MPMediaLibraryAuthorizationStatusDenied: {
                NSLog(@"Authorization status denied");
                break;
            }
            case MPMediaLibraryAuthorizationStatusAuthorized: {
                NSLog(@"Authorization status authorized");
                break;
            }
            default: {
                break;
            }
        }
    }];
}