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

    private StageCell[] cachedLayout;

    public StageCell[] Layout
    {
        get
        {
            if (cachedLayout == null)
            {
                cachedLayout = towerLocations.Select(obj => new StageCell(obj, cellRadius)).ToArray();
            }

            return cachedLayout;
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
