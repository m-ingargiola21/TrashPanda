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

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        HideoutObjectMovementX();
	}

    private void HideoutObjectMovementX()
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
        print(hideoutObjectCurrentPosition);

    }
}
