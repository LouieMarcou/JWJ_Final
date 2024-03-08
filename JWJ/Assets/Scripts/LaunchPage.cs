using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

public class LaunchPage : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private const string IsSceneOpened = "IsSceneOpened";

    // Start is called before the first frame update
    void Start()
    {
        if(Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            GoToNextScene();
        }
        else
        {
            FadeBackground();
        }
    }

    public void GetPermissions()
    {
        Permission.RequestUserPermission(Permission.Camera);
        GoToNextScene();
    }

    void GoToNextScene()
    {
        if (PlayerPrefs.GetInt(IsSceneOpened) == 0)
        {
            PlayerPrefs.SetInt(IsSceneOpened, 1);

            SceneManager.LoadScene("Loading");
        }
        else
        {
            SceneManager.LoadScene("JWJ");
        }
    }

    private void FadeBackground()
    {
        if (animator != null)
        {
            animator.Play("Base Layer.JesusBackgroundFadeAway");
        }
    }
}
