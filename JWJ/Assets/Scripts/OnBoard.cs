using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBoard : MonoBehaviour
{

    private bool sceneChangeInput = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("JWJ");

        asyncLoad.allowSceneActivation = false;

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            if(asyncLoad.progress >= 0.9f && sceneChangeInput)
            {
                asyncLoad.allowSceneActivation=true;
            }
            yield return null;
        }
    }


    public void SetSceneChangeInput()
    {
        sceneChangeInput = true;
    }
}
