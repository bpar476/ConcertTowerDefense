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
                cachedLayout = towerLocations.Select(obj =>
             {
                 var x = obj.transform.position.x;
                 var y = obj.transform.position.y;
                 var cell = new Cell();
                 cell.bottomLeft = new Vector2(x - cellRadius, y - cellRadius);
                 cell.topRight = new Vector2(x + cellRadius, y + cellRadius);
                 return cell;
             }).ToArray();
            }

            return cachedLayout;
        }
    }

    public struct Cell
    {
        public Vector2 bottomLeft;
        public Vector3 topRight;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (var cell in Layout)
        {
            var center = new Vector3(cell.bottomLeft.x + cellRadius, cell.bottomLeft.y + cellRadius);
            Gizmos.DrawWireCube(center, new Vector3(cellRadius * 2, cellRadius * 2, 0));
        }
    }
}
