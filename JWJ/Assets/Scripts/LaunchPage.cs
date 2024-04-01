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

    /// <summary>
    /// Gets the camera permissions from the user and goes the the next scene
    /// </summary>
    public void GetPermissions()
    {
        Permission.RequestUserPermission(Permission.Camera);
        GoToNextScene();
    }

    /// <summary>
    /// Decides which scene to go to based on player prefs
    /// </summary>
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
}
