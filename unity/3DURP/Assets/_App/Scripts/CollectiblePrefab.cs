using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblePrefab : MonoBehaviour
{
    public GameObject cube;
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }
    void OnTriggerEnter(Collider other)
    {
        scoreManager.Score++;
        Destroy(cube);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
