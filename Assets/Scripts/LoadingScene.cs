using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//TLM
public class LoadingScene : MonoBehaviour {

    [SerializeField]
    Slider progressSlider;
    [SerializeField]
    public string sceneToOpen;


    private const string loadingSceneName = "LoadingScene";
    private static string sceneToLoad = "MainMenuNew";

	// Use this for initialization
	void Start () {
        progressSlider.value = 0;
        StartCoroutine(BeginLoading());
	}
	
    public static void LoadNewScene(string sceneToLoad)
    {
        LoadingScene.sceneToLoad = sceneToLoad;
        SceneManager.LoadScene(loadingSceneName);
    }
    
    private IEnumerator BeginLoading()
    {

        yield return new WaitForSeconds(0.5f);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while (!async.isDone)
        {
            progressSlider.value = async.progress;
            yield return null;
        }
        
        progressSlider.value = 1;
        SceneManager.UnloadScene(loadingSceneName);
    }
}
