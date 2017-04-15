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

    float cameraMoveSpeed = 10.0f;
    float cameraRotationSpeed = .25f;
    float startTime;
    float distanceToMoveCamera;
    float timeToWait = 3.0f;
    bool hasFinishedWaiting = false;

	void Start ()
    {
        //ResetCamera();
        StartCoroutine(WaitForRoundToStart());
	}
	
	void Update ()
    {
        if (hasFinishedWaiting)
        {
            MoveCameraToGamePosition(cameraStartPointOne, cameraEndPointOne);

            RotateCameraToGamePosition(cameraStartPointOne, cameraEndPointOne);
        }
    }

    IEnumerator WaitForRoundToStart()
    {
        yield return new WaitForSeconds(timeToWait);

        hasFinishedWaiting = true;
        startTime = Time.time;
    }

    public void ResetCamera(Transform startCameraPoint, Transform endCameraPoint)
    {
        distanceToMoveCamera = Vector3.Distance(startCameraPoint.position, endCameraPoint.position);
        transform.position = startCameraPoint.position;
        transform.rotation = startCameraPoint.rotation;
    }

    void MoveCameraToGamePosition(Transform startCameraPoint, Transform endCameraPoint)
    {
        //I'm not sure why this isn't working, probably something with Time.time, it can be pretty wonky. To make it smooth just make an animation curve for the three axis and rotations
            //For example: AnimationCurve movementCurveX = new AnimationCurve(new Keyframe(startCameraPoint.position.x, 0), new Keyframe(endCameraPoint.position.x, framesToMove));
                //float framesToMove = 30f; float frameCount = 0;
        //Then in update if (isMoving) movementCurve.Evaluate(frameCount); frameCount += Time.deltaTime;
            //Better yet put the movement script in a different component like CinematicCameraMovement and OnEnable() it'll do the Update function so it's not asking if it's Cinematic every frame

        float distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
        float fractionOfDistance = distanceCovered / distanceToMoveCamera;
        transform.position = Vector3.Lerp(startCameraPoint.position, endCameraPoint.position, fractionOfDistance);
    }

    void RotateCameraToGamePosition(Transform startCameraPoint, Transform endCameraPoint)
    {
        transform.rotation = Quaternion.Slerp(startCameraPoint.rotation, endCameraPoint.rotation, (Time.time - startTime) * cameraRotationSpeed);
    }
}
