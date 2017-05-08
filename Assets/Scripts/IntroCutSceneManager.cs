using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//TLM
public class IntroCutSceneManager : MonoBehaviour {
    [SerializeField]
    string sceneToLoad;

    [SerializeField]
    Text[] textPanels;

    int indexActiveTextPanel;
    bool isDone;

    private void Awake() {
        isDone = false;
        indexActiveTextPanel = 0;
        textPanels[0].gameObject.SetActive(true);
    }

    private void Update() {
        if (Input.GetButtonDown("Fire1") && isDone)
        {
            textPanels[textPanels.Length-1].gameObject.SetActive(false);
            SceneManager.LoadScene(sceneToLoad);
            SceneManager.UnloadScene("IntroCutScene");
            
        }
        else if(Input.GetButtonDown("Fire1") && !isDone)
        {
            textPanels[indexActiveTextPanel].gameObject.SetActive(false);
            indexActiveTextPanel++;
            textPanels[indexActiveTextPanel].gameObject.SetActive(true);

            if (indexActiveTextPanel >= textPanels.Length -1)
            {
                isDone = true;
                indexActiveTextPanel = 0;
            }
        }
    }
}
