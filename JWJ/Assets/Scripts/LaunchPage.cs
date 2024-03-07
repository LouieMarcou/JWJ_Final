using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LaunchPage : MonoBehaviour
{
    private const string IsSceneOpened = "IsSceneOpened";

    // Start is called before the first frame update
    void Start()
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
}
