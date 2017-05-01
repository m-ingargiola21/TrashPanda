using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool openInventory = false;
    public bool OpenInventory
    {
        get { return openInventory; }
        set { openInventory = value; }
    }
    bool isPaused;
    bool isRunning;

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
        if (!openInventory)
        {
            if (Input.GetButtonDown("xButton"))
                HandleOpenInventory();

            if (Input.GetButton("rButton"))
            {
                if (!isPushingBox)
                    StartCoroutine(HandleCrouching());
            }
            else if (Input.GetButtonUp("rButton"))
            {
                if (!isPushingBox)
                {
                    StopCoroutine(HandleCrouching());
                    isCrouching = false;
                    speed = 6f;
                }
            }
                
            if (Input.GetButtonDown("aButton") && isInRangeOfBox)
            {
                HandleBoxInput();
            }
            
            if (Input.GetButtonDown("startButton"))
            {
                HandlePauseInput();
            }

            if (Input.GetButton("bButton"))
            {
                if (!isRunning)
                    StartCoroutine(HandleRunning());
            }
            else if (Input.GetButtonUp("bButton"))
            {
                StopCoroutine(HandleRunning());
                isRunning = false;
                speed = 6f;
            }
        }
    }

    void HandlePauseInput()
    {
        if (!isPaused)
        {
            GameObject.Find("PausePanel").SetActive(true);
            isPaused = true;
            Time.timeScale = 0;
        }
        else
        {
            GameObject.Find("PausePanel").SetActive(false);
            isPaused = false;
            Time.timeScale = 1;
        }
    }

    void HandleBoxInput()
    {
        if (!boxMove.hasPlayerAttached)
        {
            speed = 2f;
            if (!isCrouching)
                isCrouching = true;
            isPushingBox = true;
            boxMove.ToggleAttachToBox();
        }
        else
        {
            speed = 6f;
            if (isCrouching)
                isCrouching = false;
            isPushingBox = false;
            boxMove.ToggleAttachToBox();
        }
    }

    public void QuitToMainMenu()
    {
        GameManager.instance.ChangeSceneWithLoad(GameManager.gameState.MainMenuNew);
    }

    void HandleOpenInventory()
    {
        if (!openInventory)
            openInventory = true;
        else
            openInventory = false;
    }
    
    IEnumerator HandleCrouching()
    {
        while (Input.GetButton("rButton"))
        {
            isCrouching = true;
            speed = 3f;
            yield return null;
        }
    }

    IEnumerator HandleRunning()
    {
        while (Input.GetButton("bButton"))
        {
            isCrouching = false;
            isRunning = true;
            speed = 12f;
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if(CameraController.HasFinishedWaiting)
        {
            // Store the input axes.
            float moveHorizontal = Input.GetAxisRaw("mHorizontal");
            float moveVertical = Input.GetAxisRaw("mVertical");

            // Move the player around the scene.
            Move(moveHorizontal, moveVertical);

            // Turn the player to face the joystick input.
            Turning(moveHorizontal, moveVertical);

            // Animate the player.
            //Animating(moveHorizontal, moveVertical);
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
        // Set the movement vector based on the axis input.
        movement.Set(horizontal, 0f, vertical);

        // Normalise the movement vector and make it proportional to the speed per second.
        movement = movement.normalized * speed * Time.deltaTime;
        Quaternion movRotation = Quaternion.Euler(horizontal, 0f, vertical);

        if (movement != Vector3.zero)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), speed * Time.deltaTime);
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("IsWalking", walking);
    }
}
