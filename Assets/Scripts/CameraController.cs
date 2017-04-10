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
            //MoveCameraToGamePosition();

            //RotateCameraToGamePosition();
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
        float distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
        float fractionOfDistance = distanceCovered / distanceToMoveCamera;
        transform.position = Vector3.Lerp(startCameraPoint.position, endCameraPoint.position, fractionOfDistance);
    }

    void RotateCameraToGamePosition(Transform startCameraPoint, Transform endCameraPoint)
    {
        transform.rotation = Quaternion.Slerp(startCameraPoint.rotation, endCameraPoint.rotation, (Time.time - startTime) * cameraRotationSpeed);
    }
}
