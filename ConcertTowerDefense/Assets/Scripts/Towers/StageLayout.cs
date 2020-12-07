using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageLayout : MonoBehaviour
{
    /// <summary>
    /// Game objects representing the legal positions for a tower
    /// to be placed
    /// </summary>
    [SerializeField]
    private GameObject[] towerLocations;

    /// <summary>
    /// Half the width of the cell. Cells are square so this is
    /// also half the height;
    /// </summary>
    [SerializeField]
    private float cellRadius;

    private Cell[] cachedLayout;

    public Cell[] Layout
    {
        get
        {
            if (cachedLayout == null)
            {
                cachedLayout = towerLocations.Select(obj => new Cell(obj, cellRadius)).ToArray();
            }

            return cachedLayout;
        }
    }

    public class Cell
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

        public Cell(GameObject cell, float radius)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var cell in Layout)
        {
            Gizmos.DrawWireCube(cell.Center, new Vector3(cellRadius * 2, cellRadius * 2, 0));
        }
    }
}
