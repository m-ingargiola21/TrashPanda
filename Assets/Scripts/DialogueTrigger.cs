using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField]
    string initialDialogueToDisplay;
    [SerializeField]
    string[][] questionDialogueAndAnswerOptions;
    [SerializeField]
    GameObject dialoguePanel;
	
    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            dialoguePanel.SetActive(true);
            dialoguePanel.GetComponentInChildren<Text>().text = initialDialogueToDisplay;
        }
    }

}
