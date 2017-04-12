using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[RequireComponent(typeof(Animator))]
public class DialogueTrigger : MonoBehaviour {
    //[SerializeField]
    //string initialDialogueToDisplay;
    //[SerializeField]
    //string[][] questionDialogueAndAnswerOptions;
    [SerializeField]
    GameObject dialoguePanel;
    //[SerializeField]
    //bool[] changesRelationshipStat;
    //[SerializeField]
    //RelationshipManager relationshipManager;

    //[SerializeField]
    //string [] answerString;

    //private void Awake() {
    //    if (changesRelationshipStat.Length != answerString.Length)
    //    {
    //        throw new Exception("Answers must address all of the components individually matching the elements indexies.");
    //    }
    //    relationshipManager.AddDialogueAnswerListener(gameObject);
    //}

    //void OnCollisionEnter(Collision coll)
    //{
    //    if(coll.gameObject.tag == "Player")
    //    {
    //        dialoguePanel.SetActive(true);


    //    }
    //}

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            if (!GetComponent<Animator>().isActiveAndEnabled)
                GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetBool("IsConversating", true);
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().CurrentConversationController = gameObject;

        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            GetComponent<Animator>().Play("Hello", 0); //Start the state machine from hello, the default state
            GetComponent<Animator>().SetBool("IsConversating", false);
        }
    }
    public void Activate() {

        return;
    }

}
