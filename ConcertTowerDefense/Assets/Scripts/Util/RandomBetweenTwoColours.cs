using UnityEngine;
using UnityEngine.UI;

public class RandomBetweenTwoColours : MonoBehaviour
{
    [SerializeField]
    private Color from;

    [SerializeField]
    private Color to;

    [SerializeField]
    private int framesBetweenSwap = 0;

    private new SpriteRenderer renderer;
    private Image image;
    private int framesSinceSwap = 0;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        if (framesSinceSwap >= framesBetweenSwap)
        {
            var color = Color.Lerp(from, to, Random.Range(0f, 1.0f));
            if (renderer != null)
            {
                renderer.color = color;
            }
            else if (image != null)
            {
                image.color = color;
            }
            framesSinceSwap = 0;
        }
        else
        {
            framesSinceSwap++;
        }
    }
}
