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
    bool isReactingSound;
    bool isReactingSight;
    bool isLookingForPlayer;
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
                    if (!coroutineRunning && !isLookingForPlayer && !isReactingSight && !isReactingSound)
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
            
            if (!playerMovement.IsCrouching && playerMovement.isMoving)
            {
                agent.autoBraking = true;
                //Animation
                detectedPlayer = other.transform;

                StopCoroutines();
                StartCoroutine(ReactToLoudPlayerMovement());
            }
        }
    }

    public void StartSightReaction(Transform PlayerTransform)
    {
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, PlayerTransform.position - transform.position);
        
        detectedPlayer = PlayerTransform;

        LayerMask layers = ((1 << LayerMask.NameToLayer("Default")) | ((1 << LayerMask.NameToLayer("TransparentFX")))
            | ((1 << LayerMask.NameToLayer("Ignore Raycast"))) | ((1 << LayerMask.NameToLayer("UI"))) | ((1 << LayerMask.NameToLayer("Floor"))));

        if (Physics.Raycast(ray, out hit, 22f, layers, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.tag == "Player")
            {
                if (!isReactingSight && !isLookingForPlayer)
                    StartCoroutine(ReactToSeeingPlayer());
            }
        }
        
    }

    IEnumerator ReactToSeeingPlayer()
    {
        isReactingSight = true;

        agent.speed = 10f;

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
                        {
                            StartCoroutine(LookForPlayer());
                            hasReachedDestination = true;
                        }
                        else
                        {
                            //Fail Mission...
                        }

                    }
                }
            }
            yield return null;
        }

        agent.speed = 5f;

        isReactingSight = false;
    }

    IEnumerator ReactToLoudPlayerMovement()
    {
        isReactingSound = true;

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
                        {
                            StartCoroutine(LookForPlayer());
                            hasReachedDestination = true;
                        }
                        else
                        {
                            //Fail Mission...
                        }
                        
                    }
                }
            }
            yield return null;
        }
        
        isReactingSound = false;
    }

    IEnumerator LookForPlayer()
    {
        isLookingForPlayer = true;

        //Play searching animation
        yield return new WaitForSeconds(3f);

        agent.destination = pathPoints[whichPoint].position;

        isLookingForPlayer = false;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Caught The Player");
            //hasCaughtPlayer = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && !isLookingForPlayer && !isReactingSight && !isReactingSound)
        {
            playerMovement = other.GetComponent<PlayerMovement>();

            if (!playerMovement.IsCrouching && playerMovement.isMoving)
            {
                agent.autoBraking = true;

                detectedPlayer = other.transform;


                StopCoroutines();
                StartCoroutine(ReactToLoudPlayerMovement());
            }
        }
    }

    void StopCoroutines()
    {
        StopAllCoroutines();
        coroutineRunning = false;
        isLookingForPlayer = false;
        isReactingSound = false;
        isReactingSight = false;

        agent.speed = 5f;
    }
}
