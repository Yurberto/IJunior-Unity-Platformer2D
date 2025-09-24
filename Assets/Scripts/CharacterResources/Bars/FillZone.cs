using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FillZone : MonoBehaviour 
{
    private Image _image;

    public float Value => _image.fillAmount;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _image.type = Image.Type.Filled;
        _image.fillMethod = Image.FillMethod.Horizontal;
    }

    public void ApplyFill(float fillAmount)
    {
        if (_image == null)
            return;
        
        _image.fillAmount = fillAmount;
    }
}
