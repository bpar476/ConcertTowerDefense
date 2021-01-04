using UnityEngine;

public class RandomBetweenTwoColours : MonoBehaviour
{
    [SerializeField]
    private Color from;

    [SerializeField]
    private Color to;

    private new SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        renderer.color = Color.Lerp(from, to, Random.Range(0f, 1.0f));
    }
}
