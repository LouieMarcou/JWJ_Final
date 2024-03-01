using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(AudioSource))]
public class AudioManDancePanel : MonoBehaviour
{

    #region Properties
    
     public Sprite audioOffSprite;
     public Button danceOne;
        

    //public GameObject IOSGameObject;
    public GameObject ObjectManagerMain;
    public GameObject ObjectManagerDance;
    public GameObject ObjectManagerPrayer;

    public AudioSource audioMain;
    public AudioSource audioPray;
    public AudioSource audioDance;


    //Dance
    public AudioClip maroonPanel;
    public AudioClip tealPanel;

  
	//iOS
	public AudioSource iOSMusicAudioSource;

    [SerializeField]
    private Animator AnimatorDance;

    //Dance 
    bool faster_teal;
    bool slower_maroon;

    //Music 
    bool music;
    //Booleans - Source
    bool sourceMain;
    bool sourceDance;
    bool sourcePray;
    bool sourceIOS;
    //Reset from Prayer ordance Panel back to Main Panel
    bool reset_back_main;
    //??Matt do we need a reset button back to the Main Panel from the Music Panel? 
    bool anim_idle;
    bool anim_slow;
    bool anim_fast;
    bool anim_none;


    public enum AudioType
    {
        //Dance
        Maroon,
        Teal,
        //iOS
        IOS,
        Silent
    }

    public enum AnimationType
    {

        Idle,
        Fast,
        Slow,
        None

    }
    public enum SourceType
    {

        FromMain,
        FromPray,
        FromDance,
        FromIOS
    }

    public enum ObjectManager
    {
        MainObjectManager,
        PrayerObjectManager,
        DanceObjectManager,
    }

    AudioType currentAudioType;
    AnimationType currentAnimationType;
    SourceType currentAudioSource;
    ObjectManager currentObjectManager;

    [SerializeField] Button tealButton;
    [SerializeField] Button maroonButton;
    [SerializeField] Button iosButton;
    [SerializeField] List<Button> buttons;

    private GameObject jesusGameObject;
    [SerializeField] GameObject debugImage;

    #endregion

    #region Unity Methods


    //Changed so that it is only called when the script becomes active
    void Reset()
 
    {
        //Dance
        ObjectManagerDance.GetComponent<AudioSource>();
        if (ObjectManagerDance == null)
        {
            Debug.Log(" From Reset _ AudioSource(ObjectManagerDance) is NULL ..." + currentAudioSource + currentAudioType + currentAnimationType);
            enabled = true;
        }

        else if (ObjectManagerDance != null)
        {
           Debug.Log("From Reset_)AudioSource(ObjectManagerDance) was not Null..." + currentAudioSource+ currentAudioType + currentAnimationType);
           enabled = true;
        }

        
        //Animation
        AnimatorDance.GetComponent<Animator>();
        if (AnimatorDance == null)
        {
            //Debug.Log("Dance Panel - Animator has not been set..." + currentAnimationType);
            enabled = false;
        }

        else if (AnimatorDance != null)
        {
            //Debug.Log("Dance Panel - Animator has been successfully set ..." + currentAnimationType);
            enabled = false;
        }
       
    }


    void Update()
    {
        /*if (sourceMain == true && sourceDance == false && sourcePray == false && sourceIOS == false)
        {
            audioMain.GetComponent<AudioSource>().enabled = true;
        }

        else if (sourceMain == false && sourceDance == true && sourcePray == false && sourceIOS == false)
        {

            audioDance.GetComponent<AudioSource>().enabled = true;
        }

        else if (sourceMain == false && sourceDance == false && sourcePray == true && sourceIOS == false)
        {

            audioPray.GetComponent<AudioSource>().enabled = true;
        }

        else if (sourceMain == false && sourceDance == false && sourcePray == false && sourceIOS == true)
        {

            iOSMusicAudioSource.GetComponent<AudioSource>().enabled = true;
        }

        if (faster_teal == false && slower_maroon == false && reset_back_main == false && music == false && anim_idle == false && anim_slow == false && anim_fast == false && anim_none == false)

        {
            CurrentAudioAndAnimationNone();

        }

        else if (faster_teal == false && slower_maroon == false && reset_back_main == false && music == false && anim_idle == true && anim_slow == false && anim_fast == false && anim_none == false)

        {
            CurrentAudioNoneAndAnimationIdle();

        }

        else if (faster_teal == false && slower_maroon == true && reset_back_main == false && music == false && anim_idle == false && anim_slow == true && anim_fast == false && anim_none == false)

        {
            CurrentAudioMaroonAndAnimSlow();

        }

        else if (faster_teal == true && slower_maroon == false && reset_back_main == false && music == false && anim_idle == false && anim_slow == false && anim_fast == true && anim_none == false)

        {
            CurrentAudioTealAndAnimFast();
        }

        else if (faster_teal == false && slower_maroon == false && reset_back_main == false && music == true && anim_idle == false && anim_slow == false && anim_fast == false && anim_none == true)

        {
            CurrentAudioIOSAndAnimationNone();

        }

        else if (faster_teal == false && slower_maroon == false && reset_back_main == false && music == true && anim_idle == false && anim_slow == true && anim_fast == false && anim_none == false)

        {
            CurrentAudioIOSandAnimSlow();

        }

        else if (faster_teal == false && slower_maroon == false && reset_back_main == false && music == true && anim_idle == false && anim_slow == false && anim_fast == true && anim_none == false)

        {
            CurrentAudioIOSandAnimFast();
        }


        //Reset 
        else if (faster_teal == false && slower_maroon == false && reset_back_main == true && music == false && anim_idle == true && anim_slow == false && anim_fast == false && anim_none == false)
        {
            ResetAudioandAnimation();
        }*/
    }

    #endregion

    public void DanceMaroonPanel(Button button)
    {
        //Reset();

        Debug.Log("Maroon Button hit for testing - LK" + currentAudioSource + currentAudioType + currentAnimationType);

        //ObjectManagerMain.gameObject.SetActive(false);
        //ObjectManagerDance.gameObject.SetActive(true);
        //ObjectManagerPrayer.gameObject.SetActive(false);
        // If the IOS native player is currently playing, make it stop.
        //musicManager.stopMusic();
        //IOSGameObject.gameObject.SetActive(false);
        
        //iOSMusic.instance.iOSMusicAudioSource.GetComponent<AudioSource>().enabled = false;

        audioMain = GetComponent<AudioSource>();
        audioDance = GetComponent<AudioSource>();

        if(audioDance.isPlaying && audioDance.clip == maroonPanel)
        {
            audioDance.Pause();
        }
        else if(audioDance.isPlaying && audioDance.clip != maroonPanel)
        {
            audioDance.Stop();
            audioDance.clip = maroonPanel;
            audioDance.Play();
        }
        else if(audioDance.isPlaying == false)
        {
            audioDance.clip = maroonPanel;
            audioDance.Play();
            
        }
        //ChangeButtonSprites(button);
        /*//if (audioMain.isPlaying)
        //{
        //    audioMain.GetComponent<AudioSource>().enabled = false;
        //    Debug.Log("Maroon Button hit Audiosource was Main Panel now Dance Panel" + currentAudioSource + currentAudioType + currentAnimationType);
        //    //Source
        //    sourceMain = false;
        //    sourceDance = true;
        //    sourcePray = false;
        //    sourceIOS = false;
        //    //update is called
        //    enabled = true;

        //    //audioSwitch
        //    if (currentAudioType == AudioType.Maroon || currentAudioType == AudioType.Teal || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        //    {
        //        //Audio dance
        //        faster_teal = false;
        //        slower_maroon = true;
        //        music = false;
        //        reset_back_main = false;
        //        //animation type stays slow
        //        anim_idle = false;
        //        anim_slow = true;
        //        anim_fast = false;
        //        anim_none = false;
        //        //This should call to CurrentAudioMaroonAndAnimSlow
        //        enabled = true;
        //    }

        //}

        //else if (!audioMain.isPlaying)
        //{

        //    audioDance = GetComponent<AudioSource>();
        //    if (audioDance.isPlaying)
        //    {
        //        audioDance.GetComponent<AudioSource>().enabled = true;
        //        //Source
        //        sourceMain = false;
        //        sourceDance = true;
        //        sourcePray = false;
        //        sourceIOS = false;
        //        //update is called
        //        enabled = true;

        //        //audioSwitch
        //        if (currentAudioType == AudioType.Maroon || currentAudioType == AudioType.Teal || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        //        {
        //            //Audio dance
        //            faster_teal = false;
        //            slower_maroon = true;
        //            music = false;
        //            reset_back_main = false;
        //            //animation type stays slow
        //            anim_idle = false;
        //            anim_slow = true;
        //            anim_fast = false;
        //            anim_none = false;
        //            //This should call to CurrentAudioMaroonAndAnimSlow
        //            enabled = true;
        //        }

        //    }
        //    else if (!audioDance.isPlaying)
        //    {
        //        iOSMusicAudioSource = GetComponent<AudioSource>();
        //        //If the iOS music is playing 
        //        if (iOSMusicAudioSource.isPlaying)
        //        {
        //            //IOS music is already playing so keep that audioSource
        //            //Disable audiosource for Teal 
        //            iOSMusicAudioSource.GetComponent<AudioSource>().enabled = true;
        //            audioDance.GetComponent<AudioSource>().enabled = false;
        //            //Source
        //            sourceMain = false;
        //            sourceDance = false;
        //            sourcePray = false;
        //            sourceIOS = true;
        //            //update is called
        //            enabled = true;

        //            if (currentAudioType == AudioType.IOS && currentAnimationType == AnimationType.None || currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast)
        //            {
        //                //This is when the current audio is IOS and jesus is currently not dancing or dancing fast
        //                //User Hits the teal Button
        //                //audio type stays IOS
        //                //animation dances fast
        //                //Audio 
        //                faster_teal = false;
        //                slower_maroon = false;
        //                music = true;
        //                reset_back_main = false;
        //                //animation type stays fast
        //                anim_idle = false;
        //                anim_slow = true;
        //                anim_fast = false;
        //                anim_none = false;
        //                //CurrentAudioIOSAndAnimSlow
        //                //call to update
        //                enabled = true;

        //            }
        //        }

        //        //IOS music is not playing audiosource should be audioDance
        //        else if (!iOSMusicAudioSource.isPlaying)
        //        {
        //            audioDance.GetComponent<AudioSource>().enabled = false;
        //            Debug.Log("#1 _ Dance Panel - Audio is Maroon and animation is slow..." + currentAudioSource + currentAudioType + currentAnimationType);
        //            //Source
        //            sourceMain = false;
        //            sourceDance = true;
        //            sourcePray = false;
        //            sourceIOS = false;
        //            //update is called
        //            enabled = true;

        //            //audioSwitch
        //            if (currentAudioType == AudioType.Maroon || currentAudioType == AudioType.Teal || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        //            {
        //                //Audio dance
        //                faster_teal = false;
        //                slower_maroon = true;
        //                music = false;
        //                reset_back_main = false;
        //                //animation type stays slow
        //                anim_idle = false;
        //                anim_slow = true;
        //                anim_fast = false;
        //                anim_none = false;
        //                //This should call to CurrentAudioTealAndAnimFast
        //                enabled = true;
        //            }
        //        }
        //    }
        //}*/



    }
    
    

    
    public void DanceTealPanel(Button button)
    { 
         //Reset();
          
        Debug.Log("Teal Button hit for testing - LK" + currentAudioSource + currentAudioType + currentAnimationType);   

        //ObjectManagerMain.gameObject.SetActive(false);
        //ObjectManagerDance.gameObject.SetActive(true);
        //ObjectManagerPrayer.gameObject.SetActive(false);
        // If the IOS native player is currently playing, make it stop.
        //musicManager.stopMusic();
        //IOSGameObject.gameObject.SetActive(false);
        
         //iOSMusic.instance.iOSMusicAudioSource.GetComponent<AudioSource>().enabled = false;


        audioMain = GetComponent<AudioSource>();
        audioDance = GetComponent <AudioSource>();

        if(audioDance.isPlaying && audioDance.clip == tealPanel)
        {
            audioDance.Pause();
        }
        else if(audioDance.isPlaying && audioDance.clip != tealPanel)
        {
            audioDance.Stop();
            audioDance.clip = tealPanel;
            audioDance.Play();
        }
        else
        {
            audioDance.clip = tealPanel;
            audioDance.Play();
        }
        //ChangeButtonSprites(button);
        //jesusGameObject.GetComponent<JesusAnimationController>().StartDanceOne();
        jesusGameObject.GetComponent<JesusAnimationController>().ChangeAnimationState("danceOne");
        /*//if (audioMain.isPlaying)
        //{
        //    audioMain.GetComponent<AudioSource>().enabled = false;
        //    //Source
        //    sourceMain = false;
        //    sourceDance = true;
        //    sourcePray = false;
        //    sourceIOS = false;
        //    //update is called
        //    enabled = true;

        //    //audioSwitch
        //    if (currentAudioType == AudioType.Maroon || currentAudioType == AudioType.Teal || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        //    {
        //        //Audio dance
        //        faster_teal = true;
        //        slower_maroon = false;
        //        music = false;
        //        reset_back_main = false;
        //        //animation type stays slow
        //        anim_idle = false;
        //        anim_slow = false;
        //        anim_fast = true;
        //        anim_none = false;
        //        //This should call to CurrentAudioTealAndAnimFast
        //        enabled = true;
        //    }

        //}

        //else if (!audioMain.isPlaying)
        //{


        //    audioDance = GetComponent<AudioSource>();
        //    if (audioDance.isPlaying)
        //    {
        //        audioDance.GetComponent<AudioSource>().enabled = true;
        //        //Source
        //        sourceMain = false;
        //        sourceDance = true;
        //        sourcePray = false;
        //        sourceIOS = false;
        //        //update is called
        //        enabled = true;

        //        //audioSwitch
        //        if (currentAudioType == AudioType.Maroon || currentAudioType == AudioType.Teal || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        //        {
        //            //Audio dance
        //            faster_teal = true;
        //            slower_maroon = false;
        //            music = false;
        //            reset_back_main = false;
        //            //animation type stays slow
        //            anim_idle = false;
        //            anim_slow = false;
        //            anim_fast = true;
        //            anim_none = false;
        //            //This should call to CurrentAudioTealAndAnimFast
        //            enabled = true;
        //        }

        //    }
        //        else if (!audioDance.isPlaying)
        //        {
        //            iOSMusicAudioSource = GetComponent<AudioSource>();
        //            //If the iOS music is playing 
        //            if (iOSMusicAudioSource.isPlaying)
        //            {
        //            //IOS music is already playing so keep that audioSource
        //            //Disable audiosource for Teal 
        //            iOSMusicAudioSource.GetComponent<AudioSource>().enabled = true;
        //            audioDance.GetComponent<AudioSource>().enabled = false;
        //            //Source
        //            sourceMain = false;
        //            sourceDance = false;
        //            sourcePray = false;
        //            sourceIOS = true;
        //            //update is called
        //            enabled = true;

        //            if (currentAudioType == AudioType.IOS && currentAnimationType == AnimationType.None || currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast)
        //            {
        //                //This is when the current audio is IOS and jesus is currently not dancing or dancing fast
        //                //User Hits the teal Button
        //                //audio type stays IOS
        //                //animation dances fast
        //                //Audio 
        //                faster_teal = false;
        //                slower_maroon = false;
        //                music = true;
        //                reset_back_main = false;
        //                //animation type stays fast
        //                anim_idle = false;
        //                anim_slow = false;
        //                anim_fast = true;
        //                anim_none = false;
        //                //CurrentAudioIOSAndAnimFast
        //                //call to update
        //                enabled = true;

        //            }


        //        }

        //        else if (!iOSMusicAudioSource.isPlaying)
        //        {
        //            audioDance.GetComponent<AudioSource>().enabled = false;
        //            //Source
        //            sourceMain = false;
        //            sourceDance = true;
        //            sourcePray = false;
        //            sourceIOS = false;
        //            //update is called
        //            enabled = true;

        //            //audioSwitch
        //            if (currentAudioType == AudioType.Maroon || currentAudioType == AudioType.Teal || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Fast || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        //            {
        //                //Audio dance
        //                faster_teal = true;
        //                slower_maroon = false;
        //                music = false;
        //                reset_back_main = false;
        //                //animation type stays slow
        //                anim_idle = false;
        //                anim_slow = false;
        //                anim_fast = true;
        //                anim_none = false;
        //                //This should call to CurrentAudioTealAndAnimFast
        //                enabled = true;
        //            }
        //        }
        //    }

        //}*/

    }
    public void DanceIOSPanel()

    {

        //Reset();
         danceOne.GetComponent<Image>().sprite = audioOffSprite;

        //This turns off the main menu
        audioMain.GetComponent<AudioSource>().enabled = false;
        audioDance.GetComponent<AudioSource>().enabled = false;
        audioPray.GetComponent<AudioSource>().enabled = false;

        //iOSMusic.instance.iOSMusicAudioSource.GetComponent<AudioSource>().enabled = true;

        //Source
        sourceMain = false;
        sourceDance = false;
        sourcePray = false;
        sourceIOS = true;
        //call to update
        enabled = true;
        

        //audioSwitch
        if (currentAudioType == AudioType.Maroon || currentAudioType == AudioType.IOS  || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        {
            //Audio dance
            faster_teal = false;
            slower_maroon = false;
            music = true;
            reset_back_main = false;
            //animation type stays slow
            anim_idle = false;
            anim_slow = true;
            anim_fast = false;
            anim_none = false;
            //This should call to CurrentAudioIOSAndAnimSlow
            enabled = true;
        }

        //audioSwitch
        if ( currentAudioType == AudioType.IOS || currentAudioType == AudioType.Teal || currentAudioType == AudioType.Silent && currentAnimationType == AnimationType.Fast || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.None)

        {
            //Audio dance
            faster_teal = false;
            slower_maroon = false;
            music = true;
            reset_back_main = false;
            //animation type stays slow
            anim_idle = false;
            anim_slow = false;
            anim_fast = true;
            anim_none = false;
            //This should call to CurrentAudioIOSAndAnimFast
            enabled = true;
        }
    }
   
    public void BackDance()

    {
        //Reset();
        //check audioSource
        //if the user never hit any dance icons or the IOS icon on the Dance panel
        //and hits reset

        audioDance.Stop();
    
        /*ObjectManagerMain.GetComponent<AudioSource>();
        if (ObjectManagerMain == null)
        {
            Debug.Log("From Dance Back_Object Manager was Null " + currentAudioSource + currentAudioType + currentAnimationType);
            enabled = true;
        }
        
        else if (ObjectManagerMain != null)
        {
            Debug.Log("From Dance Back_Object Manager was NOT Null " + currentAudioSource + currentAudioType + currentAnimationType);

            audioMain.GetComponent<AudioSource>();
            if (audioMain.isPlaying)
            {
                //the audiosource is audioMain and should not be stopped
                ObjectManagerMain.gameObject.SetActive(true);
                ObjectManagerDance.gameObject.SetActive(true);
                Debug.Log("Back Dance - audioMain was playing and user hit the BACK from Dance - audio main should not be stop" + currentAudioSource + currentAudioType + currentAnimationType);

                //iOSMusic.instance.iOSMusicAudioSource.GetComponent<AudioSource>().enabled = false;
                
                sourceMain = true;
                sourceDance = false;
                sourcePray = false;
                sourceIOS = false;
                //update is called
                enabled = true;

            }
            //if the user hit either the Dance icons or the IOS icon on the Dance panel
            //and hits reset
            //the audiosource is audioDance or IOS and should not be stopped 
            else if (!audioMain.isPlaying)
            {
                ObjectManagerMain.gameObject.SetActive(false);
                ObjectManagerDance.gameObject.SetActive(true);
                Debug.Log("Back Dance - audioMain was not playing, audio from Dance - NEVER seems to get called?" + currentAudioSource + currentAudioType + currentAnimationType);


                //Debug.Log("audioMain was not playing, audioDance was playing and user hit the BACK from Dance" + currentAudioSource + currentAudioType + currentAnimationType);

                sourceMain = false;
                sourceDance = true;
                sourcePray = false;
                sourceIOS = false;
                //update is called
                enabled = true;

            }

            //Debug.Log("Dance Start - AudioSource from Main is playing and it is....." + currentAudioSource + currentAudioType + currentAnimationType);
            //enabled = true;
        }*/


        
        
        /*
        //Animation
        AnimatorDance.GetComponent<Animator>();
        if (AnimatorDance != null)
        {
            AnimatorDance.GetComponent<Animator>().enabled = false;
            Debug.Log("Animation on Dance Panel was active and was stopped");
        }
        */
        
    }

    public void SetJesus(GameObject obj)
    {
        //debugImage.SetActive(true);
        jesusGameObject = obj;
    }

    #region Unity callbacks


    //This would be when audio and animation is none
    void CurrentAudioAndAnimationNone()
    {

        currentAudioType = AudioType.Silent;
        currentAnimationType = AnimationType.None;
        currentAudioSource = SourceType.FromMain;
        currentObjectManager = ObjectManager.DanceObjectManager;
        //update prohibited
        enabled = false;

    }

    //Immediately after start audio is none and animation is idle
    void CurrentAudioNoneAndAnimationIdle()

    {
        currentAudioType = AudioType.Silent;
        currentAnimationType = AnimationType.Idle;
        currentAudioSource = SourceType.FromMain;
        currentObjectManager = ObjectManager.DanceObjectManager;
        //Audio
        //None
        //Animation
        AnimatorDance.Play("IdleCentered", 0, 0f);
        Debug.Log("Audio is silent and animation is idle..." +currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //update prohibited
        enabled = false;
    }
    
    //Dance
    void CurrentAudioMaroonAndAnimSlow()
    {
        danceOne.GetComponent<Image>().sprite = audioOffSprite;
        currentAudioType = AudioType.Maroon;
        currentAnimationType = AnimationType.Slow;
        currentAudioSource = SourceType.FromDance;
        currentObjectManager = ObjectManager.DanceObjectManager;

        //Audio
        audioDance.clip = maroonPanel;
        audioDance.Play();
        //Animation
        AnimatorDance.Play("danceOne", 0, 0f);
        Debug.Log("Dance Panel - Audio is Maroon and animation is slow..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //call to update prohibited
        enabled = false;
    }

    void CurrentAudioTealAndAnimFast()
    {
        currentAudioType = AudioType.Teal;
        currentAnimationType = AnimationType.Fast;
        currentAudioSource = SourceType.FromDance;
        currentObjectManager = ObjectManager.DanceObjectManager;

        //Audio
        audioDance.clip = tealPanel;
        audioDance.Play();
        //Animation
        AnimatorDance.Play("danceTwo", 0, 0f);

        Debug.Log("#2 _ Dance Panel - Audio is Teal and animation is fast..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //call to update prohibited
        enabled = false;
    }
    
    
    //Music
    void CurrentAudioIOSAndAnimationNone()
    {

        currentAudioType = AudioType.IOS;
        currentAnimationType = AnimationType.None;
        currentAudioSource = SourceType.FromIOS;
        currentObjectManager = ObjectManager.DanceObjectManager;
        Debug.Log("Dance Panel - Audio is IOS and animation is none..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //Update Prohibited
        enabled = false;
    }

    void CurrentAudioIOSandAnimSlow()
    {
        //danceOne.GetComponent<Image>().sprite = audioOffSprite;
        currentAudioType = AudioType.IOS;
        currentAnimationType = AnimationType.Slow;
        currentAudioSource = SourceType.FromIOS;
        currentObjectManager = ObjectManager.DanceObjectManager;
        //Animation
        AnimatorDance.Play("danceOne", 0, 0f);
        Debug.Log("1A. Dance Panel - Audio is IOS and animation is slow..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //Update Prohibited
        enabled = false;
    }

    void CurrentAudioIOSandAnimFast()
    {
        //danceOne.GetComponent<Image>().sprite = audioOffSprite;
        currentAudioType = AudioType.IOS;
        currentAnimationType = AnimationType.Fast;
        currentAudioSource = SourceType.FromIOS;
        currentObjectManager = ObjectManager.DanceObjectManager;
        //Audio
        //None
        //Animation
        AnimatorDance.Play("danceTwo", 0, 0f);
        Debug.Log("1B. Dance Panel - Audio is IOS and animation is fast..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //Update Prohibited
        enabled = false;
    }
    

    //Reset
    void ResetAudioandAnimation()

    {
        currentAudioType = AudioType.Silent;
        currentAnimationType = AnimationType.Idle;
        currentObjectManager = ObjectManager.DanceObjectManager;
        //Audio
        //None
        //Animation
        AnimatorDance.Play("IdleCentered", 0, 0f);

        Debug.Log("Dance Reset No Audio and animation swtches to Idle" + currentAudioSource + currentAudioType + currentAnimationType);
        //Update Prohibited
        enabled = false;

    }
    #endregion


}
