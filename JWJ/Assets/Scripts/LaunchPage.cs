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

#if UNITY_IPHONE
    void Start()
    {
        if(Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            GoToNextScene();
        }
        else
        {
            GetPermissions();
        }
    }

    void GetPermissions()
    {
        Application.RequestUserAuthorization(UserAuthorization.WebCam);
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

#endif

#if UNITY_ANDROID
    void Start()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            GoToNextScene();
        }
        else
        {
            GetPermissions();
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

#endif
    // Start is called before the first frame update
    //void Start()
    //{
    //    //GetPermissions();
    //    if (Permission.HasUserAuthorizedPermission(Permission.Camera) || Application.HasUserAuthorization(UserAuthorization.WebCam))
    //    {
    //        GoToNextScene();
    //    }
    //    else
    //    {
    //        GetPermissions();
    //    }
    //}

    //public void GetPermissions()
    //{
    //    if(Application.platform == RuntimePlatform.Android)
    //        Permission.RequestUserPermission(Permission.Camera);
    //    else if(Application.platform == RuntimePlatform.IPhonePlayer)
    //        Application.RequestUserAuthorization(UserAuthorization.WebCam);
    //    GoToNextScene();
    //}

    //void GoToNextScene()
    //{
    //    if (PlayerPrefs.GetInt(IsSceneOpened) == 0)
    //    {
    //        PlayerPrefs.SetInt(IsSceneOpened, 1);

    //        SceneManager.LoadScene("Loading");
    //    }
    //    else
    //    {
    //        SceneManager.LoadScene("JWJ");
    //    }
    //}

    //private void FadeBackground()
    //{
    //    if (animator != null)
    //    {
    //        animator.Play("Base Layer.JesusBackgroundFadeAway");
    //    }
    //}
}
