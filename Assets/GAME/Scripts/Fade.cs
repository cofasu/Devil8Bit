using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    //private RectTransform _rt;

    public static bool _playFadeOut = true;
    public static bool _playFadeIn = false;

    private Image _img;

    [SerializeField]
    private Color _FadeColor = new Color(0, 0, 0, 0);

    [SerializeField]
    [Range(0.002f, 0.02f)]
    private float _FadeSpeed = 0.006f;

    [SerializeField]
    private bool _AwakeFadeOut = true;

    [SerializeField]
    private bool _autoDestroy = true;

    [SerializeField]
    private float _destroyInvokeTime = 0.0f;

    void Awake()
    {
        //_rt = GetComponent<RectTransform>();
        //_rt.sizeDelta = new Vector2(Screen.width*1.5f, Screen.height*1.5f);
        _img = GetComponent<Image>();

        _img.color = _FadeColor;

        if (_AwakeFadeOut) _img.enabled = true;

        if (_playFadeIn) _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 0);

        if (_playFadeOut) _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, 1);

    }
    void GODestroy()
    {
        Destroy(gameObject);
    }
    void Update()
    {
        if (_img.enabled)
        {
            if (_AwakeFadeOut)
            {
                _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _img.color.a - _FadeSpeed);
                if (_img.color.a <= 0)
                {
                    if (_autoDestroy) Invoke("GODestroy", _destroyInvokeTime);
                    else _img.enabled = false;

                }
            }
            else if (_playFadeOut)
            {
                _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _img.color.a - _FadeSpeed);
                if (_img.color.a <= 0)
                {
                    if (_autoDestroy) Invoke("GODestroy", _destroyInvokeTime);
                    else
                    {
                        _playFadeOut = false;
                        _img.enabled = false;
                    }
                }
            }
            else if (_playFadeIn)
            {
                _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, _img.color.a + _FadeSpeed);
                if (_img.color.a >= 1)
                {
                    if (_autoDestroy) Invoke("GODestroy", _destroyInvokeTime);
                    else _playFadeIn = false;
                }
            }
        }
    }
}
