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


    /// <summary>
    /// Runs the fade animation and sets sceneChangeInput to true so it can go to the next scene
    /// </summary>
    IEnumerator StartFadeAway()
    {
        animator.Play("Base Layer.JesusBackgroundFadeIn");
        yield return new WaitForSeconds(2f);
        sceneChangeInput = true;
        //SceneManager.LoadScene("JWJ");
    }

    /// <summary>
    /// Loads the next scene in the background so that when the user is ready the next scene will load faster
    /// </summary>
    /// <returns></returns>
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
