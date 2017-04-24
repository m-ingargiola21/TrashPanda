using UnityEngine;
using System.Collections;
using System;

public class HideoutObject : MonoBehaviour
//M.I. 
{
    #region Axis Name Variables
    [SerializeField] string placeHideoutObjectAxisName;
    [SerializeField] string movementHorizontalAxisName;
    [SerializeField] string movementVerticalAxisName;
    #endregion

    #region Movement Variables
    [SerializeField] float hideoutObjectMovementX;
    [SerializeField] float hideoutObjectMovementZ;
    [SerializeField] float deadZone;

    float movementVerticalInput;
    float movementHorizontalInput;
    #endregion

    [SerializeField] Rigidbody hideoutObjectRigidbody;

    bool hasHitWestWall = false;
    bool hasHitNorthWall = false;
    bool hasHitEastWall = false;
    bool hasHitSouthWall = false;

    Color defaultColor;
    Color colDefaultColor = Color.white;

    // Use this for initialization
    void Start()
    {
        //set material.color for original (new Color)?
        hideoutObjectRigidbody = GetComponent<Rigidbody>();
        defaultColor = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        HideoutObjectMovement();
        PlaceHideoutGameObject();
    }

    void FixedUpdate()
    {
        //movementVerticalInput = Input.GetAxis(movementVerticalAxisName);
        //movementHorizontalInput = Input.GetAxis(movementHorizontalAxisName);
        //HideoutObjectMovementRigidbody();

    }

    private void HideoutObjectMovementRigidbody() //RIGIDBODY MOVEMENT IN FIXEDUPDATE
    {
        if (Input.GetButtonDown(movementVerticalAxisName))
        {
            movementVerticalInput = Input.GetAxis(movementVerticalAxisName);

            if (movementVerticalInput > deadZone && !hasHitNorthWall) //moves hideout object forward
                hideoutObjectRigidbody.MovePosition(transform.position - transform.forward);
            else if (movementVerticalInput < -deadZone && !hasHitSouthWall) //moves hideout object backward
                hideoutObjectRigidbody.MovePosition(transform.position + transform.forward);

        }

        if (Input.GetButtonDown(movementHorizontalAxisName))
        {
            movementHorizontalInput = Input.GetAxis(movementHorizontalAxisName);

            if (movementHorizontalInput > deadZone && !hasHitWestWall) //moves hideout object right
                hideoutObjectRigidbody.MovePosition(transform.position - transform.right);
            else if (movementHorizontalInput < -deadZone && !hasHitEastWall) //moves hideout object left
                hideoutObjectRigidbody.MovePosition(transform.position + transform.right);
        }

    }

    private void HideoutObjectMovement()
    {
        if (Input.GetButtonDown(movementVerticalAxisName))
        {
            movementVerticalInput = Input.GetAxis(movementVerticalAxisName);

            if (movementVerticalInput > deadZone && !hasHitSouthWall) //moves hideout object forward
                transform.Translate(0, 0, -hideoutObjectMovementZ);
            else if (movementVerticalInput < -deadZone && !hasHitNorthWall) //moves hideout object backward
                transform.Translate(0, 0, hideoutObjectMovementZ);
        }

        if (Input.GetButtonDown(movementHorizontalAxisName))
        {
            movementHorizontalInput = Input.GetAxis(movementHorizontalAxisName);

            if (movementHorizontalInput > deadZone && !hasHitWestWall) //moves hideout object left
                transform.Translate(-hideoutObjectMovementX, 0, 0);
            else if (movementHorizontalInput < -deadZone && !hasHitEastWall) //moves hideout object right
                transform.Translate(hideoutObjectMovementX, 0, 0);
        }

        //Current position of hideout object and prints postition to console
        Vector3 hideoutObjectCurrentPosition = transform.position;
        //print(hideoutObjectCurrentPosition);
    }//end HideoutObjectMovement

    private void OnCollisionEnter(Collision col) //detects which wall object is colliding with
    {
        if (col.gameObject.tag == "Wall")
        {
            print(col.gameObject.name);
            switch (col.gameObject.name)
            {
                case "WestWallPlane":
                    hasHitWestWall = true;
                    break;
                case "NorthWallPlane":
                    hasHitNorthWall = true;
                    break;
                case "EastWallPlane":
                    hasHitEastWall = true;
                    break;
                case "SouthWallPlane":
                    hasHitSouthWall = true;
                    break;

                default:
                    break;
            }
            print("collision with wall detected");
        }
    }//end OnCollsionEnter

    private void OnCollisionStay(Collision col) //if objects collide they turn red
    {
        if (col.gameObject.tag == "HideoutObject")
        {
            print("collision with other hideout object detected");

            Vector3 myPositon = transform.position;
            Vector3 colPostition = col.gameObject.transform.position;

            if (col.gameObject.GetComponent<Renderer>().material.color != Color.red)
                colDefaultColor = col.gameObject.GetComponent<Renderer>().material.color;

            if (myPositon.x == colPostition.x && myPositon.z == colPostition.z)
            {
                col.gameObject.GetComponent<Renderer>().material.color = Color.red;
                gameObject.GetComponent<Renderer>().material.color = Color.red;
            }
            else
            {
                col.gameObject.GetComponent<Renderer>().material.color = colDefaultColor;
                gameObject.GetComponent<Renderer>().material.color = defaultColor;
            }
        }
    }//end OnCollisonStay

    private void OnCollisionExit(Collision col) //detects which wall object is NOT collding with
    {
        if (col.gameObject.tag == "Wall")
        {
            switch (col.gameObject.name)
            {
                case "WestWallPlane":
                    hasHitWestWall = false;
                    break;
                case "NorthWallPlane":
                    hasHitNorthWall = false;
                    break;
                case "EastWallPlane":
                    hasHitEastWall = false;
                    break;
                case "SouthWallPlane":
                    hasHitSouthWall = false;
                    break;

                default:
                    break;
            }
            print("not colliding with wall");
        }
    }//end OnCollisionExit

    private void PlaceHideoutGameObject()
    {
        if (Input.GetButton(placeHideoutObjectAxisName))
        {
            hideoutObjectRigidbody.isKinematic = false;
            WaitForGravity(3);
           

        }
    }//end PlaceHideoutGameObject

    private IEnumerator WaitForGravity(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        hideoutObjectRigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

}//end of HideoutObject Class
