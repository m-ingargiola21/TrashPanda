using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RelationshipStatus {
Good, Bad, Neutrual, None};

public class RelationshipManager : MonoBehaviour {

    List<GameObject> subjects = new List<GameObject>();
    [SerializeField]
    string[] npcIDs;
    RelationshipStatus[] relationshipStatus;

    private void Start() {
        for (int i = 0; i < npcIDs.Length; i++)
        {
            relationshipStatus[i] = RelationshipStatus.None;
        }
    }

    public void AddDialogueAnswerListener(GameObject obj) {
        subjects.Add(obj);
    }

    public void UpdateRelationshipStatus(string npcID, RelationshipStatus newStatus) {

    }
}
