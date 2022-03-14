using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ShowTrackablesOnEnable : MonoBehaviour
{
    [SerializeField] ARSessionOrigin sessionOrigin;
    ARPlaneManager planeManager;
    ARPointCloudManager cloudManager;
    bool isStarted;

    private void Awake()
    {
        planeManager = sessionOrigin.GetComponent<ARPlaneManager>();
        cloudManager = sessionOrigin.GetComponent<ARPointCloudManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        isStarted = true;
    }

    private void OnEnable()
    {
        ShowTrackables(true);
    }

    private void OnDisable()
    {
        if (isStarted)
        {
            ShowTrackables(false);
        }
    }

    void ShowTrackables(bool show)
    {
        if (cloudManager)
        {
            cloudManager.SetTrackablesActive(show);
            cloudManager.enabled = show;
        }
        if (planeManager)
        {
            planeManager.SetTrackablesActive(show);
            planeManager.enabled = show;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
