using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum gameState { LoadingScene, MainMenuNew, IntroCutScene, Cinematic, Hideout, Mission, Credits };
    public gameState GameState;

    [SerializeField]
    LoadingScene loadingScene;
    [SerializeField]
    EventSystem inventoryEventSys;
    [SerializeField]
    StandaloneInputModule inventorySAIM;

    bool shouldQuit = false;
    bool isInvetoryAccessible;
    bool isInventoryOpen;
    gameState sceneToLoad;
    PlayerMovement playerMovement;


    void Start ()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }

        StartCoroutine(GameStateManager());
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (isInvetoryAccessible)
        {
            if (playerMovement.OpenInventory)
                if (!isInventoryOpen)
                    HandleInventoryInput();

                if (Input.GetButtonDown("xButton"))
                    HandleNoInventoryInput();
        }
	}

    void HandleInventoryInput()
    {
        isInventoryOpen = true;
        inventoryEventSys.enabled = true;
        inventorySAIM.enabled = true;
    }

    void HandleNoInventoryInput()
    {
        isInventoryOpen = false;
        inventoryEventSys.enabled = false;
        inventorySAIM.enabled = false;
    }

    public void ChangeSceneWithLoad(gameState _sceneToLoad)
    {
        sceneToLoad = _sceneToLoad;
        GameState = gameState.LoadingScene;
    }
    
    IEnumerator GameStateManager()
    {
        while (!shouldQuit)
        {
            switch (GameState)
            {
                case gameState.LoadingScene:
                    LoadingScene.LoadNewScene(sceneToLoad.ToString());
                    isInvetoryAccessible = false;
                    HandleNoInventoryInput();
                    GameState = sceneToLoad;
                    break;
                case gameState.MainMenuNew:
                    isInvetoryAccessible = false;
                    HandleNoInventoryInput();
                    break;
                case gameState.IntroCutScene:
                    isInvetoryAccessible = false;
                    HandleNoInventoryInput();
                    break;
                case gameState.Cinematic:
                    isInvetoryAccessible = false;
                    HandleNoInventoryInput();
                    break;
                case gameState.Hideout:
                    if (SceneManager.GetActiveScene().name == "Hideout")
                    {
                        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
                        isInvetoryAccessible = true;
                    } 

                    break;
                case gameState.Mission:;
                    if (SceneManager.GetActiveScene().name == "Hideout")
                    {
                        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
                        isInvetoryAccessible = true;
                    }

                    break;
                case gameState.Credits:
                    isInvetoryAccessible = false;
                    HandleNoInventoryInput();
                    break;
                default:

                    break;
            }
            yield return null;
        }
    }
}
