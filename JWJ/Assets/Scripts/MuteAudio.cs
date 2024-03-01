using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[RequireComponent (typeof(AudioSource))]
public class MuteAudio : MonoBehaviour
{
    public GameObject soundControlButton;
    public Sprite audioOffSprite;
    public Sprite audioOnSprite;

    //public delegate void ReloadAction();
    //public static event ReloadAction OnReloadAction;

    //iOS
    //public GameObject IOSGameObject;
    //bool iOSPlaying;
    //public AudioSource iOSMusicAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
        //iOSPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (AudioListener.pause == true)
        {
            soundControlButton.GetComponent<Image>().sprite = audioOffSprite;
        }
        else
        {
            soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
        }
        
     /*
        if (iOSPlaying == true)
        {
            //IOSGameObject.GetComponent<AudioSource>().enabled = true;
          Debug.Log("iOS Playing is equal to true");
          IOSGameObject.GetComponent<iOSMusicGUI>().PlayPause();
        }
        else if (iOSPlaying == false)
        { 
            //IOSGameObject.GetComponent<AudioSource>().enabled = false;
            //Debug.Log("iOS Playing is equal to false");
            IOSGameObject.GetComponent<iOSMusicGUI>().PlayPause();
            //IOSGameObject.GetComponent<iOSMusicGUI>().UnPauseAudio();
        }
       */
    }

    public void SoundControl()
    {
        if (AudioListener.pause == true)
        {
            AudioListener.pause = false;
            //soundControlButton.GetComponent<Image>().sprite = audioOnSprite;
        }
        else
        {
            AudioListener.pause = true;
            //soundControlButton.GetComponent<Image>().sprite = audioOffSprite;
        }
        /*
        if (iOSPlaying == true)
        {
            iOSPlaying = false;

        }
        else if (iOSPlaying == false)
        {
            iOSPlaying = true;
        }
       
        //IOSGameObject = GetComponent<AudioSource>().PauseAudio();
        
        IOSGameObject.GetComponent<AudioSource>();
        if (IOSGameObject != null)
        {
            iOSPlaying = true;
            Debug.Log("iOS Game Object is not null and therefore playing");
        }
        
        else if (IOSGameObject == null)
        {
            iOSPlaying = false;
             Debug.Log("iOS Game Object is  null and NOT playing");
        }
        */
    }
}