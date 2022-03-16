using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class MovingScript : MonoBehaviour
{
    void OnEnable()
    {
        UIController.ShowUI("Main");
    }
    // Start is called before the first frame update

    [SerializeField] GameObject placedPrefab;
    [SerializeField] ARRaycastManager raycaster;
    GameObject spawnedObject;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    bool runAnim;
    float duration;
    float multiplier;
    float ratio;
    float ratio2;
    Vector3 previousPosition;
    Vector3 previousRotation;
    Vector3 finalPosition;
    Vector3 finalRotation;


    void Start()
    {
        duration = 0.9f;    //animation duration control
        multiplier = 1 / duration;
        ratio = 0;
        ratio2 = 0;
    }

    void OnPlaceObject(InputValue value)
    {
        //get the screen touch position
        Vector2 touchPosition = value.Get<Vector2>();

        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            //get the hit point (pose) on the plane
            Pose hitPose = hits[0].pose;

            //if this is the first time placing an object
            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
                previousPosition = spawnedObject.transform.position;
                finalPosition = hitPose.position;
            }
            else
            {
                SetPositionAndRotation(hitPose);
            }
        }
    }

    void SetPositionAndRotation(Pose hitPose)
    {

        previousPosition = spawnedObject.transform.position;
        previousRotation = spawnedObject.transform.eulerAngles;
        finalPosition = hitPose.position;

        Vector3 diff = finalPosition - previousPosition;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(diff.x, diff.z);
        finalRotation = angle * Vector3.up;

        runAnim = true;
    }

    void spawnCube(ARPlane plane)
    {
        float planeX = plane.size.x;

        //Vector2 planeZ = plane.size.z;
    }


    // Update is called once per frame
    void Update()
    {
        if (ARPlane)  //podmínka na detekci ARPlane
        {

        }
        if (runAnim)
        {
            ratio += Time.deltaTime * 1.1f;
            ratio2 += Time.deltaTime * 1.3f;
            spawnedObject.transform.position = Vector3.Lerp(previousPosition, finalPosition, ratio);
            spawnedObject.transform.eulerAngles = Vector3.Lerp(previousRotation, finalRotation, ratio2);

            if (spawnedObject.transform.position == finalPosition)
            {
                ratio = 0;
                ratio2 = 0;
                runAnim = false;
            }
        }
    }
}
