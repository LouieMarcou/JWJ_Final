using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool reloading = false;

    private void OnEnable()
    {
        Camera.onPostRender += OnPostRenderCallback;
    }

    private void OnDisable()
    {
        Camera.onPostRender -= OnPostRenderCallback;
    }

    private void Start()
    {
        //if (animator != null)
        //{
        //    animator.Play("Base Layer.JesusBackgroundFadeAway");
        //}
    }

    public void ReloadScene()
    {
        if (reloading)
            return;
        //have an animation event trigger so that background fades back in then after the animation is complete load the scene
        //make everything stop

        StartCoroutine(WaitUntilReload());
        
    }

    private IEnumerator WaitUntilReload()
    {
        reloading = true;
        animator.Play("Base Layer.JesusBackgroundFadeIn");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("JWJ");
    }

    private void OnPostRenderCallback(Camera cam)
    {
        if(cam == Camera.main)
        {
            if (animator != null)
            {
                animator.Play("Base Layer.JesusBackgroundFadeAway");
            }
        }
    }
}
