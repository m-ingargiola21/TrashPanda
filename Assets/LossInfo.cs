using UnityEngine;
using System.Collections;

public class LossInfo : MonoBehaviour
{
    [SerializeField]
    GameObject lossPanel;
    [SerializeField]
    PlayerMovement playerInfo;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Loss()
    {
        lossPanel.SetActive(true);
        playerInfo.OpenInventory = true;
    }

    public void MainMenu()
    {
        LoadingScene.LoadNewScene("MainmenuNew");
    }

    public void Restart()
    {
        LoadingScene.LoadNewScene("Level");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
