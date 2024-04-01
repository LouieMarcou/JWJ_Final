using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class JesusAnimationController : MonoBehaviour
{
    public Animator animator;

    private string currentState;
    const string JESUS_IDLE = "IdleCentered";
    const string JESUS_DANCE_ONE = "danceOne";
    const string JESUS_DANCE_TWO = "danceTwo";
    const string JESUS_DANCE_THREE = "danceThree";
    const string JESUS_PRAY = "Pray 0";

    //public delegate void PauseJesus();
    //public static event PauseJesus OnPause;


    private void OnEnable()
    {
        MuteAudio.OnClick += ChangeJesusAnimationSpeed;
    }

    private void OnDisable()
    {
        MuteAudio.OnClick -= ChangeJesusAnimationSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if(newState == currentState && newState != JESUS_PRAY)
        {
            return;
        }

        else if(newState == JESUS_DANCE_TWO)
        {
            animator.Play(JESUS_IDLE);
            currentState = newState;
            StartCoroutine(DelayABit(8.6f));
        }
        else if(newState == JESUS_PRAY)
        {
            StopCoroutine(DelayABit(0f));
            animator.Rebind();
            animator.Update(1f);
            currentState = newState;
            animator.Play(newState);

        }
        else
        {
            StopCoroutine(DelayABit(0f));
            animator.Play(JESUS_IDLE);
            animator.Play(newState);
            currentState = newState;
        }
    }

    //Waits until specficed time to start the animation
    IEnumerator DelayABit(float time)
    {
        yield return new WaitForSeconds(time);
        animator.Play(currentState);
    }

    /// <summary>
    /// Changes the animation speed
    /// </summary>
    private void ChangeJesusAnimationSpeed()
    {
        if(animator.speed == 0f) { animator.speed = 1f; }
        else if(animator.speed == 1f) {  animator.speed = 0f; }
    }
}
