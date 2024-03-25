#import <Foundation/Foundation.h>
#import <AVFoundation/AVFoundation.h>
#import <MediaPlayer/MediaPlayer.h>
#import <StoreKit/StoreKit.h>

@interface HeaderFileTest : NSObject <AVAudioPlayerDelegate, MPMediaPickerControllerDelegate>

+(HeaderFileTest*)TestMusicManager;

- (void)setupiOSMusic;
- (void)findAppleMusic:(NSString*)songName;

@end
