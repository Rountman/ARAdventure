using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MovingScript : MonoBehaviour
{
    [SerializeField]
    ARRaycastManager m_RaycastManager;
    [SerializeField]
    GameObject spawnedPrefab;
    List<ARRaycastHit> m_Hits = new List<ARRaycastHit>();
    Vector3 previousPos;
    Vector3 finalPos;
    Quaternion Angle;
    float ratio;
    float duration;
    float multiplier;

    void Start()
    {
        previousPos = new Vector3(0f, -0.2f, 0.5f); //helicopter starting location
        finalPos = new Vector3();
        duration = 0.9f;    //animation duration control
        multiplier = 1 / duration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && spawnedPrefab.transform.position == finalPos)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                m_RaycastManager.Raycast(touch.position, m_Hits);

                if (m_Hits[0].trackable is ARPlane plane)
                {
                    previousPos = finalPos;
                    ratio = 0;
                    
                    finalPos = m_Hits[0].pose.position;
                    Angle.SetFromToRotation(previousPos, finalPos);
                    Debug.Log(Angle);
                }
            }
        }

        if (spawnedPrefab.transform.position != finalPos)
        {
            ratio += Time.deltaTime * multiplier;
            spawnedPrefab.transform.position = Vector3.Lerp(previousPos, finalPos, ratio);
            spawnedPrefab.transform.rotation *= Angle;
        }
    }
}
