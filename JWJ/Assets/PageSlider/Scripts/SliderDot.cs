using TS.PageSlider;
using UnityEngine;
using UnityEngine.UI;

namespace TS.PageSlider.Demo
{
    public class SliderDot : MonoBehaviour
    {
        [SerializeField] private Color _colorDefault;
        [SerializeField] private Color _colorSelected;

        private Image _image;
        private PageDot _dot;

        private void Awake()
        {
            _image = GetComponent<Image>();

            _dot = GetComponent<PageDot>();
            _dot.OnActiveStateChanged.AddListener(PageDot_ActiveStateChanged);
        }

        private void PageDot_ActiveStateChanged(bool active)
        {
            _image.color = active ? _colorSelected : _colorDefault;
        }
    }
}
