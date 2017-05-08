using UnityEngine;
using System.Collections;
using UnityEngine.UI;


//TLM
public class AnswerPanel : MonoBehaviour {

    [SerializeField]
    GameObject[] answerObjs;

    DialogueManager dialogueManager;


    public void AddAnswers(string[] answers) {
        
        for (int i = 0; i < answers.Length; i++)
        {
            answerObjs[i].GetComponentInChildren<Text>().text = answers[i];

        }
    }

    public void RemoveAnswers() {
        for (int i = 0; i < answerObjs.Length; i++)
        {
            answerObjs[i].GetComponentInChildren<Text>().text = "";
        }
    }

    public void CatchAnswer(int id) {
        Debug.Log("ID: " + id);
        string inputName = "Answer" + id;
        dialogueManager.CurrentConversationController.GetComponent<Animator>().SetBool(inputName, true);
    }

	// Use this for initialization
	void Start () {
        dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
