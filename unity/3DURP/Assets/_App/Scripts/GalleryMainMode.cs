using UnityEngine;
using UnityEngine.InputSystem;

public class GalleryMainMode : MonoBehaviour
{
    [SerializeField] EditPictureMode editMode;
    [SerializeField] SelectImageMode selectImage;
    Camera camera;
    void OnEnable()
    {
        UIController.ShowUI("Main");
    }
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    public void SelectImageToAdd()
    {
        selectImage.isReplacing = false;
        InteractionController.EnableMode("AddPicture");
    }
    public void OnSelectObject(InputValue value)
    {
        Vector2 touchPosition = value.Get<Vector2>();
        FindObjectToEdit(touchPosition);
    }

    void FindObjectToEdit(Vector2 touchPosition)
    {
        Ray ray = camera.ScreenPointToRay(touchPosition);
        RaycastHit hit;
        int layerMask = 1 << LayerMask.NameToLayer("PlacedObjects");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            FramedPhoto picture = hit.collider.GetComponent<FramedPhoto>();
            editMode.currentPicture = picture;
            InteractionController.EnableMode("EditPicture");
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
