using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform cameraStartPointOne;
    [SerializeField]
    Transform cameraEndPointOne;
    [SerializeField]
    Transform cameraStartPointTwo;
    [SerializeField]
    Transform cameraEndPointTwo;
    [SerializeField]
    Transform cameraStartPointThree;
    [SerializeField]
    Transform cameraEndPointThree;
    [SerializeField]
    Transform cameraPlayerLockPosition;
    [SerializeField]
    Animator anim;
    [SerializeField]
    GameObject playerGameObject;
    [SerializeField]
    float cameraFollowSpeed = 2f;
    [SerializeField]
    bool shouldFollowCamera;
    [SerializeField]
    bool shouldFollowPlayer;

    float playerFollowYOffset = 37.58335f;
    float cameraMoveSpeed = 10.0f;
    float cameraRotationSpeed = .25f;
    float startTime;
    float distanceToMoveCamera;
    float timeToWait = 3.0f;
    float progress;
    float duration = 2f;
    WaitForSeconds fadeTime = new WaitForSeconds(.25f);
    private static bool hasFinishedWaiting = false;
    public static bool HasFinishedWaiting
    {
        get { return hasFinishedWaiting; }
    }

	void Start ()
    {
        if (shouldFollowCamera)
            StartCoroutine(WaitForIntroCameraPan());
        else
            hasFinishedWaiting = true;
	}
	
	void Update ()
    {
        if (hasFinishedWaiting && shouldFollowPlayer)
        {
            float interpolation = cameraFollowSpeed * Time.deltaTime;

            Vector3 position = transform.position;
            position.y = Mathf.Lerp(transform.position.y, playerGameObject.transform.position.y + playerFollowYOffset, interpolation);
            position.x = Mathf.Lerp(transform.position.x, playerGameObject.transform.position.x, interpolation);

            transform.position = position;
        }
    }

    IEnumerator WaitForIntroCameraPan()
    {
        hasFinishedWaiting = false;

        float increment = Time.deltaTime / duration;
        
        ResetCamera(cameraStartPointOne);
        yield return fadeTime;
        FadeToBlack();
        yield return fadeTime;
        ResetCamera(cameraEndPointOne);
        yield return fadeTime;
        FadeAway();
        progress = 0f;
        while (progress < 1)
        {
            MoveCameraToGamePosition(cameraEndPointOne, cameraStartPointTwo, progress);
            progress += increment;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        FadeToBlack();
        yield return fadeTime;
        ResetCamera(cameraEndPointTwo);
        yield return fadeTime;
        FadeAway();
        progress = 0f;
        while (progress < 1)
        {
            MoveCameraToGamePosition(cameraEndPointTwo, cameraEndPointThree, progress);
            progress += increment;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        //yield return new WaitForSeconds(1.5f);
        //progress = 0f;
        //while (progress < 1)
        //{
        //    increment = Time.deltaTime;
        //    MoveCameraToGamePosition(cameraStartPointThree, cameraEndPointThree, progress);
        //    progress += increment;
        //    yield return new WaitForSeconds(Time.deltaTime);
        //}
        yield return fadeTime;
        FadeToBlack();
        yield return fadeTime;
        ResetCamera(cameraPlayerLockPosition);
        yield return fadeTime;
        FadeAway();

        hasFinishedWaiting = true;

    }

    public void ResetCamera(Transform endCameraPoint)
    {
        //distanceToMoveCamera = Vector3.Distance(startCameraPoint.position, endCameraPoint.position);
        transform.position = endCameraPoint.position;
        transform.rotation = endCameraPoint.rotation;
    }

    void MoveCameraToGamePosition(Transform startCameraPoint, Transform endCameraPoint, float lProgress)
    {
        //I'm not sure why this isn't working, probably something with Time.time, it can be pretty wonky. To make it smooth just make an animation curve for the three axis and rotations
            //For example: AnimationCurve movementCurveX = new AnimationCurve(new Keyframe(startCameraPoint.position.x, 0), new Keyframe(endCameraPoint.position.x, framesToMove));
                //float framesToMove = 30f; float frameCount = 0;
        //Then in update if (isMoving) movementCurve.Evaluate(frameCount); frameCount += Time.deltaTime;
            //Better yet put the movement script in a different component like CinematicCameraMovement and OnEnable() it'll do the Update function so it's not asking if it's Cinematic every frame

        //float distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
        //float fractionOfDistance = distanceCovered / distanceToMoveCamera;
        transform.position = Vector3.Lerp(startCameraPoint.position, endCameraPoint.position, lProgress);
    }

    void RotateCameraToGamePosition(Transform startCameraPoint, Transform endCameraPoint, float lProgress)
    {
        //transform.rotation = Quaternion.Slerp(startCameraPoint.rotation, endCameraPoint.rotation, (Time.time - startTime) * cameraRotationSpeed);
        transform.rotation = Quaternion.Slerp(startCameraPoint.rotation, endCameraPoint.rotation, lProgress);
    }

    public void FadeToBlack()
    {
        anim.SetTrigger("FadeBlack");
    }
    public void FadeAway()
    {
        anim.SetTrigger("FadeAway");
    }
}
