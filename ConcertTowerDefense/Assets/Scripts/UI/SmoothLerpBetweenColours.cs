using UnityEngine;
using UnityEngine.UI;

public class SmoothLerpBetweenColours : MonoBehaviour
{
    [SerializeField]
    private Color from;
    [SerializeField]
    private Color to;
    [SerializeField]
    private float period;

    private Color target;
    private Color prev;
    private float progress = 0;
    private Image image;

    private void Awake()
    {
        target = to;
        prev = from;
        image = GetComponent<Image>();
    }

    void Update()
    {
        image.color = Color.Lerp(from, to, progress);
        progress = 0.5f * Mathf.Sin(1 / period * Time.time) + 0.5f;
    }
}
