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
            StartCoroutine(DelayABit());
        }
        else if(newState == JESUS_PRAY)
        {
            animator.Play(JESUS_PRAY);
            currentState = newState;
        }
        else
        {
            animator.Play(JESUS_IDLE);
            animator.Play(newState);
            currentState = newState;

        }
        

        
    }

    IEnumerator DelayABit()
    {
        yield return new WaitForSeconds(3.0f);
        animator.Play(currentState);
    }
}
