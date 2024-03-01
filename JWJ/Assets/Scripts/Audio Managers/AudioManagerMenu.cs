using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;



[RequireComponent(typeof(AudioSource))]
public class AudioManagerMenu : MonoBehaviour
{

    //public static iOSMusic instance = null;
    
    #region Properties
    public Sprite audioOnSprite;
    public Button danceOne;
    
    public GameObject IOSGameObject;
    public GameObject ObjectManagerMain;
    public GameObject ObjectManagerPrayer;
    public GameObject ObjectManagerDance;

    //public AudioSource audioIOS;
    public AudioSource audioMain;
    public AudioSource audioPray;
    public AudioSource audioDance;

    //iOS
    //private AudioSource _audioSource;
    public AudioSource iOSMusicAudioSource;
        /*
    {
        get { return _audioSource; }
        set { _audioSource = value; }
    }
    */

    //Dance
    public AudioClip maroonMain;
    //Prayer
    public AudioClip englishMain;

    [SerializeField]
    private Animator AnimatorMain;

    //Booleans - Audio
    //Dance
    bool slower_maroon;
    //prayer 
    bool english;
    //Booleans - Source
    bool sourceMain;
    bool sourceDance;
    bool sourcePray;
    bool sourceIOS;
    //Booleans - Animations
    bool anim_idle;
    bool anim_slow;
    bool anim_pray;
    bool anim_none;
    //Enumerations
    public enum AudioType
    {
        //Dance
        Maroon,
        //Pray
        English,
        //None
        Silent
    }

    public enum AnimationType
    {

        Idle,
        Pray,
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

   

    #endregion

    #region Unity Methods


    // AWAKE is called before any Start functiosn and also after a prefab is instantiated (made active)
    private void Awake()
    {
        ObjectManagerMain.GetComponent<AudioSource>();
        if (ObjectManagerMain == null)
        {
            //Debug.Log("From Main- Object Manager was Null " + currentAudioSource + currentAudioType + currentAnimationType);
            //CurrentAudioNoneAndAnimationIdle();
            enabled = true;
        }

        else if (ObjectManagerMain != null)
        {
            //Debug.Log("From Main Object Manager was NOT Null " + currentAudioSource + currentAudioType + currentAnimationType);
            CurrentAudioNoneAndAnimationIdle();
            enabled = true;
        }
    }

        

    //Changed it to Reset so that it is only called when the script becomes active
    void Reset()
    //void Start()
    {
        
        sourceMain = true;
        sourceDance = false;
        sourcePray = false;
        sourceIOS = false;
        //update is called
        enabled = true;


        //Main
        var temnp = ObjectManagerMain.GetComponent<AudioSource>();//fix later
        if (ObjectManagerMain == null)
        {
            ObjectManagerMain.gameObject.SetActive(true);
            //Debug.Log("From Reset_ObjectManagerMain was NULL..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
            enabled = false;
        }

        else if (ObjectManagerMain != null)
        {

            ObjectManagerMain.gameObject.SetActive(true);
            //Debug.Log("From Reset_ObjectManagerMain was not NULL....." +currentObjectManager+ currentAudioSource + currentAudioType + currentAnimationType);
            enabled = true;
        }



        //Animation
        AnimatorMain.GetComponent<Animator>();
        if (AnimatorMain == null)
        {
            //Debug.Log("Main - Animator (AnimatorMain) has not been set..." + currentAnimationType);
            //AnimatorMain.GetComponent<Animator>().enabled = true;
            //CurrentAudioNoneAndAnimationIdle();
            enabled = false;
        }

        else if (AnimatorMain != null)
        {
            //Debug.Log("Main - Animator (AnimatorMain) has been successfully set ..." + currentAnimationType);
            enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {

       // Debug.Log(" UPDATE Main - This is the source and type" + currentAudioSource + currentAudioType + currentAnimationType);

        /*if (sourceMain == true && sourceDance == false && sourcePray== false && sourceIOS == false)
        {
            audioMain.GetComponent<AudioSource>().enabled = true;
            audioDance.GetComponent<AudioSource>().enabled = false;
            audioPray.GetComponent<AudioSource>().enabled = false;
            //iOSMusicAudioSource.GetComponent<AudioSource>().enabled = false;
           
            //Debug.Log("Main - sourceMain is true ..." + currentObjectManager + currentAudioType + currentAnimationType + currentAudioSource);
            enabled = true;

        }

        else if (sourceMain == false && sourceDance == true && sourcePray == false && sourceIOS == false)
        {
            audioMain.GetComponent<AudioSource>().enabled = false;
            audioDance.GetComponent<AudioSource>().enabled = true;
            audioPray.GetComponent<AudioSource>().enabled = false;
            iOSMusicAudioSource.GetComponent<AudioSource>().enabled = false;
        
            //Debug.Log("Main - sourceDance is true ..." + currentObjectManager + currentAudioType + currentAnimationType + currentAudioSource);
            enabled = true;
        }

        else if (sourceMain == false && sourceDance == false && sourcePray == true && sourceIOS == false)
        {

            audioMain.GetComponent<AudioSource>().enabled = false;
            audioDance.GetComponent<AudioSource>().enabled = false;
            audioPray.GetComponent<AudioSource>().enabled = true;
            iOSMusicAudioSource.GetComponent<AudioSource>().enabled = false;
            //Debug.Log("Main - sourcePray is true ..." + currentObjectManager + currentAudioType + currentAnimationType + currentAudioSource);

            //Debug.Log("Main - sourcePray is true ..." + currentAudioType + currentAnimationType + currentAudioSource);
            enabled = true;
        }

        else if (sourceMain == false && sourceDance == false && sourcePray == false && sourceIOS == true)
        {
           
            audioMain.GetComponent<AudioSource>().enabled = false;
            audioDance.GetComponent<AudioSource>().enabled = false;
            audioPray.GetComponent<AudioSource>().enabled = false;
            iOSMusicAudioSource.GetComponent<AudioSource>().enabled = true;
            //Debug.Log("Main - sourceIOS is true ..." + currentObjectManager + currentAudioType + currentAnimationType + currentAudioSource);
            enabled = true;
        }
        //Main Panel
        //Audio and animation is none 
        if (english == false  && slower_maroon == false && anim_idle == false && anim_slow == false && anim_pray == false && anim_none == true)

        {
            ResetAudioandAnimation(); // Line - 281 
        }

        //Audio is silent and animation is idle 
        else if (english == false && slower_maroon == false && anim_idle == true && anim_slow == false && anim_pray == false && anim_none == false)

        {
            CurrentAudioNoneAndAnimationIdle();// Line- 252
        }

        //Dance
        //Audio is maroon and animation is slow 
        else if (english == false && slower_maroon == true && anim_idle == false && anim_slow == true && anim_pray == false && anim_none == false)

        {
            CurrentAudioMaroonAndAnimSlow(); // Line  - 266
        }

        //Prayer
        //User hits the Prayer button on the main panel and audio is silent and animation is Idle
        //Audio switches to english and animation switches to Pray 
        else if (english == true && slower_maroon == false && anim_idle == false && anim_slow == false && anim_pray == true && anim_none == false)

        {
            CurrentAudioEnglishAndAnimPray();//Line - 277
        }*/

    }
    #endregion

    //User has tapped in to the main canvas
    public void DanceMain()
    {
        /*
        //fasterTeal.interactable = true;
        
        //Reset();

        ObjectManagerPrayer.gameObject.SetActive(false);
        ObjectManagerDance.gameObject.SetActive(true);
        danceOne.GetComponent<Image>().sprite = audioOnSprite;
        // If the IOS native player is currently playing, make it stop.
        //musicManager.stopMusic();
       //Debug.Log("ManagerMenu DANCE - calls to music manager to stop music");
        //IOSGameObject.gameObject.SetActive(false);
        
        audioMain = GetComponent<AudioSource>();
        if (audioMain.isPlaying)
        {
            sourceMain = true;
            sourceDance = false;
            sourcePray = false;
            sourceIOS = false;
            //update is called
            enabled = true;
            //Debug.Log("From Main Panel audio is playing..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        }
        else if (!audioMain.isPlaying)
        {
            sourceMain = true;
            sourceDance = false;
            sourcePray = false;
            sourceIOS = false;
            //update is called
            enabled = true;
            //Debug.Log("From Main Panel audio  is not playing..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        }

        else if (audioPray.isPlaying)
        {
            sourceMain = false;
            sourceDance = false;
            sourcePray = true;
            sourceIOS = false;
            //update is called
            enabled = true;
            //Debug.Log("From Main Pray audio  is playing..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        }

        else if (iOSMusicAudioSource.isPlaying)
        {
            sourceMain = false;
            sourceDance = false;
            sourcePray = false;
            sourceIOS = true;
            //update is called
            enabled = true;
            //Debug.Log("From IOS audio audio is playing..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        }


        if (currentAudioType == AudioType.English || currentAudioType == AudioType.Silent || currentAudioType == AudioType.Maroon && currentAnimationType == AnimationType.Pray || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.None)

        {
            english = false;
            slower_maroon = true;
            //animation switches to slow
            anim_slow = true;
            anim_pray = false;
            anim_none = false;
            //This should call to CurrentAudioMaroonAndAnimSlow 
            //call to update
            enabled = true;
        }*/
    }
   
    public void PrayMain()

    {
        //Reset();

        ObjectManagerPrayer.gameObject.SetActive(true);
        ObjectManagerDance.gameObject.SetActive(false);
        // If the IOS native player is currently playing, make it stop.
        //musicManager.stopMusic();
        //Debug.Log("ManagerMenu PRAY - calls to music manager to stop music");
        //IOSGameObject.gameObject.SetActive(false);

        
        sourceMain = true;
        sourceDance = false;
        sourcePray = false;
        sourceIOS = false;
        //update is called
        enabled = true;
        

        if (currentAudioType == AudioType.English || currentAudioType == AudioType.Silent || currentAudioType == AudioType.Maroon && currentAnimationType == AnimationType.Slow || currentAnimationType == AnimationType.Idle || currentAnimationType == AnimationType.Pray ||currentAnimationType == AnimationType.None)


        {
            //from the main panel
            //audio type switchs from silent to english 
            english = true;
            slower_maroon = false;
            //animation swtiches to pray
            anim_idle = false;
            anim_slow = false;
            anim_pray = true;
            anim_none = false;
            //This should call to CurrentAudioEnglishAndAnimPray - 
            //call to update
            enabled = true;
        }

    }

    public void ResetScene()
    {

        ResetAudioandAnimation();
       

    }


    #region Unity callbacks

    //Immediately after start audio is none and animation is idle - Used
    void CurrentAudioNoneAndAnimationIdle()

    {
        currentAudioType = AudioType.Silent;
        currentAnimationType = AnimationType.Idle;
        currentAudioSource = SourceType.FromMain;
        currentObjectManager = ObjectManager.MainObjectManager;
        //Audio
        //None
        //Animation
        AnimatorMain.Play("IdleCentered", 0,0f);
        //Debug.Log("Main - Audio is None and animation is Idle..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        //update prohibited
        enabled = false;
    }

    //Dance 
    void CurrentAudioMaroonAndAnimSlow()
    {
        currentAudioType = AudioType.Maroon;
        currentAnimationType = AnimationType.Slow;
        currentAudioSource = SourceType.FromMain;
        currentObjectManager = ObjectManager.MainObjectManager;

        ///Audio
        audioMain.clip = maroonMain;
        audioMain.Play();
        //Animation
        AnimatorMain.Play("danceOne", 0, 0f);
        Debug.Log("Main - Current audio is Maroon and animation is slow..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //call to update prohibited
        enabled = false;
    }

    
    //Pray
    void CurrentAudioEnglishAndAnimPray()

    {
        currentAudioType = AudioType.English;
        currentAnimationType = AnimationType.Pray;
        currentAudioSource = SourceType.FromMain;
        currentObjectManager = ObjectManager.MainObjectManager;

        ///Audio
        //prayPanel.PlayOneShot(hebrewPanel, 1.0f);
        audioMain.clip = englishMain;
        audioMain.Play();
        //Animation
        AnimatorMain.Play("Pray", 0, 0f);

        Debug.Log("Main - Current Audio is English and animation is Pray ..." + currentObjectManager + currentAudioSource+ currentAudioType + currentAnimationType);
        enabled = false;

    }
   
    //Reset
    //Check with Matt to find out it this was called ResetCompleteAudioandAnimation before
    void ResetAudioandAnimation()

    {
        currentAudioType = AudioType.Silent;
        currentAnimationType = AnimationType.None;
        //Debug.Log("Main - Reset No Audio and should be no animation" + currentAudioType + currentAnimationType);
        //Update Prohibited
        enabled = false;

    }
}
#endregion



