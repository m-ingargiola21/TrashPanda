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
    bool hasCaughtPlayer;
    float timeToWaitBeforePathing = 0f;
    int whichPoint = 0;

    Transform detectedPlayer;
    PlayerMovement playerMovement;

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

        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
        
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

    public void StopAtDestination()
    {
        agent.autoBraking = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            
            if (!playerMovement.IsCrouching)
            {
                agent.autoBraking = true;
                //Animation
                detectedPlayer = other.transform;

            }
        }
    }

    IEnumerator ReactToLoudPlayerMovement()
    {
        coroutineRunning = true;

        agent.SetDestination(detectedPlayer.position);

        bool hasReachedDestination = false;

        while (!hasReachedDestination)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        if (!hasCaughtPlayer)
                            StartCoroutine(LookForPlayer());
                        else
                        {
                            //Fail Mission...
                        }
                    }
                }
            }
        }

        yield return null;

        coroutineRunning = false;
    }

    IEnumerator LookForPlayer()
    {
        //Play searching animation
        yield return new WaitForSeconds(3f);

        agent.destination = pathPoints[whichPoint].position;
    }

    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        Debug.Log("Caught The Player");
    //        hasCaughtPlayer = true;
    //    }
    //}

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = other.GetComponent<PlayerMovement>();

            if (!playerMovement.IsCrouching)
            {
                agent.autoBraking = true;

                detectedPlayer = other.transform;

            }
        }
    }
}
