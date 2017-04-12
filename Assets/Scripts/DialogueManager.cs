using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour {
    [SerializeField]
    public GameObject dialoguePanel;
    [SerializeField]
    public GameObject answerPanel;

    public GameObject CurrentConversationController;

    public void GenerateAnswers(string [] answers) {
        dialoguePanel.gameObject.SetActive(true);
        answerPanel.GetComponent<AnswerPanel>().AddAnswers(answers);
    }

    public void RemoveAnswers() {
        answerPanel.GetComponent<AnswerPanel>().RemoveAnswers();
    }

}
