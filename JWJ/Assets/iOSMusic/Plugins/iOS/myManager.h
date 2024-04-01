#import <Foundation/Foundation.h>
#import <AVFoundation/AVFoundation.h>
#import <MediaPlayer/MediaPlayer.h>
#import <StoreKit/StoreKit.h>

@interface myManager : NSObject <AVAudioPlayerDelegate, MPMediaPickerControllerDelegate>

+(myManager*)MusicManager;

-(void)setupiOSMusic;
-(void)openNativeMusicPicker:(bool)shouldAppendToPlaylist;
-(void)playPause;
-(void)loadAudioClip:(bool)shouldAppendToPlaylist;
-(void)nextSong;
-(void)previousSong;
-(void)randomSongWithNativeAudioPlayer;
-(void)randomSongWithAudioSource;
-(void)queryAppleMusic:(NSString*)productID;
-(void)stopAppleMusic;
-(void)playPlaylistWithName:(NSString*)playlistName;
-(void)getPlaylists;
-(void)getLibraryContents;
-(void)playItemWithPersistentID:(NSString*)itemPersistentID;

@end
