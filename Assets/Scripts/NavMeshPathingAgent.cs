using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshPathingAgent : MonoBehaviour
{

    [SerializeField]
    List<Transform> pathPoints = new List<Transform>();
    [SerializeField]
    NavMeshAgent agent;
    [SerializeField]
    bool isRotationOnly;

    //float distance = 0f;
    
    float time = 0f;
    bool coroutineRunning;
    bool hasReachedDestination;
    float timeToWaitBeforePathing = 0f;
    int whichPoint = 0;

    void Start()
    {
        time = 0;
        agent.destination = pathPoints[whichPoint].position;
    }

    void Update()
    {
        time += Time.deltaTime;

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    if (!coroutineRunning)
                        StartCoroutine(WaitThenChangeDirections());
                }
            }
        }

        //if (!coroutineRunning)
        //    StartCoroutine(SwitchDirections());
    }

    IEnumerator WaitThenChangeDirections()
    {
        coroutineRunning = true;

        timeToWaitBeforePathing = Random.Range(0f, 5f);

        yield return new WaitForSeconds(timeToWaitBeforePathing);

        PickNextPathpoint();

        coroutineRunning = false;
    }

    void PickNextPathpoint()
    {
        whichPoint++;

        if (whichPoint == pathPoints.Count)
            whichPoint = 0;
        
        if (!isRotationOnly)
        {
            if (whichPoint < pathPoints.Count)
            {
                agent.SetDestination(pathPoints[whichPoint].position);
            }
        }
        else
        {

        }
    }

    IEnumerator RotateGuard()
    {

        yield return null;
    }

    //IEnumerator SwitchDirections()
    //{
    //    coroutineRunning = true;

    //    if (agent.destination == pathPointOne.position || agent.destination == transform.position)
    //        agent.destination = pathPointTwo.position;
    //    else if (agent.destination == pathPointTwo.position || agent.destination == transform.position)
    //        agent.destination = pathPointOne.position;

    //    yield return new WaitForSeconds(10);
        
    //    coroutineRunning = false;
    //}

    public void StopAtDestination()
    {
        agent.autoBraking = true;
    }
}
