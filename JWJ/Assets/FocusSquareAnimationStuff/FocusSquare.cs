using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusSquare : MonoBehaviour
{
    [SerializeField] private Animator animator;
    public delegate void OnScanForJesus();
    public static OnScanForJesus onScanForJesus;

    private void OnEnable()
    {
        JesusSpawner.onObjectPlaced += StopAnimation;
    }

    private void OnDisable()
    {
        JesusSpawner.onObjectPlaced -= StopAnimation;
    }

    private void Start()
    {
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        animator.Play("FocusSquare_Found_Animation");
    }
    public void StopAnimation()
    {
        animator.StopPlayback();
        gameObject.SetActive(false);
    }
}
