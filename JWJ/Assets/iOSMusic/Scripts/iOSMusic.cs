﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;

[RequireComponent (typeof(AudioSource))]
public class iOSMusic : MonoBehaviour {

    public static iOSMusic instance = null;
	private float[] musicData;

    [SerializeField]
	private AudioSource _audioSource;
	public AudioSource iOSMusicAudioSource
	{
	    get { return _audioSource; }
	    set { _audioSource = value; }
	}

	private AudioClip _audioClip;
	public AudioClip iOSMusicClip
	{
		get { return _audioClip; }
		set { _audioClip = value; }
	}

	private bool _hasAudioClipStartedPlaying;
	public bool HasAudioClipStartedPlaying
	{
		get { return _hasAudioClipStartedPlaying; }
		set { _hasAudioClipStartedPlaying = value; }
	}

	private bool _isAudioClipPaused;
	public bool IsAudioClipPaused
	{
		get { return _isAudioClipPaused; }
		set { _isAudioClipPaused = value; }	
	}

	private bool _shouldAppendToPlaylist;
	public bool ShouldAppendToPlaylist
	{
	    get { return _shouldAppendToPlaylist; }
	    set { _shouldAppendToPlaylist = value; }
	}

	private Hashtable mediaLibraryContents;
	private List<Hashtable> mediaItemMetadataElements;
	private string currentItemID;

	void Awake () {
        if (instance == null)   
        {             
            instance = this;
        } else if (instance != this)
        {
			Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
	
	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();

		ShouldAppendToPlaylist = HasAudioClipStartedPlaying = false;

        musicManager.setupiOSMusic();

        #region Playlist-functionality

        // retrieve the names of all of the user's local music playlists
        // you can do this periodically to refresh as the user may create/delete playlists over time
        //musicManager.getPlaylists();

        // play a playlist by name when you need to - I have a playlist named "Run" on my device that I listen to while running
        //musicManager.playPlaylistWithName("Run");

        #endregion

        #region Custom-UI-generation

		//mediaLibraryContents = new Hashtable();
		//musicManager.getLibraryContents();

        #endregion
    }

    // Update is called once per frame
    void Update () {
	    CheckAudioSourcePlayback();
	}

	void CheckAudioSourcePlayback()
	{
	    // If an song playing via an Audio Source finishes playing, attempt to load the next song in the playlist via an Audio Source.
		if(HasAudioClipStartedPlaying && !iOSMusicAudioSource.isPlaying && !IsAudioClipPaused)
	    {
			HasAudioClipStartedPlaying = false;
	        musicManager.nextSong();
	    }
	}

	public void PauseAudio()
	{
		iOSMusicAudioSource.Pause();
		IsAudioClipPaused = true;
	}

	public void UnPauseAudio()
	{
		iOSMusicAudioSource.UnPause();
		IsAudioClipPaused = false;
	}

	public void HandleAppendToggleChange(bool toggle)
	{
	    // Handles the "Append to Playlist" toggle state change.
	    ShouldAppendToPlaylist = toggle;
	}

    #region ShuffleMode

    public void RandomSongWithNativeAudioPlayer()
    {
		// get a random song from the user's library and play it with the native audio audio player
		musicManager.randomSongWithNativeAudioPlayer();
    }

	public void RandomSongWithAudioSource()
    {
		// get a random song from the user's library that is compatible with Audio Source mode and play it
		musicManager.randomSongWithAudioSource();
    }

    #endregion

    void ResetButtonStates()
	{
	    if(GameObject.Find("Button - Next") && GameObject.Find("Button - Previous"))
	    {
		    GameObject.Find("Button - Next").GetComponent<Button>().interactable = true;
		    GameObject.Find("Button - Previous").GetComponent<Button>().interactable = true;
		}
	}

	public void LoadAudioClip() 
	{
		if(iOSMusicAudioSource.isPlaying) {
			iOSMusicAudioSource.Stop();
			Resources.UnloadUnusedAssets();

			if(iOSMusicAudioSource.clip !=null){
				iOSMusicAudioSource.clip = null;
			}
		}

		string path = Application.persistentDataPath.Substring (0, Application.persistentDataPath.Length - 5);
		path = path.Substring (0, path.LastIndexOf ('/'));
		string songPath = path + "/Documents/" + "song" + ".m4a";
		StartCoroutine(LoadMusic (songPath));
	
	}

	IEnumerator LoadMusic(string songPath) {
		if(System.IO.File.Exists(songPath)) {
			iOSMusicAudioSource.Stop();

            using (var uwr = UnityWebRequestMultimedia.GetAudioClip("file://" + songPath, AudioType.AUDIOQUEUE))
            {
                ((DownloadHandlerAudioClip)uwr.downloadHandler).streamAudio = true;

                yield return uwr.SendWebRequest();

                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    Debug.LogError(uwr.error);
                    yield break;
                }

                DownloadHandlerAudioClip dlHandler = (DownloadHandlerAudioClip)uwr.downloadHandler;

                if (dlHandler.isDone)
                {
                    AudioClip audioClip = dlHandler.audioClip;

                    if (audioClip != null)
                    {
                        _audioClip = DownloadHandlerAudioClip.GetContent(uwr);

                        // extract music data - optional
                        //ExtractMusicData(_audioClip);

                        iOSMusicAudioSource.clip = _audioClip;
                        iOSMusicAudioSource.loop = false;
                        iOSMusicAudioSource.Play();
                        HasAudioClipStartedPlaying = true;
                        Debug.Log("Playing song using Audio Source!");
                        ResetButtonStates();
                    }
                    else
                    {
                        Debug.Log("Couldn't find a valid AudioClip :(");
                    }
                }
                else
                {
                    Debug.Log("The download process is not completely finished.");
                }
            }
        }
        else 
		{
			Debug.Log("Unable to locate converted song file.");
		}
	}

    void ExtractMusicData(AudioClip songClip)
    {
        musicData = new float[songClip.samples * songClip.channels];

        Debug.Log("Extracting music data... found " + musicData.Length.ToString() + " samples!");

        // Use GetData() to access audio sample data
        songClip.GetData(musicData, 0);

        // do some processing
        //for (int i = 0; i < musicData.Length; ++i)
        //{
        //}
    }
	
	#region Metadata extraction

	void ExtractTitle (string title) {
		Debug.Log ("Title: " + title);

		Hashtable metadataElement = new Hashtable();
		metadataElement.Add("Title", title);
		mediaItemMetadataElements.Add(metadataElement);
	}

	void ExtractArtist (string artist) {
		Debug.Log ("Artist: " + artist);

		Hashtable metadataElement = new Hashtable();
		metadataElement.Add("Artist", artist);
		mediaItemMetadataElements.Add(metadataElement);
	}

	void ExtractAlbumTitle (string albumTitle) {
		Debug.Log ("Album title: " + albumTitle);

		Hashtable metadataElement = new Hashtable();
		metadataElement.Add("Album Title", albumTitle);
		mediaItemMetadataElements.Add(metadataElement);
	}

	void ExtractBPM (string bpm) {
		Debug.Log ("BPM: " + bpm);

		Hashtable metadataElement = new Hashtable();
		metadataElement.Add("BPM", bpm);
		mediaItemMetadataElements.Add(metadataElement);
	}

	void ExtractGenre (string genre) {
		Debug.Log ("Genre: " + genre);

		Hashtable metadataElement = new Hashtable();
		metadataElement.Add("Genre", genre);
		mediaItemMetadataElements.Add(metadataElement);
	}

	void ExtractLyrics (string lyrics) {
		Debug.Log ("Lyrics: " + lyrics);

		Hashtable metadataElement = new Hashtable();
		metadataElement.Add("Lyrics", lyrics);
		mediaItemMetadataElements.Add(metadataElement);
	}

	void ExtractDuration (string duration) {
		Debug.Log ("Duration: " + duration);

		Hashtable metadataElement = new Hashtable();
		metadataElement.Add("Duration", duration);
		mediaItemMetadataElements.Add(metadataElement);

		FinalizeItemExtraction();
	}
	
	void ExtractArtwork () {
		Texture2D tex = null;
		byte[] fileData;
		string artworkPath = Application.persistentDataPath + "/songArtwork.png";

		if (File.Exists(artworkPath))     {
			fileData = File.ReadAllBytes(artworkPath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); 

			// Convert newly created texture to a Sprite and assign it to the Canvas Image object.
			Sprite artworkSprite = Sprite.Create(tex, new Rect(0,0,tex.width,tex.height), new Vector2(.5f,.5f),40);
			GameObject.Find("Image - Album Artwork").GetComponent<Image>().sprite = artworkSprite;

			Hashtable metadataElement = new Hashtable();
			metadataElement.Add("Artwork", artworkSprite);
			mediaItemMetadataElements.Add(metadataElement);
		}		
	}

	void ExtractLibraryMediaItem(string itemPersistentID)
    {
		print("Received a persistent id for media item: " + itemPersistentID);

		if (!mediaLibraryContents.ContainsKey(itemPersistentID))
		{
			currentItemID = itemPersistentID;

			if(GameObject.Find("Text - List of Music Library Contents"))
            {
				GameObject.Find("Text - List of Music Library Contents").GetComponent<Text>().text = GameObject.Find("Text - List of Music Library Contents").GetComponent<Text>().text + "\n" + currentItemID;
			}

			mediaItemMetadataElements.Clear();
		}
	}

	void FinalizeItemExtraction()
    {
		mediaLibraryContents.Add(currentItemID, mediaItemMetadataElements);

		//print("finished extracting item " + currentItemID);
    }

	void HandleEndOfLibraryExtraction()
    {
		//print("finished library extraction of " + mediaLibraryContents.Count.ToString() + " items");
    }

	public void PlayMediaWithPersistentID(InputField inputField)
    {
		musicManager.playItemWithPersistentID(inputField.text);
    }

	void ExtractPlaylists(string playListName)
    {
		//print("found a music playlist named: " + playListName);

		// store these names in an array and use in custom view later
    }

	#endregion

	void UserDidCancel () {
	    ResetButtonStates();
		Debug.Log("User has cancelled the song selection.");
	}
}