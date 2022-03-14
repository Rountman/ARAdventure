using UnityEngine;
using UnityEngine.UI;

public class ImageButtons : MonoBehaviour
{
    [SerializeField] GameObject imageButtonPrefab;
    [SerializeField] ImagesData imagesData;
    [SerializeField] AddPictureMode addPicture;
    [SerializeField] SelectImageMode selectImage;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        foreach (ImageInfo image in imagesData.images)
        {
            GameObject obj = Instantiate(imageButtonPrefab, transform);
            RawImage rawImage = obj.GetComponent<RawImage>();
            rawImage.texture = image.texture;
            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(() => OnClick(image));
        }
    }

    void OnClick(ImageInfo image)
    {
        selectImage.ImageSelected(image);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
