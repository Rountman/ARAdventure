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
    private Vector3 diff;
    private float angle;
    private float ratio;
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
                    finalPosition = m_Hits[0].pose.position;
                    ratio = 0;

                    Debug.Log($"previousPosition: {previousPosition}");
                    Debug.Log($"finalPosition: {finalPosition}");
                    
                    diff = finalPosition - previousPosition;
                    angle = Mathf.Rad2Deg * Mathf.Atan2(diff.x, diff.z);
                    Vector3 forward = spawnedPrefab.transform.forward;
                    //angle = Vector3.SignedAngle(diff, forward + finalPosition, Vector3.up);
                    var rot = angle * Vector3.up;

                    spawnedPrefab.transform.eulerAngles = (angle) * Vector3.up;
                }
            }
        }

        if (spawnedPrefab.transform.position != finalPosition)
        {
            ratio += Time.deltaTime * multiplier;
            spawnedPrefab.transform.position = Vector3.Lerp(previousPosition, finalPosition, ratio);
        }
    }
}
