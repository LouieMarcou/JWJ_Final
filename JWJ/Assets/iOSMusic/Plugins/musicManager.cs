using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class musicManager {

    [DllImport("__Internal")]
    private static extern void _setupiOSMusic();

    public static void setupiOSMusic()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
            _setupiOSMusic();
    }

    [DllImport("__Internal")]
	private static extern void _openNativeMusicPicker(bool toggle);
	
	public static void openNativeMusicPicker(bool toggle)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_openNativeMusicPicker(toggle);
	}
	
	[DllImport("__Internal")]
	private static extern void _playPause();
	
	public static void playPause()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_playPause();
	}

	[DllImport("__Internal")]
	private static extern void _loadAudioClip(bool toggle);
	
	public static void loadAudioClip(bool toggle)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_loadAudioClip(toggle);
	}

	[DllImport("__Internal")]
	private static extern void _previousSong();
	
	public static void previousSong()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_previousSong();
	}

	[DllImport("__Internal")]
	private static extern void _nextSong();
	
	public static void nextSong()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_nextSong();
	}

	[DllImport("__Internal")]
	private static extern void _randomSongWithNativeAudioPlayer();

	public static void randomSongWithNativeAudioPlayer()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_randomSongWithNativeAudioPlayer();
	}

	[DllImport("__Internal")]
	private static extern void _randomSongWithAudioSource();

	public static void randomSongWithAudioSource()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_randomSongWithAudioSource();
	}

	[DllImport("__Internal")]
	private static extern void _queryAppleMusic(string productID);

	public static void queryAppleMusic(string productID)
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_queryAppleMusic(productID);
	}

	[DllImport("__Internal")]
	private static extern void _stopAppleMusic();
	
	public static void stopAppleMusic()
	{
		if( Application.platform == RuntimePlatform.IPhonePlayer )
			_stopAppleMusic();
	}

	[DllImport("__Internal")]
	private static extern void _getLibraryContents();

	public static void getLibraryContents()
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_getLibraryContents();
	}

	[DllImport("__Internal")]
	private static extern void _getPlaylists();

	public static void getPlaylists()
    {
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_getPlaylists();
    }

	[DllImport("__Internal")]
	private static extern void _playPlaylistWithName(string playlistName);

	public static void playPlaylistWithName(string playlistName)
    {
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_playPlaylistWithName(playlistName);
    }

	[DllImport("__Internal")]
	private static extern void _playItemWithPersistentID(string persistentID);

	public static void playItemWithPersistentID(string persistentID)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer)
			_playItemWithPersistentID(persistentID);
	}
}
