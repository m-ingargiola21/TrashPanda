using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField]
    string initialDialogueToDisplay;
    [SerializeField]
    string[][] questionDialogueAndAnswerOptions;
    [SerializeField]
    GameObject dialoguePanel;
    [SerializeField]
    bool[] changesRelationshipStat;
    [SerializeField]
    RelationshipManager relationshipManager;
    
    [SerializeField]
    string [] answerString;

    private void Awake() {
        if (changesRelationshipStat.Length != answerString.Length)
        {
            throw new Exception("Answers must address all of the components individually matching the elements indexies.");
        }
        relationshipManager.AddDialogueAnswerListener(gameObject);
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            dialoguePanel.SetActive(true);
            dialoguePanel.GetComponentInChildren<Text>().text = initialDialogueToDisplay;
        }
    }

    public void Activate() {

        return;
    }

}
