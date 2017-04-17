using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum gameState { MainMenu, LoadingScene, Cinematic, Hideout, Mission };
    public gameState GameState;

    bool shouldQuit = false;

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
	
	}

    IEnumerator GameStateManager()
    {
        while (!shouldQuit)
        {
            switch (GameState)
            {
                case gameState.MainMenu:

                    break;
                case gameState.LoadingScene:

                    break;
                case gameState.Cinematic:

                    break;
                case gameState.Hideout:

                    break;
                case gameState.Mission:

                    break;
                default:

                    break;
            }
            yield return null;
        }
    }
}
