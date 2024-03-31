using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField] private AudioClip danceOneClip;
    [SerializeField] private AudioClip danceTwoClip;
    [SerializeField] private AudioClip iOSSongClip;

    [SerializeField] private List<AudioClip> prayerAudioClipList;

    private GameObject jesusGameObject;
    private JesusAnimationController jesusAnim;


    public void DanceOne()
    {
        CheckClip(danceOneClip);
        jesusAnim.ChangeAnimationState("danceOne");
    }

    public void DanceTwo() 
    { 
        CheckClip(danceTwoClip);
        jesusAnim.ChangeAnimationState("danceTwo");
    }
    public void IOSDance()
    {
        //Needs to get song from apple store or wherever it is store.
        //CheckClip(iOSSongClip);
        audioSource.Stop();
        jesusAnim.ChangeAnimationState("danceThree");
    }


    public void Prayer(string language)
    {
        foreach(var clip in prayerAudioClipList)
        {
            if(clip.name.ToLower().Contains(language.ToLower()))
            {
                CheckClip(clip);
                break;
            }
        }
        jesusAnim.ChangeAnimationState("Pray 0");
    }


    public void Back()
    {
        //Reset Animations
        audioSource.Stop();
        jesusAnim.ChangeAnimationState("IdleCentered");
    }



    /// <summary>
    /// Checks if the inputted audio clip is playing and pauses it, plays it, or resumes it
    /// </summary>
    /// <param name="clip"></param>
    private void CheckClip(AudioClip clip)
    {
        if(audioSource.isPlaying && audioSource.clip == clip)
        {
            //audioSource.Pause();
            //jesusAnim.animator.speed = 0;
        }
        else if (audioSource.isPlaying && audioSource.clip != clip)
        {
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();
            jesusAnim.animator.speed = 1;
        }
        else if (audioSource.isPlaying == false)
        {
            audioSource.clip = clip;
            audioSource.Play();
            jesusAnim.animator.speed = 1;
        }
    }

    public void PlayAnimation()
    {
        jesusAnim.animator.speed = 1;
    }

    public void PauseAnimation()
    {
        jesusAnim.animator.speed = 0;
    }

    public void SetJesus(GameObject obj)
    {
        jesusGameObject = obj;
        jesusAnim = jesusGameObject.GetComponent<JesusAnimationController>();
    }
}
