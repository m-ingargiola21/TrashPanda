using UnityEngine;
using System.Collections;

public class NavMeshPathingAgent : MonoBehaviour
{

    [SerializeField]
    Transform pathPointOne;
    [SerializeField]
    Transform pathPointTwo;
    [SerializeField]
    NavMeshAgent agent;

    //float distance = 0f;
    float time = 0f;
    bool coroutineRunning;

    void Start()
    {
        time = 0;
        agent.destination = pathPointOne.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (!coroutineRunning)
            StartCoroutine(SwitchDirections());
    }
    IEnumerator SwitchDirections()
    {
        coroutineRunning = true;

        if (agent.destination == pathPointOne.position || agent.destination == transform.position)
            agent.destination = pathPointTwo.position;
        else if (agent.destination == pathPointTwo.position || agent.destination == transform.position)
            agent.destination = pathPointOne.position;

        yield return new WaitForSeconds(10);
        
        coroutineRunning = false;
    }
}
