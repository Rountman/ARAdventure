using System.Collections;
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
    GameObject spawnedObject;
    ARRaycastManager raycaster;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    /*
        private Vector3 previousPosition;
        private Vector3 finalPosition;
        private Vector3 previousRotation;
        private Vector3 finalRotation;
        private Vector3 diff;
        private float angle;
        private float ratio;
        private float ratio2;
        private float duration;
        private float multiplier;
    */

    void Start()
    {
        /*duration = 0.9f;    //animation duration control
        multiplier = 1 / duration;*/
        raycaster = GetComponent<ARRaycastManager>();
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
                //instantiate the prefab at the hit position and rotation
                spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);
            }
            else
            {
                //change the position of the previously instantiated object
                spawnedObject.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        /*if (Input.touchCount > 0 && spawnedPrefab.transform.position == finalPosition)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                m_RaycastManager.Raycast(touch.position, m_Hits);

                if (m_Hits[0].trackable is ARPlane plane)
                {
                    previousPosition = spawnedPrefab.transform.position;
                    previousRotation = spawnedPrefab.transform.eulerAngles;
                    finalPosition = m_Hits[0].pose.position;
                    ratio = 0;

                    //spawnCube(plane);

                    Debug.Log($"planePos: {plane.transform.position}");

                    diff = finalPosition - previousPosition;
                    finalPosition.y += 0.05f;
                    angle = Mathf.Rad2Deg * Mathf.Atan2(diff.x, diff.z);
                    var rot = angle * Vector3.up;

                    finalRotation = angle * Vector3.up;
                }
            }
        }

        if (spawnedPrefab.transform.position != finalPosition)
        {
            ratio += Time.deltaTime * multiplier;
            ratio2 += Time.deltaTime * multiplier;
            spawnedPrefab.transform.position = Vector3.Lerp(previousPosition, finalPosition, ratio);
            spawnedPrefab.transform.eulerAngles = Vector3.Lerp(previousRotation, finalRotation, ratio2);
        }*/
    }

    void spawnCube(ARPlane plane)
    {
        float planeX = plane.size.x;
        //Vector2 planeZ = plane.size.z;
    }
}
