using UnityEngine;
using System.Collections;

public class NavMeshAgentSight : MonoBehaviour
{
    NavMeshPathingAgent parentAgent;

	// Use this for initialization
	void Start ()
    {
        parentAgent = transform.parent.GetComponent<NavMeshPathingAgent>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            parentAgent.StartSightReaction(other.transform);
        }
    }
}
