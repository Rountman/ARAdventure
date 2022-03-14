using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MovingScript : MonoBehaviour
{
    [SerializeField]
    private ARRaycastManager m_RaycastManager;
    [SerializeField]
    GameObject spawnedPrefab;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();

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

    private void Start()
    {
        duration = 0.9f;    //animation duration control
        multiplier = 1 / duration;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.touchCount > 0 && spawnedPrefab.transform.position == finalPosition)
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
        }
    }

    private void spawnCube(ARPlane plane){
        float planeX = plane.size.x;
        //Vector2 planeZ = plane.size.z;
    }
}
