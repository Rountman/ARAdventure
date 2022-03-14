using UnityEngine;

public class SelectImageMode : MonoBehaviour
{
    [SerializeField] AddPictureMode addPicture;
    [SerializeField] EditPictureMode editPicture;
    public bool isReplacing = false;

    public void ImageSelected(ImageInfo image)
    {
        if (isReplacing)
        {
            editPicture.currentPicture.SetImage(image);
            InteractionController.EnableMode("EditPicture");
        }
        else
        {
            addPicture.imageInfo = image;
            InteractionController.EnableMode("AddPicture");
        }
    }
    private void OnEnable()
    {
        UIController.ShowUI("SelectImage");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
