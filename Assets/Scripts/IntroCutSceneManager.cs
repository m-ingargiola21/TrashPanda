using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class IntroCutSceneManager : MonoBehaviour {
    [SerializeField]
    string sceneToLoad;
    private void Update() {
        if (Input.GetButtonDown("Fire1"))
        {
            SceneManager.LoadScene(sceneToLoad);
            SceneManager.UnloadScene("IntroCutScene");
        }
    }
}
