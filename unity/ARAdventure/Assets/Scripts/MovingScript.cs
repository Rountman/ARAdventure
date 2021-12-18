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

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 0)
            return;

        else
        {
            m_RaycastManager.Raycast(Input.GetTouch(0).position, m_Hits);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (m_Hits[0].trackable is ARPlane plane)
                {
                    spawnedPrefab.transform.position = m_Hits[0].pose.position;
                }
            }
        }
    }
}
