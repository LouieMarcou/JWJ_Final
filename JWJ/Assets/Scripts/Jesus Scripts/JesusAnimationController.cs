using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if(newState == currentState)
        {
            return;
        }

        animator.Play(newState);

        currentState = newState;
    }
}
