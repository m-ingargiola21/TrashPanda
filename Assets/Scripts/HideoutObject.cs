using UnityEngine;
using System.Collections;
using System;

public class HideoutObject : MonoBehaviour
//M.I.
{
    [SerializeField] string movementVerticalAxisName;
    [SerializeField] string movementHorizontalAxisName;
    [SerializeField] float hideoutObjectMovementX;
    [SerializeField] float hideoutObjectMovementZ;

    [SerializeField] Rigidbody hideoutObjectRigidbody;

    // Use this for initialization
    void Start ()
    {
        hideoutObjectRigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HideoutObjectMovement();
	}

    void FixedUpdate()
    {
        //HideoutObjectMovementRigidbody();
    }

    private void HideoutObjectMovementRigidbody()
    {
        if (Input.GetButtonDown(movementVerticalAxisName))
            if (Input.GetAxisRaw(movementVerticalAxisName) > 0) //moves hideout object forward
                hideoutObjectRigidbody.MovePosition(transform.position - transform.forward);
            else if (Input.GetAxisRaw(movementVerticalAxisName) < 0) //moves hideout object backward
                hideoutObjectRigidbody.MovePosition(transform.position + transform.forward);

        if (Input.GetButtonDown(movementHorizontalAxisName))
            if (Input.GetAxisRaw(movementHorizontalAxisName) > 0) //moves hideout object right
                hideoutObjectRigidbody.MovePosition(transform.position - transform.right);
            else if (Input.GetAxisRaw(movementHorizontalAxisName) < 0) //moves hideout object left
                hideoutObjectRigidbody.MovePosition(transform.position + transform.right);
    }

    private void HideoutObjectMovement()
    {
        if (Input.GetButtonDown(movementVerticalAxisName))
            if (Input.GetAxisRaw(movementVerticalAxisName) > 0) //moves hideout object forward
                transform.Translate(0, 0, -hideoutObjectMovementZ);
            else if(Input.GetAxisRaw(movementVerticalAxisName) < 0) //moves hideout object backward
                transform.Translate(0, 0, hideoutObjectMovementZ);

        if (Input.GetButtonDown(movementHorizontalAxisName))
            if (Input.GetAxisRaw(movementHorizontalAxisName) > 0) //moves hideout object left
                transform.Translate(-hideoutObjectMovementX, 0, 0);
            else if (Input.GetAxisRaw(movementHorizontalAxisName) < 0) //moves hideout object right
                transform.Translate(hideoutObjectMovementX, 0, 0);


        //Current position of hideout object and prints postition to console
        Vector3 hideoutObjectCurrentPosition = transform.position;
        //print(hideoutObjectCurrentPosition);

    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Wall")
        {
            print("collision with wall detected");
        }

        if (col.gameObject.tag == "HideoutObject")//changes box to red if it collides with other object
        {
            print("collision with other hideout object detected");
            col.gameObject.GetComponent<Renderer>().material.color = Color.red;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }

        //TODO: changes box back to original if it collides with other object
    }
}
