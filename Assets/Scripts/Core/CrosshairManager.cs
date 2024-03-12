using UnityEngine;
using UnityEngine.UI;

public class CrosshairManager : MonoBehaviour
{
    [SerializeField]
    private Image image;
    
    [SerializeField]
    private Sprite handImage;

    [SerializeField]
    private Sprite defaultImage;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("z"))
        {
            image.sprite = handImage;
        }
        else if (Input.GetKeyUp("z"))
        {
            image.sprite = defaultImage;
        }
    }
}
