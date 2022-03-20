using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.InputSystem;

public class MovingScript : MonoBehaviour
{
    [SerializeField] GameObject placedPrefab;
    [SerializeField] GameObject collectiblePrefab;
    [SerializeField] ARRaycastManager raycaster;
    GameObject spawnedObject;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    bool runAnim;
    float duration;
    float multiplier;
    float ratio;
    float ratio2;
    int counter;
    Vector3 previousPosition;
    Vector3 previousRotation;
    Vector3 finalPosition;
    Vector3 finalRotation;
    Vector2 range1;
    Vector2 range2;
    float planeHeight;

    void OnEnable()
    {
        UIController.ShowUI("Main");
    }

    void Start()
    {
        duration = 0.9f;    //animation duration control
        multiplier = 1 / duration;
        ratio = 0;
        ratio2 = 0;
        counter = 0;
    }

    void OnPlaceObject(InputValue value)
    {
        if (runAnim)
        {
            return;
        }
        //get the screen touch position
        Vector2 touchPosition = value.Get<Vector2>();

        if (raycaster.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            //get the hit point (pose) on the plane
            Pose hitPose = hits[0].pose;

            if (hits[0].trackable is ARPlane plane)
            {
                var planeManager = raycaster.GetComponent<ARPlaneManager>();
                if (planeManager)
                {
                    plane.boundaryChanged += BoundaryChange;
                }
            }

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

    void BoundaryChange(ARPlaneBoundaryChangedEventArgs args)
    {
        SetRange(args);
    }

    void SetRange(in ARPlaneBoundaryChangedEventArgs args)
    {
        ARPlane p = args.plane;
        range1.x = p.center.x - p.size.x / 2;
        range1.y = p.center.x + p.size.x / 2;
        range2.x = p.center.z - p.size.y / 2;
        range2.y = p.center.z + p.size.y / 2;
        planeHeight = p.center.y;
    }

    void SetPositionAndRotation(Pose hitPose)
    {
        previousPosition = spawnedObject.transform.position;
        previousRotation = spawnedObject.transform.eulerAngles;
        finalPosition = hitPose.position;

        Vector3 diff = finalPosition - previousPosition;
        float angle = Mathf.Rad2Deg * Mathf.Atan2(diff.x, diff.z);
        finalRotation = angle * Vector3.up;
        float rot = previousRotation.y - finalRotation.y;
        if (rot > 180)
        {
            finalRotation.y += 360f;
        }
        runAnim = true;
    }

    void SpawnCube()
    {
        Vector3 position = new Vector3(Random.Range(range1.x, range1.y), planeHeight, Random.Range(range2.x, range2.y));
        Instantiate(collectiblePrefab, position, Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        if (counter == 180 && planeHeight != null)
        {
            SpawnCube();
            counter = 0;
        }
        else
        {
            counter++;
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