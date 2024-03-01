using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class AudioManPrayPanel : MonoBehaviour
{

    #region Properties
    public GameObject IOSGameObject;
    public GameObject ObjectManagerMain;
    public GameObject ObjectManagerDance;
    public GameObject ObjectManagerPrayer;

    //Prayer
    public AudioSource audioMain;
    public AudioSource audioPray;
    public AudioSource audioDance;
    //iOS
    public AudioSource iOSMusicAudioSource;

    public AudioClip englishPanel;
    public AudioClip hebrewPanel;
    public AudioClip italianPanel;
    public AudioClip spanishPanel;

    [SerializeField]
    private Animator AnimatorPray;


    bool english;
    bool hebrew;
    bool italian;
    bool spanish;
    //Booleans - Source
    bool sourceMain;
    bool sourceDance;
    bool sourcePray;
    bool sourceIOS;
    //Reset from Prayer ordance Panel back to Main Panel
    bool reset_back_main;
    bool anim_idle;
    bool anim_pray;
    bool anim_none;

    public enum AudioType
    {
        
        //Pray
        English,
        Hebrew,
        Italian,
        Spanish,
        Silent
    }

    public enum AnimationType
    {

        Idle,
        Pray,
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

    void Start()
    {

        //Pray
        ObjectManagerMain.GetComponent<AudioSource>();
        if (ObjectManagerMain == null)
        {
            //Debug.Log(" Pray Start - AudioSource from Main is NOT playing" + currentAudioSource + currentAudioType + currentAnimationType);
            enabled = false;
        }

        else if (ObjectManagerMain != null)
        {
            //Debug.Log("Pray Start - AudioSource from Main is playing and it is....." + currentAudioSource + currentAudioType + currentAnimationType);
            enabled = true;
        }
    }
    //Changed so that it is only called when the script becomes active
    void Reset()
    {
        

        ObjectManagerPrayer.GetComponent<AudioSource>();
        //This calls to the Audio Pray 
        if (ObjectManagerPrayer == null)
        {
            Debug.Log(" AudioSource (ObjectManagerPrayer) has not been set..." + currentAudioType + currentAnimationType);
            enabled = false;
        }

        else if (ObjectManagerPrayer != null)
        {
            Debug.Log(" AudioSource (ObjectManagerPrayer) has been successfully set ..." + currentAudioType + currentAnimationType);
            enabled = true;
        }

        //Animation
        AnimatorPray.GetComponent<Animator>();
        if (AnimatorPray == null)
        {
            Debug.Log("Prayer Panel - Animator (AnimatorPrayer) has not been set..." + currentAnimationType);
            enabled = false;
        }

        else if (AnimatorPray != null)
        {
            Debug.Log("Prayer Panel - Animator (AnimatorPrayer) has been successfully set ..." + currentAnimationType);
            enabled = false;
        }

    }
       
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(" UPDATE pray - This is the source and type" + currentAudioSource + currentAudioType + currentAnimationType);




        /*if (sourceMain == true && sourceDance == false && sourcePray == false && sourceIOS == false)
        {
            audioMain.GetComponent<AudioSource>().enabled = true;
            //Debug.Log("Pray - sourceMain is true ..." + currentObjectManager + currentAudioSource +currentAudioType + currentAnimationType);

        }

        else if (sourceMain == false && sourceDance == true && sourcePray == false && sourceIOS == false)
        {
            
            audioDance.GetComponent<AudioSource>().enabled = true;
            //Debug.Log("Pray - sourceDance is true ..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        }

        else if (sourceMain == false && sourceDance == false && sourcePray == true && sourceIOS == false)
        {
            
            audioPray.GetComponent<AudioSource>().enabled = true;
            //Debug.Log("Pray - sourcePray is true ..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        }

        else if (sourceMain == false && sourceDance == false && sourcePray == false && sourceIOS == true)
        {
            
            iOSMusicAudioSource.GetComponent<AudioSource>().enabled = true;
            //Debug.Log("Pray - sourceIOS is true ..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        }

        //Audio is silent and animation is idle Line 
        if (english == false && hebrew == false && italian == false && spanish == false && reset_back_main == true && anim_idle == false && anim_pray == true && anim_none == false)

        {
            CurrentAudioNoneAndAnimationIdle();
        }
         
        //Prayer
        //User hits the Prayer button on the prayer panel and audio is audio is either Hebrew, Italian or Spanish
        //Audio switches to english and animation stays Pray 
        else if (english == true && hebrew == false && italian == false && spanish == false && reset_back_main == false && anim_idle == false && anim_pray == true && anim_none == false)

        {
            CurrentAudioEnglishAndAnimPray();//Line - 
        }
      
        //User hits the Hebrew button on the prayer panel and audio is either English, Italian or Spanish
        //Audio switches to hebrew and animation stays Pray 
        else if (english == false && hebrew == true && italian == false && spanish == false && reset_back_main == false && anim_idle == false && anim_pray == true && anim_none == false)

        {
        CurrentAudioHebrewAndAnimPray(); // Line - 
        }

        //User hits the Italian button on the prayer panel and audio is either English, Hebrew or Spanish
        //Audio switches to Italian and animation stays Pray 
        else if (english == false && hebrew == false && italian == true && spanish == false && reset_back_main == false && anim_idle == false && anim_pray == true && anim_none == false)

        {
            CurrentAudioItalianAndAnimPray(); // Line - 
        }

        //User hits the Spanish button on the prayer panel and audio is either English, Hebrew or Italian
        //Audio switches to Spanish and animation stays Pray 
        else if (english == false && hebrew == false && italian == false && spanish == true && reset_back_main == false && anim_idle == false && anim_pray == true && anim_none == false)

        {
            CurrentAudioSpanishAndAnimPray(); // Line - 
        }


        //Reset 
        else if (english == false && hebrew == false && italian == false && spanish == false && reset_back_main == true && anim_idle == false && anim_pray == false && anim_none == false)
        {
            ResetAudioandAnimation(); // Line - 
        }*/
       
    }
    #endregion

    public void EnglishPanel()
    {
        //Reset();

        ObjectManagerPrayer.gameObject.SetActive(true);
        ObjectManagerMain.gameObject.SetActive(false);

        //Debug.Log("English Button is Hit..." + currentAudioType + currentAnimationType);

        CheckClip(englishPanel);

       /* sourceMain = false;
        sourceDance = false;
        sourcePray = true;
        sourceIOS = false;
        //update is called
        enabled = true;

        if (currentAudioType == AudioType.Silent || currentAudioType == AudioType.English || currentAudioType == AudioType.Hebrew || currentAudioType == AudioType.Italian || currentAudioType == AudioType.Spanish && currentAnimationType == AnimationType.None || currentAnimationType == AnimationType.Pray || currentAnimationType == AnimationType.Idle)
        {
            //Audio switches from English, Hebrew, Italian or Spanish to English
            //Audio
            english = true;
            hebrew = false;
            italian = false;
            spanish = false;
            //animation pray
            reset_back_main = false;
            anim_idle = false;
            anim_pray = true;
            anim_none = false;
            //This should call to CurrentAudioEnglishAndAnimPray // Line - 
            //call to update
            enabled = true;
        }*/
    }

    public void HebrewPanel()
    {
        //Reset();

        ObjectManagerPrayer.gameObject.SetActive(true);
        ObjectManagerMain.gameObject.SetActive(false);
        //Debug.Log("Hebrew Button is Hit..." + currentAudioType + currentAnimationType);

        CheckClip(hebrewPanel);

        /*sourceMain = false;
        sourceDance = false;
        sourcePray = true;
        sourceIOS = false;
        //update is called
        enabled = true;

        if (currentAudioType == AudioType.Silent || currentAudioType == AudioType.English || currentAudioType == AudioType.Hebrew || currentAudioType == AudioType.Italian || currentAudioType == AudioType.Spanish && currentAnimationType == AnimationType.None|| currentAnimationType == AnimationType.Pray || currentAnimationType == AnimationType.Idle)
        {

        //Audio switches from English, Hebrew, Italian or Spanish to Hebrew
        //Audio
        english = false;
        hebrew = true;
        italian = false;
        spanish = false;
        //animation Pray
        reset_back_main = false;
        anim_idle = false;
        anim_pray = true;
        anim_none = false;
       //This should call to CurrentAudioHebrewAndAnimPray // Line - 
       //call to update
        enabled = true;
        } */
    }

    public void ItalianPanel()
    {

        //Reset();

        ObjectManagerPrayer.gameObject.SetActive(true);
        ObjectManagerMain.gameObject.SetActive(false);
        //Debug.Log("Italian Button is Hit..." + currentAudioType + currentAnimationType);

        CheckClip(italianPanel);
        /*sourceMain = false;
        sourceDance = false;
        sourcePray = true;
        sourceIOS = false;
        //update is called
        enabled = true;
      
        if (currentAudioType == AudioType.Silent || currentAudioType == AudioType.English || currentAudioType == AudioType.Hebrew || currentAudioType == AudioType.Italian || currentAudioType == AudioType.Spanish && currentAnimationType == AnimationType.None || currentAnimationType == AnimationType.Pray || currentAnimationType == AnimationType.Idle)
        {

            //Audio switches from English, Hebrew, Italian or Spanish to Italian
            //Audio
            english = false;
            hebrew = false;
            italian = true;
            spanish = false;
            reset_back_main = false;
            //animation Pray
            anim_idle = false;
            anim_pray = true;
            anim_none = false;
            //This should call to CurrentAudioItalianAndAnimPray // Line - 
            //call to update
            enabled = true;

        }*/
    }
 
  
    public void SpanishPanel()
    {
        //Reset();

        ObjectManagerPrayer.gameObject.SetActive(true);
        ObjectManagerMain.gameObject.SetActive(false);
        //Source
        CheckClip(spanishPanel);

        /*sourceMain = false;
        sourceDance = false;
        sourcePray = true;
        sourceIOS = false;
        //update is called
        enabled = true;

        if (currentAudioType == AudioType.Silent || currentAudioType == AudioType.English || currentAudioType == AudioType.Hebrew || currentAudioType == AudioType.Italian || currentAudioType == AudioType.Spanish && currentAnimationType == AnimationType.None || currentAnimationType == AnimationType.Pray || currentAnimationType == AnimationType.Idle)
        {

            //Audio switches from English, Hebrew, Italian or Spanish to Hebrew
            //Audio
            english = false;
            hebrew = false;
            italian = false;
            spanish = true;
            reset_back_main = false;
            //animation Pray
            anim_idle = false;
            anim_pray = true;
            anim_none = false;
            //This should call to CurrentAudioHebrewAndAnimPray // Line - 
            //call to update
            enabled = true;

        }*/
    }



    public void BackPray()
    {
        audioPray.Stop();

        //Reset();
        //check audioSource
        //if the user never hit any prayer icons on the prayer panel
        //and hits reset
        //the audiosource is audioMain and should stop
        audioMain.GetComponent<AudioSource>();
        //if the audioMain is not active (not null)
        //turn off the audioMain on Back and let the user start all over
        /*if (audioMain.isPlaying)
        {

            ObjectManagerMain.gameObject.SetActive(true);
            ObjectManagerPrayer.gameObject.SetActive(false);
            Debug.Log("audioMain was still playing and user hit the BACK from Prayer - audio continues from Source Main" + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

            sourceMain = true;
            sourceDance = false;
            sourcePray = false;
            sourceIOS = false;
            //update is called
            enabled = true;
         
        }
        //if the user hit ANY prayer icons on the prayer panel
        //audioMain would be null
        //and hits reset
        //the audiosource is audioPray and should not be stopped 
        else if (!audioMain.isPlaying)
        {
            ObjectManagerMain.gameObject.SetActive(false);
            ObjectManagerPrayer.gameObject.SetActive(true);
            Debug.Log("audioMain was not playing, audioPrayer was playing and user hit the BACK from Prayer" + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

            sourceMain = false;
            sourceDance = false;
            sourcePray = true;
            sourceIOS = false;
            //update is called
            enabled = true;
           
        }*/
        /*
        //check if object is animated
        AnimatorPray.GetComponent<Animator>();
        if (AnimatorPray != null)
        {

            AnimatorPray.GetComponent<Animator>().enabled = false;
            Debug.Log("Animation on Prayer Panel was active and was stopped");
        }
        */
      
    }

    /// <summary>
    /// Checks if the inputted audio clip is playing and pauses it, plays it, or resumes it
    /// </summary>
    /// <param name="clip"></param>
    private void CheckClip(AudioClip clip)
    {
        if(audioPray.isPlaying && audioPray.clip == clip)
        {
            audioPray.Pause();
        }
        else if(audioPray.isPlaying && audioPray.clip != clip)
        {
            audioPray.Stop();
            audioPray.clip = clip;
            audioPray.Play();
        }
        else if (audioPray.isPlaying == false)
        {
            audioPray.clip = clip;
            audioPray.Play();
        }
    }
    
    #region Unity callbacks 



    void  CurrentAudioNoneAndAnimationIdle()

    {

        currentAudioType = AudioType.Silent;
        currentAnimationType = AnimationType.Idle;
        currentAudioSource = SourceType.FromMain;
        currentObjectManager = ObjectManager.PrayerObjectManager;
        //Audio
        //None
        //Animation
        AnimatorPray.Play("IdleCentered", 0, 0f);
   
        Debug.Log("Pray Panel from Reset - Audio goes back to Silent and Idle ..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);

        ObjectManagerMain.gameObject.SetActive(true);
        ObjectManagerPrayer.gameObject.SetActive(false);


        enabled = false;

    }
    //Pray
    void CurrentAudioEnglishAndAnimPray()

    {
        currentAudioType = AudioType.English;
        currentAnimationType = AnimationType.Pray;
        currentAudioSource = SourceType.FromPray;
        currentObjectManager = ObjectManager.PrayerObjectManager;

        //Audio
        audioPray.clip = englishPanel;
        audioPray.Play();
        //Animation
        AnimatorPray.Play("Pray", 0, 0f); ;

        Debug.Log("Pray Panel - Audio is English and anim Pray ..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        enabled = false;

    }
    void CurrentAudioHebrewAndAnimPray()

    {
        
        currentAudioType = AudioType.Hebrew;
        currentAnimationType = AnimationType.Pray;
        currentAudioSource = SourceType.FromPray;
        currentObjectManager = ObjectManager.PrayerObjectManager;

        //Audio
        //prayPanel.PlayOneShot(hebrewPanel, 1.0f);
        audioPray.clip = hebrewPanel;
        audioPray.Play();
        //Animation
        AnimatorPray.Play("Pray", 0, 0f);

        Debug.Log("Pray Panel - - Audio is Hebrew and anim Pray..." + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        enabled = false;

    }

   
    void CurrentAudioItalianAndAnimPray()

    {
        currentAudioType = AudioType.Italian;
        currentAnimationType = AnimationType.Pray;
        currentAudioSource = SourceType.FromPray;
        currentObjectManager = ObjectManager.PrayerObjectManager;
        //Audio
        audioPray.clip = italianPanel;
        audioPray.Play();
        //Animation
        AnimatorPray.Play("Pray", 0, 0f);

        Debug.Log("Pray Panel - - Audio is Italian and anim Pray" + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        enabled = false;

    }
   
    
    void CurrentAudioSpanishAndAnimPray()

    {
        currentAudioType = AudioType.Spanish;
        currentAnimationType = AnimationType.Pray;
        currentAudioSource = SourceType.FromPray;
        currentObjectManager = ObjectManager.PrayerObjectManager;
        //Audio
        audioPray.clip = spanishPanel;
        audioPray.Play();
        //Animation
        AnimatorPray.Play("Pray", 0, 0f);

        Debug.Log("Pray Panel -  Audio is Spanish and anim Pray" + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        enabled = false;

    }
  
    //Reset
    void ResetAudioandAnimation()

    {
        currentAudioType = AudioType.Silent;
        currentAnimationType = AnimationType.Idle;
        currentAudioSource = SourceType.FromMain;
        currentObjectManager = ObjectManager.PrayerObjectManager;
        Debug.Log("Pray Panel - Reset No Audio and animation back to Idle " + currentObjectManager + currentAudioSource + currentAudioType + currentAnimationType);
        //Update Prohibited
        enabled = false;

    }


    #endregion
}


