using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBoard : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private bool sceneChangeInput = false;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("Base Layer.JesusBackgroundFadeAway");
        StartCoroutine(LoadYourAsyncScene());
    }

    public void GoToNextScene()
    {
        StartCoroutine(StartFadeAway());
    }

    IEnumerator StartFadeAway()
    {
        animator.Play("Base Layer.JesusBackgroundFadeIn");
        yield return new WaitForSeconds(2f);
        sceneChangeInput = true;
        //SceneManager.LoadScene("JWJ");
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
