using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BoxMovement : MonoBehaviour
{
    Image buttonToPress;
    public bool hasPlayerAttached;
    PlayerMovement pMovement;
    Transform pTransform;
    Vector3 distanceToPlayerOffet = new Vector3(0f, 0f, 0f);

    void Start ()
    {
        buttonToPress = GetComponentInChildren<Image>();
        buttonToPress.enabled = false;
	}
	
	void Update ()
    {

	}

    void FixedUpdate()
    {
        if (hasPlayerAttached)
        {
            buttonToPress.enabled = false;
            distanceToPlayerOffet.Set(pTransform.position.x - transform.position.x, 
                transform.position.y, pTransform.position.z - transform.position.z);
            transform.Translate(distanceToPlayerOffet);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            pMovement = other.GetComponent<PlayerMovement>();
            pTransform = other.transform;
            pMovement.IsInRangeOfBox = true;
            pMovement.boxMove = this;
            buttonToPress.enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerMovement>().IsInRangeOfBox = false;
            buttonToPress.enabled = false;
        }
    }

    public IEnumerator MatchPlayerMovement()
    {
        while(hasPlayerAttached)
        {
            distanceToPlayerOffet.Set(pTransform.position.x - transform.position.x, transform.position.y, pTransform.position.z - transform.position.z);
            yield return null;
        }
    }

    public void ToggleAttachToBox()
    {
        hasPlayerAttached = !hasPlayerAttached;
    }
}
