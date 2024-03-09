using UnityEngine;
using UnityEngine.UI;

public class ChangeSliderDotColor : MonoBehaviour
{
    [SerializeField] private Color _colorDefault;
    [SerializeField] private Color _colorSelected;

    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void ChangeToSelected()
    {
        _image.color = _colorSelected;
    }

    public void ChangeToDefault()
    {
        _image.color = _colorDefault;
    }

}
