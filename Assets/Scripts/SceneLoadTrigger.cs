using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour {
    [SerializeField]
    string sceneToLoad;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
