using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float speed = 6f;

    bool isInRangeOfBox = false;
    public bool IsInRangeOfBox
    {
        get { return isInRangeOfBox; }
        set { isInRangeOfBox = value; }
    }
    bool isPushingBox = false;
    public bool IsPushingBox
    {
        get { return isPushingBox; }
        set { isPushingBox = value; }
    }
    bool isCrouching = false;
    public bool IsCrouching
    {
        get { return isCrouching; }
        set { isCrouching = value; }
    }
    public BoxMovement boxMove;
    Vector3 movement;
    Rigidbody playerRigidbody;
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetButton("rButton"))
        {
            isCrouching = true;
            speed = 3f;
        }
        else
        {
            isCrouching = false;
            speed = 6f;
        }

        if (Input.GetButtonDown("aButton") && isInRangeOfBox)
        {
            speed = 2f;
            if (isCrouching)
                isCrouching = false;
            isPushingBox = true;
            boxMove.ToggleAttachToBox();
        }
    }

    void FixedUpdate()
    {
        if(CameraController.HasFinishedWaiting)
        {
            // Store the input axes.
            float moveHorizontal = Input.GetAxisRaw("mHorizontal");
            float moveVertical = Input.GetAxisRaw("mVertical");
            float dirHorizontal = Input.GetAxisRaw("dHorizontal");
            float dirVertical = Input.GetAxisRaw("dVertical");

            // Move the player around the scene.
            Move(moveHorizontal, moveVertical);

            // Turn the player to face the joystick input.
            Turning(dirHorizontal, dirVertical);

            // Animate the player.
            //Animating(horizontal, vertical);
        }
    }

    void Move(float horizontal, float vertical)
    {
        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning(float horizontal, float vertical)
    {
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg, transform.eulerAngles.z);
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
