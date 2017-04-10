using UnityEngine;
using System.Collections;

public class ShowPanels : MonoBehaviour {

	public GameObject settingsPanel;							
	public GameObject settingsTint;							
	public GameObject menuPanel;							
	public GameObject pausePanel;
    public GameObject fileSelectPanel;



    //Call this function to activate and display the Options panel during the main menu
    public void ShowSettingsPanel()
	{
		settingsPanel.SetActive(true);
		settingsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideSettingsPanel()
	{
		settingsPanel.SetActive(false);
		settingsTint.SetActive(false);
	}
    public void ShowFileSelectPanel()
    {
        fileSelectPanel.SetActive(true);
        settingsTint.SetActive(true);
    }

    //Call this function to deactivate and hide the Options panel during the main menu
    public void HideFileSelectPanel()
    {
        fileSelectPanel.SetActive(false);
        settingsTint.SetActive(false);
    }
    //Call this function to activate and display the main menu panel during the main menu
    public void ShowMenu()
	{
		menuPanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);
	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		settingsTint.SetActive(true);
	}

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		settingsTint.SetActive(false);

	}
}
