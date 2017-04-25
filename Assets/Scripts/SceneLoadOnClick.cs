using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadOnClick : MonoBehaviour {

    [SerializeField]
    string sceneToLoad;

    public void LoadScene() {
        SceneManager.LoadScene(sceneToLoad);
    }
}
