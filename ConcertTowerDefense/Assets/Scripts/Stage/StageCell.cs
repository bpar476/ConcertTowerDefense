using UnityEngine;

public class StageCell
{
    public Vector2 BottomLeft
    {
        get
        {
            return new Vector2(position.x - radius, position.y - radius);
        }
    }
    public Vector2 TopRight
    {
        get
        {
            return new Vector2(position.x + radius, position.y + radius);
        }
    }

    public Vector2 Center
    {
        get
        {
            return position;
        }
    }

    private Vector2 position;
    private float radius;
    private SpriteRenderer renderer;
    private Color originalColor;

    public StageCell(GameObject cell, float radius)
    {
        this.renderer = cell.GetComponent<SpriteRenderer>();
        this.position = cell.transform.position;
        this.radius = radius;
        this.originalColor = this.renderer.color;
    }

    public void Highlight(Color tint)
    {
        renderer.color = originalColor * tint;
    }

    public void UnHighlight()
    {
        renderer.color = originalColor;
    }
}
